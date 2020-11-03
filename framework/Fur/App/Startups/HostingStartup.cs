using Fur.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Fur.HostingStartup))]

namespace Fur
{
    /// <summary>
    /// 配置程序启动时自动注入
    /// </summary>
    [SkipScan]
    public sealed class HostingStartup : IHostingStartup
    {
        /// <summary>
        /// 配置应用启动
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            // 自动装载配置
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                InternalApp.AddConfigureFiles(config, hostingContext.HostingEnvironment);
            });

            // 自动注入 AddApp() 服务
            builder.ConfigureServices(services =>
            {
                // 注册 Startup 过滤器
                services.AddTransient<IStartupFilter, StartupFilter>();

                // 添加全局配置和存储服务提供器
                InternalApp.InternalServices = services;

                // 初始化应用服务
                services.AddApp();
            });
        }
    }
}