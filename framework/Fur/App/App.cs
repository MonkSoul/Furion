using Fur.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        /// <remarks>
        /// 通过它可以获取第三方容器注入的服务，也就是不是 asp.net core 托管的，或者自己实现 Ioc/DI 的方式
        /// 如采用 autofac 替换默认 IOC 容器，这样就可以通过 Application.GetAutofacRoot() 获取 Autofac容器对象，无需采用静态类手工存储该容器
        /// </remarks>
        /// <example>
        /// <code>
        /// var container = Application.GetAutofacRoot();
        /// var service = container.Resolve(typeof(TService));
        /// </code>
        /// </example>
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
        /// 应用环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment => GetService<IWebHostEnvironment>();

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
        /// <returns>IEnumerable</returns>
        private static IEnumerable<Assembly> GetAssemblies()
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
}