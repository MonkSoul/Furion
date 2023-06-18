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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Furion.Schedule;

/// <summary>
/// Schedule 模块拓展类
/// </summary>
[SuppressSniffer]
public static class ScheduleExtensions
{
    /// <summary>
    /// 获取动态作业日志对象
    /// </summary>
    /// <param name="serviceProvider"><see cref="IServiceProvider"/></param>
    /// <returns><see cref="ILogger"/></returns>
    public static ILogger GetLogger(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<ILogger<System.Logging.DynamicJob>>();
    }

    /// <summary>
    /// 获取调度主机服务对象
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="ScheduleHostedService"/></returns>
    public static IHostedService GetScheduleHostedService(this IServiceCollection services)
    {
        return services.BuildServiceProvider().GetScheduleHostedService();
    }

    /// <summary>
    /// 获取调度主机服务对象
    /// </summary>
    /// <param name="serviceProvider"><see cref="IServiceProvider"/></param>
    /// <returns><see cref="ScheduleHostedService"/></returns>
    public static IHostedService GetScheduleHostedService(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetServices<IHostedService>().Single(t => t.GetType() == typeof(ScheduleHostedService));
    }

    /// <summary>
    /// 判断类型是否是 IJob 实现类型
    /// </summary>
    /// <param name="jobType">类型</param>
    /// <returns><see cref="bool"/></returns>
    public static bool IsJobType(this Type jobType)
    {
        // 检查 jobType 类型是否实现 IJob 接口
        if (!typeof(IJob).IsAssignableFrom(jobType)
            || jobType.IsInterface
            || jobType.IsAbstract) return false;

        return true;
    }

    /// <summary>
    /// 扫描类型集合并创建作业计划构建器集合
    /// </summary>
    /// <param name="jobTypes">作业类型集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static SchedulerBuilder[] ScanToBuilders(this IEnumerable<Type> jobTypes)
    {
        return jobTypes.Where(t => t.IsJobType()).Select(t => t.ScanToBuilder()).ToArray();
    }

    /// <summary>
    /// 扫描类型并创建作业计划构建器
    /// </summary>
    /// <param name="jobType">作业类型</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static SchedulerBuilder ScanToBuilder(this Type jobType)
    {
        // 扫描触发器构建器
        var triggerBuilders = jobType.ScanTriggers();

        // 检查类型是否贴有 [JobDetail] 特性
        if (!jobType.IsDefined(typeof(JobDetailAttribute), true))
        {
            return SchedulerBuilder.Create(JobBuilder.Create(jobType), triggerBuilders);
        }

        var jobDetailAttribute = jobType.GetCustomAttribute<JobDetailAttribute>(true);
        // 创建作业计划构建器返回
        return SchedulerBuilder.Create(JobBuilder.Create(jobType)
                .SetJobId(jobDetailAttribute.JobId)
                .SetGroupName(jobDetailAttribute.GroupName)
                .SetDescription(jobDetailAttribute.Description)
                .SetConcurrent(jobDetailAttribute.Concurrent)
            , triggerBuilders);
    }

    /// <summary>
    /// 扫描作业类型触发器特性
    /// </summary>
    /// <param name="jobType">作业类型</param>
    /// <returns><see cref="TriggerBuilder"/>[]</returns>
    public static TriggerBuilder[] ScanTriggers(this Type jobType)
    {
        // 空检查
        if (jobType == null) throw new ArgumentNullException(nameof(jobType));

        // 检查 jobType 类型是否实现 IJob 接口
        if (!jobType.IsJobType()) throw new InvalidOperationException($"The <{jobType.Name}> does not implement IJob interface.");

        // 扫描所有 [Trigger] 特性
        var triggerAttributes = jobType.GetCustomAttributes<TriggerAttribute>(true);

        var triggerBuilders = new List<TriggerBuilder>();

        // 遍历所有作业触发器特性并添加到集合中
        foreach (var triggerAttribute in triggerAttributes)
        {
            // 创建作业触发器并添加到当前作业触发器构建器中
            var triggerBuilder = TriggerBuilder.Create(triggerAttribute.RuntimeTriggerType)
                .SetArgs(triggerAttribute.RuntimeTriggerArgs)
                .SetTriggerId(triggerAttribute.TriggerId)
                .SetDescription(triggerAttribute.Description)
                .SetMaxNumberOfRuns(triggerAttribute.MaxNumberOfRuns)
                .SetMaxNumberOfErrors(triggerAttribute.MaxNumberOfErrors)
                .SetNumRetries(triggerAttribute.NumRetries)
                .SetRetryTimeout(triggerAttribute.RetryTimeout)
                .SetStartTime(triggerAttribute.RuntimeStartTime)
                .SetEndTime(triggerAttribute.RuntimeEndTime)
                .SetStartNow(triggerAttribute.StartNow)
                .SetRunOnStart(triggerAttribute.RunOnStart)
                .SetResetOnlyOnce(triggerAttribute.ResetOnlyOnce);

            triggerBuilders.Add(triggerBuilder);
        }

        return triggerBuilders.ToArray();
    }

