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

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;

namespace Furion;

/// <summary>
/// 内部 App 副本
/// </summary>
internal static class InternalApp
{
    /// <summary>
    /// 应用服务
    /// </summary>
    internal static IServiceCollection InternalServices;

    /// <summary>
    /// 根服务
    /// </summary>
    internal static IServiceProvider RootServices;

    /// <summary>
    /// 配置对象
    /// </summary>
    internal static IConfiguration Configuration;

    /// <summary>
    /// 获取Web主机环境
    /// </summary>
    internal static IWebHostEnvironment WebHostEnvironment;

    /// <summary>
    /// 获取泛型主机环境
    /// </summary>
    internal static IHostEnvironment HostEnvironment;

    /// <summary>
    /// 配置 Furion 框架（Web）
    /// </summary>
    /// <remarks>此次添加 <see cref="HostBuilder"/> 参数是为了兼容 .NET 5 直接升级到 .NET 6 问题</remarks>
    /// <param name="builder"></param>
    /// <param name="hostBuilder"></param>
    internal static void ConfigureApplication(IWebHostBuilder builder, IHostBuilder hostBuilder = default)
    {
        // 自动装载配置
        if (hostBuilder == default)
        {
            builder.ConfigureAppConfiguration((hostContext, configurationBuilder) =>
            {
                // 存储环境对象
                HostEnvironment = WebHostEnvironment = hostContext.HostingEnvironment;

                // 加载配置
                AddJsonFiles(configurationBuilder, hostContext.HostingEnvironment);

                // 加载自定义配置
                InjectOptions.WebAppConfigurationConfigure?.Invoke(hostContext, configurationBuilder);
            });
        }
        // 自动装载配置
        else ConfigureHostAppConfiguration(hostBuilder);

        // 应用初始化服务
        builder.ConfigureServices((hostContext, services) =>
        {
            // 存储配置对象
            Configuration = hostContext.Configuration;

            // 存储服务提供器
            InternalServices = services;

            // 存储根服务（解决 Web 主机还未启动时在 HostedService 中使用 App.GetService 问题
            services.AddHostedService<GenericHostLifetimeEventsHostedService>();

            // 注册 Startup 过滤器
            services.AddTransient<IStartupFilter, StartupFilter>();

            // 注册 HttpContextAccessor 服务
            services.AddHttpContextAccessor();

            // 初始化应用服务
            services.AddApp();

            // 加载自定义配置
            InjectOptions.WebServicesConfigure?.Invoke(hostContext, services);
        });
    }

    /// <summary>
    /// 配置 Furion 框架（非 Web）
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="autoRegisterBackgroundService"></param>
    internal static void ConfigureApplication(IHostBuilder builder, bool autoRegisterBackgroundService = true)
    {
        // 自动装载配置
        ConfigureHostAppConfiguration(builder);

        // 自动注入 AddApp() 服务
        builder.ConfigureServices((hostContext, services) =>
        {
            // 存储配置对象
            Configuration = hostContext.Configuration;

            // 存储服务提供器
            InternalServices = services;

            // 存储根服务
            services.AddHostedService<GenericHostLifetimeEventsHostedService>();

            // 初始化应用服务
            services.AddApp();

            // 自动注册 BackgroundService
            if (autoRegisterBackgroundService) services.AddAppHostedService();

            // 加载自定义配置
            InjectOptions.ServicesConfigure?.Invoke(hostContext, services);
        });
    }

    /// <summary>
    /// 自动装载主机配置
    /// </summary>
    /// <param name="builder"></param>
    private static void ConfigureHostAppConfiguration(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((hostContext, configurationBuilder) =>
        {
            // 存储环境对象
            HostEnvironment = hostContext.HostingEnvironment;

            // 加载配置
            AddJsonFiles(configurationBuilder, hostContext.HostingEnvironment);

            // 加载自定义配置
            InjectOptions.AppConfigurationConfigure?.Invoke(hostContext, configurationBuilder);
        });
    }

