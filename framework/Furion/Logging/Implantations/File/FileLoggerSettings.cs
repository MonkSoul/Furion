// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.Extensions.Logging;

namespace Furion.Logging;

/// <summary>
/// 文件日志配置类
/// </summary>
[SuppressSniffer]
public sealed class FileLoggerSettings
{
    /// <summary>
    /// 日志文件完整路径或文件名，推荐 .log 作为拓展名
    /// </summary>
    public string FileName { get; set; } = "application.log";

    /// <summary>
    /// 追加到已存在日志文件或覆盖它们
    /// </summary>
    public bool Append { get; set; } = true;

    /// <summary>
    /// 控制每一个日志文件最大存储大小，默认无限制，单位是 B，也就是 1024 才等于 1KB
    /// </summary>
    /// <remarks>如果指定了该值，那么日志文件大小超出了该配置就会创建的日志文件，新创建的日志文件命名规则：文件名+[递增序号].log</remarks>
    public long FileSizeLimitBytes { get; set; } = 0;

    /// <summary>
    /// 控制最大创建的日志文件数量，默认无限制，配合 <see cref="FileSizeLimitBytes"/> 使用
    /// </summary>
    /// <remarks>如果指定了该值，那么超出该值将从最初日志文件中从头写入覆盖</remarks>
    public int MaxRollingFiles { get; set; } = 0;

    /// <summary>
    /// 最低日志记录级别
    /// </summary>
    public LogLevel MinimumLevel { get; set; } = LogLevel.Trace;

    /// <summary>
    /// 是否使用 UTC 时间戳，默认 false
    /// </summary>
    public bool UseUtcTimestamp { get; set; }

    /// <summary>
    /// 日期格式化
    /// </summary>
    public string DateFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fffffff zzz dddd";

    /// <summary>
    /// 是否启用日志上下文
    /// </summary>
    public bool IncludeScopes { get; set; } = true;

    /// <summary>
    /// 显示跟踪/请求 Id
    /// </summary>
    public bool WithTraceId { get; set; } = false;

    /// <summary>
    /// 显示堆栈框架（程序集和方法签名）
    /// </summary>
    public bool WithStackFrame { get; set; } = false;
}