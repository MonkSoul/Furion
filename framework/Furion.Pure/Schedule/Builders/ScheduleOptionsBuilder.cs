// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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
    /// 作业处理程序工厂
    /// </summary>
    private Type _jobFactory;

    /// <summary>
    /// 当前作业组名称
    /// </summary>
    internal string _groupSet;

    /// <summary>
    /// 未察觉任务异常事件处理程序
    /// </summary>
    public EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskExceptionHandler { get; set; }

    /// <summary>
    /// 是否使用 UTC 时间，默认 false
    /// </summary>
    public bool UseUtcTimestamp
    {
        get => UseUtcTimestampProperty;
        set
        {
            UseUtcTimestampProperty = value;
        }
    }

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
    /// <see cref="UseUtcTimestamp"/> 公开配置
    /// </summary>
    public static bool UseUtcTimestampProperty { get; private set; } = false;

    /// <summary>
    /// 生成 SQL 的类型
    /// </summary>
    public SqlTypes BuildSqlType
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
    /// 配置 RunOnStart 提供程序
    /// </summary>
    public Func<Trigger, DateTime, DateTime?> RunOnStartProvider
    {
        get
        {
            return InternalRunOnStartProvider;
        }
        set
        {
            InternalRunOnStartProvider = value;
        }
    }

    /// <summary>
    /// 内部配置 RunOnStart 提供程序
    /// </summary>
    internal static Func<Trigger, DateTime, DateTime?> InternalRunOnStartProvider { get; private set; }

    /// <summary>
    /// 添加作业组作业
    /// </summary>
    /// <param name="groupSet">作业组名称</param>
    /// <param name="setAction"><see cref="Action"/></param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder GroupSet(string groupSet, Action setAction)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(groupSet)) throw new ArgumentNullException(nameof(groupSet));
        if (setAction is null) throw new ArgumentNullException(nameof(setAction));

        // 设置当前作业组名称（理应不存在并发问题，若有添加 lock）
        _groupSet = groupSet;

        // 调用设置
        setAction();

        // 清空当前作业组名称
        _groupSet = null;

        return this;
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilders">作业调度程序构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(params SchedulerBuilder[] schedulerBuilders)
    {
        // 空检查
        if (schedulerBuilders == null || schedulerBuilders.Length == 0) throw new ArgumentNullException(nameof(schedulerBuilders));

        // 逐条将作业计划构建器添加到集合中
        foreach (var schedulerBuilder in schedulerBuilders)
        {
            // 设置作业组名称
            var jobBuilder = schedulerBuilder.JobBuilder;
            if (!string.IsNullOrWhiteSpace(_groupSet)) jobBuilder.SetGroupName(_groupSet);

            _schedulerBuilders.Add(schedulerBuilder);
        }

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
        return AddJob(SchedulerBuilder.Create<TJob>(triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Type jobType, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(jobType, triggerBuilders));
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
        return AddJob(SchedulerBuilder.Create<TJob>(jobId, triggerBuilders));
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
        return AddJob(SchedulerBuilder.Create(jobType, jobId, triggerBuilders));
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
        return AddJob(SchedulerBuilder.Create<TJob>(jobId, concurrent, triggerBuilders));
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
        return AddJob(SchedulerBuilder.Create(jobType, jobId, concurrent, triggerBuilders));
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
        return AddJob(SchedulerBuilder.Create<TJob>(concurrent, triggerBuilders));
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
        return AddJob(SchedulerBuilder.Create(jobType, concurrent, triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(dynamicExecuteAsync, triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(dynamicExecuteAsync, jobId, triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(dynamicExecuteAsync, jobId, concurrent, triggerBuilders));
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddJob(SchedulerBuilder.Create(dynamicExecuteAsync, concurrent, triggerBuilders));
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob(Action<HttpJobMessage> buildMessage, params TriggerBuilder[] triggerBuilders)
    {
        return AddHttpJob<HttpJob>(buildMessage, triggerBuilders);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return AddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(triggerBuilders));
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        return AddHttpJob<HttpJob>(buildMessage, jobId, triggerBuilders);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return AddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(jobId, triggerBuilders));
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddHttpJob<HttpJob>(buildMessage, jobId, concurrent, triggerBuilders);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return AddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(jobId, concurrent, triggerBuilders));
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob(Action<HttpJobMessage> buildMessage, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return AddHttpJob<HttpJob>(buildMessage, concurrent, triggerBuilders);
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public ScheduleOptionsBuilder AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return AddHttpJob(buildMessage, SchedulerBuilder.Create<TJob>(concurrent, triggerBuilders));
    }

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="schedulerBuilder">作业调度程序构建器</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    private ScheduleOptionsBuilder AddHttpJob(Action<HttpJobMessage> buildMessage, SchedulerBuilder schedulerBuilder)
    {
        // 空检查
        if (buildMessage == null) throw new ArgumentNullException(nameof(buildMessage));

        // 空检查
        if (schedulerBuilder == null) throw new ArgumentNullException(nameof(schedulerBuilder));

        // 创建 HTTP 作业消息
        var httpJobMessage = new HttpJobMessage();
        buildMessage?.Invoke(httpJobMessage);

        // 设置作业组名称和描述
        schedulerBuilder.JobBuilder.SetGroupName(httpJobMessage.GroupName).SetDescription(httpJobMessage.Description);

        // 将 HTTP 作业消息序列化并存储起来
        schedulerBuilder.JobBuilder.AddOrUpdateProperty(nameof(HttpJob), Penetrates.Serialize(httpJobMessage));

        return AddJob(schedulerBuilder);
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
    /// 注册作业处理程序工厂
    /// </summary>
    /// <typeparam name="TJobFactory">实现自 <see cref="IJobFactory"/></typeparam>
    /// <returns><see cref="ScheduleOptionsBuilder"/> 实例</returns>
    public ScheduleOptionsBuilder AddJobFactory<TJobFactory>()
        where TJobFactory : class, IJobFactory
    {
        _jobFactory = typeof(TJobFactory);
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

        // 注册作业处理程序工厂
        if (_jobFactory != default)
        {
            services.AddSingleton(typeof(IJobFactory), _jobFactory);
        }

        return _schedulerBuilders;
    }
}