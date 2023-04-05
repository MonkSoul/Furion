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