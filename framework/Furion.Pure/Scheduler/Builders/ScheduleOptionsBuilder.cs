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
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Concurrent;

namespace Furion.Scheduler;

/// <summary>
/// 作业调度配置选项构建器
/// </summary>
[SuppressSniffer]
public sealed class SchedulerOptionsBuilder
{
    /// <summary>
    /// 作业调度计划构建器集合
    /// </summary>
    private readonly ConcurrentDictionary<string, JobSchedulerBuilder> _jobSchedulerBuilders = new();

    /// <summary>
    /// 作业处理程序监视器
    /// </summary>
    private Type _jobHandlerMonitor;

    /// <summary>
    /// 作业处理程序执行器
    /// </summary>
    private Type _jobHandlerExecutor;

    /// <summary>
    /// 作业调度持久化器
    /// </summary>
    private Type _schedulerPersistence;

    /// <summary>
    /// 是否使用 UTC 时间戳，默认 false
    /// </summary>
    public bool UseUtcTimestamp { get; set; } = false;

    /// <summary>
    /// 缺省休眠时间
    /// </summary>
    public int DefaultSleepMilliseconds { get; set; } = 1000;

    /// <summary>
    /// 未察觉任务异常事件处理程序
    /// </summary>
    public EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskExceptionHandler { get; set; }

    /// <summary>
    /// 是否启用日志记录
    /// </summary>
    public bool LogEnabled { get; set; } = true;

    /// <summary>
    /// 注册作业
    /// </summary>
    /// <param name="jobSchedulerBuilder">作业调度程序构建器</param>
    /// <returns><see cref="SchedulerOptionsBuilder"/></returns>
    public SchedulerOptionsBuilder AddJob(JobSchedulerBuilder jobSchedulerBuilder)
    {
        // 空检查
        if (jobSchedulerBuilder == null) throw new ArgumentNullException(nameof(jobSchedulerBuilder));

        // 将作业调度计划添加到集合中
        var succeed = _jobSchedulerBuilders.TryAdd(jobSchedulerBuilder.JobId, jobSchedulerBuilder);

        // 检查 Id 重复
        if (!succeed) throw new InvalidOperationException($"The JobId of <{jobSchedulerBuilder.JobId}> already exists.");

        return this;
    }

    /// <summary>
    /// 注册作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerOptionsBuilder"/></returns>
    public SchedulerOptionsBuilder AddJob(JobBuilder jobBuilder, params JobTriggerBuilder[] triggerBuilders)
    {
        return AddJob(JobSchedulerBuilder.Create(jobBuilder, triggerBuilders));
    }

    /// <summary>
    /// 注册作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerOptionsBuilder"/></returns>
    public SchedulerOptionsBuilder AddJob<TJob>(string jobId, params JobTriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        return AddJob(JobSchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId)
            , triggerBuilders));
    }

    /// <summary>
    /// 注册作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerOptionsBuilder"/></returns>
    public SchedulerOptionsBuilder AddJob<TJob>(string jobId, bool concurrent, params JobTriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        return AddJob(JobSchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 注册作业处理程序监视器
    /// </summary>
    /// <typeparam name="TJobHandlerMonitor">实现自 <see cref="IJobHandlerMonitor"/></typeparam>
    /// <returns><see cref="SchedulerOptionsBuilder"/> 实例</returns>
    public SchedulerOptionsBuilder AddMonitor<TJobHandlerMonitor>()
        where TJobHandlerMonitor : class, IJobHandlerMonitor
    {
        _jobHandlerMonitor = typeof(TJobHandlerMonitor);
        return this;
    }

    /// <summary>
    /// 注册作业处理程序执行器
    /// </summary>
    /// <typeparam name="TJobHandlerExecutor">实现自 <see cref="IJobHandlerExecutor"/></typeparam>
    /// <returns><see cref="SchedulerOptionsBuilder"/> 实例</returns>
    public SchedulerOptionsBuilder AddExecutor<TJobHandlerExecutor>()
        where TJobHandlerExecutor : class, IJobHandlerExecutor
    {
        _jobHandlerExecutor = typeof(TJobHandlerExecutor);
        return this;
    }

    /// <summary>
    /// 注册作业调度持久化器
    /// </summary>
    /// <typeparam name="TSchedulerPersistence">实现自 <see cref="ISchedulerPersistence"/></typeparam>
    /// <returns><see cref="SchedulerOptionsBuilder"/> 实例</returns>
    public SchedulerOptionsBuilder AddPersistence<TSchedulerPersistence>()
        where TSchedulerPersistence : class, ISchedulerPersistence
    {
        _schedulerPersistence = typeof(TSchedulerPersistence);
        return this;
    }

    /// <summary>
    /// 构建配置选项
    /// </summary>
    /// <param name="services">服务集合对象</param>
    internal ConcurrentDictionary<string, JobScheduler> Build(IServiceCollection services)
    {
        var jobSchedulers = new ConcurrentDictionary<string, JobScheduler>();

        // 构建作业调度计划和注册作业处理程序类型
        foreach (var (jobId, jobSchedulerBuilder) in _jobSchedulerBuilders)
        {
            var jobScheduler = jobSchedulerBuilder.Build();
            var jobType = jobScheduler.JobDetail.RuntimeJobType;

            _ = jobSchedulers.TryAdd(jobId, jobScheduler);

            // 注册作业处理程序为单例
            services.TryAddSingleton(jobType, jobType);
        }

        // 注册作业监视器
        if (_jobHandlerMonitor != default)
        {
            services.AddSingleton(typeof(IJobHandlerMonitor), _jobHandlerMonitor);
        }

        // 注册作业执行器
        if (_jobHandlerExecutor != default)
        {
            services.AddSingleton(typeof(IJobHandlerExecutor), _jobHandlerExecutor);
        }

        // 注册作业调度持久化器
        if (_schedulerPersistence != default)
        {
            services.AddSingleton(typeof(ISchedulerPersistence), _schedulerPersistence);
        }

        return jobSchedulers;
    }
}