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

using Furion.TimeCrontab;

namespace Furion.Schedule;

/// <summary>
/// 作业触发器构建器
/// </summary>
[SuppressSniffer]
public sealed partial class TriggerBuilder : Trigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    private TriggerBuilder()
    {
    }

    /// <summary>
    /// 创建毫秒周期（间隔）作业触发器构建器
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Period(long interval)
    {
        return Create<PeriodTrigger>(interval);
    }

    /// <summary>
    /// 创建 Cron 表达式作业触发器构建器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型，默认 <see cref="CronStringFormat.Default"/></param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Cron(string schedule, CronStringFormat format = CronStringFormat.Default)
    {
        return Create<CronTrigger>(schedule, format);
    }

    /// <summary>
    /// 创建 Cron 表达式作业触发器构建器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="args">动态参数类型，支持 <see cref="int"/>，<see cref="CronStringFormat"/> 和 object[]</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    internal static TriggerBuilder Cron(string schedule, object args)
    {
        return Create<CronTrigger>(schedule, args);
    }

    /// <summary>
    /// 创建作业触发器构建器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public static TriggerBuilder Create(string triggerId)
    {
        return new TriggerBuilder()
            .SetTriggerId(triggerId);
    }

    /// <summary>
    /// 创建作业触发器构建器
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="Trigger"/> 派生类</typeparam>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create<TTrigger>()
        where TTrigger : Trigger
    {
        return Create(typeof(TTrigger));
    }

    /// <summary>
    /// 创建作业触发器构建器
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="Trigger"/> 派生类</typeparam>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create<TTrigger>(params object[] args)
        where TTrigger : Trigger
    {
        return Create<TTrigger>().SetArgs(args);
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(string assemblyName, string triggerTypeFullName)
    {
        return new TriggerBuilder()
            .SetTriggerType(assemblyName, triggerTypeFullName)
            .Appended();
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(string assemblyName, string triggerTypeFullName, params object[] args)
    {
        return Create(assemblyName, triggerTypeFullName).SetArgs(args);
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="triggerType"><see cref="Trigger"/> 派生类</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(Type triggerType)
    {
        return new TriggerBuilder()
            .SetTriggerType(triggerType)
            .Appended();
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="triggerType"><see cref="Trigger"/> 派生类</param>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(Type triggerType, params object[] args)
    {
        return Create(triggerType).SetArgs(args);
    }

    /// <summary>
    /// 将 <see cref="Trigger"/> 转换成 <see cref="TriggerBuilder"/>
    /// </summary>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder From(Trigger trigger)
    {
        var triggerBuilder = trigger.MapTo<TriggerBuilder>();

        // 初始化运行时作业触发器类型和参数
        triggerBuilder.SetTriggerType(triggerBuilder.AssemblyName, triggerBuilder.TriggerType)
            .SetArgs(triggerBuilder.Args);

        return triggerBuilder.Updated();
    }

    /// <summary>
    /// 将 JSON 字符串转换成 <see cref="TriggerBuilder"/>
    /// </summary>
    /// <param name="json">JSON 字符串</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder From(string json)
    {
        return From(Penetrates.Deserialize<Trigger>(json)).Appended();
    }

    /// <summary>
    /// 克隆作业触发器构建器
    /// </summary>
    /// <param name="fromTriggerBuilder">被克隆的作业触发器构建器</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Clone(TriggerBuilder fromTriggerBuilder)
    {
        return Create(fromTriggerBuilder.AssemblyName, fromTriggerBuilder.TriggerType)
                     .SetArgs(fromTriggerBuilder.Args)
                     .SetDescription(fromTriggerBuilder.Description)
                     .SetStartTime(fromTriggerBuilder.StartTime)
                     .SetEndTime(fromTriggerBuilder.EndTime)
                     .SetMaxNumberOfRuns(fromTriggerBuilder.MaxNumberOfRuns)
                     .SetMaxNumberOfErrors(fromTriggerBuilder.MaxNumberOfErrors)
                     .SetNumRetries(fromTriggerBuilder.NumRetries)
                     .SetRetryTimeout(fromTriggerBuilder.RetryTimeout)
                     .SetStartNow(fromTriggerBuilder.StartNow)
                     .SetRunOnStart(fromTriggerBuilder.RunOnStart)
                     .SetResetOnlyOnce(fromTriggerBuilder.ResetOnlyOnce);
    }

    /// <summary>
    /// 从目标值填充数据到作业触发器构建器
    /// </summary>
    /// <param name="value">目标值</param>
    /// <param name="ignoreNullValue">忽略空值</param>
    /// <param name="ignorePropertyNames">忽略属性名</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder LoadFrom(object value, bool ignoreNullValue = false, string[] ignorePropertyNames = default)
    {
        if (value == null) return this;

        // 排除枚举类型，接口类型，数组类型，值类型
        var valueType = value.GetType();
        if (valueType.IsInterface
            || valueType.IsValueType
            || valueType.IsEnum
            || valueType.IsArray) throw new InvalidOperationException(nameof(value));

        var triggerBuilder = value.MapTo<TriggerBuilder>(this, ignoreNullValue, ignorePropertyNames);

        // 初始化运行时作业触发器类型和参数
        triggerBuilder.SetTriggerType(triggerBuilder.AssemblyName, triggerBuilder.TriggerType)
            .SetArgs(triggerBuilder.Args);

        return triggerBuilder;
    }

    /// <summary>
    /// 设置作业触发器 Id
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetTriggerId(string triggerId)
    {
        TriggerId = triggerId;

        return this;
    }

    /// <summary>
    /// 设置作业触发器类型
    /// </summary>
    /// <param name="assemblyName">作业触发器所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器 FullName</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetTriggerType(string assemblyName, string triggerTypeFullName)
    {
        AssemblyName = assemblyName;
        TriggerType = triggerTypeFullName;

        // 只有 assemblyName 和 triggerTypeFullName 同时存在才创建类型
        if (!string.IsNullOrWhiteSpace(assemblyName)
            && !string.IsNullOrWhiteSpace(triggerTypeFullName))
        {
            // 加载 GAC 全局应用程序缓存中的程序集及类型
            var triggerType = Penetrates.LoadAssembly(assemblyName)
                .GetType(triggerTypeFullName);

            return SetTriggerType(triggerType);
        }

        return this;
    }

    /// <summary>
    /// 设置作业触发器类型
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="Trigger"/> 派生类类型</typeparam>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetTriggerType<TTrigger>()
        where TTrigger : Trigger
    {
        return SetTriggerType(typeof(TTrigger));
    }

    /// <summary>
    /// 设置作业触发器类型
    /// </summary>
    /// <param name="triggerType">作业触发器类型</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetTriggerType(Type triggerType)
    {
        // 不做 null 检查
        if (triggerType == null) return this;

        // 检查 triggerType 类型是否派生自 Trigger
        if (!typeof(Trigger).IsAssignableFrom(triggerType)
            || triggerType == typeof(Trigger)
            || triggerType.IsInterface
            || triggerType.IsAbstract) throw new InvalidOperationException("The <triggerType> is not a valid Trigger type.");

        // 最多只能包含一个构造函数
        if (triggerType.GetConstructors().Length > 1) throw new InvalidOperationException("The <triggerType> can contain at most one constructor.");

        AssemblyName = triggerType.Assembly.GetName().Name;
        TriggerType = triggerType.FullName;
        RuntimeTriggerType = triggerType;

        return this;
    }

    /// <summary>
    /// 设置作业触发器参数
    /// </summary>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetArgs(string args)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(args) || args == "[]") args = null;

        Args = args;
        if (args == null) return this;

        var jsonObjectArray = Penetrates.Deserialize<object[]>(args);
        var runtimeArgs = new object[jsonObjectArray.Length];

        // 解决反序列化 object 类型被转换成了 JsonElement 类型
        for (var i = 0; i < jsonObjectArray.Length; i++)
        {
            runtimeArgs[i] = Penetrates.GetJsonElementValue(jsonObjectArray[i]);
        }

        RuntimeTriggerArgs = runtimeArgs;

        return this;
    }

    /// <summary>
    /// 设置作业触发器参数
    /// </summary>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetArgs(params object[] args)
    {
        Args = args == null || args.Length == 0
            ? null
            : Penetrates.Serialize(args);
        RuntimeTriggerArgs = args;

        return this;
    }

    /// <summary>
    /// 设置描述信息
    /// </summary>
    /// <param name="description">描述信息</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetDescription(string description)
    {
        Description = description;

        return this;
    }

    /// <summary>
    /// 设置作业触发器状态
    /// </summary>
    /// <param name="status">作业触发器状态</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public new TriggerBuilder SetStatus(TriggerStatus status)
    {
        Status = status;

        return this;
    }

    /// <summary>
    /// 设置起始时间
    /// </summary>
    /// <param name="startTime">起始时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetStartTime(DateTime? startTime)
    {
        StartTime = startTime;

        return this;
    }

    /// <summary>
    /// 设置结束时间
    /// </summary>
    /// <param name="endTime">结束时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetEndTime(DateTime? endTime)
    {
        EndTime = endTime;

        return this;
    }

    /// <summary>
    /// 设置最近运行时间
    /// </summary>
    /// <param name="lastRunTime">最近运行时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetLastRunTime(DateTime? lastRunTime)
    {
        LastRunTime = lastRunTime;

        return this;
    }

    /// <summary>
    /// 设置下一次运行时间
    /// </summary>
    /// <param name="nextRunTime">下一次运行时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNextRunTime(DateTime? nextRunTime)
    {
        NextRunTime = nextRunTime;

        return this;
    }

    /// <summary>
    /// 设置触发次数
    /// </summary>
    /// <param name="numberOfRuns">触发次数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNumberOfRuns(long numberOfRuns)
    {
        NumberOfRuns = numberOfRuns;

        return this;
    }

    /// <summary>
    /// 设置最大触发次数
    /// </summary>
    /// <param name="maxNumberOfRuns">最大触发次数</param>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>>n：N 次</para>
    /// </remarks>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetMaxNumberOfRuns(long maxNumberOfRuns)
    {
        MaxNumberOfRuns = maxNumberOfRuns;

        return this;
    }

    /// <summary>
    /// 设置出错次数
    /// </summary>
    /// <param name="numberOfErrors">出错次数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNumberOfErrors(long numberOfErrors)
    {
        NumberOfErrors = numberOfErrors;

        return this;
    }

    /// <summary>
    /// 设置最大出错次数
    /// </summary>
    /// <param name="maxNumberOfErrors">最大出错次数</param>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetMaxNumberOfErrors(long maxNumberOfErrors)
    {
        MaxNumberOfErrors = maxNumberOfErrors;

        return this;
    }

    /// <summary>
    /// 设置重试次数
    /// </summary>
    /// <param name="numRetries">重试次数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNumRetries(int numRetries)
    {
        NumRetries = numRetries;

        return this;
    }

    /// <summary>
    /// 设置重试间隔时间
    /// </summary>
    /// <param name="retryTimeout">重试间隔时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetRetryTimeout(int retryTimeout)
    {
        RetryTimeout = retryTimeout;

        return this;
    }

    /// <summary>
    /// 设置是否立即启动
    /// </summary>
    /// <param name="startNow">是否立即启动</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetStartNow(bool startNow)
    {
        StartNow = startNow;

        if (startNow == false && Status != TriggerStatus.NotStart)
        {
            SetNextRunTime(null);
            SetStatus(TriggerStatus.NotStart);
        }

        return this;
    }

    /// <summary>
    /// 设置是否启动时执行一次
    /// </summary>
    /// <param name="runOnStart">是否启动时执行一次</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetRunOnStart(bool runOnStart)
    {
        RunOnStart = runOnStart;

        return this;
    }

    /// <summary>
    /// 设置是否在启动时重置最大触发次数等于一次的作业
    /// </summary>
    /// <param name="resetOnlyOnce">是否在启动时重置最大触发次数等于一次的作业</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetResetOnlyOnce(bool resetOnlyOnce)
    {
        ResetOnlyOnce = resetOnlyOnce;

        return this;
    }

    /// <summary>
    /// 设置本次执行结果
    /// </summary>
    /// <param name="result">设置本次执行结果</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetResult(string result)
    {
        Result = result;

        return this;
    }

    /// <summary>
    /// 设置本次执行耗时
    /// </summary>
    /// <param name="elapsedTime">本次执行耗时</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetElapsedTime(long elapsedTime)
    {
        ElapsedTime = elapsedTime;

        return this;
    }

    /// <summary>
    /// 标记作业触发器计划为新增行为
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder Appended()
    {
        Behavior = PersistenceBehavior.Appended;
        return this;
    }

    /// <summary>
    /// 标记作业触发器计划为更新行为
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder Updated()
    {
        Behavior = PersistenceBehavior.Updated;
        return this;
    }

    /// <summary>
    /// 标记作业触发器为删除行为
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder Removed()
    {
        Behavior = PersistenceBehavior.Removed;
        return this;
    }

    /// <summary>
    /// 隐藏作业触发器公开方法
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public new DateTime GetNextOccurrence(DateTime startAt) => throw new NotImplementedException();

    /// <summary>
    /// 隐藏作业触发器公开方法
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="bool"/></returns>
    public new bool ShouldRun(JobDetail jobDetail, DateTime startAt) => throw new NotImplementedException();

    /// <summary>
    /// 构建 <see cref="Trigger"/> 对象
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="Trigger"/></returns>
    internal Trigger Build(string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        // 避免类型还未初始化，强制检查一次
        SetTriggerType(AssemblyName, TriggerType);
        SetArgs(Args);

        // 检查 StartTime 和 EndTime 的关系，StartTime 不能大于 EndTime
        if (StartTime != null && EndTime != null
            && StartTime.Value > EndTime.Value) throw new InvalidOperationException("The start time cannot be greater than the end time.");

        JobId = jobId;

        // 判断是否带参数
        var hasArgs = !(RuntimeTriggerArgs == null || RuntimeTriggerArgs.Length == 0);

        // 反射创建作业触发器对象
        var triggerInstance = RuntimeTriggerType != null
            ? ((!hasArgs
                ? Activator.CreateInstance(RuntimeTriggerType)
                : Activator.CreateInstance(RuntimeTriggerType, RuntimeTriggerArgs)))
            : null;

        return this.MapTo<Trigger>(triggerInstance);
    }
}