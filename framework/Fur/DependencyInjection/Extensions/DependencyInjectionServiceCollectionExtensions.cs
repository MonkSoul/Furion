// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur;
using Fur.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入拓展类
    /// </summary>
    [NonBeScan]
    public static class DependencyInjectionServiceCollectionExtensions
    {
        /// <summary>
        /// 添加依赖注入接口
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="autoScan">自动扫描注入</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, bool autoScan = true)
        {
            // 扫描所有继承 AppStartup 的类
            var startups = App.CanBeScanTypes
                .Where(u => typeof(AppStartup).IsAssignableFrom(u) && u.IsClass && !u.IsAbstract && !u.IsGenericType)
                .OrderByDescending(u => GetOrder(u));

            // 注册自定义 starup
            foreach (var type in startups)
            {
                var startup = Activator.CreateInstance(type) as AppStartup;
                startup.ConfigureServices(services);
            }

            // 添加自动扫描注入
            if (autoScan) services.AddAutoScanInjection();

            return services;
        }

        /// <summary>
        /// 添加自动扫描注入
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddAutoScanInjection(this IServiceCollection services)
        {
            // 查找所有需要依赖注入的类型
            var injectTypes = App.CanBeScanTypes
                .Where(u => typeof(IDependency).IsAssignableFrom(u) && u.IsClass && !u.IsInterface && !u.IsAbstract);

            // 执行依赖注入
            foreach (var type in injectTypes)
            {
                // 获取注册方式
                var injectionAttribute = !type.IsDefined(typeof(InjectionAttribute)) ? new InjectionAttribute() : type.GetCustomAttribute<InjectionAttribute>();

                // 获取所有能注册的接口
                var canInjectInterfaces = type.GetInterfaces().Where(u => !typeof(IDependency).IsAssignableFrom(u));

                // 注册暂时服务
                if (typeof(ITransient).IsAssignableFrom(type))
                {
                    AddTransient(services, type, injectionAttribute, canInjectInterfaces);
                }
                // 注册作用域服务
                else if (typeof(IScoped).IsAssignableFrom(type))
                {
                    AddScoped(services, type, injectionAttribute, canInjectInterfaces);
                }
                // 注册单例服务
                else if (typeof(ISingleton).IsAssignableFrom(type))
                {
                    AddSingleton(services, type, injectionAttribute, canInjectInterfaces);
                }
            }

            return services;
        }

        /// <summary>
        /// 添加代理
        /// </summary>
        /// <typeparam name="TDispatchProxy">代理类</typeparam>
        /// <typeparam name="TITDispatchProxy">被代理接口依赖</typeparam>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDispatchProxy<TDispatchProxy, TITDispatchProxy>(this IServiceCollection services)
            where TDispatchProxy : DispatchProxy, IDispatchProxy
            where TITDispatchProxy : class
        {
            // 注册代理类
            services.AddScoped<DispatchProxy, TDispatchProxy>();

            // 代理依赖接口类型
            var typeDependency = typeof(TITDispatchProxy);

            // 获取所有的代理接口类型
            var sqlDispatchProxyInterfaceTypes = App.CanBeScanTypes
                .Where(u => typeDependency.IsAssignableFrom(u) && u.IsInterface && u != typeDependency);

            // 获取代理创建方法
            var dispatchCreateMethod = typeof(DispatchProxy).GetMethod(nameof(DispatchProxy.Create));

            // 注册代理类型
            foreach (var interfaceType in sqlDispatchProxyInterfaceTypes)
            {
                services.AddScoped(interfaceType, provider =>
                {
                    var proxy = dispatchCreateMethod.MakeGenericMethod(interfaceType, typeof(TDispatchProxy)).Invoke(null, null);
                    ((TDispatchProxy)proxy).ServiceProvider = provider;

                    return proxy;
                });
            }

            return services;
        }

        /// <summary>
        /// 注册暂时服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="canInjectInterfaces">能被注册的接口</param>
        private static void AddTransient(IServiceCollection services, Type type, InjectionAttribute injectionAttribute, IEnumerable<Type> canInjectInterfaces)
        {
            // 注册自己
            if (injectionAttribute.InjectionScope is InjectionScopeOptions.Self or InjectionScopeOptions.All)
            {
                switch (injectionAttribute.Injection)
                {
                    case InjectionOptions.TryAdd:
                        services.TryAddTransient(type);
                        break;

                    case InjectionOptions.Add:
                        services.AddTransient(type);
                        break;

                    default: break;
                }
            }

            if (!canInjectInterfaces.Any()) return;

            // 只注册第一个接口
            if (injectionAttribute.InjectionScope == InjectionScopeOptions.FirstOneInterface)
            {
                var firstInterface = canInjectInterfaces.First();

                switch (injectionAttribute.Injection)
                {
                    case InjectionOptions.TryAdd:
                        services.TryAddTransient(firstInterface, type);
                        break;

                    case InjectionOptions.Add:
                        services.AddTransient(firstInterface, type);
                        break;

                    default: break;
                }
            }
            // 注册多个接口
            else if (injectionAttribute.InjectionScope is InjectionScopeOptions.ImplementedInterfaces or InjectionScopeOptions.All)
            {
                foreach (var inter in canInjectInterfaces)
                {
                    switch (injectionAttribute.Injection)
                    {
                        case InjectionOptions.TryAdd:
                            services.TryAddTransient(inter, type);
                            break;

                        case InjectionOptions.Add:
                            services.AddTransient(inter, type);
                            break;

                        default: break;
                    }
                }
            }
        }

        /// <summary>
        /// 注册作用域服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="canInjectInterfaces">能被注册的接口</param>
        private static void AddScoped(IServiceCollection services, Type type, InjectionAttribute injectionAttribute, IEnumerable<Type> canInjectInterfaces)
        {
            // 注册自己
            if (injectionAttribute.InjectionScope is InjectionScopeOptions.Self or InjectionScopeOptions.All)
            {
                switch (injectionAttribute.Injection)
                {
                    case InjectionOptions.TryAdd:
                        services.TryAddScoped(type);
                        break;

                    case InjectionOptions.Add:
                        services.AddScoped(type);
                        break;

                    default: break;
                }
            }

            if (!canInjectInterfaces.Any()) return;

            // 只注册第一个接口
            if (injectionAttribute.InjectionScope == InjectionScopeOptions.FirstOneInterface)
            {
                var firstInterface = canInjectInterfaces.First();

                switch (injectionAttribute.Injection)
                {
                    case InjectionOptions.TryAdd:
                        services.TryAddScoped(firstInterface, type);
                        break;

                    case InjectionOptions.Add:
                        services.AddScoped(firstInterface, type);
                        break;

                    default: break;
                }
            }
            // 注册多个接口
            else if (injectionAttribute.InjectionScope is InjectionScopeOptions.ImplementedInterfaces or InjectionScopeOptions.All)
            {
                foreach (var inter in canInjectInterfaces)
                {
                    switch (injectionAttribute.Injection)
                    {
                        case InjectionOptions.TryAdd:
                            services.TryAddScoped(inter, type);
                            break;

                        case InjectionOptions.Add:
                            services.AddScoped(inter, type);
                            break;

                        default: break;
                    }
                }
            }
        }

        /// <summary>
        /// 注册单例服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="canInjectInterfaces">能被注册的接口</param>
        private static void AddSingleton(IServiceCollection services, Type type, InjectionAttribute injectionAttribute, IEnumerable<Type> canInjectInterfaces)
        {
            // 注册自己
            if (injectionAttribute.InjectionScope is InjectionScopeOptions.Self or InjectionScopeOptions.All)
            {
                switch (injectionAttribute.Injection)
                {
                    case InjectionOptions.TryAdd:
                        services.TryAddSingleton(type);
                        break;

                    case InjectionOptions.Add:
                        services.AddSingleton(type);
                        break;

                    default: break;
                }
            }

            if (!canInjectInterfaces.Any()) return;

            // 只注册第一个接口
            if (injectionAttribute.InjectionScope == InjectionScopeOptions.FirstOneInterface)
            {
                var firstInterface = canInjectInterfaces.First();

                switch (injectionAttribute.Injection)
                {
                    case InjectionOptions.TryAdd:
                        services.TryAddSingleton(firstInterface, type);
                        break;

                    case InjectionOptions.Add:
                        services.AddSingleton(firstInterface, type);
                        break;

                    default: break;
                }
            }
            // 注册多个接口
            else if (injectionAttribute.InjectionScope is InjectionScopeOptions.ImplementedInterfaces or InjectionScopeOptions.All)
            {
                foreach (var inter in canInjectInterfaces)
                {
                    switch (injectionAttribute.Injection)
                    {
                        case InjectionOptions.TryAdd:
                            services.TryAddSingleton(inter, type);
                            break;

                        case InjectionOptions.Add:
                            services.AddSingleton(inter, type);
                            break;

                        default: break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取 Startup 排序
        /// </summary>
        /// <param name="type">排序类型</param>
        /// <returns>int</returns>
        private static int GetOrder(Type type)
        {
            return !type.IsDefined(typeof(StartupOrderAttribute), true)
                ? 0
                : type.GetCustomAttribute<StartupOrderAttribute>(true).Order;
        }
    }
}