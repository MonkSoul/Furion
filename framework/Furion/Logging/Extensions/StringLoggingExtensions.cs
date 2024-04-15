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

using Microsoft.Extensions.Logging;

namespace Furion.Logging.Extensions;

/// <summary>
/// 字符串日志输出拓展
/// </summary>
[SuppressSniffer]
public static class StringLoggingExtensions
{
    /// <summary>
    /// 设置消息格式化参数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static StringLoggingPart SetArgs(this string message, params object[] args)
    {
        return StringLoggingPart.Default().SetMessage(message).SetArgs(args);
    }

    /// <summary>
    /// 设置日志级别
    /// </summary>
    /// <param name="message"></param>
    /// <param name="level"></param>
    public static StringLoggingPart SetLevel(this string message, LogLevel level)
    {
        return StringLoggingPart.Default().SetMessage(message).SetLevel(level);
    }

    /// <summary>
    /// 设置事件 Id
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    public static StringLoggingPart SetEventId(this string message, EventId eventId)
    {
        return StringLoggingPart.Default().SetMessage(message).SetEventId(eventId);
    }

    /// <summary>
    /// 设置日志分类
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="TClass"></typeparam>
    public static StringLoggingPart SetCategory<TClass>(this string message)
    {
        return StringLoggingPart.Default().SetMessage(message).SetCategory<TClass>();
    }

    /// <summary>
    /// 设置异常对象
    /// </summary>
    public static StringLoggingPart SetException(this string message, Exception exception)
    {
        return StringLoggingPart.Default().SetMessage(message).SetException(exception);
    }

    /// <summary>
    /// 设置日志服务作用域
    /// </summary>
    /// <param name="message"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static StringLoggingPart SetLoggerScoped(this string message, IServiceProvider serviceProvider)
    {
        return StringLoggingPart.Default().SetMessage(message).SetLoggerScoped(serviceProvider);
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="message"></param>
    /// <param name="properties">建议使用 ConcurrentDictionary 类型</param>
    /// <returns></returns>
    public static StringLoggingPart ScopeContext(this string message, IDictionary<object, object> properties)
    {
        return StringLoggingPart.Default().SetMessage(message).ScopeContext(properties);
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="message"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static StringLoggingPart ScopeContext(this string message, Action<LogContext> configure)
    {
        return StringLoggingPart.Default().SetMessage(message).ScopeContext(configure);
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="message"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static StringLoggingPart ScopeContext(this string message, LogContext context)
    {
        return StringLoggingPart.Default().SetMessage(message).ScopeContext(context);
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogInformation(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).LogInformation();
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogInformation(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).LogInformation();
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogInformation(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetException(exception).LogInformation();
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogInformation(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogInformation();
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogInformation<TClass>(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).LogInformation();
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogInformation<TClass>(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).LogInformation();
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogInformation<TClass>(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetException(exception).LogInformation();
    }

    /// <summary>
    /// LogInformation
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogInformation<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogInformation();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogWarning(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).LogWarning();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogWarning(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).LogWarning();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogWarning(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetException(exception).LogWarning();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogWarning(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogWarning();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogWarning<TClass>(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).LogWarning();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogWarning<TClass>(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).LogWarning();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogWarning<TClass>(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetException(exception).LogWarning();
    }

    /// <summary>
    /// LogWarning
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogWarning<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogWarning();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogError(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).LogError();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogError(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).LogError();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogError(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetException(exception).LogError();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogError(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogError();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogError<TClass>(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).LogError();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogError<TClass>(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).LogError();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogError<TClass>(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetException(exception).LogError();
    }

    /// <summary>
    /// LogError
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogError<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogError();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogDebug(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).LogDebug();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogDebug(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).LogDebug();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogDebug(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetException(exception).LogDebug();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogDebug(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogDebug();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogDebug<TClass>(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).LogDebug();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogDebug<TClass>(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).LogDebug();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogDebug<TClass>(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetException(exception).LogDebug();
    }

    /// <summary>
    /// LogDebug
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogDebug<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogDebug();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogTrace(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).LogTrace();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogTrace(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).LogTrace();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogTrace(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetException(exception).LogTrace();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogTrace(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogTrace();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogTrace<TClass>(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).LogTrace();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogTrace<TClass>(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).LogTrace();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogTrace<TClass>(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetException(exception).LogTrace();
    }

    /// <summary>
    /// LogTrace
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogTrace<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogTrace();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogCritical(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).LogCritical();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogCritical(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).LogCritical();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogCritical(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetException(exception).LogCritical();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogCritical(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogCritical();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public static void LogCritical<TClass>(this string message, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).LogCritical();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    public static void LogCritical<TClass>(this string message, EventId eventId, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).LogCritical();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogCritical<TClass>(this string message, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetException(exception).LogCritical();
    }

    /// <summary>
    /// LogCritical
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <param name="args"></param>
    public static void LogCritical<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
    {
        StringLoggingPart.Default().SetCategory<TClass>().SetMessage(message).SetArgs(args).SetEventId(eventId).SetException(exception).LogCritical();
    }
}