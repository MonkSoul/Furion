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

using Furion.Logging;

namespace Microsoft.Extensions.Logging;

/// <summary>
/// <see cref="ILoggerFactory"/> 拓展
/// </summary>
[SuppressSniffer]
public static class ILoggerFactoryExtensions
{
    /// <summary>
    /// 添加文件日志记录器
    /// </summary>
    /// <param name="factory">日志工厂</param>
    /// <param name="fileName">日志文件完整路径或文件名，推荐 .log 作为拓展名</param>
    /// <param name="append">追加到已存在日志文件或覆盖它们</param>
    /// <returns><see cref="ILoggerFactory"/></returns>
    public static ILoggerFactory AddFile(this ILoggerFactory factory, string fileName, bool append = true)
    {
        // 添加文件日志记录器提供程序
        factory.AddProvider(new FileLoggerProvider(fileName, append));

        return factory;
    }

    /// <summary>
    /// 添加文件日志记录器
    /// </summary>
    /// <param name="factory">日志工厂</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggerFactory"/></returns>
    public static ILoggerFactory AddFile(this ILoggerFactory factory, Action<FileLoggerOptions> configure = default)
    {
        return factory.AddFile(() => "Logging:File", configure);
    }

    /// <summary>
    /// 添加文件日志记录器
    /// </summary>
    /// <param name="factory">日志工厂</param>
    /// <param name="configuraionKey">获取配置文件对应的 Key</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggerFactory"/></returns>
    public static ILoggerFactory AddFile(this ILoggerFactory factory, Func<string> configuraionKey, Action<FileLoggerOptions> configure = default)
    {
        // 创建文件日志记录器提供程序
        var fileLoggerProvider = Penetrates.CreateFromConfiguration(configuraionKey, configure);

        // 如果从配置文件中加载配置失败，则跳过注册
        if (fileLoggerProvider == default) return factory;

        // 添加文件日志记录器提供程序
        factory.AddProvider(fileLoggerProvider);

        return factory;
    }

    /// <summary>
    /// 添加数据库日志记录器
    /// </summary>
    /// <typeparam name="TDatabaseLoggingWriter">实现自 <see cref="IDatabaseLoggingWriter"/></typeparam>
    /// <param name="factory">日志工厂</param>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggerFactory"/></returns>
    public static ILoggerFactory AddDatabase<TDatabaseLoggingWriter>(this ILoggerFactory factory, IServiceProvider serviceProvider, Action<DatabaseLoggerOptions> configure)
         where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        var options = new DatabaseLoggerOptions();
        configure?.Invoke(options);

        var databaseLoggerProvider = new DatabaseLoggerProvider(options);

        // 解决数据库写入器中循环引用数据库仓储问题
        if (databaseLoggerProvider._serviceProvider == null)
        {
            databaseLoggerProvider.SetServiceProvider(serviceProvider);
        }

        // 添加数据库日志记录器提供程序
        factory.AddProvider(databaseLoggerProvider);

        return factory;
    }

    /// <summary>
    /// 添加数据库日志记录器
    /// </summary>
    /// <typeparam name="TDatabaseLoggingWriter">实现自 <see cref="IDatabaseLoggingWriter"/></typeparam>
    /// <param name="factory">日志工厂</param>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="configuraionKey">配置文件对于的 Key</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggerFactory"/></returns>
    public static ILoggerFactory AddDatabase<TDatabaseLoggingWriter>(this ILoggerFactory factory, IServiceProvider serviceProvider, string configuraionKey = default, Action<DatabaseLoggerOptions> configure = default)
         where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        return factory.AddDatabase<TDatabaseLoggingWriter>(() => configuraionKey ?? "Logging:Database", serviceProvider, configure);
    }

    /// <summary>
    /// 添加数据库日志记录器
    /// </summary>
    /// <typeparam name="TDatabaseLoggingWriter">实现自 <see cref="IDatabaseLoggingWriter"/></typeparam>
    /// <param name="factory">日志工厂</param>
    /// <param name="configuraionKey">获取配置文件对应的 Key</param>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggerFactory"/></returns>
    public static ILoggerFactory AddDatabase<TDatabaseLoggingWriter>(this ILoggerFactory factory, Func<string> configuraionKey, IServiceProvider serviceProvider, Action<DatabaseLoggerOptions> configure = default)
        where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        // 创建数据库日志记录器提供程序
        var databaseLoggerProvider = Penetrates.CreateFromConfiguration(configuraionKey, configure);

        // 如果从配置文件中加载配置失败，则跳过注册
        if (databaseLoggerProvider == default) return factory;

        // 解决数据库写入器中循环引用数据库仓储问题
        if (databaseLoggerProvider._serviceProvider == null)
        {
            databaseLoggerProvider.SetServiceProvider(serviceProvider);
        }

        // 添加数据库日志记录器提供程序
        factory.AddProvider(databaseLoggerProvider);

        return factory;
    }
}