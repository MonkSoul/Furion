// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
            hostBuilder.ConfigureAppConfiguration((hostContext, configurationBuilder) =>
            {
                // 存储环境对象
                InternalApp.HostEnvironment = hostContext.HostingEnvironment;

                // 加载配置
                InternalApp.AddJsonFiles(configurationBuilder, hostContext.HostingEnvironment);
            });

            // 自动注入 AddApp() 服务
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                // 存储配置对象
                InternalApp.Configuration = hostContext.Configuration;

                // 添加全局配置和存储服务提供器
                InternalApp.InternalServices = services;

                // 初始化应用服务
                services.AddApp();

                // 自动注册 BackgroundService
                if (autoRegisterBackgroundService) services.AddAppHostedService();
            });

            return hostBuilder;
        }
    }
}