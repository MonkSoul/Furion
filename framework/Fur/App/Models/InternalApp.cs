using Fur.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace Fur
{
    /// <summary>
    /// 内部 App 副本
    /// </summary>
    [SkipScan]
    internal static class InternalApp
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        internal static IServiceCollection InternalServices;

        /// <summary>
        /// 全局配置构建器
        /// </summary>
        internal static IConfigurationBuilder ConfigurationBuilder;

        /// <summary>
        /// 添加配置文件
        /// </summary>
        /// <param name="configurationBuilder"></param>
        internal static void AddConfigureFiles(IConfigurationBuilder configurationBuilder)
        {
            AutoAddJsonFiles(configurationBuilder);
            AutoAddXmlFiles(configurationBuilder);

            // 存储配置
            ConfigurationBuilder = configurationBuilder;
        }

        /// <summary>
        /// 自动加载自定义 .json 配置文件
        /// </summary>
        /// <param name="configurationBuilder"></param>
        private static void AutoAddJsonFiles(IConfigurationBuilder configurationBuilder)
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
        private static void AutoAddXmlFiles(IConfigurationBuilder configurationBuilder)
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