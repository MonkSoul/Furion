using Fur.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
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
            get
            {
                if (_settings == null)
                    _settings = ServiceProvider.GetService<IOptions<AppSettingsOptions>>().Value;
                return _settings;
            }
        }

        /// <summary>
        /// 服务提供器
        /// </summary>
        public static ServiceProvider ServiceProvider { get => Services.BuildServiceProvider(); }

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static IConfiguration Configuration { get => ServiceProvider.GetService<IConfiguration>(); }

        /// <summary>
        /// 应用环境
        /// </summary>
        public static IWebHostEnvironment HostEnvironment { get => ServiceProvider.GetService<IWebHostEnvironment>(); }

        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 应用服务
        /// </summary>
        internal static IServiceCollection Services;

        static App()
        {
            Assemblies = GetAssemblies();
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="jsonKey">配置中对应的Key</param>
        /// <returns></returns>
        public static TOptions GetOptions<TOptions>(string jsonKey)
            where TOptions : class
            => Configuration.GetSection(jsonKey).Get<TOptions>();

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns></returns>
        public static TOptions GetOptions<TOptions>()
            where TOptions : class
            => ServiceProvider.GetService<IOptions<TOptions>>().Value;

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns></returns>
        public static TOptions GetOptionsMonitor<TOptions>()
            where TOptions : class
            => ServiceProvider.GetService<IOptionsMonitor<TOptions>>().CurrentValue;

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns></returns>
        public static TOptions GetOptionsSnapshot<TOptions>()
            where TOptions : class
            => ServiceProvider.GetService<IOptionsSnapshot<TOptions>>().Value;

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
        /// <returns></returns>
        private static IEnumerable<Assembly> GetAssemblies()
        {
            // 需排除的程序集名称
            var excludeAssemblyNames = new string[] {
                "Fur.Database.Migrations"
            };

            var dependencyConext = DependencyContext.Default;

            // 读取项目程序集或Fur官方发布的包
            return dependencyConext.CompileLibraries
                .Where(u => (u.Type == "project" && !excludeAssemblyNames.Contains(u.Name)) || (u.Type == "package" && u.Name.StartsWith(nameof(Fur))))
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));
        }
    }
}