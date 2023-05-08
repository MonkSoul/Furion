// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.Logging;

namespace Furion.Schedule;

/// <summary>
/// 作业计划工厂默认实现类
/// </summary>
internal sealed partial class SchedulerFactory
{
    /// <summary>
    /// 查找所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <param name="active">是否是有效的作业</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetJobs(string group = default, bool active = false)
    {
        var jobs = string.IsNullOrWhiteSpace(group)
            ? _schedulers.Values
            : _schedulers.Values.Where(s => s.GroupName == group);

        return !active
            ? jobs
            : jobs.Where(s => s.JobDetail.RuntimeJobType != null);
    }

    /// <summary>
    /// 查找所有作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <param name="active">是否是有效的作业</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    public IEnumerable<SchedulerModel> GetJobsOfModels(string group = default, bool active = false)
    {
        return GetJobs(group, active).Select(s => s.GetModel());
    }

    /// <summary>
    /// 查找下一批触发的作业
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetNextRunJobs(DateTime startAt, string group = default)
    {
        // 转换时间为 Unspecified 格式
        var unspecifiedStartAt = Penetrates.GetUnspecifiedTime(startAt);

        // 查找所有下一批执行的作业计划
        var nextRunSchedulers = (GetJobs(group, true) as IEnumerable<Scheduler>)
            .Where(s => s.Triggers.Values.Any(t => t.NextShouldRun(unspecifiedStartAt)));

        return nextRunSchedulers;
    }

