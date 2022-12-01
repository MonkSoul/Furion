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
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetJobs(string group = default)
    {
        return string.IsNullOrWhiteSpace(group)
            ? _schedulers.Values
            : _schedulers.Values.Where(s => s.GroupName == group);
    }

    /// <summary>
    /// 查找所有作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    public IEnumerable<SchedulerModel> GetJobsOfModels(string group = default)
    {
        return GetJobs(group).Select(s => s.GetModel());
    }

    /// <summary>
    /// 查找下一个触发的作业
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    public IEnumerable<IScheduler> GetNextRunJobs(DateTime startAt, string group = default)
    {
        // 采用 DateTimeKind.Unspecified 转换当前时间并忽略毫秒之后部分（用于减少误差）
        var unspecifiedStartAt = new DateTime(startAt.Year
            , startAt.Month
            , startAt.Day
            , startAt.Hour
            , startAt.Minute
            , startAt.Second
            , startAt.Millisecond);

        // 创建查询构建器
        var queryBuilder = _schedulers.Values
                 .Where(s => s.JobDetail.RuntimeJobType != null && s.JobHandler != null
                     && s.Triggers.Values.Any(t => t.IsNormalStatus() && t.NextRunTime != null && t.NextRunTime.Value >= unspecifiedStartAt));

        // 判断作业组名称是否存在
        if (!string.IsNullOrWhiteSpace(group))
        {
            queryBuilder = queryBuilder.Where(s => s.GroupName == group);
        }

        // 查找所有下一次执行的作业计划
        var nextRunSchedulers = queryBuilder
            .Select(s => new Scheduler(s.JobDetail, s.Triggers));

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
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryGetJob(string jobId, out IScheduler scheduler)
    {
        var scheduleResult = TryGetJob(jobId, false, out var originScheduler);
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
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TrySaveJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true)
    {
        // 空检查
        if (schedulerBuilder == null) throw new ArgumentNullException(nameof(schedulerBuilder));

        // 解析作业计划构建器状态
        var isAppended = schedulerBuilder.Behavior == PersistenceBehavior.Appended;
        var isUpdated = schedulerBuilder.Behavior == PersistenceBehavior.Updated;
        var isRemoved = schedulerBuilder.Behavior == PersistenceBehavior.Removed;

        Scheduler originScheduler = default;
        // 获取作业 Id
        var jobId = schedulerBuilder.JobBuilder.JobId;

        // 检查更新或删除是否包含作业 Id 且作业调度器中存在该作业
        if (PreloadCompleted && (isUpdated || isRemoved))
        {
            // 查找作业
            var scheduleResult = TryGetJob(jobId, true, out originScheduler);
            if (scheduleResult != ScheduleResult.Succeed)
            {
                scheduler = default;
                return scheduleResult;
            }
        }

        // 处理 Preload 标记为删除且作业 Id 为空情况
        if (!PreloadCompleted && isRemoved && string.IsNullOrWhiteSpace(jobId))
        {
            scheduler = default;

            // 输出作业
            _logger.LogWarning("Empty identity scheduler has been removed.");
            return ScheduleResult.Succeed;
        }

        // 构建新的作业计划
        var newScheduler = schedulerBuilder.Build(_schedulers.Count + 1);

        // 处理新增和更新作业情况
        if (isAppended || isUpdated)
        {
            // 初始化作业内部信息
            newScheduler.JobDetail.Blocked = false;
            newScheduler.Factory = this;
            newScheduler.UseUtcTimestamp = UseUtcTimestamp;
            newScheduler.Logger = _logger;

            // 实例化作业处理程序
            var jobType = newScheduler.JobDetail.RuntimeJobType;
            if (jobType != null)
            {
                newScheduler.JobHandler = null;    // 释放引用
                newScheduler.JobHandler = (_serviceProvider.GetService(jobType)
                    ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;
            }

            // 获取当前时间用来计算触发器下一次触发时间
            var startAt = Penetrates.GetNowTime(UseUtcTimestamp);

            // 更新作业触发器下一次运行时间
            foreach (var trigger in newScheduler.Triggers.Values)
            {
                // 处理作业触发器删除情况
                if (trigger.Behavior == PersistenceBehavior.Removed) continue;

                // 计算下一次触发时间
                trigger.NextRunTime = trigger.CheckRunOnStartAndReturnNextRunTime(startAt);

                // 处理作业触发器新增情况
                if (trigger.Behavior == PersistenceBehavior.Appended)
                {
                    trigger.ResetMaxNumberOfRunsEqualOnceOnStart(startAt);
                }

                // 检查作业触发器状态
                trigger.CheckNextOccurrence(newScheduler.JobDetail, startAt);
            }

            // 将作业计划添加或更新到内存中
            var succeed = !PreloadCompleted || isAppended
                ? _schedulers.TryAdd(newScheduler.JobId, newScheduler)
                : _schedulers.TryUpdate(newScheduler.JobId, newScheduler, originScheduler);

            if (!succeed)
            {
                scheduler = default;

                // 输出日志
                if (!PreloadCompleted || isAppended)
                {
                    _logger.LogWarning("The Scheduler of <{JobId}> already exists.", newScheduler.JobId);
                }
                else
                {
                    _logger.LogWarning("The Scheduler of <{JobId}> updated failed.", newScheduler.JobId);
                }

                return ScheduleResult.Failed;
            }
        }
        else
        {
            // 将作业计划从内存中移除
            var succeed = _schedulers.TryRemove(newScheduler.JobId, out originScheduler);
            if (!succeed)
            {
                scheduler = default;

                // 输出日志
                _logger.LogWarning("The Scheduler of <{JobId}> removed failed.", newScheduler.JobId);
                return ScheduleResult.Failed;
            }
        }

        // 将作业信息运行数据写入持久化
        Shorthand(newScheduler.JobDetail, schedulerBuilder.Behavior);

        // 获取最终返回的作业计划
        var finalScheduler = isRemoved ? originScheduler : newScheduler;

        // 将作业触发器运行信息写入持久化
        foreach (var trigger in finalScheduler.Triggers.Values)
        {
            Shorthand(finalScheduler.JobDetail, trigger, trigger.Behavior);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        if (immediately) CancelSleep();

        scheduler = finalScheduler;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true)
    {
        var scheduleResult = TrySaveJob(schedulerBuilder?.Appended(), out scheduler, immediately);
        if (scheduleResult == ScheduleResult.Succeed)
        {
            // 输出日志
            _logger.LogInformation("The Scheduler of <{JobId}> successfully added to the schedule.", ((Scheduler)scheduler).JobId);
        }

        return scheduleResult;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilders">作业计划构建器集合</param>
    public void AddJob(params SchedulerBuilder[] schedulerBuilders)
    {
        // 空检查
        if (schedulerBuilders == null || schedulerBuilders.Length == 0) throw new ArgumentNullException(nameof(schedulerBuilders));

        // 逐条将作业计划构建器添加到集合中
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
    /// <param name="immediately">使作业调度器立即载入</param>
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
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryAddJob<TJob>(TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>(), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryAddJob(Type jobType, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create(jobType), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryAddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create(dynamicHandler), triggerBuilders), out scheduler, immediately);
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
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicHandler, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(
            JobBuilder.Create<TJob>()
                               .SetJobId(jobId), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Type jobType, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(
            JobBuilder.Create(jobType)
                               .SetJobId(jobId), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(
            JobBuilder.Create(dynamicHandler)
                               .SetJobId(jobId), triggerBuilders), out scheduler, immediately);
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
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicHandler, jobId, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>()
            .SetJobId(jobId)
            .SetConcurrent(concurrent), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Type jobType, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create(jobType)
            .SetJobId(jobId)
            .SetConcurrent(concurrent), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create(dynamicHandler)
            .SetJobId(jobId)
            .SetConcurrent(concurrent), triggerBuilders), out scheduler, immediately);
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
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicHandler, jobId, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob<TJob>(bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(
            JobBuilder.Create<TJob>()
                               .SetConcurrent(concurrent), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Type jobType, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(
            JobBuilder.Create(jobType)
                               .SetConcurrent(concurrent), triggerBuilders), out scheduler, immediately);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
    {
        return TryAddJob(SchedulerBuilder.Create(
            JobBuilder.Create(dynamicHandler)
                               .SetConcurrent(concurrent), triggerBuilders), out scheduler, immediately);
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
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        _ = TryAddJob(dynamicHandler, concurrent, triggerBuilders, out var _);
    }

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">新的作业计划</param>
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true)
    {
        var scheduleResult = TrySaveJob(schedulerBuilder?.Updated(), out scheduler, immediately);
        if (scheduleResult == ScheduleResult.Succeed)
        {
            // 输出日志
            _logger.LogInformation("The Scheduler of <{JobId}> successfully updated to the schedule.", ((Scheduler)scheduler).JobId);
        }

        return scheduleResult;
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
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveJob(string jobId, out IScheduler scheduler, bool immediately = true)
    {
        // 查找作业
        var scheduleResult = TryGetJob(jobId, true, out var originScheduler);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            scheduler = default;
            return scheduleResult;
        }

        scheduler = originScheduler;
        return TryRemoveJob(originScheduler, immediately);
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
    /// <param name="immediately">使作业调度器立即载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveJob(IScheduler scheduler, bool immediately = true)
    {
        var scheduleResult = TrySaveJob(scheduler?.GetBuilder()?.Removed(), out _, immediately);
        if (scheduleResult == ScheduleResult.Succeed)
        {
            // 输出日志
            _logger.LogInformation("The Scheduler of <{JobId}> successfully removed to the schedule.", ((Scheduler)scheduler).JobId);
        }

        return scheduleResult;
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
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsJob(string jobId, string group = default)
    {
        if (TryGetJob(jobId, false, out var originScheduler) != ScheduleResult.Succeed) return false;

        // 检查作业组名称
        if (!string.IsNullOrWhiteSpace(group))
        {
            if (originScheduler.JobDetail.GroupName != group) return false;
        }

        return true;
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
    /// 校对作业计划
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
}