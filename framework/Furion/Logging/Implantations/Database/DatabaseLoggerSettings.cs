using Microsoft.Extensions.Logging;

namespace Furion.Logging;

/// <summary>
/// 数据库日志记录器配置选项
/// </summary>
[SuppressSniffer]
public sealed class DatabaseLoggerSettings
{
    /// <summary>
    /// 最低日志记录级别
    /// </summary>
    public LogLevel MinimumLevel { get; set; } = LogLevel.Trace;
}