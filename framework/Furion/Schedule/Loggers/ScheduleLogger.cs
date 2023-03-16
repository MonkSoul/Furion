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
using System.Logging;

namespace Furion.Schedule;

/// <summary>
/// 作业调度器日志默认实现类
/// </summary>
internal class ScheduleLogger : IScheduleLogger
{
    /// <summary>
    /// 日志对象
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志对象</param>
    /// <param name="logEnabled">是否启用日志记录</param>
    public ScheduleLogger(ILogger<ScheduleService> logger
        , bool logEnabled)
    {
        _logger = logger;
        LogEnabled = logEnabled;
    }

    /// <summary>
    /// 是否启用日志记录
    /// </summary>
    /// <remarks>以后这里的日志应该读取配置文件的 Logging:Level </remarks>
    private bool LogEnabled { get; }

    /// <summary>
    /// 记录 Information 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    public void LogInformation(string message, params object[] args)
    {
        Log(LogLevel.Information, message, args);
    }

    /// <summary>
    /// 记录 Trace 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    public void LogTrace(string message, params object[] args)
    {
        Log(LogLevel.Trace, message, args);
    }

    /// <summary>
    /// 记录 Debug 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    public void LogDebug(string message, params object[] args)
    {
        Log(LogLevel.Debug, message, args);
    }

    /// <summary>
    /// 记录 Warning 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    public void LogWarning(string message, params object[] args)
    {
        Log(LogLevel.Warning, message, args);
    }

    /// <summary>
    /// 记录 Critical 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    public void LogCritical(string message, params object[] args)
    {
        Log(LogLevel.Critical, message, args);
    }

    /// <summary>
    /// 记录 Error 日志
    /// </summary>
    /// <param name="ex">异常消息</param>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    public void LogError(Exception ex, string message, params object[] args)
    {
        Log(LogLevel.Error, message, args, ex);
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    /// <param name="ex">异常</param>
    public void Log(LogLevel logLevel, string message, object[] args = default, Exception ex = default)
    {
        // 如果未启用日志记录则直接返回
        if (!LogEnabled) return;

        if (logLevel == LogLevel.Error)
        {
            _logger.LogError(ex, message, args);
        }
        else
        {
            _logger.Log(logLevel, message, args);
        }
    }
}