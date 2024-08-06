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
        var targetProperties = targetType.GetProperties(bindFlags).Where(u => u.CanWrite);

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
    /// 将时间格式化输出字符串
    /// </summary>
    /// <param name="dateTime"><see cref="DateTime"/></param>
    /// <returns><see cref="string"/></returns>
    internal static string ToFormatString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff zzz");
    }

    /// <summary>
    /// 将时间格式化输出字符串
    /// </summary>
    /// <param name="dateTime"><see cref="DateTime"/></param>
    /// <returns><see cref="string"/></returns>
    internal static string ToFormatString(this DateTime? dateTime)
    {
        return dateTime?.ToFormatString();
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