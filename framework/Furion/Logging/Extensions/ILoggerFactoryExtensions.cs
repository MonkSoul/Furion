// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
        databaseLoggerProvider.SetServiceProvider(serviceProvider);

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

        // 添加数据库日志记录器提供程序
        databaseLoggerProvider.SetServiceProvider(serviceProvider);
        factory.AddProvider(databaseLoggerProvider);

        return factory;
    }
}