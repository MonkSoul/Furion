// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.Extensions.Logging;
using System.Logging;

namespace Furion.Logging;

/// <summary>
/// 构建字符串日志部分类
/// </summary>
[SuppressSniffer]
public sealed partial class StringLoggingPart
{
    /// <summary>
    /// 静态缺省日志部件
    /// </summary>
    public static StringLoggingPart Default()
    {
        return new();
    }

    /// <summary>
    /// 日志内容
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// 日志级别
    /// </summary>
    public LogLevel Level { get; private set; } = LogLevel.Information;

    /// <summary>
    /// 消息格式化参数
    /// </summary>
    public object[] Args { get; private set; }

    /// <summary>
    /// 事件 Id
    /// </summary>
    public EventId? EventId { get; private set; }

    /// <summary>
    /// 日志分类类型
    /// </summary>
    public Type CategoryType { get; private set; } = typeof(StringLogging);

    /// <summary>
    /// 异常对象
    /// </summary>
    public Exception Exception { get; private set; }

    /// <summary>
    /// 日志对象所在作用域
    /// </summary>
    public IServiceProvider LoggerScoped { get; private set; }

    /// <summary>
    /// 日志上下文
    /// </summary>
    public LogContext LogContext { get; private set; }
}