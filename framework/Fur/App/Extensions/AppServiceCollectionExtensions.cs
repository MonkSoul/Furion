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

using Fur;
using Fur.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 应用服务集合拓展类（由框架内部调用）
    /// </summary>
    [SkipScan]
    internal static class AppServiceCollectionExtensions
    {
        /// <summary>
        /// MiniProfiler 插件路径
        /// </summary>
        private const string MiniProfilerRouteBasePath = "/index-mini-profiler";

        /// <summary>
        /// 添加应用配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">服务配置</param>
        /// <returns>服务集合</returns>
        internal static IServiceCollection AddApp(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            // 注册全局配置选项
            services.AddConfigurableOptions<AppSettingsOptions>();

            // 注册 IHttpContextAccessor
            services.AddHttpContextAccessor();

            // 注册分布式内存缓存
            services.AddDistributedMemoryCache();

            // 注册全局 Startup 扫描
            services.AddStartup();

            // 注册对象映射
            services.AddObjectMapper();

            // 注册MiniProfiler 组件
            if (App.Settings.InjectMiniProfiler == true)
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = MiniProfilerRouteBasePath;
                }).AddEntityFramework();
            }

            // 自定义服务
            configure?.Invoke(services);

            // 注册全局依赖注入
            services.AddDependencyInjection();

            return services;
        }

        /// <summary>
        /// 添加 Startup 自动扫描
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="autoScan">自动扫描注入</param>
        /// <returns>服务集合</returns>
        internal static IServiceCollection AddStartup(this IServiceCollection services, bool autoScan = true)
        {
            // 扫描所有继承 AppStartup 的类
            var startups = App.CanBeScanTypes
                .Where(u => typeof(AppStartup).IsAssignableFrom(u) && u.IsClass && !u.IsAbstract && !u.IsGenericType)
                .OrderByDescending(u => GetOrder(u));

            // 注册自定义 starup
            foreach (var type in startups)
            {
                var startup = Activator.CreateInstance(type) as AppStartup;
                App.Startups.Add(startup);

                // 获取所有符合依赖注入格式的方法，如返回值void，且第一个参数是 IServiceCollection 类型
                var serviceMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(u => u.ReturnType == typeof(void)
                        && u.GetParameters().Length > 0
                        && u.GetParameters().First().ParameterType == typeof(IServiceCollection));

                if (!serviceMethods.Any()) continue;

                // 自动安装属性调用
                foreach (var method in serviceMethods)
                {
                    method.Invoke(startup, new[] { services });
                }
            }

            // 添加自动扫描注入
            if (autoScan) services.AddAutoScanInjection();

            return services;
        }

        /// <summary>
        /// 获取 Startup 排序
        /// </summary>
        /// <param name="type">排序类型</param>
        /// <returns>int</returns>
        private static int GetOrder(Type type)
        {
            return !type.IsDefined(typeof(AppStartupAttribute), true)
                ? 0
                : type.GetCustomAttribute<AppStartupAttribute>(true).Order;
        }
    }
}