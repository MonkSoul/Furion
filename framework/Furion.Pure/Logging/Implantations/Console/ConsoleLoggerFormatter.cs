// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Furion.Logging;

/// <summary>
/// 控制台日志格式化程序
/// </summary>
[SuppressSniffer]
public sealed class ConsoleLoggerFormatter : ConsoleFormatter, IDisposable
{
    /// <summary>
    /// 日志格式化选项刷新 Token
    /// </summary>
    private readonly IDisposable _formatOptionsReloadToken;

    /// <summary>
    /// 日志选项刷新 Token
    /// </summary>
    private readonly IDisposable _optionsReloadToken;

    /// <summary>
    /// 日志格式化配置选项
    /// </summary>
    private ConsoleFormatterSettingsOptions _formatterOptions;

    /// <summary>
    /// 日志配置选项
    /// </summary>
    private SimpleConsoleFormatterOptions _options;

    /// <summary>
    /// 是否启用控制台颜色
    /// </summary>
    private bool _disableColors;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="formatterOptions"></param>
    /// <param name="options"></param>
    public ConsoleLoggerFormatter(IOptionsMonitor<ConsoleFormatterSettingsOptions> formatterOptions
        , IOptionsMonitor<SimpleConsoleFormatterOptions> options)
        : base("console-format")
    {
        (_formatOptionsReloadToken, _formatterOptions) = (formatterOptions.OnChange(ReloadFormatterOptions), formatterOptions.CurrentValue);
        (_optionsReloadToken, _options) = (options.OnChange(ReloadLoggerOptions), options.CurrentValue);
        _disableColors = _options.ColorBehavior == LoggerColorBehavior.Disabled || (_options.ColorBehavior == LoggerColorBehavior.Default && Console.IsOutputRedirected);
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

        string standardMessage;

        // 是否自定义了自定义日志格式化程序，如果是则使用
        if (_formatterOptions.MessageFormat != null)
        {
            // 设置日志消息模板
            standardMessage = _formatterOptions.MessageFormat(
                new LogMessage(logEntry.Category, logEntry.LogLevel, logEntry.EventId, message, logEntry.Exception, null));
        }
        else
        {
            // 获取标准化日志消息
            standardMessage = Penetrates.OutputStandardMessage(message
               , logEntry.Category
               , _formatterOptions.UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now
               , logEntry.LogLevel
               , logEntry.EventId
               , logEntry.Exception
               , _formatterOptions.DateFormat
               , true
               , _disableColors);
        }

        // 空检查
        if (message is null) return;

        // 写入控制台
        textWriter.WriteLine(standardMessage);
    }

    /// <summary>
    /// 释放非托管资源
    /// </summary>
    public void Dispose()
    {
        _formatOptionsReloadToken?.Dispose();
        _optionsReloadToken?.Dispose();
    }

    /// <summary>
    /// 刷新日志格式化选项
    /// </summary>
    /// <param name="options"></param>
    private void ReloadFormatterOptions(ConsoleFormatterSettingsOptions options)
    {
        _formatterOptions = options;
    }

    /// <summary>
    /// 刷新日志选项
    /// </summary>
    /// <param name="options"></param>
    private void ReloadLoggerOptions(SimpleConsoleFormatterOptions options)
    {
        _options = options;
        _disableColors = _options.ColorBehavior == LoggerColorBehavior.Disabled || (_options.ColorBehavior == LoggerColorBehavior.Default && Console.IsOutputRedirected);
    }
}