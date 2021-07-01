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

using Furion.DependencyInjection;
using Furion.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Security.Claims;

namespace Furion
{
    /// <summary>
    /// 全局应用类
    /// </summary>
    [SuppressSniffer]
    public static class App
    {
        /// <summary>
        /// 私有设置，避免重复解析
        /// </summary>
        private static AppSettingsOptions _settings;

        /// <summary>
        /// 应用全局配置
        /// </summary>
        public static AppSettingsOptions Settings => _settings ??= GetOptions<AppSettingsOptions>();

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static IConfiguration Configuration { get; internal set; }

        /// <summary>
        /// 获取Web主机环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment => InternalApp.WebHostEnvironment;

        /// <summary>
        /// 获取泛型主机环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IHostEnvironment HostEnvironment => InternalApp.HostEnvironment;

        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 有效程序集类型
        /// </summary>
        public static readonly IEnumerable<Type> EffectiveTypes;

        /// <summary>
        /// 存储根服务，可能为空
        /// </summary>
        public static IServiceProvider RootServices => InternalApp.RootServices;

        /// <summary>
        /// 获取请求上下文
        /// </summary>
        public static HttpContext HttpContext => HttpContextLocal.Current();

        /// <summary>
        /// 获取请求上下文用户
        /// </summary>
        public static ClaimsPrincipal User => HttpContext?.User;

        /// <summary>
        /// 未托管的对象即可
        /// </summary>
        public static readonly ConcurrentBag<IDisposable> UnmanagedObjects;

