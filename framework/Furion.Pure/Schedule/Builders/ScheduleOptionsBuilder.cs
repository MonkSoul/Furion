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
using System.Reflection;

namespace Furion.Schedule;

/// <summary>
/// 作业调度器配置选项构建器
/// </summary>
[SuppressSniffer]
public sealed class ScheduleOptionsBuilder
{
    /// <summary>
    /// 作业计划构建器集合
    /// </summary>
    private readonly List<SchedulerBuilder> _schedulerBuilders = new();

    /// <summary>
    /// 作业处理程序监视器
    /// </summary>
    private Type _jobMonitor;

    /// <summary>
    /// 作业处理程序执行器
    /// </summary>
    private Type _jobExecutor;

    /// <summary>
    /// 作业调度持久化器
    /// </summary>
    private Type _jobPersistence;

    /// <summary>
    /// 作业集群服务
    /// </summary>
    private Type _jobClusterServer;

    /// <summary>
    /// 未察觉任务异常事件处理程序
    /// </summary>
    public EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskExceptionHandler { get; set; }

    /// <summary>
    /// 是否使用 UTC 时间，默认 false
    /// </summary>
    public bool UseUtcTimestamp { get; set; } = false;

    /// <summary>
    /// 是否启用日志记录
    /// </summary>
    public bool LogEnabled { get; set; } = true;

    /// <summary>
    /// 作业集群 Id
    /// </summary>
    public string ClusterId { get; set; } = string.Empty;

    /// <summary>
    /// 作业信息配置选项
    /// </summary>
    public JobDetailOptions JobDetail { get; } = new();

    /// <summary>
    /// 作业触发器配置选项
    /// </summary>
    public TriggerOptions Trigger { get; } = new();

    /// <summary>
    /// 生成 SQL 的类型
    /// </summary>
    public SqlTypes BuilSqlType
    {
        get
        {
            return InternalBuildSqlType;
        }
        set
        {
            InternalBuildSqlType = value;
        }
    }

    /// <summary>
    /// 内部生成 SQL 的类型
    /// </summary>
    internal static SqlTypes InternalBuildSqlType { get; private set; } = SqlTypes.Standard;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度程序构建器</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(SchedulerBuilder schedulerBuilder)
    {
        // 空检查
        if (schedulerBuilder == null) throw new ArgumentNullException(nameof(schedulerBuilder));

        // 将作业计划构建器添加到集合中
        _schedulerBuilders.Add(schedulerBuilder);

        return this;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(JobBuilder jobBuilder, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(jobBuilder, triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob<TJob>(params TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>()
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Type jobType, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(jobType)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob<TJob>(string jobId, params TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Type jobType, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(jobType).SetJobId(jobId)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob<TJob>(string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>().SetJobId(jobId).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Type jobType, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(jobType).SetJobId(jobId).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob<TJob>(bool concurrent, params TriggerBuilder[] triggerBuilders)
         where TJob : class, IJob
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create<TJob>().SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Type jobType, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(jobType).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(dynamicHandler), triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(dynamicHandler).SetJobId(jobId)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(dynamicHandler).SetJobId(jobId).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicHandler">运行时动态作业处理程序</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<IServiceProvider, JobExecutingContext, CancellationToken, Task> dynamicHandler, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(JobBuilder.Create(dynamicHandler).SetConcurrent(concurrent)
            , triggerBuilders));
    }

    /// <summary>
    /// 注册作业处理程序监视器
    /// </summary>
    /// <typeparam name="TJobMonitor">实现自 <see cref="IJobMonitor"/></typeparam>
    /// <returns><see cref="ScheduleOptionsBuilder"/> 实例</returns>
    public ScheduleOptionsBuilder AddMonitor<TJobMonitor>()
        where TJobMonitor : class, IJobMonitor
    {
        _jobMonitor = typeof(TJobMonitor);
        return this;
    }

    /// <summary>
    /// 注册作业处理程序执行器
    /// </summary>
    /// <typeparam name="TJobExecutor">实现自 <see cref="IJobExecutor"/></typeparam>
    /// <returns><see cref="ScheduleOptionsBuilder"/> 实例</returns>
    public ScheduleOptionsBuilder AddExecutor<TJobExecutor>()
        where TJobExecutor : class, IJobExecutor
    {
        _jobExecutor = typeof(TJobExecutor);
        return this;
    }

    /// <summary>
    /// 注册作业调度持久化器
    /// </summary>
    /// <typeparam name="TJobPersistence">实现自 <see cref="IJobPersistence"/></typeparam>
    /// <returns><see cref="ScheduleOptionsBuilder"/> 实例</returns>
    public ScheduleOptionsBuilder AddPersistence<TJobPersistence>()
        where TJobPersistence : class, IJobPersistence
    {
        _jobPersistence = typeof(TJobPersistence);
        return this;
    }

    /// <summary>
    /// 注册作业集群服务
    /// </summary>
    /// <typeparam name="TJobClusterServer">实现自 <see cref="IJobClusterServer"/></typeparam>
    /// <returns><see cref="ScheduleOptionsBuilder"/> 实例</returns>
    public ScheduleOptionsBuilder AddClusterServer<TJobClusterServer>()
        where TJobClusterServer : class, IJobClusterServer
    {
        _jobClusterServer = typeof(TJobClusterServer);
        return this;
    }

    /// <summary>
    /// 构建作业调度器配置选项
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <remarks><see cref="IEnumerable{SchedulerBuilder}"/></remarks>
    internal IList<SchedulerBuilder> Build(IServiceCollection services)
    {
        // 注册作业监视器
        if (_jobMonitor != default)
        {
            services.AddSingleton(typeof(IJobMonitor), _jobMonitor);
        }

        // 注册作业执行器
        if (_jobExecutor != default)
        {
            services.AddSingleton(typeof(IJobExecutor), _jobExecutor);
        }

        // 注册作业调度持久化器
        if (_jobPersistence != default)
        {
            services.AddSingleton(typeof(IJobPersistence), _jobPersistence);
        }

        // 注册作业集群服务
        if (_jobClusterServer != default)
        {
            // 初始化集群 Id
            ClusterId = !string.IsNullOrWhiteSpace(ClusterId)
                ? ClusterId
                : Assembly.GetEntryAssembly()?.GetName()?.Name ?? "cluster1";

            services.AddSingleton(typeof(IJobClusterServer), _jobClusterServer);
        }

        return _schedulerBuilders;
    }
}