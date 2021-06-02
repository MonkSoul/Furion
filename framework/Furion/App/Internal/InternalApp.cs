// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.9
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Furion
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
            // 读取忽略的配置文件
            var ignoreConfigurationFiles = appsettingsConfiguration
                    .GetSection("IgnoreConfigurationFiles")
                    .Get<string[]>()
                ?? Array.Empty<string>();

            // 加载配置
            AutoAddJsonFiles(config, env, ignoreConfigurationFiles);
            AutoAddXmlFiles(config, env, ignoreConfigurationFiles);

            // 存储配置
            ConfigurationBuilder = config;
        }

        /// <summary>
        /// 自动加载自定义 .json 配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <param name="ignoreConfigurationFiles"></param>
        private static void AutoAddJsonFiles(IConfigurationBuilder config, IHostEnvironment env, string[] ignoreConfigurationFiles)
        {
            // 获取程序目录下的所有配置文件
            var jsonFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.json", SearchOption.TopDirectoryOnly)
                .Union(
                    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                )
                .Where(u => CheckIncludeDefaultSettings(Path.GetFileName(u)) && !ignoreConfigurationFiles.Contains(Path.GetFileName(u)) && !runtimeJsonSuffixs.Any(j => u.EndsWith(j)));

            if (!jsonFiles.Any()) return;

            // 获取环境变量名
            var envName = env.EnvironmentName;
            var envFiles = new List<string>();

            // 自动加载配置文件
            foreach (var jsonFile in jsonFiles)
            {
                // 处理带环境的配置文件
                if (Path.GetFileNameWithoutExtension(jsonFile).EndsWith($".{envName}"))
                {
                    envFiles.Add(jsonFile);
                    continue;
                }

                config.AddJsonFile(jsonFile, optional: true, reloadOnChange: true);
            }

            // 配置带环境的配置文件
            envFiles.ForEach(u => config.AddJsonFile(u, optional: true, reloadOnChange: true));
        }

        /// <summary>
        /// 自动加载自定义 .xml 配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <param name="ignoreConfigurationFiles"></param>
        private static void AutoAddXmlFiles(IConfigurationBuilder config, IHostEnvironment env, string[] ignoreConfigurationFiles)
        {
            // 获取程序目录下的所有配置文件，必须以 .config.xml 结尾
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .Union(
                    Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml", SearchOption.TopDirectoryOnly)
                )
                .Where(u => CheckIncludeDefaultSettings(Path.GetFileName(u)) && !ignoreConfigurationFiles.Contains(Path.GetFileName(u)) && u.EndsWith(".config.xml", StringComparison.OrdinalIgnoreCase));

            if (!xmlFiles.Any()) return;

            // 获取环境变量名
            var envName = env.EnvironmentName;
            var envFiles = new List<string>();

            // 自动加载配置文件
            foreach (var xmlFile in xmlFiles)
            {
                // 处理带环境的配置文件
                if (Path.GetFileNameWithoutExtension(xmlFile).EndsWith($".{envName}.config"))
                {
                    envFiles.Add(xmlFile);
                    continue;
                }

                config.AddXmlFile(xmlFile, optional: true, reloadOnChange: true);
            }

            // 配置带环境的配置文件
            envFiles.ForEach(u => config.AddXmlFile(u, optional: true, reloadOnChange: true));
        }

        /// <summary>
        /// 排除特定配置文件正则表达式
        /// </summary>
        private const string excludeJsonPattern = @"^{0}(\.\w+)?\.((json)|(xml))$";

        /// <summary>
        /// 排序的配置文件前缀
        /// </summary>
        private static readonly string[] excludeJsonPrefixs = new[] { "appsettings", "bundleconfig", "compilerconfig" };

        /// <summary>
        /// 检查是否受排除的配置文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool CheckIncludeDefaultSettings(string fileName)
        {
            var isTrue = true;

            foreach (var prefix in excludeJsonPrefixs)
            {
                var isMatch = Regex.IsMatch(fileName, string.Format(excludeJsonPattern, prefix));
                if (isMatch)
                {
                    isTrue = false;
                    break;
                }
            }

            return isTrue;
        }

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
    }
}