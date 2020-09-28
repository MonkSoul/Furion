// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				   Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

/// 注入启动
[assembly: HostingStartup(typeof(Fur.ConfigurableOptions.ConfigurationInjection))]
namespace Fur.ConfigurableOptions
{
    /// <summary>
    /// 自动注入自定义配置文件
    /// </summary>
    [SkipScan]
    public class ConfigurationInjection : IHostingStartup
    {
        /// <summary>
        /// 默认排除配置项
        /// </summary>
        private static readonly string[] excludeJsons = new[] {
            "appsettings.json",
            "appsettings.Development.json",
            "appsettings.Production.json"
        };

        /// <summary>
        /// 配置自动加载启动项目配置文件
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            // 获取启动目录所有配置文件并排除 asp.net core 自带配置
            var jsonNames = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                .Where(u => !excludeJsons.Contains(Path.GetFileName(u)));
            if (!jsonNames.Any()) return;

            // 自动装载配置
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                foreach (var jsonName in jsonNames)
                {
                    configurationBuilder.AddJsonFile(jsonName, optional: true, reloadOnChange: true);
                }
            });
        }
    }
}