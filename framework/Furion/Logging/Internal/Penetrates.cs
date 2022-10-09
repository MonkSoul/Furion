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
using System.Text;

namespace Furion.Logging;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 异常分隔符
    /// </summary>
    private const string EXCEPTION_SEPARATOR = "++++++++++++++++++++++++++++++++++++++++++++++++++++++++";

    /// <summary>
    /// 从配置文件中加载配置并创建文件日志记录器提供程序
    /// </summary>
    /// <param name="configuraionKey">获取配置文件对应的 Key</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns><see cref="FileLoggerProvider"/></returns>
    internal static FileLoggerProvider CreateFromConfiguration(Func<string> configuraionKey, Action<FileLoggerOptions> configure = default)
    {
        // 检查 Key 是否存在
        var key = configuraionKey?.Invoke();
        if (string.IsNullOrWhiteSpace(key)) return default;

        // 加载配置文件中指定节点
        var fileLoggerSettings = App.GetConfig<FileLoggerSettings>(key);

        // 如果配置为空或者文件名为空，则添加文件日志记录器服务
        if (string.IsNullOrWhiteSpace(fileLoggerSettings?.FileName)) return default;

        // 创建文件日志记录器配置选项
        var fileLoggerOptions = new FileLoggerOptions
        {
            Append = fileLoggerSettings.Append,
            MinimumLevel = fileLoggerSettings.MinimumLevel,
            FileSizeLimitBytes = fileLoggerSettings.FileSizeLimitBytes,
            MaxRollingFiles = fileLoggerSettings.MaxRollingFiles
        };

        // 处理自定义配置
        configure?.Invoke(fileLoggerOptions);

        // 创建文件日志记录器提供程序
        return new FileLoggerProvider(fileLoggerSettings.FileName, fileLoggerOptions);
    }

    /// <summary>
    /// 从配置文件中加载配置并创建数据库日志记录器提供程序
    /// </summary>
    /// <param name="configuraionKey">获取配置文件对应的 Key</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="DatabaseLoggerProvider"/></returns>
    internal static DatabaseLoggerProvider CreateFromConfiguration(Func<string> configuraionKey, Action<DatabaseLoggerOptions> configure = default)
    {
        // 检查 Key 是否存在
        var key = configuraionKey?.Invoke();
        if (string.IsNullOrWhiteSpace(key)) return default;

        // 加载配置文件中指定节点
        var databaseLoggerSettings = App.GetConfig<DatabaseLoggerSettings>(key);

        // 创建数据库日志记录器配置选项
        var databaseLoggerOptions = new DatabaseLoggerOptions
        {
            MinimumLevel = databaseLoggerSettings?.MinimumLevel ?? LogLevel.Trace,
        };

        // 处理自定义配置
        configure?.Invoke(databaseLoggerOptions);

        // 创建数据库日志记录器提供程序
        return new DatabaseLoggerProvider(databaseLoggerOptions);
    }

    /// <summary>
    /// 输出标准日志消息
    /// </summary>
    /// <param name="logMsg"></param>
    /// <param name="dateFormat"></param>
    /// <param name="disableColors"></param>
    /// <param name="isConsole"></param>
    /// <returns></returns>
    internal static string OutputStandardMessage(LogMessage logMsg
        , string dateFormat = "o"
        , bool isConsole = false
        , bool disableColors = true)
    {
        // 空检查
        if (logMsg.Message is null) return null;

        // 创建默认日志格式化模板
        var formatString = new StringBuilder();

        // 获取日志级别对应控制台的颜色
        var disableConsoleColor = !isConsole || disableColors;
        var logLevelColors = GetLogLevelConsoleColors(logMsg.LogLevel, disableConsoleColor);

        _ = AppendWithColor(formatString, GetLogLevelString(logMsg.LogLevel), logLevelColors);
        formatString.Append(": ");
        formatString.Append(logMsg.LogDateTime.ToString(dateFormat));
        formatString.Append(' ');
        formatString.Append(logMsg.UseUtcTimestamp ? "U" : "L");
        formatString.Append(' ');
        _ = AppendWithColor(formatString, logMsg.LogName, disableConsoleColor
            ? new ConsoleColors(null, null)
            : new ConsoleColors(ConsoleColor.Cyan, ConsoleColor.DarkCyan));
        formatString.Append('[');
        formatString.Append(logMsg.EventId.Id);
        formatString.Append(']');
        formatString.Append(' ');
        formatString.Append($"#{logMsg.ThreadId}");
        formatString.AppendLine();

        // 对日志内容进行缩进对齐处理
        formatString.Append(PadLeftAlign(logMsg.Message));

        // 如果包含异常信息，则创建新一行写入
        if (logMsg.Exception != null)
        {
            var EXCEPTION_SEPARATOR_WITHCOLOR = AppendWithColor(default, EXCEPTION_SEPARATOR, logLevelColors).ToString();
            var exceptionMessage = $"{Environment.NewLine}{EXCEPTION_SEPARATOR_WITHCOLOR}{Environment.NewLine}{logMsg.Exception}{Environment.NewLine}{EXCEPTION_SEPARATOR_WITHCOLOR}";

            formatString.Append(PadLeftAlign(exceptionMessage));
        }

        // 返回日志消息模板
        return formatString.ToString();
    }

    /// <summary>
    /// 将日志内容进行对齐
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static string PadLeftAlign(string message)
    {
        var newMessage = string.Join(Environment.NewLine, message.Split(Environment.NewLine)
                    .Select(line => string.Empty.PadLeft(6, ' ') + line));

        return newMessage;
    }

    /// <summary>
    /// 获取日志级别短名称
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <returns></returns>
    internal static string GetLogLevelString(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "trce",
            LogLevel.Debug => "dbug",
            LogLevel.Information => "info",
            LogLevel.Warning => "warn",
            LogLevel.Error => "fail",
            LogLevel.Critical => "crit",
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel)),
        };
    }

    /// <summary>
    /// 设置日志上下文
    /// </summary>
    /// <param name="scopeProvider"></param>
    /// <param name="logMsg"></param>
    /// <param name="includeScopes"></param>
    /// <returns></returns>
    internal static LogMessage SetLogContext(IExternalScopeProvider scopeProvider, LogMessage logMsg, bool includeScopes)
    {
        // 设置日志上下文
        if (includeScopes && scopeProvider != null)
        {
            // 解析日志上下文数据
            scopeProvider.ForEachScope<object>((scope, ctx) =>
            {
                if (scope != null && scope is LogContext context)
                {
                    if (logMsg.Context == null) logMsg.Context = context;
                    else logMsg.Context = logMsg.Context?.SetRange(context.Properties);
                }
            }, null);
        }

        return logMsg;
    }

    /// <summary>
    /// 拓展 StringBuilder 增加带颜色写入
    /// </summary>
    /// <param name="message"></param>
    /// <param name="colors"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    private static StringBuilder AppendWithColor(StringBuilder formatString, string message, ConsoleColors colors)
    {
        formatString ??= new();

        // 输出控制台前景色和背景色
        if (colors.Background.HasValue) formatString.Append(GetBackgroundColorEscapeCode(colors.Background.Value));
        if (colors.Foreground.HasValue) formatString.Append(GetForegroundColorEscapeCode(colors.Foreground.Value));

        formatString.Append(message);

        // 输出控制台前景色和背景色
        if (colors.Background.HasValue) formatString.Append("\u001b[39m\u001b[22m");
        if (colors.Foreground.HasValue) formatString.Append("\u001b[49m");

        return formatString;
    }

    /// <summary>
    /// 输出控制台字体颜色 UniCode 码
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private static string GetForegroundColorEscapeCode(ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => "\u001b[30m",
            ConsoleColor.DarkRed => "\u001b[31m",
            ConsoleColor.DarkGreen => "\u001b[32m",
            ConsoleColor.DarkYellow => "\u001b[33m",
            ConsoleColor.DarkBlue => "\u001b[34m",
            ConsoleColor.DarkMagenta => "\u001b[35m",
            ConsoleColor.DarkCyan => "\u001b[36m",
            ConsoleColor.Gray => "\u001b[37m",
            ConsoleColor.Red => "\u001b[1m\u001b[31m",
            ConsoleColor.Green => "\u001b[1m\u001b[32m",
            ConsoleColor.Yellow => "\u001b[1m\u001b[33m",
            ConsoleColor.Blue => "\u001b[1m\u001b[34m",
            ConsoleColor.Magenta => "\u001b[1m\u001b[35m",
            ConsoleColor.Cyan => "\u001b[1m\u001b[36m",
            ConsoleColor.White => "\u001b[1m\u001b[37m",
            _ => "\u001b[39m\u001b[22m",
        };
    }

    /// <summary>
    /// 输出控制台背景颜色 UniCode 码
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private static string GetBackgroundColorEscapeCode(ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => "\u001b[40m",
            ConsoleColor.Red => "\u001b[41m",
            ConsoleColor.Green => "\u001b[42m",
            ConsoleColor.Yellow => "\u001b[43m",
            ConsoleColor.Blue => "\u001b[44m",
            ConsoleColor.Magenta => "\u001b[45m",
            ConsoleColor.Cyan => "\u001b[46m",
            ConsoleColor.White => "\u001b[47m",
            _ => "\u001b[49m",
        };
    }

    /// <summary>
    /// 获取控制台日志级别对应的颜色
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="disableColors"></param>
    /// <returns></returns>
    private static ConsoleColors GetLogLevelConsoleColors(LogLevel logLevel, bool disableColors = false)
    {
        if (disableColors)
        {
            return new ConsoleColors(null, null);
        }

        return logLevel switch
        {
            LogLevel.Critical => new ConsoleColors(ConsoleColor.White, ConsoleColor.Red),
            LogLevel.Error => new ConsoleColors(ConsoleColor.Black, ConsoleColor.Red),
            LogLevel.Warning => new ConsoleColors(ConsoleColor.Yellow, ConsoleColor.Black),
            LogLevel.Information => new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black),
            LogLevel.Debug => new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black),
            LogLevel.Trace => new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black),
            _ => new ConsoleColors(null, background: null),
        };
    }
}