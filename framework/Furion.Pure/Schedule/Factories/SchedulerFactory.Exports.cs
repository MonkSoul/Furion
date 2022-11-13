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
    /// 查找所有作业调度计划
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IScheduler> GetJobs()
    {
        return _schedulers.Values;
    }

    /// <summary>
    /// 获取作业调度计划
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public IScheduler GetJob(string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        return _schedulers.TryGetValue(jobId, out var scheduler)
            ? scheduler
            : default;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度程序构建器</param>
    public void AddJob(SchedulerBuilder schedulerBuilder)
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

        // 检查 作业 Id 重复
        if (_schedulers.ContainsKey(jobBuilder.JobId)) throw new InvalidOperationException($"The JobId of <{jobBuilder.JobId}> already exists.");

        // 构建作业调度计划并添加到集合中
        var scheduler = schedulerBuilder.Build();

        // 存储作业调度计划工厂
        scheduler.Factory = this;

        // 实例化作业处理程序
        var jobType = scheduler.JobDetail.RuntimeJobType;
        scheduler.JobHandler = (_serviceProvider.GetService(jobType)
            ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;

        // 初始化作业触发器下一次执行时间
        foreach (var trigger in scheduler.Triggers.Values)
        {
            trigger.NextRunTime = trigger.GetNextRunTime();

            // 记录执行信息并通知作业持久化器
            Shorthand(scheduler.JobDetail, trigger, PersistenceBehavior.AppendJob);
        }

        // 追加到集合中
        _ = _schedulers.TryAdd(jobBuilder.JobId, scheduler);

        // 通知作业调度服务强制刷新
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{jobId}> successfully added to the schedule.", jobBuilder.JobId);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(JobBuilder jobBuilder, params TriggerBuilder[] triggerBuilders)
    {
        AddJob(SchedulerBuilder.Create(jobBuilder, triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        AddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>()
            , triggerBuilders));
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
        AddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId)
            , triggerBuilders));
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
        AddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    public void RemoveJob(string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId))
        {
            throw new ArgumentNullException(nameof(jobId));
        }

        _schedulers.TryRemove(jobId, out var scheduler);

        // 逐条通知作业持久化器
        foreach (var trigger in scheduler.Triggers.Values)
        {
            // 记录执行信息并通知作业持久化器
            Shorthand(scheduler.JobDetail, trigger, PersistenceBehavior.RemoveJob);
        }

        // 通知作业调度服务强制刷新
        CancelSleep();

        // 输出日志
        _logger.LogInformation("The Scheduler of <{jobId}> has removed.", jobId);
    }

    /// <summary>
    /// 检查作业是否存在
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业调度计划</param>
    public bool ContainsJob(string jobId, out IScheduler scheduler)
    {
        var isExist = _schedulers.TryGetValue(jobId, out var value);
        scheduler = isExist ? value : default;

        return isExist;
    }
}