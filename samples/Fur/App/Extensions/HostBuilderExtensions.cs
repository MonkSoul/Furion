using Fur;
using Fur.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 主机构建器拓展类
    /// </summary>
    [SkipScan]
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="hostBuilder">Web主机构建器</param>
        /// <param name="assemblyName">外部程序集名称</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder Inject(this IWebHostBuilder hostBuilder, string assemblyName = nameof(Fur))
        {
            hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, assemblyName);
            return hostBuilder;
        }

        /// <summary>
        /// 非 Web 主机注入
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder Inject(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                InternalApp.AddConfigureFiles(config, hostingContext.HostingEnvironment);
            });

            // 自动注入 AddApp() 服务
            hostBuilder.ConfigureServices(services =>
            {
                // 添加全局配置和存储服务提供器
                InternalApp.InternalServices = services;

                // 初始化应用服务
                services.AddApp();
            });

            return hostBuilder;
        }
    }
}