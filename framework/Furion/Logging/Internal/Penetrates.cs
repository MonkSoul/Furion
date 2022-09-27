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
            _ => throw new ArgumentOutOfRangeException("logLevel"),
        };
    }

    /// <summary>
    /// 输出标准日志消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="logName"></param>
    /// <param name="timeStamp"></param>
    /// <param name="logLevel"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    internal static string OutputStandardMessage(string message
        , string logName
        , DateTime timeStamp
        , LogLevel logLevel
        , EventId eventId
        , Exception exception)
    {
        // 空检查
        if (message is null) return null;

        // 创建默认日志格式化模板
        var formatString = new StringBuilder();

        // 输出默认格式
        formatString.Append(GetLogLevelString(logLevel));
        formatString.Append(": ");
        formatString.Append(timeStamp.ToString("o"));
        formatString.Append(" ");
        formatString.Append(logName);
        formatString.Append("[");
        formatString.Append(eventId.Id);
        formatString.Append("]");
        formatString.Append(" ");
        formatString.Append($"#{Environment.CurrentManagedThreadId}");
        formatString.AppendLine();

        // 对日志内容进行缩进对齐处理
        formatString.Append(PadLeftAlign(message));

        // 如果包含异常信息，则创建新一行写入
        if (exception != null)
        {
            var exceptionMessage = $"{Environment.NewLine}{EXCEPTION_SEPARATOR}{Environment.NewLine}{exception}{Environment.NewLine}{EXCEPTION_SEPARATOR}";

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
}