    /// <summary>
    /// 查找下一批触发的作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    public IEnumerable<SchedulerModel> GetNextRunJobsOfModels(DateTime startAt, string group = default)
    {
        return GetNextRunJobs(startAt, group).Select(s => s.GetModel());
    }

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryGetJob(string jobId, out IScheduler scheduler)
    {
        var scheduleResult = InternalTryGetJob(jobId, out var originScheduler);
        scheduler = originScheduler;

        return scheduleResult;
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
    /// 保存作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TrySaveJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true)
    {
        // 空检查
        if (schedulerBuilder == null) throw new ArgumentNullException(nameof(schedulerBuilder));

        // 解析作业计划构建器状态
        var isAppended = schedulerBuilder.Behavior == PersistenceBehavior.Appended;
        var isUpdated = schedulerBuilder.Behavior == PersistenceBehavior.Updated;
        var isRemoved = schedulerBuilder.Behavior == PersistenceBehavior.Removed;

        // 原始作业计划
        Scheduler originScheduler = default;

        // 获取作业 Id
        var jobId = schedulerBuilder.JobBuilder.JobId;

        // 检查更新和删除作业时是否正确配置作业 Id 及检查作业是否存在，此操作仅在作业调度器初始化完成后工作
        if (PreloadCompleted
            && (isUpdated || isRemoved))
        {
            // 查找作业
            var scheduleResult = InternalTryGetJob(jobId, out originScheduler, true);
            if (scheduleResult != ScheduleResult.Succeed)
            {
                scheduler = default;
                return scheduleResult;
            }

            // 原作业计划触发器被清空的问题
            if (originScheduler.Triggers.Count > 0 && schedulerBuilder.TriggerBuilders.Count == 0)
            {
                // 将作业触发器运行信息写入持久化
                foreach (var (triggerId, trigger) in originScheduler.Triggers)
                {
                    Shorthand(originScheduler.JobDetail, trigger, PersistenceBehavior.Removed);

                    // 输出日志
                    _logger.LogInformation("The <{triggerId}> trigger for scheduler of <{jobId}> successfully removed to the schedule.", triggerId, jobId);
                }
            }
        }

        // 检查删除作业且不指定作业 Id 的情况，此操作仅在作业调度器未完成初始化时工作
        if (!PreloadCompleted
            && isRemoved
            && string.IsNullOrWhiteSpace(jobId))
        {
            // 输出日志
            _logger.LogWarning("The empty identity scheduler successfully removed to the schedule.");

            scheduler = default;
            return ScheduleResult.Succeed;
        }

        // 构建新的作业计划
        var newScheduler = schedulerBuilder.Build(_schedulers.Count);
        jobId = newScheduler.JobId;

        // 处理新增作业和更新作业的情况
        if (isAppended || isUpdated)
        {
            // 获取当前时间用来计算触发器下一次触发时间
            var nowTime = Penetrates.GetNowTime(UseUtcTimestamp);

            // 初始化作业内部信息
            newScheduler.JobDetail.Blocked = false;
            newScheduler.JobDetail.UpdatedTime = nowTime;

            newScheduler.Factory = this;
            newScheduler.Logger = _logger;
            newScheduler.UseUtcTimestamp = UseUtcTimestamp;

            // 实例化作业处理程序，如果设置了动态委托作业，优先使用
            var runtimeJobType = newScheduler.JobDetail.DynamicExecuteAsync == null
                ? newScheduler.JobDetail.RuntimeJobType
                : typeof(DynamicJob);

            // 是否启用作业详细执行日志
            if (runtimeJobType != null && JobDetailOptions.InternalLogEnabled)
            {
                // 初始化作业类型日志对象
                newScheduler.JobLogger = null;   // 释放引用
                newScheduler.JobLogger = _serviceProvider.GetService(
                    typeof(ILogger<>).MakeGenericType(runtimeJobType)) as ILogger;
            }

            // 存储标记已被删除的触发器
            var triggersThatRemoved = new List<Trigger>();

            // 初始化作业触发器信息
            foreach (var (triggerId, trigger) in newScheduler.Triggers)
            {
                // 处理作业触发器被标记删除的情况
                if (trigger.Behavior == PersistenceBehavior.Removed)
                {
                    // 从当前作业计划中移除
                    if (newScheduler.Triggers.Remove(triggerId, out var triggerThatRemoved))
                    {
                        triggersThatRemoved.Add(triggerThatRemoved);
                    }
                    continue;
                }

                // 处理作业触发器被标记新增的情况，此操作还需要检查作业调度器未初始化完成的情况
                if (!PreloadCompleted
                    || trigger.Behavior == PersistenceBehavior.Appended)
                {
                    // 检查是否启动时执行一次并返回下一次执行时间
                    trigger.NextRunTime = trigger.CheckRunOnStartAndReturnNextRunTime(nowTime);
                    trigger.ResetMaxNumberOfRunsEqualOnceOnStart(nowTime);
                }
                else
                {
                    trigger.NextRunTime = trigger.GetNextRunTime(nowTime);
                }

                // 检查下一次执行信息并修正 NextRunTime 和 Status
                trigger.CheckAndFixNextOccurrence(newScheduler.JobDetail, nowTime);
                trigger.UpdatedTime = nowTime;
            }

            // 将作业计划添加或更新到内存中
            var succeed = !PreloadCompleted || isAppended
                ? _schedulers.TryAdd(jobId, newScheduler)
                : _schedulers.TryUpdate(jobId, newScheduler, originScheduler);

            if (!succeed)
            {
                // 输出日志
                if (!PreloadCompleted || isAppended)
                {
                    _logger.LogWarning("The scheduler of <{jobId}> already exists.", jobId);
                }
                else
                {
                    _logger.LogWarning("The scheduler of <{jobId}> updated failed.", jobId);
                }

                scheduler = default;
                return ScheduleResult.Failed;
            }

            // 将作业触发器运行信息写入持久化
            foreach (var trigger in triggersThatRemoved)
            {
                Shorthand(newScheduler.JobDetail, trigger, trigger.Behavior);

                // 输出日志
                _logger.LogInformation("The <{TriggerId}> trigger for scheduler of <{jobId}> successfully removed to the schedule.", trigger.TriggerId, jobId);
            }

            // 清空引用
            triggersThatRemoved.Clear();
        }
        else
        {
            // 只有初始化成功才会执行此操作
            if (PreloadCompleted)
            {
                // 将作业计划从内存中移除
                var succeed = _schedulers.TryRemove(jobId, out originScheduler);
                if (!succeed)
                {
                    // 输出日志
                    _logger.LogWarning("The scheduler of <{jobId}> removed failed.", jobId);

                    scheduler = default;
                    return ScheduleResult.Failed;
                }
            }
        }

        // 将作业信息运行数据写入持久化
        Shorthand(newScheduler.JobDetail, schedulerBuilder.Behavior);

        // 获取最终返回的作业计划
        var finalScheduler = isRemoved ? originScheduler ?? newScheduler : newScheduler;

        // 将作业触发器运行信息写入持久化
        foreach (var (triggerId, trigger) in finalScheduler.Triggers)
        {
            // 如果作业已删除标记作业触发器为已删除状态
            if (isRemoved) trigger.Behavior = PersistenceBehavior.Removed;

            Shorthand(finalScheduler.JobDetail, trigger, trigger.Behavior);

            // 输出日志
            var triggerOperation = Penetrates.SetFirstLetterCase(trigger.Behavior.ToString(), false);
            // 处理作业调度器初始化未完成时做更新操作情况
            if (!PreloadCompleted && trigger.Behavior == PersistenceBehavior.Updated)
            {
                triggerOperation = "appended and updated";
            }

            _logger.LogInformation("The <{triggerId}> trigger for scheduler of <{jobId}> successfully {triggerOperation} to the schedule.", triggerId, jobId, triggerOperation);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        if (immediately) CancelSleep();

        // 输出日志
        var jobOperation = Penetrates.SetFirstLetterCase(schedulerBuilder.Behavior.ToString(), false);
        // 处理作业调度器初始化未完成时做更新操作情况
        if (!PreloadCompleted && isUpdated)
        {
            jobOperation = "appended and updated";
        }
        _logger.LogInformation("The scheduler of <{JobId}> successfully {jobOperation} to the schedule.", jobId, jobOperation);

        scheduler = finalScheduler;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 保存作业
    /// </summary>
    /// <param name="schedulerBuilders">作业计划构建器集合</param>
    public void SaveJob(params SchedulerBuilder[] schedulerBuilders)
    {
        // 空检查
        if (schedulerBuilders == null || schedulerBuilders.Length == 0) throw new ArgumentNullException(nameof(schedulerBuilders));

        // 逐条将作业计划构建器保存到作业计划中
        foreach (var schedulerBuilder in schedulerBuilders)
        {
            _ = TrySaveJob(schedulerBuilder, out var _);
        }
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true)
    {
        return TrySaveJob(schedulerBuilder?.Appended(), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilders">作业计划构建器集合</param>
    public void AddJob(params SchedulerBuilder[] schedulerBuilders)
    {
        // 空检查
        if (schedulerBuilders == null || schedulerBuilders.Length == 0) throw new ArgumentNullException(nameof(schedulerBuilders));

        // 逐条将作业计划构建器保存到作业计划中
        foreach (var schedulerBuilder in schedulerBuilders)
        {
            _ = TryAddJob(schedulerBuilder, out var _);
        }
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(JobBuilder jobBuilder, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(jobBuilder, triggerBuilders), out scheduler, immediately);
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
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryAddJob<TJob>(TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create<TJob>(triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryAddJob(Type jobType, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(jobType, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(dynamicExecuteAsync, triggerBuilders), out scheduler, immediately);
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
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Type jobType, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(jobType, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicExecuteAsync, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create<TJob>(jobId, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Type jobType, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(jobType, jobId, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(dynamicExecuteAsync, jobId, triggerBuilders), out scheduler, immediately);
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
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Type jobType, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(jobType, jobId, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicExecuteAsync, jobId, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create<TJob>(jobId, concurrent, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Type jobType, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(jobType, jobId, concurrent, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(dynamicExecuteAsync, jobId, concurrent, triggerBuilders), out scheduler, immediately);
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
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Type jobType, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(jobType, jobId, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicExecuteAsync, jobId, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create<TJob>(concurrent, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Type jobType, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(jobType, concurrent, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(dynamicExecuteAsync, concurrent, triggerBuilders), out scheduler, immediately);
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
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Type jobType, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(jobType, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicExecuteAsync, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob(Action<HttpJobMessage> buildMessage, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddHttpJob<HttpJob>(buildMessage, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddHttpJob<HttpJob>(buildMessage, triggerBuilders, out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        _ = TryAddHttpJob<TJob>(buildMessage, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob
    {
        return TryAddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddHttpJob<HttpJob>(buildMessage, jobId, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddHttpJob<HttpJob>(buildMessage, jobId, triggerBuilders, out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        _ = TryAddHttpJob<TJob>(buildMessage, jobId, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob
    {
        return TryAddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(jobId, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddHttpJob<HttpJob>(buildMessage, jobId, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddHttpJob<HttpJob>(buildMessage, jobId, concurrent, triggerBuilders, out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        _ = TryAddHttpJob<TJob>(buildMessage, jobId, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob
    {
        return TryAddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(jobId, concurrent, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob(Action<HttpJobMessage> buildMessage, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddHttpJob<HttpJob>(buildMessage, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddHttpJob<HttpJob>(buildMessage, concurrent, triggerBuilders, out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        _ = TryAddHttpJob<TJob>(buildMessage, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob
    {
        return TryAddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(concurrent, triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="schedulerBuilder">作业调度程序构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    private ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true)
    {
        // 空检查
        if (buildMessage == null) throw new ArgumentNullException(nameof(buildMessage));

        // 空检查
        if (schedulerBuilder == null) throw new ArgumentNullException(nameof(schedulerBuilder));

        // 创建 HTTP 作业消息
        var httpJobMessage = new HttpJobMessage();
        buildMessage?.Invoke(httpJobMessage);

        // 将 HTTP 作业消息序列化并存储起来
        schedulerBuilder.JobBuilder.AddOrUpdateProperty(nameof(HttpJob), Penetrates.Serialize(httpJobMessage));

        return TryAddJob(schedulerBuilder, out scheduler, immediately);
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">新的作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true)
    {
        return TrySaveJob(schedulerBuilder?.Updated(), out scheduler, immediately);
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilders">作业计划构建器集合</param>
    public void UpdateJob(params SchedulerBuilder[] schedulerBuilders)
    {
        // 空检查
        if (schedulerBuilders == null || schedulerBuilders.Length == 0) throw new ArgumentNullException(nameof(schedulerBuilders));

        // 逐条将作业计划构建器保存到作业计划中
        foreach (var schedulerBuilder in schedulerBuilders)
        {
            _ = TryUpdateJob(schedulerBuilder, out var _);
        }
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveJob(string jobId, out IScheduler scheduler, bool immediately = true)
    {
        // 查找作业
        var scheduleResult = InternalTryGetJob(jobId, out var originScheduler, true);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            scheduler = null;
            return scheduleResult;
        }

        scheduler = originScheduler;
        return TryRemoveJob(originScheduler, immediately);
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobIds">作业 Id 集合</param>
    public void RemoveJob(params string[] jobIds)
    {
        foreach (var jobId in jobIds)
        {
            _ = TryRemoveJob(jobId, out var _);
        }
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveJob(IScheduler scheduler, bool immediately = true)
    {
        return TrySaveJob(scheduler?.GetBuilder()?.Removed(), out _, immediately);
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="schedulers">作业计划集合</param>
    public void RemoveJob(params IScheduler[] schedulers)
    {
        foreach (var scheduler in schedulers)
        {
            _ = TryRemoveJob(scheduler);
        }
    }

    /// <summary>
    /// 检查作业是否存在
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsJob(string jobId, string group = default)
    {
        return InternalTryGetJob(jobId, out _, false, group) == ScheduleResult.Succeed;
    }

    /// <summary>
    /// 启动所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void StartAll(string group = default)
    {
        var schedulers = GetJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Start();
        }
    }

    /// <summary>
    /// 暂停所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void PauseAll(string group = default)
    {
        var schedulers = GetJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Pause();
        }
    }

    /// <summary>
    /// 删除所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void RemoveAll(string group = default)
    {
        var schedulers = GetJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Remove();
        }
    }

    /// <summary>
    /// 强制触发所有作业持久化记录
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void PersistAll(string group = default)
    {
        var schedulers = GetJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Persist();
        }
    }

    /// <summary>
    /// 校对所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    public void CollateAll(string group = default)
    {
        var schedulers = GetJobs(group);

        foreach (var scheduler in schedulers)
        {
            scheduler.Collate();
        }
    }

    /// <summary>
    /// 立即执行作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRunJob(string jobId)
    {
        // 查找作业
        var scheduleResult = InternalTryGetJob(jobId, out var _, true);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            return scheduleResult;
        }

        // 添加到待执行集合中
        _manualRunJobIds.Add(jobId);

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 立即执行作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    public void RunJob(string jobId)
    {
        _ = TryRunJob(jobId);
    }
}