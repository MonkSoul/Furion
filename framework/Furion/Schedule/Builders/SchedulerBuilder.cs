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

using System.Reflection;

namespace Furion.Schedule;

/// <summary>
/// 作业计划构建器
/// </summary>
[SuppressSniffer]
public sealed class SchedulerBuilder
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    private SchedulerBuilder(JobBuilder jobBuilder)
    {
        JobBuilder = jobBuilder;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    private SchedulerBuilder(JobBuilder jobBuilder, List<TriggerBuilder> triggerBuilders)
    {
        JobBuilder = jobBuilder;
        TriggerBuilders = triggerBuilders;
    }

    /// <summary>
    /// 标记作业持久化行为
    /// </summary>
    internal PersistenceBehavior Behavior { get; private set; } = PersistenceBehavior.Appended;

    /// <summary>
    /// 作业信息构建器
    /// </summary>
    internal JobBuilder JobBuilder { get; private set; }

    /// <summary>
    /// 作业触发器构建器集合
    /// </summary>
    internal List<TriggerBuilder> TriggerBuilders { get; private set; } = new();

    /// <summary>
    /// 作业触发器数量
    /// </summary>
    public int TriggerCount => TriggerBuilders.Count;

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(string jobId)
    {
        return Create(JobBuilder.Create(jobId));
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create<TJob>(params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return Create(JobBuilder.Create<TJob>(), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create<TJob>(string jobId, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return Create(JobBuilder.Create<TJob>().SetJobId(jobId), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create<TJob>(bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return Create(JobBuilder.Create<TJob>().SetConcurrent(concurrent), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create<TJob>(string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob
    {
        return Create(JobBuilder.Create<TJob>().SetJobId(jobId).SetConcurrent(concurrent), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Type jobType, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(jobType), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Type jobType, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(jobType).SetJobId(jobId), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Type jobType, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(jobType).SetConcurrent(concurrent), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Type jobType, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(jobType).SetJobId(jobId).SetConcurrent(concurrent), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(dynamicExecuteAsync), triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(dynamicExecuteAsync).SetJobId(jobId)
            , triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(dynamicExecuteAsync).SetJobId(jobId).SetConcurrent(concurrent)
            , triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, bool concurrent, params TriggerBuilder[] triggerBuilders)
    {
        return Create(JobBuilder.Create(dynamicExecuteAsync).SetConcurrent(concurrent)
            , triggerBuilders);
    }

    /// <summary>
    /// 创建作业计划构建器
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(JobBuilder jobBuilder, params TriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (jobBuilder == null) throw new ArgumentNullException(nameof(jobBuilder));

        // 创建作业计划构建器
        var schedulerBuilder = new SchedulerBuilder(jobBuilder);

        // 批量添加触发器
        if (triggerBuilders != null && triggerBuilders.Length > 0)
        {
            schedulerBuilder.TriggerBuilders.AddRange(triggerBuilders);
        }

        // 判断是否扫描 IJob 实现类 [Trigger] 特性触发器
        if (jobBuilder.IncludeAnnotations && jobBuilder.RuntimeJobType != null)
        {
            // 检查类型是否贴有 [JobDetail] 特性
            if (jobBuilder.RuntimeJobType.IsDefined(typeof(JobDetailAttribute), true))
            {
                // 这里加载之后忽略空值
                jobBuilder.LoadFrom(jobBuilder.RuntimeJobType.GetCustomAttribute<JobDetailAttribute>(true), true);
            }

            schedulerBuilder.TriggerBuilders.AddRange(jobBuilder.RuntimeJobType.ScanTriggers());
        }

        return schedulerBuilder.Appended();
    }

    /// <summary>
    /// 将 <see cref="Scheduler"/> 转换成 <see cref="SchedulerBuilder"/>
    /// </summary>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    internal static SchedulerBuilder From(Scheduler scheduler)
    {
        return new SchedulerBuilder(JobBuilder.From(scheduler.JobDetail)
            , scheduler.Triggers.Select(t => TriggerBuilder.From(t.Value)).ToList())
            .Updated();
    }

    /// <summary>
    /// 将 <see cref="IScheduler"/> 转换成 <see cref="SchedulerBuilder"/>
    /// </summary>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder From(IScheduler scheduler)
    {
        return scheduler.GetBuilder();
    }

    /// <summary>
    /// 将 JSON 字符串转换成 <see cref="SchedulerBuilder"/>
    /// </summary>
    /// <param name="json">JSON 字符串</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder From(string json)
    {
        // 反序列化成 SchedulerModel 类型
        var schedulerModel = Penetrates.Deserialize<SchedulerModel>(json);

        return new SchedulerBuilder(JobBuilder.From(schedulerModel.JobDetail)
            , schedulerModel.Triggers.Select(t => TriggerBuilder.From(t).Appended()).ToList())
            .Appended();
    }

    /// <summary>
    /// 克隆作业计划构建器
    /// </summary>
    /// <param name="fromSchedulerBuilder">被克隆的作业计划构建器</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    public static SchedulerBuilder Clone(SchedulerBuilder fromSchedulerBuilder)
    {
        // 空检查
        if (fromSchedulerBuilder == null) throw new ArgumentNullException(nameof(fromSchedulerBuilder));

        return new SchedulerBuilder(JobBuilder.Clone(fromSchedulerBuilder.JobBuilder)
            , fromSchedulerBuilder.TriggerBuilders.Select(t => TriggerBuilder.Clone(t)).ToList())
            .Appended();
    }

    /// <summary>
    /// 获取作业信息构建器
    /// </summary>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder GetJobBuilder()
    {
        return JobBuilder;
    }

    /// <summary>
    /// 获取作业触发器构建器集合
    /// </summary>
    /// <returns><see cref="List{TriggerBuilder}"/></returns>
    public IReadOnlyList<TriggerBuilder> GetTriggerBuilders()
    {
        return TriggerBuilders;
    }

    /// <summary>
    /// 获取作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder GetTriggerBuilder(string triggerId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(triggerId)) throw new ArgumentNullException(nameof(triggerId));

        return TriggerBuilders.SingleOrDefault(t => t.TriggerId == triggerId);
    }

    /// <summary>
    /// 更新作业触发器构建器
    /// </summary>
    /// <param name="jobBuilder">作业触发器构建器</param>
    /// <param name="replace">是否完全替换为新的</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder UpdateJobBuilder(JobBuilder jobBuilder, bool replace = true)
    {
        // 空检查
        if (jobBuilder == null) throw new ArgumentNullException(nameof(jobBuilder));

        jobBuilder.MapTo<JobBuilder>(JobBuilder, !replace);

        // 初始化运行时作业类型和额外数据
        JobBuilder.SetJobType(JobBuilder.AssemblyName, JobBuilder.JobType)
            .SetProperties(JobBuilder.Properties);

        return this;
    }

    /// <summary>
    /// 添加作业触发器构建器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder AddTriggerBuilder(params TriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (triggerBuilders == null) throw new ArgumentNullException(nameof(triggerBuilders));

        foreach (var triggerBuilder in triggerBuilders)
        {
            TriggerBuilders.Add(triggerBuilder.Appended());
        }

        return this;
    }

    /// <summary>
    /// 更新作业触发器构建器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="replace">是否完全替换为新的</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder UpdateTriggerBuilder(TriggerBuilder triggerBuilder, bool replace = true)
    {
        // 空检查
        if (triggerBuilder == null) throw new ArgumentNullException(nameof(triggerBuilder));

        // 获取原来的作业触发器构建器
        var originTriggerBuilder = GetTriggerBuilder(triggerBuilder?.TriggerId);
        if (originTriggerBuilder != null)
        {
            triggerBuilder.MapTo<TriggerBuilder>(originTriggerBuilder, !replace);

            // 初始化运行时作业类型和额外数据
            originTriggerBuilder.SetTriggerType(originTriggerBuilder.AssemblyName, originTriggerBuilder.TriggerType)
                .SetArgs(originTriggerBuilder.Args);
        }

        return this;
    }

    /// <summary>
    /// 更新作业触发器构建器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder UpdateTriggerBuilder(params TriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (triggerBuilders == null) throw new ArgumentNullException(nameof(triggerBuilders));

        foreach (var triggerBuilder in triggerBuilders)
        {
            UpdateTriggerBuilder(triggerBuilder);
        }

        return this;
    }

    /// <summary>
    /// 删除作业触发器构建器
    /// </summary>
    /// <param name="triggerIds">作业触发器 Id</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder RemoveTriggerBuilder(params string[] triggerIds)
    {
        // 空检查
        if (triggerIds == null || triggerIds.Length == 0) return this;

        foreach (var triggerId in triggerIds)
        {
            GetTriggerBuilder(triggerId)?.Removed();
        }

        return this;
    }

    /// <summary>
    /// 清空作业触发器构建器
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder ClearTriggerBuilders()
    {
        TriggerBuilders.ForEach(t => t.Removed());

        return this;
    }

    /// <summary>
    /// 转换成 JSON 字符串
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToJSON(NamingConventions naming = NamingConventions.CamelCase)
    {
        return Penetrates.Write(writer =>
        {
            writer.WriteStartObject();

            // 输出 JobDetail
            writer.WritePropertyName(Penetrates.GetNaming(nameof(JobDetail), naming));
            writer.WriteRawValue(JobBuilder.ConvertToJSON(naming));

            // 输出 Triggers
            writer.WritePropertyName(Penetrates.GetNaming(nameof(Triggers), naming));

            writer.WriteStartArray();
            foreach (var triggerBuilder in TriggerBuilders)
            {
                writer.WriteRawValue(triggerBuilder.ConvertToJSON(naming));
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        });
    }

    /// <summary>
    /// 将作业计划构建器转换成可枚举集合
    /// </summary>
    /// <returns><see cref="Dictionary{JobBuilder, TriggerBuilder}"/></returns>
    public Dictionary<JobBuilder, TriggerBuilder> GetEnumerable()
    {
        var enumerable = new Dictionary<JobBuilder, TriggerBuilder>(new RepeatKeyEqualityComparer());

        TriggerBuilders.ForEach(triggerBuilder => enumerable.Add(JobBuilder, triggerBuilder));

        return enumerable;
    }

    /// <summary>
    /// 标记作业计划为新增行为
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder Appended()
    {
        Behavior = PersistenceBehavior.Appended;
        return this;
    }

    /// <summary>
    /// 标记作业计划为更新行为
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder Updated()
    {
        Behavior = PersistenceBehavior.Updated;
        return this;
    }

    /// <summary>
    /// 标记作业计划为删除行为
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder Removed()
    {
        Behavior = PersistenceBehavior.Removed;

        // 标记所有作业触发器持久化为删除状态
        TriggerBuilders.ForEach(triggerBuilder => triggerBuilder.Removed());

        return this;
    }

    /// <summary>
    /// 构建 <see cref="Scheduler"/> 对象
    /// </summary>
    /// <param name="count">作业调度器中当前作业计划总量</param>
    /// <returns><see cref="Scheduler"/></returns>
    internal Scheduler Build(int count)
    {
        // 配置默认 JobId
        if (string.IsNullOrWhiteSpace(JobBuilder.JobId))
        {
            JobBuilder.SetJobId($"job{count + 1}");
        }

        // 构建作业信息和作业触发器
        var jobDetail = JobBuilder.Build();

        // 构建作业触发器
        var triggers = new Dictionary<string, Trigger>();

        // 遍历作业触发器构建器集合
        foreach (var triggerBuilder in TriggerBuilders)
        {
            // 配置默认 TriggerId
            if (string.IsNullOrWhiteSpace(triggerBuilder.TriggerId))
            {
                triggerBuilder.SetTriggerId($"{jobDetail.JobId}_trigger{triggers.Count + 1}");
            }

            // 处理作业被标记删除的情况
            if (Behavior == PersistenceBehavior.Removed)
            {
                triggerBuilder.Removed();
            }

            var trigger = triggerBuilder.Build(jobDetail.JobId);
            var succeed = triggers.TryAdd(trigger.TriggerId, trigger);

            // 作业触发器 Id 唯一检查
            if (!succeed) throw new InvalidOperationException($"The <{trigger.TriggerId}> trigger for scheduler of <{jobDetail.JobId}> already exists.");
        }

        // 创建作业计划
        return new Scheduler(jobDetail, triggers);
    }
}