    /// <summary>
    /// 加载自定义 .json 配置文件
    /// </summary>
    /// <param name="configurationBuilder"></param>
    /// <param name="hostEnvironment"></param>
    internal static void AddJsonFiles(IConfigurationBuilder configurationBuilder, IHostEnvironment hostEnvironment)
    {
        // 获取根配置
        var configuration = configurationBuilder is ConfigurationManager
            ? (configurationBuilder as ConfigurationManager)
            : configurationBuilder.Build();

        // 获取程序执行目录
        var executeDirectory = AppContext.BaseDirectory;

        // 获取自定义配置扫描目录
        var configurationScanDirectories = (configuration.GetSection("ConfigurationScanDirectories")
                .Get<string[]>()
            ?? Array.Empty<string>()).Select(u => Path.Combine(executeDirectory, u));

        // 扫描执行目录及自定义配置目录下的 *.json 文件
        var jsonFiles = new[] { executeDirectory }
                            .Concat(configurationScanDirectories)
                            .Concat(InjectOptions.InternalConfigurationScanDirectories)
                            .SelectMany(u =>
                                Directory.GetFiles(u, "*.json", SearchOption.TopDirectoryOnly));

        // 如果没有配置文件，中止执行
        if (!jsonFiles.Any()) return;

        // 获取环境变量名，如果没找到，则读取 NETCORE_ENVIRONMENT 环境变量信息识别（用于非 Web 环境）
        var envName = hostEnvironment?.EnvironmentName ?? Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT") ?? "Unknown";

        // 读取忽略的配置文件
        var ignoreConfigurationFiles = (configuration.GetSection("IgnoreConfigurationFiles")
                .Get<string[]>()
            ?? Array.Empty<string>()).Concat(InjectOptions.InternalIgnoreConfigurationFiles);

        // 处理控制台应用程序
        var _excludeJsonPrefixs = hostEnvironment == default ? excludeJsonPrefixs.Where(u => !u.Equals("appsettings")) : excludeJsonPrefixs;

        // 将所有文件进行分组
        var jsonFilesGroups = SplitConfigFileNameToGroups(jsonFiles)
                                                                .Where(u => !_excludeJsonPrefixs.Contains(u.Key, StringComparer.OrdinalIgnoreCase) && !u.Any(c => runtimeJsonSuffixs.Any(z => c.EndsWith(z, StringComparison.OrdinalIgnoreCase)) || ignoreConfigurationFiles.Contains(Path.GetFileName(c), StringComparer.OrdinalIgnoreCase) || ignoreConfigurationFiles.Any(i => new Matcher().AddInclude(i).Match(Path.GetFileName(c)).HasMatches)));

        // 遍历所有配置分组
        foreach (var group in jsonFilesGroups)
        {
            // 限制查找的 json 文件组
            var limitFileNames = new[] { $"{group.Key}.json", $"{group.Key}.{envName}.json" };

            // 查找默认配置和环境配置
            var files = group.Where(u => limitFileNames.Contains(Path.GetFileName(u), StringComparer.OrdinalIgnoreCase))
                                             .OrderBy(u => Path.GetFileName(u).Length);

            // 循环加载
            foreach (var jsonFile in files)
            {
                configurationBuilder.AddJsonFile(jsonFile, optional: true, reloadOnChange: true);
            }
        }
    }

    /// <summary>
    /// 排除的配置文件前缀
    /// </summary>
    private static readonly string[] excludeJsonPrefixs = new[] { "appsettings", "bundleconfig", "compilerconfig" };

    /// <summary>
    /// 排除运行时 Json 后缀
    /// </summary>
    private static readonly string[] runtimeJsonSuffixs = new[]
    {
            "deps.json",
            "runtimeconfig.dev.json",
            "runtimeconfig.prod.json",
            "runtimeconfig.json",
            "staticwebassets.runtime.json"
        };

    /// <summary>
    /// 对配置文件名进行分组
    /// </summary>
    /// <param name="configFiles"></param>
    /// <returns></returns>
    private static IEnumerable<IGrouping<string, string>> SplitConfigFileNameToGroups(IEnumerable<string> configFiles)
    {
        // 分组
        return configFiles.GroupBy(Function);

        // 本地函数
        static string Function(string file)
        {
            // 根据 . 分隔
            var fileNameParts = Path.GetFileName(file).Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (fileNameParts.Length == 2) return fileNameParts[0];

            return string.Join('.', fileNameParts.Take(fileNameParts.Length - 2));
        }
    }
}