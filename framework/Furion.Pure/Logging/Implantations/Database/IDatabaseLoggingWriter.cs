// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Logging;

/// <summary>
/// 数据库日志写入器
/// </summary>
public interface IDatabaseLoggingWriter
{
    /// <summary>
    /// 写入数据库
    /// </summary>
    /// <param name="logMsg">结构化日志消息</param>
    /// <param name="flush">清除缓冲区</param>
    /// <returns><see cref="Task"/></returns>
    Task WriteAsync(LogMessage logMsg, bool flush);
}