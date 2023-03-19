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
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Furion.Logging;

/// <summary>
/// 控制台默认格式化程序拓展
/// </summary>
[SuppressSniffer]
public sealed class ConsoleFormatterExtend : ConsoleFormatter, IDisposable
{
    /// <summary>
    /// 日志格式化选项刷新 Token
    /// </summary>
    private readonly IDisposable _formatOptionsReloadToken;

    /// <summary>
    /// 日志格式化配置选项
    /// </summary>
    private ConsoleFormatterExtendOptions _formatterOptions;

    /// <summary>
    /// 是否启用控制台颜色
    /// </summary>
    private bool _disableColors;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="formatterOptions"></param>
    public ConsoleFormatterExtend(IOptionsMonitor<ConsoleFormatterExtendOptions> formatterOptions)
        : base("console-format")
    {
        (_formatOptionsReloadToken, _formatterOptions) = (formatterOptions.OnChange(ReloadFormatterOptions), formatterOptions.CurrentValue);
        _disableColors = _formatterOptions.ColorBehavior == LoggerColorBehavior.Disabled || (_formatterOptions.ColorBehavior == LoggerColorBehavior.Default && Console.IsOutputRedirected);
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="logEntry"></param>
    /// <param name="scopeProvider"></param>
    /// <param name="textWriter"></param>
    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
    {
        // 获取格式化后的消息
        var message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);

        // 创建日志消息
        var logDateTime = _formatterOptions.UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now;
        var logMsg = new LogMessage(logEntry.Category, logEntry.LogLevel, logEntry.EventId, message, logEntry.Exception, null, logEntry.State, logDateTime, Environment.CurrentManagedThreadId, _formatterOptions.UseUtcTimestamp, App.GetTraceId());

        string standardMessage;

        // 是否自定义了自定义日志格式化程序，如果是则使用
        if (_formatterOptions.MessageFormat != null)
        {
            // 设置日志上下文
            logMsg = Penetrates.SetLogContext(scopeProvider, logMsg, _formatterOptions.IncludeScopes);

            // 设置日志消息模板
            standardMessage = _formatterOptions.MessageFormat(logMsg);
        }
        else
        {
            // 获取标准化日志消息
            standardMessage = Penetrates.OutputStandardMessage(logMsg
               , _formatterOptions.DateFormat
               , true
               , _disableColors
               , _formatterOptions.WithTraceId
               , _formatterOptions.WithStackFrame);
        }

        // 空检查
        if (message is null) return;

        // 判断是否自定义了日志格式化程序
        if (_formatterOptions.WriteHandler != null)
        {
            _formatterOptions.WriteHandler?.Invoke(logMsg, scopeProvider, textWriter, standardMessage, _formatterOptions);
        }
        else
        {
            // 写入控制台
            textWriter.WriteLine(standardMessage);
        }
    }

    /// <summary>
    /// 释放非托管资源
    /// </summary>
    public void Dispose()
    {
        _formatOptionsReloadToken?.Dispose();
    }

    /// <summary>
    /// 刷新日志格式化选项
    /// </summary>
    /// <param name="options"></param>
    private void ReloadFormatterOptions(ConsoleFormatterExtendOptions options)
    {
        _formatterOptions = options;
        _disableColors = options.ColorBehavior == LoggerColorBehavior.Disabled || (options.ColorBehavior == LoggerColorBehavior.Default && Console.IsOutputRedirected);
    }
}