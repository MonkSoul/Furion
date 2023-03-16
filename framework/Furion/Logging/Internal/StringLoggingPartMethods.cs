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
        return LoggerFactory.Create(builder =>
        {
            builder.AddConsoleFormatter();
        });
    }
}