// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Furion.HostingStartup))]

namespace Furion
{
    /// <summary>
    /// 配置程序启动时自动注入
    /// </summary>
    [SuppressSniffer]
    public sealed class HostingStartup : IHostingStartup
    {
        /// <summary>
        /// 配置应用启动
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            // 自动装载配置
            builder.ConfigureAppConfiguration((hostContext, configurationBuilder) =>
            {
                // 存储环境对象
                InternalApp.HostEnvironment = InternalApp.WebHostEnvironment = hostContext.HostingEnvironment;

                // 加载配置
                InternalApp.AddJsonFiles(configurationBuilder, hostContext.HostingEnvironment);
            });

            // 应用初始化服务
            builder.ConfigureServices((hostContext, services) =>
            {
                // 存储配置对象
                InternalApp.Configuration = hostContext.Configuration;

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