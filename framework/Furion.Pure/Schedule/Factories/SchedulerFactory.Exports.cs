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

namespace Furion.Schedule;

/// <summary>
/// 作业调度工厂默认实现类
/// </summary>
internal sealed partial class SchedulerFactory
{
    /// <summary>
    /// 查找所有作业
    /// </summary>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetJobs()
    {
        return _schedulers.Values;
    }

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryGetJob(string jobId, out IScheduler scheduler)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        var succeed = _schedulers.TryGetValue(jobId, out var internalScheduler);
        scheduler = internalScheduler;

        return succeed
            ? ScheduleResult.Succeed
            : ScheduleResult.NotFound;
    }

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="IScheduler"/></returns>
    public IScheduler GetJob(string jobId)
    {
        _ = TryGetJob(jobId, out var scheduler);
        return scheduler;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度计划构建器</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler)
    {
        // 空检查
        if (schedulerBuilder == null) throw new ArgumentNullException(nameof(schedulerBuilder));

        // 获取作业信息构建器
        var jobBuilder = schedulerBuilder.JobBuilder;

        // 配置默认 JobId
        if (string.IsNullOrWhiteSpace(jobBuilder.JobId))
        {
            jobBuilder.SetJobId($"job{_schedulers.Count + 1}");
        }

        // 检查作业 Id 是否存在
        var scheduleResult = TryGetJob(jobBuilder.JobId, out _);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            scheduler = default;
            return scheduleResult;
        }

        // 构建作业调度计划
        var internalScheduler = schedulerBuilder.Build();

        // 存储作业调度计划工厂
        internalScheduler.Factory = this;

        // 实例化作业处理程序
        var jobType = internalScheduler.JobDetail.RuntimeJobType;
        internalScheduler.JobHandler = (_serviceProvider.GetService(jobType)
            ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;

        // 初始化作业触发器下一次执行时间
        foreach (var trigger in internalScheduler.Triggers.Values)
        {
            trigger.NextRunTime = trigger.GetNextRunTime();
        }

        // 追加到集合中
        var succeed = _schedulers.TryAdd(jobBuilder.JobId, internalScheduler);
        if (!succeed)
        {
            scheduler = default;
            return ScheduleResult.Failed;
        }

        // 将作业信息运行数据写入持久化
        Shorthand(internalScheduler.JobDetail, null, PersistenceBehavior.Appended);

        // 将作业触发器运行信息写入持久化
        foreach (var trigger in internalScheduler.Triggers.Values)
        {
            Shorthand(internalScheduler.JobDetail, trigger, PersistenceBehavior.Appended);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{jobId}> successfully added to the schedule.", jobBuilder.JobId);

        scheduler = internalScheduler;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度计划构建器</param>
    public void AddJob(SchedulerBuilder schedulerBuilder)
    {
        _ = TryAddJob(schedulerBuilder, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(JobBuilder jobBuilder, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
    {
        return TryAddJob(SchedulerBuilder.Create(jobBuilder, triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(JobBuilder jobBuilder, TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(jobBuilder, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryAddJob<TJob>(TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>(), triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        _ = TryAddJob<TJob>(triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId), triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(string jobId, TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        _ = TryAddJob<TJob>(jobId, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"></typeparam>
    /// <param name="jobId"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>()
            .SetJobId(jobId)
            .SetConcurrent(concurrent), triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"></typeparam>
    /// <param name="jobId"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(string jobId, bool concurrent, TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        _ = TryAddJob<TJob>(jobId, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度计划构建器</param>
    /// <param name="newScheduler">新的作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateJob(SchedulerBuilder schedulerBuilder, out IScheduler newScheduler)
    {
        // 空检查
        if (schedulerBuilder == null) throw new AbandonedMutexException(nameof(schedulerBuilder));

        var jobId = schedulerBuilder.JobBuilder.JobId;

        // 空检查
        if (string.IsNullOrEmpty(schedulerBuilder.JobBuilder.JobId)) throw new ArgumentNullException(nameof(jobId));

        // 查找作业
        var scheduleResult = TryGetJob(jobId, out var scheduler);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            newScheduler = default;
            return scheduleResult;
        }

        var internalScheduler = (Scheduler)scheduler;

        // 获取更新后的作业调度计划
        var schedulerForUpdated = schedulerBuilder.Build();

        // 处理从持久化中删除情况
        if (schedulerBuilder.Behavior == PersistenceBehavior.Removed)
        {
            // 从内存集合中移除
            var succeed = _schedulers.TryRemove(jobId, out _);

            // 输出移除日志
            var args = new[] { schedulerBuilder.JobBuilder.JobId };
            newScheduler = null;

            if (succeed)
            {
                // 将作业信息运行数据写入持久化
                Shorthand(internalScheduler.JobDetail, null, PersistenceBehavior.Removed);

                // 逐条将作业触发器运行数据写入持久化
                foreach (var removedTrigger in internalScheduler.Triggers.Values)
                {
                    Shorthand(internalScheduler.JobDetail, removedTrigger, PersistenceBehavior.Removed);
                }

                _logger.LogWarning("The Scheduler of <{jobId}> has removed.", args);
                return ScheduleResult.Removed;
            }
            else
            {
                _logger.LogWarning("The Scheduler of <{jobId}> remove failed.", args);
                return ScheduleResult.Failed;
            }
        }

        // 存储作业调度计划工厂
        schedulerForUpdated.Factory = this;

        // 实例化作业处理程序
        var jobType = schedulerForUpdated.JobDetail.RuntimeJobType;
        schedulerForUpdated.JobHandler = (_serviceProvider.GetService(jobType)
            ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;

        // 逐条初始化作业触发器初始化下一次执行时间
        foreach (var triggerForUpdated in schedulerForUpdated.Triggers.Values)
        {
            triggerForUpdated.NextRunTime = triggerForUpdated.GetNextRunTime();
        }

        // 更新内存作业调度计划集合
        var updateSucceed = _schedulers.TryUpdate(internalScheduler.JobId, schedulerForUpdated, internalScheduler);
        if (!updateSucceed)
        {
            newScheduler = null;
            return ScheduleResult.Failed;
        }

        // 将作业信息运行数据写入持久化
        Shorthand(schedulerForUpdated.JobDetail);

        // 逐条将作业触发器运行数据写入持久化
        foreach (var triggerForUpdated in schedulerForUpdated.Triggers.Values)
        {
            Shorthand(schedulerForUpdated.JobDetail, triggerForUpdated);
        }

        newScheduler = schedulerForUpdated;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度计划构建器</param>
    public void UpdateJob(SchedulerBuilder schedulerBuilder)
    {
        _ = TryUpdateJob(schedulerBuilder, out var _);
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveJob(string jobId, out IScheduler scheduler)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        // 检查作业 Id 是否存在
        var scheduleResult = TryGetJob(jobId, out _);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            scheduler = default;
            return scheduleResult;
        }

        // 从集合中移除
        var succeed = _schedulers.TryRemove(jobId, out var internalScheduler);
        if (!succeed)
        {
            scheduler = default;
            return ScheduleResult.Failed;
        }

        // 将作业信息运行数据写入持久化
        Shorthand(internalScheduler.JobDetail, null, PersistenceBehavior.Removed);

        // 逐条初始化作业触发器初始化下一次执行时间
        foreach (var removedTrigger in internalScheduler.Triggers.Values)
        {
            // 将作业触发器运行数据写入持久化
            Shorthand(internalScheduler.JobDetail, removedTrigger, PersistenceBehavior.Removed);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{jobId}> has removed.", jobId);

        scheduler = internalScheduler;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    public void RemoveJob(string jobId)
    {
        _ = TryRemoveJob(jobId, out var _);
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveJob(IScheduler scheduler)
    {
        // 空检查
        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

        var internalScheduler = (Scheduler)scheduler;
        return TryRemoveJob(internalScheduler.JobId, out _);
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="scheduler">作业调度计划</param>
    public void RemoveJob(IScheduler scheduler)
    {
        _ = TryRemoveJob(scheduler);
    }

    /// <summary>
    /// 检查作业是否存在
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsJob(string jobId)
    {
        return TryGetJob(jobId, out _) == ScheduleResult.Succeed;
    }

    /// <summary>
    /// 启动所有作业
    /// </summary>
    public void StartAll()
    {
        var schedulers = GetJobs();

        foreach (var scheduler in schedulers)
        {
            scheduler.Start();
        }
    }

    /// <summary>
    /// 暂停所有作业
    /// </summary>
    public void PauseAll()
    {
        var schedulers = GetJobs();

        foreach (var scheduler in schedulers)
        {
            scheduler.Pause();
        }
    }

    /// <summary>
    /// 删除所有作业
    /// </summary>
    public void RemoveAll()
    {
        var schedulers = GetJobs();

        foreach (var scheduler in schedulers)
        {
            scheduler.Remove();
        }
    }

    /// <summary>
    /// 查找所有作业组作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetGroupJobs(string group)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(group)) throw new ArgumentNullException(nameof(group));

        return _schedulers.Values.Where(u => u.GroupName == group);
    }

    /// <summary>
    /// 启动所有作业组作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void StartGroup(string group)
    {
        var schedulers = GetGroupJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Start();
        }
    }

    /// <summary>
    /// 暂停所有作业组作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void PauseGroup(string group)
    {
        var schedulers = GetGroupJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Pause();
        }
    }

    /// <summary>
    /// 删除所有作业组作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void RemoveGroup(string group)
    {
        var schedulers = GetGroupJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Remove();
        }
    }
}