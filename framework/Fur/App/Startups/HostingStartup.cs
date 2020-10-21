// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

/// 注入启动
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
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                AutoAddJsonFile(configurationBuilder);
                AutoAddXmlFile(configurationBuilder);

                // 存储配置
                InternalApp.ConfigurationBuilder = configurationBuilder;
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

        /// <summary>
        /// 自动加载自定义 .json 配置文件
        /// </summary>
        /// <param name="configurationBuilder"></param>
        private static void AutoAddJsonFile(IConfigurationBuilder configurationBuilder)
        {
            // 获取程序目录下的所有配置文件
            var jsonNames = Directory.GetFiles(AppContext.BaseDirectory, "*.json", SearchOption.TopDirectoryOnly)
                .Union(
                    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                )
                .Where(u => !excludeJsons.Contains(Path.GetFileName(u)) && !runtimeJsonSuffixs.Any(j => u.EndsWith(j)));

            if (!jsonNames.Any()) return;

            // 自动加载配置文件
            foreach (var jsonName in jsonNames)
            {
                configurationBuilder.AddJsonFile(jsonName, optional: true, reloadOnChange: true);
            }
        }

        /// <summary>
        /// 自动加载自定义 .xml 配置文件
        /// </summary>
        /// <param name="configurationBuilder"></param>
        private static void AutoAddXmlFile(IConfigurationBuilder configurationBuilder)
        {
            // 获取程序目录下的所有配置文件，必须以 .config.xml 结尾
            var xmlNames = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .Union(
                    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml", SearchOption.TopDirectoryOnly)
                )
                .Where(u => u.EndsWith(".config.xml", StringComparison.OrdinalIgnoreCase));

            if (!xmlNames.Any()) return;

            // 自动加载配置文件
            foreach (var xmlName in xmlNames)
            {
                configurationBuilder.AddXmlFile(xmlName, optional: true, reloadOnChange: true);
            }
        }

        /// <summary>
        /// 默认排除配置项
        /// </summary>
        private static readonly string[] excludeJsons = new[] {
            "appsettings.json",
            "appsettings.Development.json",
            "appsettings.Production.json",
        };

        /// <summary>
        /// 运行时 Json 后缀
        /// </summary>
        private static readonly string[] runtimeJsonSuffixs = new[]
        {
            "deps.json",
            "runtimeconfig.dev.json",
            "runtimeconfig.prod.json",
            "runtimeconfig.json"
        };
    }
}