    /// <summary>
    /// 对象映射
    /// </summary>
    /// <typeparam name="TTarget">目标类型</typeparam>
    /// <param name="source">源对象</param>
    /// <param name="target">目标类型对象</param>
    /// <param name="ignoreNullValue">忽略空值</param>
    /// <param name="ignorePropertyNames">忽略属性名</param>
    /// <returns>目标类型对象</returns>
    internal static TTarget MapTo<TTarget>(this object source
        , object target = default
        , bool ignoreNullValue = false
        , string[] ignorePropertyNames = default)
        where TTarget : class
    {
        if (source == null) return default;

        var sourceType = source.GetType();
        var targetType = typeof(TTarget);
        var bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        // 创建目标对象
        var constructors = targetType.GetConstructors(bindFlags);
        target ??= constructors.Length == 0 ? Activator.CreateInstance<TTarget>() : constructors[0].Invoke(null);

        // 获取目标类型所有属性
        var targetProperties = targetType.GetProperties(bindFlags);

        // 支持对象和 Dictionary<string, object> 类型
        var sourcePropertyValues = source is Dictionary<string, object> sourceDic
            ? sourceDic
            : sourceType.GetProperties(bindFlags)
                        .ToDictionary(u => u.Name, u => u.GetValue(source));

        // 遍历实例属性并设置
        foreach (var property in targetProperties)
        {
            // 多种属性命名解析
            var propertyName = property.Name;
            var camelCasePropertyName = Penetrates.GetNaming(propertyName, NamingConventions.CamelCase);
            var pascalPropertyName = Penetrates.GetNaming(propertyName, NamingConventions.Pascal);
            var underScoreCasePropertyName = Penetrates.GetNaming(propertyName, NamingConventions.UnderScoreCase);

            // 处理忽略属性问题
            if (ignorePropertyNames != null && ignorePropertyNames.Length > 0)
            {
                if (ignorePropertyNames.Contains(propertyName, StringComparer.OrdinalIgnoreCase)
                    || ignorePropertyNames.Contains(camelCasePropertyName, StringComparer.OrdinalIgnoreCase)
                    || ignorePropertyNames.Contains(pascalPropertyName, StringComparer.OrdinalIgnoreCase)
                    || ignorePropertyNames.Contains(underScoreCasePropertyName, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }
            }

            // 穷举方式获取值
            object value;
            if (sourcePropertyValues.ContainsKey(propertyName)) value = sourcePropertyValues[propertyName];
            else if (sourcePropertyValues.ContainsKey(camelCasePropertyName)) value = sourcePropertyValues[camelCasePropertyName];
            else if (sourcePropertyValues.ContainsKey(pascalPropertyName)) value = sourcePropertyValues[pascalPropertyName];
            else if (sourcePropertyValues.ContainsKey(underScoreCasePropertyName)) value = sourcePropertyValues[underScoreCasePropertyName];
            else continue;

            // 忽略空值控制
            if (ignoreNullValue && value == null) continue;

            property.SetValue(target, value);
        }

        return target as TTarget;
    }

    /// <summary>
    /// 将时间输出 Unspecified 格式字符串
    /// </summary>
    /// <param name="dateTime"><see cref="DateTime"/></param>
    /// <returns><see cref="string"/></returns>
    internal static string ToUnspecifiedString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }

    /// <summary>
    /// 将时间输出 Unspecified 格式字符串
    /// </summary>
    /// <param name="dateTime"><see cref="DateTime"/></param>
    /// <returns><see cref="string"/></returns>
    internal static string ToUnspecifiedString(this DateTime? dateTime)
    {
        return dateTime?.ToUnspecifiedString();
    }

    /// <summary>
    /// 字符串长度裁剪（不准确）
    /// </summary>
    /// <param name="str"><see cref="string"/></param>
    /// <param name="maxLength">长度，默认值 6</param>
    /// <returns><see cref="string"/></returns>
    internal static string GetMaxLengthString(this string str, int maxLength = 6)
    {
        if (str == null) return default;

        return str.Length > maxLength ? str[..maxLength] + "..." : str;
    }
}