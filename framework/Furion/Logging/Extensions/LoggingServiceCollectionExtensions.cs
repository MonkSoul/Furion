// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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