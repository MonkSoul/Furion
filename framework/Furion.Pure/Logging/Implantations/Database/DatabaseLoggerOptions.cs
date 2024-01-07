// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.Extensions.Logging;

namespace Furion.Logging;

/// <summary>
/// 数据库记录器配置选项
/// </summary>
[SuppressSniffer]
public sealed class DatabaseLoggerOptions
{
    /// <summary>
    /// 自定义日志筛选器
    /// </summary>
    public Func<LogMessage, bool> WriteFilter { get; set; }

    /// <summary>
    /// 最低日志记录级别
    /// </summary>
    public LogLevel MinimumLevel { get; set; } = LogLevel.Trace;

    /// <summary>
    /// 自定义日志消息格式化程序
    /// </summary>
    public Func<LogMessage, string> MessageFormat { get; set; }

    /// <summary>
    /// 自定义数据库日志写入错误程序
    /// </summary>
    /// <remarks>主要解决日志在写入过程出现异常问题</remarks>
    /// <example>
    /// options.HandleWriteError = (err) => {
    ///     // do anything
    /// };
    /// </example>
    public Action<DatabaseWriteError> HandleWriteError { get; set; }

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
    /// 忽略日志循环输出
    /// </summary>
    /// <remarks>对性能有些许影响</remarks>
    public bool IgnoreReferenceLoop { get; set; } = true;

    /// <summary>
    /// 显示跟踪/请求 Id
    /// </summary>
    public bool WithTraceId { get; set; } = false;

    /// <summary>
    /// 显示堆栈框架（程序集和方法签名）
    /// </summary>
    public bool WithStackFrame { get; set; } = false;
}