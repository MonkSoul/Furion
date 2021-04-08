using Furion.DependencyInjection;
using Furion.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using StackExchange.Profiling;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Security.Claims;

namespace Furion
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
        public static AppSettingsOptions Settings => _settings ??= GetOptions<AppSettingsOptions>();

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static readonly IConfiguration Configuration;

        /// <summary>
        /// 应用环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment => InternalApp.WebHostEnvironment;

        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 有效程序集类型
        /// </summary>
        public static readonly IEnumerable<Type> EffectiveTypes;

        /// <summary>
        /// 服务提供器
        /// </summary>
        public static IServiceProvider ServiceProvider => HttpContext?.RequestServices ?? InternalApp.InternalServices.BuildServiceProvider();

        /// <summary>
        /// 获取请求上下文
        /// </summary>
        public static HttpContext HttpContext => HttpContextLocal.Current();

        /// <summary>
        /// 获取请求上下文用户
        /// </summary>
        public static ClaimsPrincipal User => HttpContext?.User;

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
            where TOptions : class, new()
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
            return GetService<IOptions<TOptions>>(serviceProvider)?.Value;
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
            return GetService<IOptionsMonitor<TOptions>>(serviceProvider)?.CurrentValue;
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
            var customTiming = MiniProfiler.Current.CustomTiming(category, string.IsNullOrEmpty(message) ? $"{category.ToTitleCase()} {state}" : message, state);
            if (customTiming == null) return;

            // 判断是否是警告消息
            if (isError) customTiming.Errored = true;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        static App()
        {
            // 编译配置
            Configuration = InternalApp.ConfigurationBuilder.Build();

            Assemblies = GetAssemblies();
            EffectiveTypes = Assemblies.SelectMany(u => u.GetTypes()
                .Where(u => u.IsPublic && !u.IsDefined(typeof(SkipScanAttribute), false)));

            AppStartups = new ConcurrentBag<AppStartup>();
        }

        /// <summary>
        /// 应用所有启动配置对象
        /// </summary>
        internal static ConcurrentBag<AppStartup> AppStartups;

        /// <summary>
        /// 保存文件夹的监听
        /// </summary>
        private static readonly ConcurrentDictionary<string, IFileProvider> FileProviders =
            new();

        /// <summary>
        /// 插件上下文的弱引用
        /// </summary>
        private static readonly ConcurrentDictionary<string, WeakReference> PLCReferences =
            new();

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
            var settings = GetConfig<AppSettingsOptions>("AppSettings") ?? new AppSettingsOptions { };

            var dependencyContext = DependencyContext.Default;

            // 读取项目程序集或 Furion 官方发布的包，或手动添加引用的dll
            var scanAssemblies = dependencyContext.CompileLibraries
                .Where(u =>
                       (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j))) ||
                       (u.Type == "package" && u.Name.StartsWith(nameof(Furion))) ||
                       (settings.EnabledReferenceAssemblyScan == true && u.Type == "reference"))    // 判断是否启用引用程序集扫描
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)))
                .ToList();

            // 加载 `appsetting.json` 配置的外部程序集
            if (settings.ExternalAssemblies != null && settings.ExternalAssemblies.Any())
            {
                foreach (var externalAssembly in settings.ExternalAssemblies)
                {
                    var assemblyFileName = externalAssembly.EndsWith(".dll") ? externalAssembly : $"{externalAssembly}.dll";

                    // 参照
                    // https://docs.microsoft.com/zh-cn/dotnet/standard/assembly/unloadability
                    var assembly = LoadExternalAssembly(Path.Combine(AppContext.BaseDirectory, assemblyFileName));
                    if (null != assembly)
                        scanAssemblies.Add(assembly);
                }
            }

            return scanAssemblies;
        }

        /// <summary>
        /// 加载外部程序集
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        /// <returns></returns>
        private static Assembly LoadExternalAssembly(string assemblyFilePath)
        {
            // 文件名
            string assemblyFileName = Path.GetFileName(assemblyFilePath);
            // 所在文件夹绝对路径
            string assemblyFileDirPath = Directory.GetParent(assemblyFilePath).FullName;

            // 初始化外部dll为插件Context
            PluginLoadContext loadContext = new(assemblyFilePath);
            // 记录弱引用，unload用
            var plcWR = new WeakReference(loadContext);
            PLCReferences.AddOrUpdate(assemblyFilePath, plcWR, (oldkey, oldvalue) => plcWR);

            // 获取程序集
            Assembly assembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFilePath)));

            // 添加文件夹监听，key为文件的绝对路径，每个不同文件都需要有一个监听
            var key = assemblyFilePath;
            if (!FileProviders.ContainsKey(key))
            {
                IFileProvider _fileProvider = new PhysicalFileProvider(assemblyFileDirPath);
                FileProviders.AddOrUpdate(key, _fileProvider, (oldkey, oldvalue) => _fileProvider);

                // 文件修改时触发
                ChangeToken.OnChange(
                    () => _fileProvider.Watch(assemblyFileName),
                    () => UpdateExternalAssembly(assemblyFilePath, assembly.FullName));
            }

            return assembly;
        }

        /// <summary>
        /// 更新外部程序集
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        /// <param name="assemblyFullName"></param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void UpdateExternalAssembly(string assemblyFilePath, string assemblyFullName)
        {
            lock (Assemblies)
            {
                // 移除相同FullName的程序集引用
                ((List<Assembly>)Assemblies)?.RemoveAll(ass => ass.FullName == assemblyFullName);

                // 卸载老的
                if (PLCReferences.Remove(assemblyFilePath, out var plcWR))
                {
                    // 执行程序集的卸载
                    DoUnload(plcWR);

                    // 需要做些延迟，AssemblyLoadContext机制不会立刻完成unload
                    for (int i = 0; plcWR.IsAlive && (i < 10); i++)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }

                ((List<Assembly>)Assemblies)?.Add(LoadExternalAssembly(assemblyFilePath));
            }
        }

        /// <summary>
        /// 执行卸载
        /// </summary>
        /// <param name="plcWR"></param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void DoUnload(WeakReference plcWR)
        {
            // 防止引入局部变量导致GC失败，需要单独执行unload，并且不可被优化为内联
            ((PluginLoadContext)plcWR.Target).Unload();
        }
    }
}