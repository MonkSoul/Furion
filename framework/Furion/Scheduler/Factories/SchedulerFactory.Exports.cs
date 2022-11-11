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
using Microsoft.Extensions.Logging;

namespace Furion.Scheduler;

/// <summary>
/// 作业调度工厂默认实现类
/// </summary>
internal sealed partial class SchedulerFactory
{
    /// <summary>
    /// 查找所有作业调度计划
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IJobScheduler> GetJobSchedulers()
    {
        return _jobSchedulers.Values;
    }

    /// <summary>
    /// 获取作业调度计划
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public IJobScheduler GetJobScheduler(string jobId)
    {
        // 空检查
        if (!string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        return _jobSchedulers.TryGetValue(jobId, out var jobScheduler)
            ? jobScheduler
            : default;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobSchedulerBuilder">作业调度程序构建器</param>
    public void AddJob(JobSchedulerBuilder jobSchedulerBuilder)
    {
        // 空检查
        if (jobSchedulerBuilder == null) throw new ArgumentNullException(nameof(jobSchedulerBuilder));

        // 获取作业信息构建器
        var jobBuilder = jobSchedulerBuilder.JobBuilder;

        // 配置默认 JobId
        if (string.IsNullOrWhiteSpace(jobBuilder.JobId))
        {
            jobBuilder.SetJobId($"job{_jobSchedulers.Count + 1}");
        }

        // 检查 作业 Id 重复
        if (_jobSchedulers.ContainsKey(jobBuilder.JobId)) throw new InvalidOperationException($"The JobId of <{jobBuilder.JobId}> already exists.");

        // 构建作业调度计划并添加到集合中
        var jobScheduler = jobSchedulerBuilder.Build();

        // 存储作业调度计划工厂
        jobScheduler.Factory = this;

        // 实例化作业处理程序
        var jobType = jobScheduler.JobDetail.RuntimeJobType;
        jobScheduler.JobHandler = (_serviceProvider.GetService(jobType)
            ?? ActivatorUtilities.CreateInstance(_serviceProvider, jobType)) as IJob;

        // 初始化作业触发器下一次执行时间
        foreach (var jobTrigger in jobScheduler.JobTriggers.Values)
        {
            jobTrigger.NextRunTime = jobTrigger.IncrementNextRunTime();

            // 记录执行信息并通知作业持久化器
            Record(jobScheduler.JobDetail, jobTrigger, PersistenceBehavior.Append);
        }

        // 追加到集合中
        _ = _jobSchedulers.TryAdd(jobBuilder.JobId, jobScheduler);

        // 输出日志
        _logger.Log(LogLevel.Information, "The JobScheduler of <{jobId}> successfully added to the schedule.", new[] { jobBuilder.JobId });
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob(JobBuilder jobBuilder, params JobTriggerBuilder[] triggerBuilders)
    {
        AddJob(JobSchedulerBuilder.Create(jobBuilder, triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(params JobTriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        AddJob(JobSchedulerBuilder.Create(JobBuilder.Create<TJob>()
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(string jobId, params JobTriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        AddJob(JobSchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void AddJob<TJob>(string jobId, bool concurrent, params JobTriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        AddJob(JobSchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    public void RemoveJob<TJob>(string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId))
        {
            throw new ArgumentNullException(nameof(jobId));
        }

        _jobSchedulers.TryRemove(jobId, out var jobScheduler);

        // 逐条通知作业持久化器
        foreach (var jobTrigger in jobScheduler.JobTriggers.Values)
        {
            // 记录执行信息并通知作业持久化器
            Record(jobScheduler.JobDetail, jobTrigger, PersistenceBehavior.Deleted);
        }

        // 输出日志
        _logger.Log(LogLevel.Information, "The JobScheduler of <{jobId}> has removed.", new[] { jobId });
    }
}