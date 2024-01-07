// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.Extensions.Logging;

namespace Furion.Schedule;

/// <summary>
/// 作业调度器日志服务
/// </summary>
public interface IScheduleLogger
{
    /// <summary>
    /// 记录 Information 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    void LogInformation(string message, params object[] args);

    /// <summary>
    /// 记录 Trace 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    void LogTrace(string message, params object[] args);

    /// <summary>
    /// 记录 Debug 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    void LogDebug(string message, params object[] args);

    /// <summary>
    /// 记录 Warning 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    void LogWarning(string message, params object[] args);

    /// <summary>
    /// 记录 Critical 日志
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    void LogCritical(string message, params object[] args);

    /// <summary>
    /// 记录 Error 日志
    /// </summary>
    /// <param name="ex">异常消息</param>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    void LogError(Exception ex, string message, params object[] args);

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    /// <param name="ex">异常</param>
    void Log(LogLevel logLevel, string message, object[] args = default, Exception ex = default);
}