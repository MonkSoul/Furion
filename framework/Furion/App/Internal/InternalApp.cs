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

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Furion
{
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
        /// 全局配置构建器
        /// </summary>
        internal static IConfigurationBuilder ConfigurationBuilder;

        /// <summary>
        /// 获取Web主机环境
        /// </summary>
        internal static IWebHostEnvironment WebHostEnvironment;

        /// <summary>
        /// 获取泛型主机环境
        /// </summary>
        internal static IHostEnvironment HostEnvironment;

        /// <summary>
        /// 添加配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        internal static void AddConfigureFiles(IConfigurationBuilder config, IHostEnvironment env)
        {
            var appsettingsConfiguration = config.Build();

            // 加载配置
            AutoAddJsonFiles(config, env, appsettingsConfiguration);

            // 存储配置
            ConfigurationBuilder = config;
        }

        /// <summary>
        /// 自动加载自定义 .json 配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <param name="appsettingsConfiguration"></param>
        private static void AutoAddJsonFiles(IConfigurationBuilder config, IHostEnvironment env, IConfiguration appsettingsConfiguration)
        {
            // 获取程序目录下的所有配置文件（只限顶级目标，不递归查找）
            var jsonFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.json", SearchOption.TopDirectoryOnly)
                .Union(
                    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                );

            // 如果没有配置文件，中止执行
            if (!jsonFiles.Any()) return;

            // 获取环境变量名
            var envName = env.EnvironmentName;

            // 读取忽略的配置文件
            var ignoreConfigurationFiles = appsettingsConfiguration
                    .GetSection("IgnoreConfigurationFiles")
                    .Get<string[]>()
                ?? Array.Empty<string>();

            // 将所有文件进行分组
            var jsonFilesGroups = SplitConfigFileNameToGroups(jsonFiles)
                                                                    .Where(u => !excludeJsonPrefixs.Contains(u.Key, StringComparer.OrdinalIgnoreCase) && !u.Any(c => runtimeJsonSuffixs.Any(z => c.EndsWith(z, StringComparison.OrdinalIgnoreCase)) || ignoreConfigurationFiles.Contains(Path.GetFileName(c), StringComparer.OrdinalIgnoreCase)));

            // 遍历所有配置分组
            foreach (var group in jsonFilesGroups)
            {
                // 根据文件名长度排序
                var orderGroup = group.OrderBy(u => Path.GetFileName(u).Length);
                var firstJsonFile = orderGroup.First();

                // 默认加载第一个
                config.AddJsonFile(firstJsonFile, optional: true, reloadOnChange: true);

                // 查找和当前环境相关的配置文件
                var environmentJsonFile = orderGroup.FirstOrDefault(u => Path.GetFileNameWithoutExtension(u).EndsWith($".{envName}", StringComparison.OrdinalIgnoreCase));
                if (environmentJsonFile != null && environmentJsonFile != firstJsonFile)
                {
                    // 加载当前环境的配置文件
                    config.AddJsonFile(environmentJsonFile, optional: true, reloadOnChange: true);
                }
            }
        }

        /// <summary>
        /// 排序的配置文件前缀
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
            "runtimeconfig.json"
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
}