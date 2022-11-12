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

using Furion.FriendlyException;
using Furion.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Furion.Scheduler;

/// <summary>
/// 作业调度后台主机服务
/// </summary>
internal sealed class SchedulerHostedService : BackgroundService
{
    /// <summary>
    /// 避免由 CLR 的终结器捕获该异常从而终止应用程序，让所有未觉察异常被觉察
    /// </summary>
    internal event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

    /// <summary>
    /// 日志对象
    /// </summary>
    private readonly ISchedulerLogger _logger;

    /// <summary>
    /// 作业调度工厂
    /// </summary>
    private readonly ISchedulerFactory _schedulerFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="logger">日志对象</param>
    /// <param name="schedulerFactory">作业调度工厂</param>
    /// <param name="useUtcTimestamp">是否使用 Utc 时间</param>
    public SchedulerHostedService(IServiceProvider serviceProvider
        , ISchedulerLogger logger
        , ISchedulerFactory schedulerFactory
        , bool useUtcTimestamp)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;

        Monitor = serviceProvider.GetService<IJobHandlerMonitor>();
        Executor = serviceProvider.GetService<IJobHandlerExecutor>();
        UseUtcTimestamp = useUtcTimestamp;
    }

    /// <summary>
    /// 作业处理程序监视器
    /// </summary>
    private IJobHandlerMonitor Monitor { get; }

    /// <summary>
    /// 作业处理程序执行器
    /// </summary>
    private IJobHandlerExecutor Executor { get; }

    /// <summary>
    /// 是否使用 UTC 时间
    /// </summary>
    private bool UseUtcTimestamp { get; }

    /// <summary>
    /// 执行后台任务
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Scheduler Hosted Service is running.");

        // 注册后台主机服务停止监听
        stoppingToken.Register(() =>
           _logger.LogDebug($"Scheduler Hosted Service is stopping."));

        // 初始化
        await _schedulerFactory.InitializeAsync(stoppingToken);

        // 监听服务是否取消
        while (!stoppingToken.IsCancellationRequested)
        {
            // 执行具体任务
            await BackgroundProcessing(stoppingToken);
        }

        _logger.LogCritical($"Scheduler Hosted Service is stopped.");
    }

    /// <summary>
    /// 后台调用处理程序
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        // 获取当前时间作为检查时间
        var checkTime = DateTime.UtcNow;

        // 查找所有符合触发的作业调度计划
        var jobSchedulersThatShouldRun = _schedulerFactory.GetJobSchedulersThatShouldRun(checkTime);

        // 创建一个任务工厂并保证执行任务都使用当前的计划程序
        var taskFactory = new TaskFactory(System.Threading.Tasks.TaskScheduler.Current);

        // 逐条遍历所有作业调度计划集合
        foreach (var jobSchedulerThatShouldRun in jobSchedulersThatShouldRun)
        {
            // 解构参数
            var jobScheduler = (JobScheduler)jobSchedulerThatShouldRun;
            var jobId = jobScheduler.JobId;
            var jobDetail = jobScheduler.JobDetail;
            var jobHandler = jobScheduler.JobHandler;
            var jobTriggersThatShouldRun = jobScheduler.JobTriggers;

            // 逐条遍历所有符合触发的作业触发器
            foreach (var jobTriggerThatShouldRun in jobTriggersThatShouldRun)
            {
                // 解构参数
                var (jobTriggerId, jobTrigger) = jobTriggerThatShouldRun;

                // 处理串行执行逻辑（默认并行执行）
                if (CheckIsBlocked(jobDetail, jobTrigger, checkTime)) continue;

                // 设置触发器状态为运行状态
                jobTrigger.SetStatus(JobTriggerStatus.Running);

                // 记录运行信息和计算下一个触发时间及休眠时间
                jobTrigger.Increment();

                // 记录执行信息并通知作业持久化器
                _schedulerFactory.Record(jobDetail, jobTrigger);

                // 记录作业执行信息
                LogExecution(jobDetail, jobTrigger, checkTime);

                // 通过并发执行提高吞吐量并解决 Thread.Sleep 问题
                Parallel.For(0, 1, _ =>
                {
                    // 创建新的线程执行
                    taskFactory.StartNew(async () =>
                    {
                        // 创建执行前上下文
                        var jobHandlerExecutingContext = new JobHandlerExecutingContext(jobId, jobTriggerId, jobDetail, jobTrigger, checkTime)
                        {
                            ExecutingTime = UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now
                        };

                        // 执行异常对象
                        InvalidOperationException executionException = default;

                        try
                        {
                            // 调用执行前监视器
                            if (Monitor != default)
                            {
                                await Monitor.OnExecutingAsync(jobHandlerExecutingContext, stoppingToken);
                            }

                            // 判断是否自定义了执行器
                            if (Executor == default)
                            {
                                // 调用作业处理程序并配置出错执行重试
                                await Retry.InvokeAsync(async () =>
                                {
                                    await jobHandler.ExecuteAsync(jobHandlerExecutingContext, stoppingToken);
                                }, jobTrigger.NumRetries, jobTrigger.RetryTimeout);
                            }
                            else
                            {
                                await Executor.ExecuteAsync(jobHandlerExecutingContext, jobHandler, stoppingToken);
                            }

                            // 设置触发器状态为就绪状态
                            jobTrigger.SetStatus(JobTriggerStatus.Ready);

                            // 记录执行信息并通知作业持久化器
                            _schedulerFactory.Record(jobDetail, jobTrigger);
                        }
                        catch (Exception ex)
                        {
                            // 记录错误信息，包含错误次数和运行状态
                            jobTrigger.IncrementErrors();

                            // 记录执行信息并通知作业持久化器
                            _schedulerFactory.Record(jobDetail, jobTrigger);

                            // 输出异常日志
                            _logger.LogError(ex, "Error occurred executing {jobId} {jobTriggerId}<{jobTrigger}>.", jobId, jobTriggerId, jobTrigger.ToString());

                            // 标记异常
                            executionException = new InvalidOperationException(string.Format("Error occurred executing {0} {1}<{2}>.", jobId, jobTriggerId, jobTrigger.ToString()), ex);

                            // 捕获 Task 任务异常信息并统计所有异常
                            if (UnobservedTaskException != default)
                            {
                                var args = new UnobservedTaskExceptionEventArgs(
                                    ex as AggregateException ?? new AggregateException(ex));

                                UnobservedTaskException.Invoke(this, args);
                            }
                        }
                        finally
                        {
                            // 标记上一个触发器阻塞已完成
                            if (!jobDetail.Concurrent) jobDetail.Blocked = false;

                            // 调用执行后监视器
                            if (Monitor != default)
                            {
                                // 创建执行后上下文
                                var jobHandlerExecutedContext = new JobHandlerExecutedContext(jobId, jobTriggerId, jobDetail, jobTrigger, checkTime)
                                {
                                    ExecutedTime = UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now,
                                    Exception = executionException
                                };

                                await Monitor.OnExecutedAsync(jobHandlerExecutedContext, stoppingToken);
                            }
                        }
                    }, stoppingToken);
                });
            }
        }

        // 等待作业调度后台服务被唤醒
        await _schedulerFactory.WaitForWakeUpAsync(stoppingToken);
    }

    /// <summary>
    /// 检查是否是串行执行
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="jobTrigger">作业触发器</param>
    /// <param name="checkTime">检查时间</param>
    /// <returns>返回 true 是串行执行，则阻塞并进入下一轮，返回 false 则继续执行</returns>
    private bool CheckIsBlocked(JobDetail jobDetail, JobTrigger jobTrigger, DateTime checkTime)
    {
        // 如果是并行执行则跳过
        if (jobDetail.Concurrent) return false;

        // 处理串行执行逻辑
        if (!jobDetail.Blocked)
        {
            jobDetail.Blocked = true;
            return false;
        }
        else
        {
            // 设置触发器状态为阻塞状态
            jobTrigger.SetStatus(JobTriggerStatus.Blocked);

            // 记录运行信息和计算下一个触发时间及休眠时间（忽略执行次数）
            jobTrigger.Increment();

            // 记录作业执行信息
            LogExecution(jobDetail, jobTrigger, checkTime);

            // 记录执行信息并通知作业持久化器
            _schedulerFactory.Record(jobDetail, jobTrigger);

            return true;
        }
    }

    /// <summary>
    /// 记录作业执行信息
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="jobTrigger">作业触发器</param>
    /// <param name="checkTime">检查时间</param>
    private void LogExecution(JobDetail jobDetail, JobTrigger jobTrigger, DateTime checkTime)
    {
        // 判断是否输出作业执行日志
        if (!jobTrigger.LogExecution) return;

        Parallel.For(0, 1, _ =>
        {
            _logger.LogInformation(TP.Wrapper("JobExecution", jobDetail.Description ?? jobDetail.JobType, new[]
            {
                $"##JobId## {jobDetail.JobId}"
                , $"##JobType## {jobDetail.JobType}"
                , $"##Concurrent## {jobDetail.Concurrent}"
                , $"##CheckTime## {checkTime}"
                , "━━━━━━━━━━━━  JobTrigger ━━━━━━━━━━━━"
                , $"##TriggerId## {jobTrigger.TriggerId}"
                , $"##TriggerType## {jobTrigger.TriggerType}"
                , $"##TriggerArgs## {jobTrigger.Args}"
                , $"##Status## {jobTrigger.Status}"
                , $"##LastRunTime## {jobTrigger.LastRunTime}"
                , $"##NextRunTime## {jobTrigger.NextRunTime}"
                , $"##NumberOfRuns## {jobTrigger.NumberOfRuns}"
                , $"##MaxNumberOfRuns## {jobTrigger.MaxNumberOfRuns}"
                , $"##NumberOfErrors## {jobTrigger.NumberOfErrors}"
                , $"##MaxNumberOfRuns## {jobTrigger.MaxNumberOfErrors}"
                , $"##Description## {jobTrigger.Description??jobTrigger.ToString()}"
            }));
        });
    }
}