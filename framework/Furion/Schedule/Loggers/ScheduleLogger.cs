// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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