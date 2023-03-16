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

using Furion;
using Furion.Logging;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 日志服务拓展类
/// </summary>
[SuppressSniffer]
public static class LoggingServiceCollectionExtensions
{
    /// <summary>
    /// 添加控制台默认格式化器
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">添加更多配置</param>
    /// <returns></returns>
    public static IServiceCollection AddConsoleFormatter(this IServiceCollection services, Action<ConsoleFormatterExtendOptions> configure = default)
    {
        return services.AddLogging(builder => builder.AddConsoleFormatter(configure));
    }

    /// <summary>
    /// 添加日志监视器服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">添加更多配置</param>
    /// <param name="jsonKey">配置文件对于的 Key，默认为 Logging:Monitor</param>
    /// <returns></returns>
    public static IServiceCollection AddMonitorLogging(this IServiceCollection services, Action<LoggingMonitorSettings> configure = default, string jsonKey = "Logging:Monitor")
    {
        // 读取配置
        var settings = App.GetConfig<LoggingMonitorSettings>(jsonKey)
            ?? new LoggingMonitorSettings();
        settings.IsMvcFilterRegister = false;   // 解决过去 Mvc Filter 全局注册的问题
        settings.FromGlobalFilter = true;   // 解决局部和全局触发器同时配置触发两次问题
        settings.IncludeOfMethods ??= Array.Empty<string>();
        settings.ExcludeOfMethods ??= Array.Empty<string>();
        settings.MethodsSettings ??= Array.Empty<LoggingMonitorMethod>();

        // 添加外部配置
        configure?.Invoke(settings);

        // 配置日志过滤器
        LoggingMonitorSettings.InternalWriteFilter = settings.WriteFilter;

        // 如果配置 GlobalEnabled = false 且 IncludeOfMethods 和 ExcludeOfMethods 都为空，则不注册服务
        if (settings.GlobalEnabled == false
            && settings.IncludeOfMethods.Length == 0
            && settings.ExcludeOfMethods.Length == 0) return services;

        // 注册日志监视器过滤器
        services.AddMvcFilter(new LoggingMonitorAttribute(settings));

        return services;
    }

    /// <summary>
    /// 添加文件日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="fileName">日志文件完整路径或文件名，推荐 .log 作为拓展名</param>
    /// <param name="append">追加到已存在日志文件或覆盖它们</param>
    /// <returns></returns>
    public static IServiceCollection AddFileLogging(this IServiceCollection services, string fileName, bool append = true)
    {
        return services.AddLogging(builder => builder.AddFile(fileName, append));
    }

    /// <summary>
    /// 添加文件日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="fileName">日志文件完整路径或文件名，推荐 .log 作为拓展名</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddFileLogging(this IServiceCollection services, string fileName, Action<FileLoggerOptions> configure)
    {
        return services.AddLogging(builder => builder.AddFile(fileName, configure));
    }

    /// <summary>
    /// 添加文件日志服务（从配置文件中读取配置）
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddFileLogging(this IServiceCollection services, Action<FileLoggerOptions> configure = default)
    {
        return services.AddLogging(builder => builder.AddFile(configure));
    }

    /// <summary>
    /// 添加文件日志服务（从配置文件中读取配置）
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuraionKey">获取配置文件对应的 Key</param>
    /// <param name="configure">文件日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddFileLogging(this IServiceCollection services, Func<string> configuraionKey, Action<FileLoggerOptions> configure = default)
    {
        return services.AddLogging(builder => builder.AddFile(configuraionKey, configure));
    }

    /// <summary>
    /// 添加数据库日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseLogging<TDatabaseLoggingWriter>(this IServiceCollection services, Action<DatabaseLoggerOptions> configure)
        where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        return services.AddLogging(builder => builder.AddDatabase<TDatabaseLoggingWriter>(configure));
    }

    /// <summary>
    /// 添加数据库日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuraionKey">配置文件对于的 Key</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseLogging<TDatabaseLoggingWriter>(this IServiceCollection services, string configuraionKey = default, Action<DatabaseLoggerOptions> configure = default)
        where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        return services.AddLogging(builder => builder.AddDatabase<TDatabaseLoggingWriter>(configuraionKey, configure));
    }

    /// <summary>
    /// 添加数据库日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuraionKey">获取配置文件对于的 Key</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseLogging<TDatabaseLoggingWriter>(this IServiceCollection services, Func<string> configuraionKey, Action<DatabaseLoggerOptions> configure = default)
        where TDatabaseLoggingWriter : class, IDatabaseLoggingWriter
    {
        return services.AddLogging(builder => builder.AddDatabase<TDatabaseLoggingWriter>(configuraionKey, configure));
    }
}