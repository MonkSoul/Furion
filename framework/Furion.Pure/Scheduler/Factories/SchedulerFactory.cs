// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Furion.Scheduler;

/// <summary>
/// 作业调度工厂默认实现类
/// </summary>
internal sealed partial class SchedulerFactory : ISchedulerFactory, IDisposable
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 日志对象
    /// </summary>
    private readonly ISchedulerLogger _logger;

    /// <summary>
    /// 长时间运行的后台任务
    /// </summary>
    /// <remarks>实现不间断写入</remarks>
    private readonly Task _processQueueTask;

    /// <summary>
    /// 作业调度后台服务休眠 Token
    /// </summary>
    private CancellationTokenSource _delayCancellationTokenSource;

    /// <summary>
    /// 作业调度计划集合
    /// </summary>
    private readonly ConcurrentDictionary<string, JobScheduler> _jobSchedulers = new();

    /// <summary>
    /// 持久化记录消息队列（线程安全）
    /// </summary>
    private readonly BlockingCollection<PersistenceContext> _persistenceMessageQueue = new(1024);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="logger">日志对象</param>
    /// <param name="jobSchedulers">作业调度计划集合</param>
    public SchedulerFactory(IServiceProvider serviceProvider
        , ISchedulerLogger logger
        , ConcurrentDictionary<string, JobScheduler> jobSchedulers)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _jobSchedulers = jobSchedulers;

        Persistence = _serviceProvider.GetService<ISchedulerPersistence>();

        // 创建长时间运行的后台任务，并将记录消息队列中数据写入持久化中
        _processQueueTask = Task.Factory.StartNew(state => ((SchedulerFactory)state).ProcessQueue()
            , this, TaskCreationOptions.LongRunning);
    }

    /// <summary>
    /// 持久化服务
    /// </summary>
    private ISchedulerPersistence Persistence { get; }

    /// <summary>
    /// 初始化作业调度计划
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    public Task InitializeAsync(CancellationToken stoppingToken = default)
    {
        // 输出作业调度初始化日志
        _logger.LogDebug("Schedule Hosted Service is Initializing.");

        // 逐条初始化作业调度计划处理程序
        foreach (var jobScheduler in _jobSchedulers.Values)
        {
            // 获取作业调度计划构建器
            var jobSchedulerBuilder = JobSchedulerBuilder.From(jobScheduler);

            // 判断是否配置了持久化服务
            if (Persistence != null)
            {
                // 加载持久化数据
                Persistence.Load(jobSchedulerBuilder);

                // 处理从持久化中删除情况
                if (jobSchedulerBuilder != null
                    && jobSchedulerBuilder.Behavior == PersistenceBehavior.Deleted)
                {
                    // 从内存中移除
                    var succeed = _jobSchedulers.TryRemove(jobScheduler.JobId, out _);

                    // 输出移除日志
                    var args = new[] { jobSchedulerBuilder.JobBuilder.JobId };
                    if (succeed) _logger.LogWarning("The JobScheduler of <{jobId}> has removed.", args);
                    else _logger.LogWarning("The JobScheduler of <{jobId}> remove failed.", args);
                }
            }

            // 获取更新后的作业调度计划
            var jobSchedulerForUpdated = jobSchedulerBuilder.Build();

            // 存储作业调度计划工厂
            jobSchedulerForUpdated.Factory = this;

            // 实例化作业处理程序
            var jobType = jobSchedulerForUpdated.JobDetail.RuntimeJobType;
            jobSchedulerForUpdated.JobHandler = (_serviceProvider.GetService(jobType)
                ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;

            // 初始化作业触发器下一次执行时间
            foreach (var jobTriggerForUpdated in jobSchedulerForUpdated.JobTriggers.Values)
            {
                jobTriggerForUpdated.NextRunTime = jobTriggerForUpdated.IncrementNextRunTime();
            }

            // 更新内存作业调度计划集合
            _jobSchedulers.TryUpdate(jobSchedulerForUpdated.JobId, jobSchedulerForUpdated, jobScheduler);
        }

        // 输出作业调度初始化日志
        _logger.LogDebug("Schedule Hosted Service initialization completed.");

        return Task.CompletedTask;
    }

    /// <summary>
    /// 查找所有符合触发的作业调度计划
    /// </summary>
    /// <param name="checkTime">检查时间</param>
    /// <returns></returns>
    public IEnumerable<IJobScheduler> GetJobSchedulersThatShouldRun(DateTime checkTime)
    {
        bool triggerShouldRun(JobTrigger t) => t.InternalShouldRun(checkTime);

        // 查找所有符合执行的作业调度计划
        var jobSchedulersThatShouldRun = _jobSchedulers.Values
                .Where(s => s.JobHandler != null
                    && s.JobTriggers.Values.Any(triggerShouldRun))
                .Select(s => new JobScheduler(s.JobDetail, s.JobTriggers.Values.Where(triggerShouldRun).ToDictionary(t => t.TriggerId, t => t))
                {
                    Factory = this,
                    JobHandler = s.JobHandler,
                });

        return jobSchedulersThatShouldRun;
    }

    /// <summary>
    /// 等待作业调度后台服务被唤醒
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    public async Task WaitForWakeUpAsync(CancellationToken stoppingToken = default)
    {
        // 输出作业调度服务进入休眠日志
        _logger.LogDebug("Schedule Hosted Service enters hibernation.");

        // 创建关联 Token
        _delayCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

        // 监听休眠被取消
        _delayCancellationTokenSource.Token.Register(() =>
           _logger.LogWarning("Schedule Hosted Service cancels hibernation."));

        // 获取作业调度计划总休眠时间
        var sleepMilliseconds = GetSleepMilliseconds();
        var delay = sleepMilliseconds != null
            ? sleepMilliseconds.Value
            : long.MaxValue;

        try
        {
            // 进入休眠状态
            await Task.Delay(TimeSpan.FromMilliseconds(delay), _delayCancellationTokenSource.Token);
        }
        catch { }
    }

    /// <summary>
    /// 强制唤醒作业调度后台服务
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    public void ForceRefresh()
    {
        _delayCancellationTokenSource?.Cancel(false);
        _delayCancellationTokenSource = null;
    }

    /// <summary>
    /// 记录作业执行状态
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="jobTrigger">作业触发器</param>
    /// <param name="behavior">作业持久化行为</param>
    public void Record(JobDetail jobDetail, JobTrigger jobTrigger, PersistenceBehavior behavior = PersistenceBehavior.Update)
    {
        // 空检查
        if (Persistence == null) return;

        // 只有队列可持续入队才写入
        if (!_persistenceMessageQueue.IsAddingCompleted)
        {
            try
            {
                // 创建持久化上下文
                var context = new PersistenceContext(jobDetail.JobId
                    , jobTrigger.TriggerId
                    , jobDetail
                    , jobTrigger
                    , behavior);

                _persistenceMessageQueue.Add(context);
                return;
            }
            catch (InvalidOperationException) { }
        }
    }

    /// <summary>
    /// 释放非托管资源
    /// </summary>
    public void Dispose()
    {
        // 标记记录消息队列停止写入
        _persistenceMessageQueue.CompleteAdding();

        try
        {
            // 取消休眠任务
            _delayCancellationTokenSource?.Cancel(false);
            _delayCancellationTokenSource = null;

            // 设置 1.5秒的缓冲时间，避免还有消息没有完成持久化
            _processQueueTask.Wait(1500);
        }
        catch (TaskCanceledException) { }
        catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is TaskCanceledException) { }
    }

    /// <summary>
    /// 获取作业调度计划总休眠时间
    /// </summary>
    /// <returns></returns>
    private double? GetSleepMilliseconds()
    {
        // 空检查
        if (!_jobSchedulers.Any()) return null;

        // 获取当前时间作为检查时间
        var nowTime = DateTime.UtcNow;
        // 采用 DateTimeKind.Unspecified 转换当前时间并忽略毫秒之后部分（用于减少误差）
        var checkTime = new DateTime(nowTime.Year
            , nowTime.Month
            , nowTime.Day
            , nowTime.Hour
            , nowTime.Minute
            , nowTime.Second
            , nowTime.Millisecond);

        // 获取所有作业调度计划下一批执行时间
        var nextRunTimes = _jobSchedulers.Values
            .SelectMany(u => u.JobTriggers.Values
                .Where(t => t.NextRunTime != null && t.NextRunTime.Value >= checkTime)
                .Select(t => t.NextRunTime.Value));

        // 空检查
        if (!nextRunTimes.Any()) return null;

        // 获取最早触发的时间
        var earliestTriggerTime = nextRunTimes.Min();

        // 计算总休眠时间
        var sleepMilliseconds = (earliestTriggerTime - checkTime).TotalMilliseconds;

        return sleepMilliseconds;
    }

    /// <summary>
    /// 将记录消息持久化
    /// </summary>
    private void ProcessQueue()
    {
        foreach (var context in _persistenceMessageQueue.GetConsumingEnumerable())
        {
            try
            {
                Persistence?.Persist(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Schedule Hosted Service persist failed.");
            }
        }
    }
}