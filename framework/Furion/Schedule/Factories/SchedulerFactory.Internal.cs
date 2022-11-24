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

namespace Furion.Schedule;

/// <summary>
/// 作业计划工厂默认实现类（内部服务）
/// </summary>
internal sealed partial class SchedulerFactory : ISchedulerFactory
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 作业调度器日志服务
    /// </summary>
    private readonly IScheduleLogger _logger;

    /// <summary>
    /// 长时间运行的后台任务
    /// </summary>
    /// <remarks>实现作业运行消息持久化</remarks>
    private readonly Task _processQueueTask;

    /// <summary>
    /// 作业调度器取消休眠 Token
    /// </summary>
    /// <remarks>用于取消休眠状态（唤醒）</remarks>
    private CancellationTokenSource _sleepCancellationTokenSource;

    /// <summary>
    /// 作业计划集合
    /// </summary>
    private readonly ConcurrentDictionary<string, Scheduler> _schedulers = new();

    private readonly IList<SchedulerBuilder> _schedulerBuilders;

    /// <summary>
    /// 作业持久化记录消息队列（线程安全）
    /// </summary>
    private readonly BlockingCollection<PersistenceContext> _persistenceMessageQueue = new(1024);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="logger">作业调度器日志服务</param>
    /// <param name="schedulerBuilders">初始作业计划构建集合</param>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    public SchedulerFactory(IServiceProvider serviceProvider
        , IScheduleLogger logger
        , IList<SchedulerBuilder> schedulerBuilders
        , bool useUtcTimestamp)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _schedulerBuilders = schedulerBuilders;

        Persistence = _serviceProvider.GetService<IJobPersistence>();
        UseUtcTimestamp = useUtcTimestamp;

        // 初始化作业调度器取消休眠 Token
        CreateCancellationTokenSource();

        if (Persistence != null)
        {
            // 创建长时间运行的后台任务，并将作业运行消息写入持久化中
            _processQueueTask = Task.Factory.StartNew(state => ((SchedulerFactory)state).ProcessQueue()
                , this, TaskCreationOptions.LongRunning);
        }
    }

    /// <summary>
    /// 是否使用 UTC 时间
    /// </summary>
    internal bool UseUtcTimestamp { get; }

    /// <summary>
    /// 作业调度持久化服务
    /// </summary>
    private IJobPersistence Persistence { get; }

    /// <summary>
    /// 作业调度器初始化
    /// </summary>
    public void Preload()
    {
        // 输出作业调度度初始化日志
        _logger.LogInformation("Schedule Hosted Service is preloading.");

        // 装载初始作业计划
        var initialSchedulerBuilders = _schedulerBuilders.Concat(Persistence?.Preload() ?? Array.Empty<SchedulerBuilder>());

        // 如果作业调度器中包含作业计划构建器
        if (initialSchedulerBuilders.Any())
        {
            // 逐条遍历并新增到内存中
            foreach (var schedulerBuilder in initialSchedulerBuilders)
            {
                _ = TryUpdateJob(Persistence?.OnLoading(schedulerBuilder) ?? schedulerBuilder
                    , out _
                    , false);
            }
        }

        // 释放引用内存并立即回收GC
        _schedulerBuilders.Clear();
        GC.Collect();

        // 取消作业调度器休眠状态（强制唤醒）
        if (!_schedulers.IsEmpty) CancelSleep();

        // 输出作业调度器初始化日志
        _logger.LogInformation("Schedule Hosted Service preload completed, and a total of <{Count}> schedulers are added.", _schedulers.Count);
    }

    /// <summary>
    /// 查找下一个触发的作业
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetNextRunJobs(DateTime startAt, string group = default)
    {
        // 定义静态内部函数用于委托检查
        bool triggerShouldRun(Scheduler s, Trigger t) => t.InternalShouldRun(s.JobDetail, startAt);

        // 创建查询构建器
        var queryBuilder = _schedulers.Values
                 .Where(s => s.JobDetail.RuntimeJobType != null && s.JobHandler != null
                     && s.Triggers.Values.Any(t => triggerShouldRun(s, t)));

        // 判断作业组名称是否存在
        if (!string.IsNullOrWhiteSpace(group))
        {
            queryBuilder = queryBuilder.Where(s => s.GroupName == group);
        }

        // 查找所有符合执行的作业计划
        var nextRunSchedulers = queryBuilder
            .Select(s => new Scheduler(s.JobDetail, s.Triggers.Values.Where(t => triggerShouldRun(s, t)).ToDictionary(t => t.TriggerId, t => t))
            {
                Factory = this,
                Logger = _logger,
                UseUtcTimestamp = UseUtcTimestamp,
                JobHandler = s.JobHandler,
            });

        return nextRunSchedulers;
    }

    /// <summary>
    /// 查找下一个触发的作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    public IEnumerable<SchedulerModel> GetNextRunJobsOfModels(DateTime startAt, string group = default)
    {
        return GetNextRunJobs(startAt, group).Select(s => s.GetModel());
    }

    /// <summary>
    /// 使作业调度器进入休眠状态
    /// </summary>
    public async Task SleepAsync()
    {
        // 输出作业调度器进入休眠日志
        _logger.LogDebug("Schedule Hosted Service enters hibernation.");

        // 获取作业调度器总休眠时间
        var sleepMilliseconds = GetSleepMilliseconds();
        var delay = sleepMilliseconds != null
            ? sleepMilliseconds.Value
            : -1;   // -1 标识无穷值休眠

        try
        {
            // 进入休眠状态
            await Task.Delay(TimeSpan.FromMilliseconds(delay), _sleepCancellationTokenSource.Token);
        }
        catch
        {
            // 重新初始化作业调度器取消休眠 Token
            CreateCancellationTokenSource();
        }
    }

    /// <summary>
    /// 取消作业调度器休眠状态（强制唤醒）
    /// </summary>
    public void CancelSleep()
    {
        if (!_sleepCancellationTokenSource.IsCancellationRequested) _sleepCancellationTokenSource.Cancel();

        // 重新初始化作业调度器取消休眠 Token
        CreateCancellationTokenSource();
    }

    /// <summary>
    /// 将作业信息运行数据写入持久化
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="behavior">作业持久化行为</param>
    public void Shorthand(JobDetail jobDetail, PersistenceBehavior behavior = PersistenceBehavior.Updated)
    {
        Shorthand(jobDetail, null, behavior);
    }

    /// <summary>
    /// 将作业触发器运行数据写入持久化
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="behavior">作业持久化行为</param>
    public void Shorthand(JobDetail jobDetail, Trigger trigger, PersistenceBehavior behavior = PersistenceBehavior.Updated)
    {
        // 设置更新时间
        var nowTime = Penetrates.GetNowTime(UseUtcTimestamp);
        if (trigger == null) jobDetail.UpdatedTime = nowTime;
        else trigger.UpdatedTime = nowTime;

        // 空检查
        if (Persistence == null) return;

        // 只有队列可持续入队才写入
        if (!_persistenceMessageQueue.IsAddingCompleted)
        {
            try
            {
                // 创建作业信息/触发器持久化上下文
                var context = trigger == null ?
                    new PersistenceContext(jobDetail, behavior)
                    : new PersistenceTriggerContext(jobDetail, trigger, behavior);

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
        // 标记作业持久化记录消息队列停止写入
        _persistenceMessageQueue.CompleteAdding();

        try
        {
            // 取消当前任务并释放作业调度器取消休眠 Token
            if (!_sleepCancellationTokenSource.IsCancellationRequested) _sleepCancellationTokenSource.Cancel();
            _sleepCancellationTokenSource.Dispose();

            // 设置 1.5秒的缓冲时间，避免还有消息没有完成持久化
            _processQueueTask?.Wait(1500);
        }
        catch (TaskCanceledException) { }
        catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is TaskCanceledException) { }
    }

    /// <summary>
    /// 获取作业调度器总休眠时间
    /// </summary>
    /// <returns><see cref="double"/></returns>
    private double? GetSleepMilliseconds()
    {
        // 空检查
        if (!_schedulers.Any()) return null;

        // 获取当前时间作为检查时间
        var checkTime = Penetrates.GetUnspecifiedNowTime(UseUtcTimestamp);

        // 获取所有作业计划下一批执行时间
        var nextRunTimes = _schedulers.Values
            .Where(s => s.JobDetail.RuntimeJobType != null && s.JobHandler != null)
            .SelectMany(u => u.Triggers.Values
                .Where(t => t.IsNormalStatus() && t.NextRunTime != null && t.NextRunTime.Value >= checkTime)
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
    /// 监听作业计划变更并调用持久化方法
    /// </summary>
    private void ProcessQueue()
    {
        foreach (var context in _persistenceMessageQueue.GetConsumingEnumerable())
        {
            try
            {
                // 作业触发器更改通知
                if (context is PersistenceTriggerContext triggerContext)
                {
                    Persistence.OnTriggerChanged(triggerContext);
                }
                // 作业信息更改通知
                else Persistence.OnChanged(context);
            }
            catch (Exception ex)
            {
                if (context is PersistenceTriggerContext triggerContext) _logger.LogError(ex, "Persistence of <{TriggerId}> trigger of <{JobId}> job failed.", triggerContext.TriggerId, triggerContext.JobId);
                else _logger.LogError(ex, "The JobDetail of <{JobId}> persist failed.", context.JobId);
            }
        }
    }

    /// <summary>
    /// 创建新的作业调度器取消休眠 Token
    /// </summary>
    private void CreateCancellationTokenSource()
    {
        _sleepCancellationTokenSource?.Dispose();

        // 初始化作业调度器休眠 Token
        _sleepCancellationTokenSource = new CancellationTokenSource();

        // 监听休眠被取消
        _sleepCancellationTokenSource.Token.Register(() =>
        {
            _logger.LogWarning("Schedule Hosted Service cancels hibernation and GC.Collect().");

            // 通知 GC 垃圾回收器立即回收
            GC.Collect();
        });
    }
}