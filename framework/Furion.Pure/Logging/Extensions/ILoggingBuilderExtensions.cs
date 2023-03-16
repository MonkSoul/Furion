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

using Furion.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;

namespace Microsoft.Extensions.Logging;

/// <summary>
/// 日志构建器拓展类
/// </summary>
[SuppressSniffer]
public static class ILoggingBuilderExtensions
{
    /// <summary>
    /// 添加控制台默认格式化器
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddConsoleFormatter(this ILoggingBuilder builder, Action<ConsoleFormatterExtendOptions> configure = default)
    {
        configure ??= (options) => { };

        return builder.AddConsole(options => options.FormatterName = "console-format")
                      .AddConsoleFormatter<ConsoleFormatterExtend, ConsoleFormatterExtendOptions>(configure);
    }

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
            return new FileLoggerProvider(fileName ?? "application.log", append);
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

            return new FileLoggerProvider(fileName ?? "application.log", options);
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
        // 注册文件日志记录器提供器
        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>((serviceProvider) =>
        {
            return Penetrates.CreateFromConfiguration(configuraionKey, configure);
        }));

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
        builder.Services.TryAddTransient<TDatabaseLoggingWriter, TDatabaseLoggingWriter>();

        // 注册数据库日志记录器提供器
        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider>((serviceProvider) =>
        {
            // 解决在 IDatabaseLoggingWriter 实现类直接注册仓储导致死循环的问题
            var stackTrace = new System.Diagnostics.StackTrace();
            var frames = stackTrace.GetFrames();

            if (frames.Any(u => u.HasMethod() && u.GetMethod().Name == "ResolveDbContext")
            || frames.Count(u => u.HasMethod() && u.GetMethod().Name.StartsWith("<AddDatabase>")) > 1)
            {
                return new EmptyLoggerProvider();
            }

            var options = new DatabaseLoggerOptions();
            configure?.Invoke(options);

            // 数据库日志记录器提供程序
            var databaseLoggerProvider = new DatabaseLoggerProvider(options);
            databaseLoggerProvider.SetServiceProvider(serviceProvider, typeof(TDatabaseLoggingWriter));

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
        builder.Services.TryAddTransient<TDatabaseLoggingWriter, TDatabaseLoggingWriter>();

        // 注册数据库日志记录器提供器
        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider>((serviceProvider) =>
        {
            // 解决在 IDatabaseLoggingWriter 实现类直接注册仓储导致死循环的问题
            var stackTrace = new System.Diagnostics.StackTrace();
            var frames = stackTrace.GetFrames();

            if (frames.Any(u => u.HasMethod() && u.GetMethod().Name == "ResolveDbContext")
            || frames.Count(u => u.HasMethod() && u.GetMethod().Name.StartsWith("<AddDatabase>")) > 1)
            {
                return new EmptyLoggerProvider();
            }

            // 创建数据库日志记录器提供程序
            var databaseLoggerProvider = Penetrates.CreateFromConfiguration(configuraionKey, configure);
            databaseLoggerProvider.SetServiceProvider(serviceProvider, typeof(TDatabaseLoggingWriter));

            return databaseLoggerProvider;
        }));

        return builder;
    }
}