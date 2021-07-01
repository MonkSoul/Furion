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

using Furion;
using Furion.DependencyInjection;
using Furion.UnifyResult;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 应用服务集合拓展类（由框架内部调用）
    /// </summary>
    [SuppressSniffer]
    public static class AppServiceCollectionExtensions
    {
        /// <summary>
        /// MiniProfiler 插件路径
        /// </summary>
        private const string MiniProfilerRouteBasePath = "/index-mini-profiler";

        /// <summary>
        /// Mvc 注入基础配置（带Swagger）
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="swaggerGenConfigure"></param>
        /// <returns>IMvcBuilder</returns>
        public static IMvcBuilder AddInject(this IMvcBuilder mvcBuilder, Action<SwaggerGenOptions> swaggerGenConfigure = null)
        {
            mvcBuilder.AddSpecificationDocuments(swaggerGenConfigure)
                      .AddDynamicApiControllers()
                      .AddDataValidation()
                      .AddFriendlyException();

            return mvcBuilder;
        }

        /// <summary>
        /// 服务注入基础配置（带Swagger）
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>IMvcBuilder</returns>
        /// <param name="swaggerGenConfigure"></param>
        public static IServiceCollection AddInject(this IServiceCollection services, Action<SwaggerGenOptions> swaggerGenConfigure = null)
        {
            services.AddSpecificationDocuments(swaggerGenConfigure)
                    .AddDynamicApiControllers()
                    .AddDataValidation()
                    .AddFriendlyException();

            return services;
        }

        /// <summary>
        /// Mvc 注入基础配置
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="includeDynamicApiController"></param>
        /// <returns>IMvcBuilder</returns>
        public static IMvcBuilder AddInjectBase(this IMvcBuilder mvcBuilder, bool includeDynamicApiController = true)
        {
            if (includeDynamicApiController) mvcBuilder.AddDynamicApiControllers();

            mvcBuilder.AddDataValidation()
                      .AddFriendlyException();

            return mvcBuilder;
        }

        /// <summary>
        /// Mvc 注入基础配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="includeDynamicApiController"></param>
        /// <returns>IMvcBuilder</returns>
        public static IServiceCollection AddInjectBase(this IServiceCollection services, bool includeDynamicApiController = true)
        {
            if (includeDynamicApiController) services.AddDynamicApiControllers();

            services.AddDataValidation()
                    .AddFriendlyException();

            return services;
        }

        /// <summary>
        /// Mvc 注入基础配置和规范化结果
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="swaggerGenConfigure"></param>
        /// <returns></returns>
        public static IMvcBuilder AddInjectWithUnifyResult(this IMvcBuilder mvcBuilder, Action<SwaggerGenOptions> swaggerGenConfigure = null)
        {
            mvcBuilder.AddInject(swaggerGenConfigure)
                      .AddUnifyResult();

            return mvcBuilder;
        }

        /// <summary>
        /// 注入基础配置和规范化结果
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfigure"></param>
        /// <returns></returns>
        public static IServiceCollection AddInjectWithUnifyResult(this IServiceCollection services, Action<SwaggerGenOptions> swaggerGenConfigure = null)
        {
            services.AddInject(swaggerGenConfigure)
                    .AddUnifyResult();

            return services;
        }

        /// <summary>
        /// Mvc 注入基础配置和规范化结果
        /// </summary>
        /// <typeparam name="TUnifyResultProvider"></typeparam>
        /// <param name="mvcBuilder"></param>
        /// <param name="swaggerGenConfigure"></param>
        /// <returns></returns>
        public static IMvcBuilder AddInjectWithUnifyResult<TUnifyResultProvider>(this IMvcBuilder mvcBuilder, Action<SwaggerGenOptions> swaggerGenConfigure = null)
            where TUnifyResultProvider : class, IUnifyResultProvider
        {
            mvcBuilder.AddInject(swaggerGenConfigure)
                      .AddUnifyResult<TUnifyResultProvider>();

            return mvcBuilder;
        }

        /// <summary>
        /// Mvc 注入基础配置和规范化结果
        /// </summary>
        /// <typeparam name="TUnifyResultProvider"></typeparam>
        /// <param name="swaggerGenConfigure"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInjectWithUnifyResult<TUnifyResultProvider>(this IServiceCollection services, Action<SwaggerGenOptions> swaggerGenConfigure = null)
            where TUnifyResultProvider : class, IUnifyResultProvider
        {
            services.AddInject(swaggerGenConfigure)
                    .AddUnifyResult<TUnifyResultProvider>();

            return services;
        }

        /// <summary>
        /// 自动添加主机服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppHostedService(this IServiceCollection services)
        {
            // 获取所有 BackgroundService 类型
            var backgroundServiceTypes = App.EffectiveTypes.Where(u => typeof(BackgroundService).IsAssignableFrom(u));
            var addHostServiceMethod = typeof(ServiceCollectionHostedServiceExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                .Where(u => u.Name.Equals("AddHostedService") && u.IsGenericMethod && u.GetParameters().Length == 1)
                                .FirstOrDefault();

            foreach (var type in backgroundServiceTypes)
            {
                addHostServiceMethod.MakeGenericMethod(type).Invoke(null, new object[] { services });
            }

            return services;
        }

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
            var appSettings = App.Settings;

            // 注册内存和分布式内存
            services.AddMemoryCache();  // .NET 5.0.3+ 需要手动注册了
            if (appSettings.EnabledDistributedMemoryCache == true) services.AddDistributedMemoryCache();

            // 注册全局依赖注入
            services.AddDependencyInjection();

            // 添加 HttContext 访问器
            services.AddHttpContextAccessor();

            // 注册全局 Startup 扫描
            services.AddStartups();

            // 注册MiniProfiler 组件
            if (appSettings.InjectMiniProfiler == true)
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = MiniProfilerRouteBasePath;
                }).AddRelationalDiagnosticListener();
            }

            // 添加对象映射
            services.AddObjectMapper();

            // 添加虚拟文件服务
            if (appSettings.EnabledVirtualFileServer == true) services.AddVirtualFileServer();

            // 自定义服务
            configure?.Invoke(services);

            return services;
        }

        /// <summary>
        /// 添加主机应用配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">服务配置</param>
        /// <returns>服务集合</returns>
        internal static IServiceCollection AddHostApp(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            // 注册全局配置选项
            services.AddConfigurableOptions<AppSettingsOptions>();
            var appSettings = App.Settings;

            // 注册内存和分布式内存
            services.AddMemoryCache();  // .NET 5.0.3+ 需要手动注册了
            if (appSettings.EnabledDistributedMemoryCache == true) services.AddDistributedMemoryCache();

            // 注册全局依赖注入
            services.AddDependencyInjection();

            // 注册全局 Startup 扫描
            services.AddStartups();

            // 添加对象映射
            services.AddObjectMapper();

            // 添加虚拟文件服务
            if (appSettings.EnabledVirtualFileServer == true) services.AddVirtualFileServer();

            // 自定义服务
            configure?.Invoke(services);

            return services;
        }

        /// <summary>
        /// 添加 Startup 自动扫描
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        internal static IServiceCollection AddStartups(this IServiceCollection services)
        {
            // 扫描所有继承 AppStartup 的类
            var startups = App.EffectiveTypes
                .Where(u => typeof(AppStartup).IsAssignableFrom(u) && u.IsClass && !u.IsAbstract && !u.IsGenericType)
                .OrderByDescending(u => GetOrder(u));

            // 注册自定义 starup
            foreach (var type in startups)
            {
                var startup = Activator.CreateInstance(type) as AppStartup;
                App.AppStartups.Add(startup);

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