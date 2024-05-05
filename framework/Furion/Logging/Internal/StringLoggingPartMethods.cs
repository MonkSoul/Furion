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

namespace Furion.Logging;

/// <summary>
/// 构建字符串日志部分类
/// </summary>
public sealed partial class StringLoggingPart
{
    /// <summary>
    /// Information
    /// </summary>
    public void LogInformation()
    {
        SetLevel(LogLevel.Information).Log();
    }

    /// <summary>
    /// Warning
    /// </summary>
    public void LogWarning()
    {
        SetLevel(LogLevel.Warning).Log();
    }

    /// <summary>
    /// Error
    /// </summary>
    public void LogError()
    {
        SetLevel(LogLevel.Error).Log();
    }

    /// <summary>
    /// Debug
    /// </summary>
    public void LogDebug()
    {
        SetLevel(LogLevel.Debug).Log();
    }

    /// <summary>
    /// Trace
    /// </summary>
    public void LogTrace()
    {
        SetLevel(LogLevel.Trace).Log();
    }

    /// <summary>
    /// Critical
    /// </summary>
    public void LogCritical()
    {
        SetLevel(LogLevel.Critical).Log();
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <returns></returns>
    public void Log()
    {
        if (Message == null) return;

        // 获取日志实例
        var (logger, loggerFactory, hasException) = GetLogger();
        using var scope = logger.ScopeContext(LogContext);

        // 如果没有异常且事件 Id 为空
        if (Exception == null && EventId == null)
        {
            logger.Log(Level, Message, Args);
        }
        // 如果存在异常且事件 Id 为空
        else if (Exception != null && EventId == null)
        {
            logger.Log(Level, Exception, Message, Args);
        }
        // 如果异常为空且事件 Id 不为空
        else if (Exception == null && EventId != null)
        {
            logger.Log(Level, EventId.Value, Message, Args);
        }
        // 如果存在异常且事件 Id 不为空
        else if (Exception != null && EventId != null)
        {
            logger.Log(Level, EventId.Value, Exception, Message, Args);
        }
        else { }

        // 释放临时日志工厂
        if (hasException == true)
        {
            loggerFactory?.Dispose();
        }
    }

    /// <summary>
    /// 获取日志实例
    /// </summary>
    /// <returns></returns>
    internal (ILogger, ILoggerFactory, bool) GetLogger()
    {
        // 解析日志分类名
        var categoryType = CategoryType ?? typeof(System.Logging.StringLogging);

        ILoggerFactory loggerFactory = null;
        ILogger logger = null;
        var hasException = false;

        // 解决启动时打印日志问题
        if (App.RootServices == null)
        {
            hasException = true;
            loggerFactory = CreateDisposeLoggerFactory();
        }
        else
        {
            try
            {
                logger = App.GetRequiredService(typeof(ILogger<>).MakeGenericType(categoryType)) as ILogger;
            }
            catch
            {
                hasException = true;
                loggerFactory = CreateDisposeLoggerFactory();
            }
        }

        // 创建日志实例
        logger ??= loggerFactory.CreateLogger(categoryType.FullName);

        return (logger, loggerFactory, hasException);
    }

    /// <summary>
    /// 创建待释放的日志工厂
    /// </summary>
    /// <returns></returns>
    private static ILoggerFactory CreateDisposeLoggerFactory()
    {
        var consoleFormatterExtendOptions = App.GetOptions<ConsoleFormatterExtendOptions>();

        Action<ConsoleFormatterExtendOptions> configure = consoleFormatterExtendOptions is not null
            ? (opt =>
            {
                opt.IncludeScopes = consoleFormatterExtendOptions.IncludeScopes;
                opt.TimestampFormat = consoleFormatterExtendOptions.TimestampFormat;
                opt.UseUtcTimestamp = consoleFormatterExtendOptions.UseUtcTimestamp;
                opt.ColorBehavior = consoleFormatterExtendOptions.ColorBehavior;
                opt.MessageFormat = consoleFormatterExtendOptions.MessageFormat;
                opt.DateFormat = consoleFormatterExtendOptions.DateFormat;
                opt.WriteFilter = consoleFormatterExtendOptions.WriteFilter;
                opt.WriteHandler = consoleFormatterExtendOptions.WriteHandler;
                opt.WithTraceId = consoleFormatterExtendOptions.WithTraceId;
                opt.WithStackFrame = consoleFormatterExtendOptions.WithStackFrame;
                opt.MessageProcess = consoleFormatterExtendOptions.MessageProcess;
            })
        : null;

        return LoggerFactory.Create(builder =>
        {
            builder.AddConsoleFormatter(configure);
        });
    }
}