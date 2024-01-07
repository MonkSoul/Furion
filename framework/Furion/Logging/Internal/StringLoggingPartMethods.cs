// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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
            })
        : null;

        return LoggerFactory.Create(builder =>
        {
            builder.AddConsoleFormatter(configure);
        });
    }
}