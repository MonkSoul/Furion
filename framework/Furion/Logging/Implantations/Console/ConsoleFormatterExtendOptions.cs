// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Furion.Logging;

/// <summary>
/// 控制台默认格式化选项拓展
/// </summary>
[SuppressSniffer]
public sealed class ConsoleFormatterExtendOptions : ConsoleFormatterOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ConsoleFormatterExtendOptions()
        : base()
    {
        // 默认启用控制台日志上下文功能
        IncludeScopes = true;
    }

    /// <summary>
    /// 控制是否启用颜色
    /// </summary>
    public LoggerColorBehavior ColorBehavior { get; set; }

    /// <summary>
    /// 自定义日志消息格式化程序
    /// </summary>
    public Func<LogMessage, string> MessageFormat { get; set; }

    /// <summary>
    /// 日期格式化
    /// </summary>
    public string DateFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fffffff zzz dddd";

    /// <summary>
    /// 自定义日志筛选器
    /// </summary>
    public Func<LogMessage, bool> WriteFilter { get; set; }

    /// <summary>
    /// 自定义格式化日志处理程序
    /// </summary>
    public Action<LogMessage, IExternalScopeProvider, TextWriter, string, ConsoleFormatterExtendOptions> WriteHandler { get; set; }

    /// <summary>
    /// 显示跟踪/请求 Id
    /// </summary>
    public bool WithTraceId { get; set; } = false;

    /// <summary>
    /// 显示堆栈框架（程序集和方法签名）
    /// </summary>
    public bool WithStackFrame { get; set; } = false;
}