        /// <summary>
        /// 服务提供器
        /// </summary>
        public static IServiceProvider ServiceProvider
        {
            get
            {
                // 从 HttpContext 获取服务提供器
                var servicesProvider = HttpContext?.RequestServices;
                if (servicesProvider != null) return servicesProvider;

                // 创建新的服务提供器，并存储起来
                var newServicesProvider = InternalApp.InternalServices.BuildServiceProvider();
                UnmanagedObjects.Add(newServicesProvider);
                return newServicesProvider;
            }
        }

        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetService<TService>(IServiceProvider serviceProvider = default)
            where TService : class
        {
            return GetService(typeof(TService), serviceProvider) as TService;
        }

        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static object GetService(Type type, IServiceProvider serviceProvider = default)
        {
            return (serviceProvider ?? ServiceProvider).GetService(type);
        }

        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetRequiredService<TService>(IServiceProvider serviceProvider = default)
            where TService : class
        {
            return GetRequiredService(typeof(TService), serviceProvider) as TService;
        }

        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static object GetRequiredService(Type type, IServiceProvider serviceProvider = default)
        {
            return (serviceProvider ?? ServiceProvider).GetRequiredService(type);
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="jsonKey">配置中对应的Key</param>
        /// <returns>TOptions</returns>
        public static TOptions GetConfig<TOptions>(string jsonKey)
        {
            return Configuration.GetSection(jsonKey).Get<TOptions>();
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns>TOptions</returns>
        public static TOptions GetOptions<TOptions>(IServiceProvider serviceProvider = default)
            where TOptions : class, new()
        {
            return GetService<IOptions<TOptions>>(serviceProvider ?? RootServices)?.Value;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns>TOptions</returns>
        public static TOptions GetOptionsMonitor<TOptions>(IServiceProvider serviceProvider = default)
            where TOptions : class, new()
        {
            return GetService<IOptionsMonitor<TOptions>>(serviceProvider ?? RootServices)?.CurrentValue;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns>TOptions</returns>
        public static TOptions GetOptionsSnapshot<TOptions>(IServiceProvider serviceProvider = default)
            where TOptions : class, new()
        {
            // 这里不能从根服务解析，因为是 Scoped 作用域
            return GetService<IOptionsSnapshot<TOptions>>(serviceProvider)?.Value;
        }

        /// <summary>
        /// 打印验证信息到 MiniProfiler
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="state">状态</param>
        /// <param name="message">消息</param>
        /// <param name="isError">是否为警告消息</param>
        public static void PrintToMiniProfiler(string category, string state, string message = null, bool isError = false)
        {
            // 判断是否注入 MiniProfiler 组件
            if (Settings.InjectMiniProfiler != true) return;

            // 打印消息
            var customTiming = MiniProfiler.Current.CustomTiming(category, string.IsNullOrWhiteSpace(message) ? $"{category.ToTitleCase()} {state}" : message, state);
            if (customTiming == null) return;

            // 判断是否是警告消息
            if (isError) customTiming.Errored = true;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        static App()
        {
            // 未托管的对象
            UnmanagedObjects = new ConcurrentBag<IDisposable>();

            // 编译配置
            Configuration = InternalApp.ConfigurationBuilder.Build();

            // 加载程序集
            var assObject = GetAssemblies();
            Assemblies = assObject.Assemblies;
            ExternalAssemblies = assObject.ExternalAssemblies;

            // 获取有效的类型集合
            EffectiveTypes = Assemblies.SelectMany(u => u.GetTypes()
                .Where(u => u.IsPublic && !u.IsDefined(typeof(SuppressSnifferAttribute), false)));

            AppStartups = new ConcurrentBag<AppStartup>();
        }

        /// <summary>
        /// 应用所有启动配置对象
        /// </summary>
        internal static ConcurrentBag<AppStartup> AppStartups;

        /// <summary>
        /// 外部程序集
        /// </summary>
        internal static IEnumerable<Assembly> ExternalAssemblies;

        /// <summary>
        /// 获取应用有效程序集
        /// </summary>
        /// <returns>IEnumerable</returns>
        private static (IEnumerable<Assembly> Assemblies, IEnumerable<Assembly> ExternalAssemblies) GetAssemblies()
        {
            // 需排除的程序集后缀
            var excludeAssemblyNames = new string[] {
                "Database.Migrations"
            };

            // 读取应用配置
            var settings = GetConfig<AppSettingsOptions>("AppSettings") ?? new AppSettingsOptions { };
            var supportPackageNamePrefixs = settings.SupportPackageNamePrefixs ?? Array.Empty<string>();

            var dependencyContext = DependencyContext.Default;

            // 读取项目程序集或 Furion 官方发布的包，或手动添加引用的dll，或配置特定的包前缀
            var scanAssemblies = dependencyContext.CompileLibraries
                .Where(u =>
                       (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j))) ||
                       (u.Type == "package" && (u.Name.StartsWith(nameof(Furion)) || supportPackageNamePrefixs.Any(p => u.Name.StartsWith(p)))) ||
                       (settings.EnabledReferenceAssemblyScan == true && u.Type == "reference"))    // 判断是否启用引用程序集扫描
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));

            IEnumerable<Assembly> externalAssemblies = Array.Empty<Assembly>();

            // 加载 `appsetting.json` 配置的外部程序集
            if (settings.ExternalAssemblies != null && settings.ExternalAssemblies.Any())
            {
                foreach (var externalAssembly in settings.ExternalAssemblies)
                {
                    // 加载外部程序集
                    var assemblyFileFullPath = Path.Combine(AppContext.BaseDirectory
                        , externalAssembly.EndsWith(".dll") ? externalAssembly : $"{externalAssembly}.dll");

                    // 模块化文件存在再加载
                    if (!File.Exists(assemblyFileFullPath)) continue;

                    var assembly = new[] { Assembly.LoadFrom(assemblyFileFullPath) };

                    // 合并程序集
                    scanAssemblies = scanAssemblies.Concat(assembly);
                    externalAssemblies = externalAssemblies.Concat(assembly);
                }
            }

            return (scanAssemblies, externalAssemblies);
        }

        /// <summary>
        /// 释放所有未托管的对象
        /// </summary>
        public static void DisposeUnmanagedObjects()
        {
            while (!UnmanagedObjects.IsEmpty)
            {
                UnmanagedObjects.TryTake(out var serviceProvider);
                serviceProvider?.Dispose();
            }
        }
    }
}