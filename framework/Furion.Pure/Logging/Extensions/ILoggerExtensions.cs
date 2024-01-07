// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Logging;

namespace Microsoft.Extensions.Logging;

/// <summary>
/// <see cref="ILogger"/> 拓展
/// </summary>
[SuppressSniffer]
public static class ILoggerExtensions
{
    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="properties">建议使用 ConcurrentDictionary 类型</param>
    /// <returns></returns>
    public static IDisposable ScopeContext(this ILogger logger, IDictionary<object, object> properties)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));

        return logger.BeginScope(new LogContext { Properties = properties });
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IDisposable ScopeContext(this ILogger logger, Action<LogContext> configure)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));

        var logContext = new LogContext();
        configure?.Invoke(logContext);

        return logger.BeginScope(logContext);
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IDisposable ScopeContext(this ILogger logger, LogContext context)
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));

        return logger.BeginScope(context);
    }
}