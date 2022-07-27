using Furion.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.Logging;

/// <summary>
/// 日志构建器拓展类
/// </summary>
[SuppressSniffer]
public static class ILoggingBuilderExtensions
{
    /// <summary>
    /// 添加文件日志记录器
    /// </summary>
    /// <param name="builder">日志构建器</param>
    /// <param name="fileName">日志文件完整路径或文件名，推荐 .log 作为拓展名</param>
    /// <param name="append">追加到已存在日志文件或覆盖它们</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string fileName, bool append = true)
    {
        // 注册文件日志记录器提供器
        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>((serviceProvider) =>
        {
            return new FileLoggerProvider(fileName, append);
        }));

        return builder;
    }

    /// <summary>
    /// 添加文件日志记录器
    /// </summary>
    /// <param name="builder">日志构建器</param>
    /// <param name="fileName">日志文件完整路径或文件名，推荐 .log 作为拓展名</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string fileName, Action<FileLoggerOptions> configure)
    {
        // 注册文件日志记录器提供器
        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>((serviceProvider) =>
        {
            var options = new FileLoggerOptions();
            configure?.Invoke(options);

            return new FileLoggerProvider(fileName, options);
        }));

        return builder;
    }

    /// <summary>
    /// 添加文件日志记录器（从配置文件中）默认 Key 为："Logging:File"
    /// </summary>
    /// <param name="builder">日志构建器</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<FileLoggerOptions> configure = default)
    {
        return builder.AddFile(() => "Logging:File", configure);
    }

    /// <summary>
    /// 添加文件日志记录器（从配置文件中）
    /// </summary>
    /// <param name="builder">日志构建器</param>
    /// <param name="configuraionKey">获取配置文件对应的 Key</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Func<string> configuraionKey, Action<FileLoggerOptions> configure = default)
    {
        // 创建文件日志记录器提供程序
        var fileLoggerProvider = Penetrates.CreateFromConfiguration(configuraionKey, configure);

        // 如果从配置文件中加载配置失败，则跳过注册
        if (fileLoggerProvider == default) return builder;

        // 注册文件日志记录器提供器
        builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>((serviceProvider) =>
        {
            return fileLoggerProvider;
        });

        return builder;
    }

    /// <summary>
    /// 添加数据库日志记录器
    /// </summary>
    /// <typeparam name="TDatabaseLoggingWriter">实现自 <see cref="IDatabaseLoggingWriter"/></typeparam>
    /// <param name="builder">日志构建器</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddDatabase<TDatabaseLoggingWriter>(this ILoggingBuilder builder, Action<DatabaseLoggerOptions> configure)
        where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        // 注册数据库日志写入器
        builder.Services.TryAddTransient(typeof(IDatabaseLoggingWriter), typeof(TDatabaseLoggingWriter));

        // 注册数据库日志记录器提供器
        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider, DatabaseLoggerProvider>((serviceProvider) =>
        {
            var options = new DatabaseLoggerOptions();
            configure?.Invoke(options);

            // 数据库日志记录器提供程序
            var databaseLoggerProvider = new DatabaseLoggerProvider(options);
            databaseLoggerProvider.SetServiceProvider(serviceProvider);

            return databaseLoggerProvider;
        }));

        return builder;
    }

    /// <summary>
    /// 添加数据库日志记录器
    /// </summary>
    /// <typeparam name="TDatabaseLoggingWriter">实现自 <see cref="IDatabaseLoggingWriter"/></typeparam>
    /// <param name="builder">日志构建器</param>
    /// <param name="configuraionKey">配置文件对于的 Key</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddDatabase<TDatabaseLoggingWriter>(this ILoggingBuilder builder, string configuraionKey = default, Action<DatabaseLoggerOptions> configure = default)
        where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        return builder.AddDatabase<TDatabaseLoggingWriter>(() => configuraionKey ?? "Logging:Database", configure);
    }

    /// <summary>
    /// 添加数据库日志记录器（从配置文件中）
    /// </summary>
    /// <typeparam name="TDatabaseLoggingWriter">实现自 <see cref="IDatabaseLoggingWriter"/></typeparam>
    /// <param name="builder">日志构建器</param>
    /// <param name="configuraionKey">获取配置文件对于的 Key</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddDatabase<TDatabaseLoggingWriter>(this ILoggingBuilder builder, Func<string> configuraionKey, Action<DatabaseLoggerOptions> configure = default)
        where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        // 注册数据库日志写入器
        builder.Services.TryAddTransient(typeof(IDatabaseLoggingWriter), typeof(TDatabaseLoggingWriter));

        // 创建数据库日志记录器提供程序
        var databaseLoggerProvider = Penetrates.CreateFromConfiguration(configuraionKey, configure);

        // 如果从配置文件中加载配置失败，则跳过注册
        if (databaseLoggerProvider == default) return builder;

        // 注册数据库日志记录器提供器
        builder.Services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>((serviceProvider) =>
        {
            databaseLoggerProvider.SetServiceProvider(serviceProvider);
            return databaseLoggerProvider;
        });

        return builder;
    }
}