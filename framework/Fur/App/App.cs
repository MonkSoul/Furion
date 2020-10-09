// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace Fur
{
    /// <summary>
    /// 全局应用类
    /// </summary>
    [SkipScan]
    public static class App
    {
        /// <summary>
        /// 私有设置，避免重复解析
        /// </summary>
        private static AppSettingsOptions _settings;

        /// <summary>
        /// 应用全局配置
        /// </summary>
        public static AppSettingsOptions Settings
        {
            // 避免重复解析
            get
            {
                if (_settings == null)
                    _settings = GetOptions<AppSettingsOptions>();
                return _settings;
            }
        }

        /// <summary>
        /// 瞬时服务提供器，每次都是不一样的实例
        /// </summary>
        public static IServiceProvider Services => InternalApp.InternalServices.BuildServiceProvider();

        /// <summary>
        /// 应用服务提供器
        /// </summary>
        public static IServiceProvider ApplicationServices { get; internal set; }

        /// <summary>
        /// 请求服务提供器，相当于使用构造函数注入方式
        /// </summary>
        /// <remarks>每一个请求一个作用域，由于基于请求，所以可能有空异常</remarks>
        /// <exception cref="ArgumentNullException">空异常</exception>
        public static IServiceProvider RequestServices => GetService<IHttpContextAccessor>()?.HttpContext?.RequestServices;

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static readonly IConfiguration Configuration;

        /// <summary>
        /// 应用环境
        /// </summary>
        public static IWebHostEnvironment HostEnvironment => GetService<IWebHostEnvironment>();

        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 能够被扫描的类型
        /// </summary>
        public static readonly IEnumerable<Type> CanBeScanTypes;

        /// <summary>
        /// 应用所有启动配置对象
        /// </summary>
        internal static ConcurrentBag<AppStartup> Startups;

        /// <summary>
        /// 构造函数
        /// </summary>
        static App()
        {
            Configuration = InternalApp.ConfigurationBuilder.Build();

            Assemblies = GetAssemblies();
            CanBeScanTypes = Assemblies.SelectMany(u => u.GetTypes()
                .Where(u => u.IsPublic && !u.IsDefined(typeof(SkipScanAttribute), false)));

            Startups = new ConcurrentBag<AppStartup>();
        }

        /// <summary>
        /// 获取瞬时服务
        /// </summary>
        /// <typeparam name="TService">服务</typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
        {
            return Services.GetService<TService>();
        }

        /// <summary>
        /// 获取作用域服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetRequestService<TService>()
        {
            return RequestServices.GetService<TService>();
        }

        /// <summary>
        /// 获取瞬时服务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return Services.GetService(type);
        }

        /// <summary>
        /// 获取作用域服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRequestService(Type type)
        {
            return RequestServices.GetService(type);
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="jsonKey">配置中对应的Key</param>
        /// <returns>TOptions</returns>
        public static TOptions GetOptions<TOptions>(string jsonKey)
            where TOptions : class, new()
        {
            return Configuration.GetSection(jsonKey).Get<TOptions>();
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns>TOptions</returns>
        public static TOptions GetOptions<TOptions>()
            where TOptions : class, new()
        {
            return GetService<IOptions<TOptions>>().Value;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns>TOptions</returns>
        public static TOptions GetOptionsMonitor<TOptions>()
            where TOptions : class, new()
        {
            return GetService<IOptionsMonitor<TOptions>>().CurrentValue;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns>TOptions</returns>
        public static TOptions GetOptionsSnapshot<TOptions>()
            where TOptions : class, new()
        {
            return GetService<IOptionsSnapshot<TOptions>>().Value;
        }

        /// <summary>
        /// 不支持解析服务错误提示
        /// </summary>
        private const string NotSupportedResolveMessage = "Reading {0} instances on non HTTP requests is not supported.";

        /// <summary>
        /// 获取非泛型仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static IRepository GetRepository()
        {
            return GetRequestService<IRepository>()
                ?? throw Oops.Oh(NotSupportedResolveMessage, typeof(NotSupportedException), nameof(IRepository));
        }

        /// <summary>
        /// 获取实体仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>IRepository<TEntity></returns>
        public static IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return GetRequestService<IRepository<TEntity>>()
                ?? throw Oops.Oh(NotSupportedResolveMessage, typeof(NotSupportedException), nameof(IRepository<TEntity>));
        }

        /// <summary>
        /// 获取实体仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>IRepository<TEntity, TDbContextLocator></returns>
        public static IRepository<TEntity, TDbContextLocator> GetRepository<TEntity, TDbContextLocator>()
            where TEntity : class, IPrivateEntity, new()
            where TDbContextLocator : class, IDbContextLocator
        {
            return GetRequestService<IRepository<TEntity, TDbContextLocator>>()
                ?? throw Oops.Oh(NotSupportedResolveMessage, typeof(NotSupportedException), nameof(IRepository<TEntity, TDbContextLocator>));
        }

        /// <summary>
        /// 获取Sql仓储
        /// </summary>
        /// <returns>ISqlRepository</returns>
        public static ISqlRepository GetSqlRepository()
        {
            return GetRequestService<ISqlRepository>()
                ?? throw Oops.Oh(NotSupportedResolveMessage, typeof(NotSupportedException), nameof(ISqlRepository));
        }

        /// <summary>
        /// 获取Sql仓储
        /// </summary>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>ISqlRepository<TDbContextLocator></returns>
        public static ISqlRepository<TDbContextLocator> GetSqlRepository<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            return GetRequestService<ISqlRepository<TDbContextLocator>>()
                ?? throw Oops.Oh(NotSupportedResolveMessage, typeof(NotSupportedException), nameof(ISqlRepository<TDbContextLocator>));
        }

        /// <summary>
        /// 获取Sql代理
        /// </summary>
        /// <returns>ISqlRepository</returns>
        public static TSqlDispatchProxy GetSqlDispatchProxy<TSqlDispatchProxy>()
            where TSqlDispatchProxy : class, ISqlDispatchProxy
        {
            return GetRequestService<TSqlDispatchProxy>()
                ?? throw Oops.Oh(NotSupportedResolveMessage, typeof(NotSupportedException), nameof(ISqlDispatchProxy));
        }

        /// <summary>
        /// 获取瞬时数据库上下文
        /// </summary>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns></returns>
        public static DbContext GetDbContext<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            var dbContextResolve = GetService<Func<Type, ITransient, DbContext>>();
            return dbContextResolve(typeof(TDbContextLocator), default);
        }

        /// <summary>
        /// 获取作用域数据库上下文
        /// </summary>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns></returns>
        public static DbContext GetRequestDbContext<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            var dbContextResolve = GetRequestService<Func<Type, IScoped, DbContext>>();
            return dbContextResolve(typeof(TDbContextLocator), default);
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
            var caseCategory = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(category);
            var customTiming = MiniProfiler.Current.CustomTiming(category, string.IsNullOrEmpty(message) ? $"{caseCategory} {state}" : message, state);

            // 判断是否是警告消息
            if (isError) customTiming.Errored = true;
        }

        /// <summary>
        /// 获取应用有效程序集
        /// </summary>
        /// <returns>IEnumerable<Assembly></returns>
        internal static IEnumerable<Assembly> GetAssemblies()
        {
            // 需排除的程序集后缀
            var excludeAssemblyNames = new string[] {
                "Database.Migrations"
            };

            // 读取应用配置
            var settings = GetOptions<AppSettingsOptions>("AppSettings") ?? new AppSettingsOptions { };

            var dependencyContext = DependencyContext.Default;

            // 读取项目程序集或 Fur 官方发布的包，或手动添加引用的dll
            var scanAssemblies = dependencyContext.CompileLibraries
                .Where(u => (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j)))
                    || (u.Type == "package" && u.Name.StartsWith(nameof(Fur)))
                    || (settings.EnabledReferenceAssemblyScan == true && u.Type == "reference"))    // 判断是否启用引用程序集扫描
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)))
                .ToList();

            // 加载 `appsetting.json` 配置的外部程序集
            if (settings.ExternalAssemblies != null && settings.ExternalAssemblies.Any())
            {
                foreach (var externalAssembly in settings.ExternalAssemblies)
                {
                    scanAssemblies.Add(Assembly.Load(externalAssembly));
                }
            }

            return scanAssemblies;
        }
    }

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
        /// 全局配置构建器
        /// </summary>
        internal static IConfigurationBuilder ConfigurationBuilder;
    }
}