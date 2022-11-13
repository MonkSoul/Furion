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
    /// <returns><see cref="bool"/></returns>
    public bool TryGetJob(string jobId, out IScheduler scheduler)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        var succeed = _schedulers.TryGetValue(jobId, out var internalScheduler);
        scheduler = internalScheduler;

        return succeed;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度计划构建器</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="bool"/></returns>
    public bool TryAddJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler)
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
        var isExist = TryGetJob(jobBuilder.JobId, out _);
        if (isExist)
        {
            scheduler = default;
            return false;
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
            return succeed;
        }

        // 记录作业调度计划状态
        foreach (var trigger in internalScheduler.Triggers.Values)
        {
            Shorthand(internalScheduler.JobDetail, trigger, PersistenceBehavior.AppendJob);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{jobId}> successfully added to the schedule.", jobBuilder.JobId);

        scheduler = internalScheduler;
        return succeed;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="bool"/></returns>
    public bool TryAddJob(JobBuilder jobBuilder, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
    {
        return TryAddJob(SchedulerBuilder.Create(jobBuilder, triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <remarks><see cref="bool"/></remarks>
    public bool TryAddJob<TJob>(TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>(), triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="bool"/></returns>
    public bool TryAddJob<TJob>(string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId), triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"></typeparam>
    /// <param name="jobId"><see cref="IJob"/></param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="bool"/></returns>
    public bool TryAddJob<TJob>(string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
         where TJob : class, IJob
    {
        return TryAddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>()
            .SetJobId(jobId)
            .SetConcurrent(concurrent), triggerBuilders), out scheduler);
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="bool"/></returns>
    public bool TryRemoveJob(string jobId, out IScheduler scheduler)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        var succeed = _schedulers.TryRemove(jobId, out var internalScheduler);
        if (!succeed)
        {
            scheduler = default;
            return succeed;
        }

        // 记录作业调度计划状态
        foreach (var trigger in internalScheduler.Triggers.Values)
        {
            Shorthand(internalScheduler.JobDetail, trigger, PersistenceBehavior.RemoveJob);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{jobId}> has removed.", jobId);

        scheduler = internalScheduler;
        return succeed;
    }

    /// <summary>
    /// 检查作业是否存在
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsJob(string jobId)
    {
        return TryGetJob(jobId, out _);
    }
}