// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 主机构建器拓展类
    /// </summary>
    [SuppressSniffer]
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="hostBuilder">Web主机构建器</param>
        /// <param name="assemblyName">外部程序集名称</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder Inject(this IWebHostBuilder hostBuilder, string assemblyName = default)
        {
            var frameworkPackageName = assemblyName ?? typeof(HostBuilderExtensions).Assembly.GetName().Name;
            hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, frameworkPackageName);

            return hostBuilder;
        }

        /// <summary>
        /// 泛型主机注入
        /// </summary>
        /// <param name="hostBuilder">泛型主机注入构建器</param>
        /// <param name="autoRegisterBackgroundService">是否自动注册 BackgroundService</param>
        /// <returns>IWebHostBuilder</returns>
        public static IHostBuilder Inject(this IHostBuilder hostBuilder, bool autoRegisterBackgroundService = true)
        {
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                // 存储环境对象
                InternalApp.HostEnvironment = hostingContext.HostingEnvironment;

                // 加载配置
                InternalApp.AddConfigureFiles(config, InternalApp.HostEnvironment);
            });

            // 自动注入 AddApp() 服务
            hostBuilder.ConfigureServices(services =>
            {
                // 添加主机启动停止监听
                services.AddHostedService<AppHostedService>();

                // 添加全局配置和存储服务提供器
                InternalApp.InternalServices = services;

                // 初始化应用服务
                services.AddHostApp();

                // 自动注册 BackgroundService
                if (autoRegisterBackgroundService) services.AddAppHostedService();
            });

            return hostBuilder;
        }
    }
}