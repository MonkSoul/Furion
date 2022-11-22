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
/// 作业计划工厂默认实现类
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
    /// 查找所有作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    public IEnumerable<SchedulerModel> GetJobsOfModels()
    {
        return GetJobs().Select(s => s.GetModel());
    }

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
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
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler)
    {
        // 空检查
        if (schedulerBuilder == null) throw new ArgumentNullException(nameof(schedulerBuilder));

        // 构建作业计划
        var internalScheduler = schedulerBuilder.Build(_schedulers.Count + 1);

        // 存储作业计划工厂
        internalScheduler.Factory = this;
        internalScheduler.UseUtcTimestamp = UseUtcTimestamp;
        internalScheduler.Logger = _logger;

        // 实例化作业处理程序
        var jobType = internalScheduler.JobDetail.RuntimeJobType;
        if (jobType != null)
        {
            internalScheduler.JobHandler = (_serviceProvider.GetService(jobType)
            ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;
        }

        // 初始化作业触发器下一次运行时间
        foreach (var trigger in internalScheduler.Triggers.Values)
        {
            trigger.NextRunTime = trigger.CheckRunOnStarAndReturnNextRunTime(UseUtcTimestamp);
        }

        // 追加到集合中
        var succeed = _schedulers.TryAdd(internalScheduler.JobId, internalScheduler);
        if (!succeed)
        {
            scheduler = default;
            _logger.LogWarning("The JobId of <{JobId}> already exists.", internalScheduler.JobId);

            return ScheduleResult.Failed;
        }

        // 将作业信息运行数据写入持久化
        Shorthand(internalScheduler.JobDetail, PersistenceBehavior.Appended);

        // 将作业触发器运行信息写入持久化
        foreach (var internalTrigger in internalScheduler.Triggers.Values)
        {
            Shorthand(internalScheduler.JobDetail, internalTrigger, PersistenceBehavior.Appended);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{JobId}> successfully added to the schedule.", internalScheduler.JobId);

        scheduler = internalScheduler;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    public void AddJob(SchedulerBuilder schedulerBuilder)
    {
        _ = TryAddJob(schedulerBuilder, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
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
    public void AddJob(JobBuilder jobBuilder, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(jobBuilder, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
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
    public void AddJob<TJob>(params TriggerBuilder[] triggerBuilders)
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
    /// <param name="scheduler">作业计划</param>
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
    public void AddJob<TJob>(string jobId, params TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        _ = TryAddJob<TJob>(jobId, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
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
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        _ = TryAddJob<TJob>(jobId, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>()
            .SetConcurrent(concurrent), triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(bool concurrent, params TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        _ = TryAddJob<TJob>(concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">新的作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler)
    {
        // 空检查
        if (schedulerBuilder == null) throw new AbandonedMutexException(nameof(schedulerBuilder));

        var jobId = schedulerBuilder.JobBuilder.JobId;

        // 如果标记为更新或删除的作业计划构建器必须包含 Id
        if ((schedulerBuilder.Behavior == PersistenceBehavior.Updated || schedulerBuilder.Behavior == PersistenceBehavior.Removed)
            && string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        // 处理从持久化中删除情况
        if (schedulerBuilder.Behavior == PersistenceBehavior.Removed)
        {
            return TryRemoveJob(jobId, out scheduler);
        }
        // 处理从持久化中添加情况
        else if (schedulerBuilder.Behavior == PersistenceBehavior.Appended)
        {
            return TryAddJob(schedulerBuilder, out scheduler);
        }

        // 查找作业
        var scheduleResult = TryGetJob(jobId, out var internalScheduler);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            scheduler = default;

            // 输出日志
            _logger.LogWarning("The Scheduler of <{jobId}> is not found.", jobId);
            return scheduleResult;
        }

        // 原始作业计划
        var originScheduler = (Scheduler)internalScheduler;

        // 记录更新时间
        var updatedTime = Penetrates.GetNowTime(UseUtcTimestamp);

        // 获取更新后的作业计划
        var schedulerForUpdated = schedulerBuilder.Build(_schedulers.Count + 1);
        schedulerForUpdated.JobDetail.UpdatedTime = updatedTime;

        // 存储作业计划工厂
        schedulerForUpdated.Factory = this;
        schedulerForUpdated.UseUtcTimestamp = UseUtcTimestamp;
        schedulerForUpdated.Logger = _logger;

        // 实例化作业处理程序
        var jobType = schedulerForUpdated.JobDetail.RuntimeJobType;
        if (jobType != null)
        {
            schedulerForUpdated.JobHandler = (_serviceProvider.GetService(jobType)
            ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;
        }

        // 逐条初始化作业触发器初始化下一次执行时间
        foreach (var triggerForUpdated in schedulerForUpdated.Triggers.Values)
        {
            triggerForUpdated.NextRunTime = triggerForUpdated.GetNextRunTime(UseUtcTimestamp);
            triggerForUpdated.UpdatedTime = updatedTime;
        }

        // 更新内存作业计划集合
        var updateSucceed = _schedulers.TryUpdate(jobId, schedulerForUpdated, originScheduler);
        if (!updateSucceed)
        {
            scheduler = null;

            // 输出日志
            _logger.LogWarning("The Scheduler of <{jobId}> update failed.", jobId);
            return ScheduleResult.Failed;
        }

        // 将作业信息运行数据写入持久化
        Shorthand(schedulerForUpdated.JobDetail);

        // 逐条将作业触发器运行数据写入持久化（处理作业触发器被删除情况）
        var triggerIdsOfRemoved = new List<string>();
        foreach (var (triggerId, triggerForOrigin) in originScheduler.Triggers)
        {
            // 处理作业触发器被删除的情况
            if (!schedulerForUpdated.Triggers.TryGetValue(triggerId, out _))
            {
                triggerIdsOfRemoved.Add(triggerId);
                Shorthand(schedulerForUpdated.JobDetail, triggerForOrigin, PersistenceBehavior.Removed);
                continue;
            }
        }

        // 逐条将作业触发器运行数据写入持久化（处理作业触发器被新增/更新情况）
        foreach (var (triggerId, triggerForUpdated) in schedulerForUpdated.Triggers)
        {
            // 排除已被删除的作业触发器 Id
            if (triggerIdsOfRemoved.Contains(triggerId)) continue;

            // 处理作业触发器新增的情况
            if (!originScheduler.Triggers.TryGetValue(triggerId, out _))
            {
                Shorthand(schedulerForUpdated.JobDetail, triggerForUpdated, PersistenceBehavior.Appended);
                continue;
            }

            // 处理作业触发器被更新情况
            Shorthand(schedulerForUpdated.JobDetail, triggerForUpdated, PersistenceBehavior.Updated);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{JobId}> successfully updated to the schedule.", schedulerForUpdated.JobId);

        scheduler = schedulerForUpdated;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    public void UpdateJob(SchedulerBuilder schedulerBuilder)
    {
        _ = TryUpdateJob(schedulerBuilder, out var _);
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveJob(string jobId, out IScheduler scheduler)
    {
        // 检查作业 Id 是否存在
        var scheduleResult = TryGetJob(jobId, out _);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            scheduler = default;

            // 输出日志
            _logger.LogWarning("The Scheduler of <{jobId}> is not found.", jobId);
            return scheduleResult;
        }

        // 从集合中移除
        var succeed = _schedulers.TryRemove(jobId, out var schedulerForRemoved);
        if (!succeed)
        {
            scheduler = default;

            // 输出日志
            _logger.LogWarning("The Scheduler of <{jobId}> remove failed.", jobId);
            return ScheduleResult.Failed;
        }

        // 将作业信息运行数据写入持久化
        Shorthand(schedulerForRemoved.JobDetail, PersistenceBehavior.Removed);

        // 逐条初始化作业触发器初始化下一次执行时间
        foreach (var triggerForRemoved in schedulerForRemoved.Triggers.Values)
        {
            // 将作业触发器运行数据写入持久化
            Shorthand(schedulerForRemoved.JobDetail, triggerForRemoved, PersistenceBehavior.Removed);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{jobId}> successfully removed to the schedule.", jobId);

        scheduler = schedulerForRemoved;
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
    /// <param name="scheduler">作业计划</param>
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
    /// <param name="scheduler">作业计划</param>
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
    /// 查找作业组所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetGroupJobs(string group)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(group)) throw new ArgumentNullException(nameof(group));

        return _schedulers.Values.Where(u => !string.IsNullOrWhiteSpace(u.GroupName) && u.GroupName == group);
    }

    /// <summary>
    /// 查找作业组所有作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    public IEnumerable<SchedulerModel> GetGroupJobsOfModels(string group)
    {
        return GetGroupJobs(group).Select(s => s.GetModel());
    }

    /// <summary>
    /// 启动作业组所有作业
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
    /// 暂停作业组所有作业
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
    /// 删除作业组所有作业
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

    /// <summary>
    /// 检查作业组作业是否存在
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsJob(string group, string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(group)) throw new ArgumentNullException(nameof(group));

        var scheduleResult = TryGetJob(jobId, out var scheduler);
        if (scheduleResult != ScheduleResult.Succeed) return false;

        // 判断作业组是否相等
        var internalScheduler = (Scheduler)scheduler;
        if (internalScheduler.JobDetail.GroupName != group) return false;

        return true;
    }

    /// <summary>
    /// 强制触发所有作业持久化记录
    /// </summary>
    public void PersistAll()
    {
        var schedulers = GetJobs();

        foreach (var scheduler in schedulers)
        {
            scheduler.Persist();
        }
    }

    /// <summary>
    /// 强制触发作业组所有作业持久化记录
    /// </summary>
    public void PersistGroup(string group)
    {
        var schedulers = GetGroupJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Persist();
        }
    }
}