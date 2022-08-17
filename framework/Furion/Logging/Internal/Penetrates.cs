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

namespace Furion.Logging;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
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
            MinimumLevel = databaseLoggerSettings.MinimumLevel,
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
    internal static string GetShortLogLevel(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "TRC",
            LogLevel.Debug => "DBG",
            LogLevel.Information => "INF",
            LogLevel.Warning => "WRN",
            LogLevel.Error => "ERR",
            LogLevel.Critical => "CRT",
            LogLevel.None => "NON",
            _ => logLevel.ToString().ToUpper(),
        };
    }
}