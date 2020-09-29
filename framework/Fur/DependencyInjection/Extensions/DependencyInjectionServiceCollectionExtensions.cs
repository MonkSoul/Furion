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
    [SkipScan]
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
                // 获取所有符合依赖注入格式的方法，如返回值void，且第一个参数是 IServiceCollection 类型
                var serviceMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(u => u.ReturnType == typeof(void)
                        && u.GetParameters().Length > 0
                        && u.GetParameters().First().ParameterType == typeof(IServiceCollection));

                if (!serviceMethods.Any()) continue;

                var startup = Activator.CreateInstance(type) as AppStartup;
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
                    RegisterService(services, Fur.DependencyInjection.RegisterType.Transient, type, injectionAttribute, canInjectInterfaces);
                }
                // 注册作用域服务
                else if (typeof(IScoped).IsAssignableFrom(type))
                {
                    RegisterService(services, Fur.DependencyInjection.RegisterType.Scoped, type, injectionAttribute, canInjectInterfaces);
                }
                // 注册单例服务
                else if (typeof(ISingleton).IsAssignableFrom(type))
                {
                    RegisterService(services, Fur.DependencyInjection.RegisterType.Singleton, type, injectionAttribute, canInjectInterfaces);
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
        /// 注册服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="registerType">类型作用域</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="canInjectInterfaces">能被注册的接口</param>
        private static void RegisterService(IServiceCollection services, RegisterType registerType, Type type, InjectionAttribute injectionAttribute, IEnumerable<Type> canInjectInterfaces)
        {
            // 注册自己
            if (injectionAttribute.InjectionScope is InjectionScopeOptions.Self or InjectionScopeOptions.All)
            {
                RegisterType(services, registerType, type, injectionAttribute);
            }

            if (!canInjectInterfaces.Any()) return;

            // 只注册第一个接口
            if (injectionAttribute.InjectionScope == InjectionScopeOptions.FirstOneInterface)
            {
                RegisterType(services, registerType, type, injectionAttribute, canInjectInterfaces.First());
            }
            // 注册多个接口
            else if (injectionAttribute.InjectionScope is InjectionScopeOptions.ImplementedInterfaces or InjectionScopeOptions.All)
            {
                foreach (var inter in canInjectInterfaces)
                {
                    RegisterType(services, registerType, type, injectionAttribute, inter);
                }
            }
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="inter">接口</param>
        private static void RegisterType(IServiceCollection services, RegisterType registerType, Type type, InjectionAttribute injectionAttribute, Type inter = null)
        {
            if (registerType == Fur.DependencyInjection.RegisterType.Transient) RegisterTransientType(services, type, injectionAttribute, inter);
            if (registerType == Fur.DependencyInjection.RegisterType.Scoped) RegisterScopeType(services, type, injectionAttribute, inter);
            if (registerType == Fur.DependencyInjection.RegisterType.Singleton) RegisterSingletonType(services, type, injectionAttribute, inter);
        }

        /// <summary>
        /// 注册瞬时接口实例类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="inter">接口</param>
        private static void RegisterTransientType(IServiceCollection services, Type type, InjectionAttribute injectionAttribute, Type inter = null)
        {
            switch (injectionAttribute.Injection)
            {
                case InjectionOptions.TryAdd:
                    if (inter == null) services.TryAddTransient(type);
                    else services.TryAddTransient(inter, type);
                    break;

                case InjectionOptions.Add:
                    if (inter == null) services.AddTransient(type);
                    else services.AddTransient(inter, type);
                    break;

                default: break;
            }
        }

        /// <summary>
        /// 注册作用域接口实例类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="inter">接口</param>
        private static void RegisterScopeType(IServiceCollection services, Type type, InjectionAttribute injectionAttribute, Type inter = null)
        {
            switch (injectionAttribute.Injection)
            {
                case InjectionOptions.TryAdd:
                    if (inter == null) services.TryAddScoped(type);
                    else services.TryAddScoped(inter, type);
                    break;

                case InjectionOptions.Add:
                    if (inter == null) services.AddScoped(type);
                    else services.AddScoped(inter, type);
                    break;

                default: break;
            }
        }

        /// <summary>
        /// 注册单例接口实例类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="type">类型</param>
        /// <param name="injectionAttribute">注入特性</param>
        /// <param name="inter">接口</param>
        private static void RegisterSingletonType(IServiceCollection services, Type type, InjectionAttribute injectionAttribute, Type inter = null)
        {
            switch (injectionAttribute.Injection)
            {
                case InjectionOptions.TryAdd:
                    if (inter == null) services.TryAddSingleton(type);
                    else services.TryAddSingleton(inter, type);
                    break;

                case InjectionOptions.Add:
                    if (inter == null) services.AddSingleton(type);
                    else services.AddSingleton(inter, type);
                    break;

                default: break;
            }
        }

        /// <summary>
        /// 获取 Startup 排序
        /// </summary>
        /// <param name="type">排序类型</param>
        /// <returns>int</returns>
        private static int GetOrder(Type type)
        {
            return !type.IsDefined(typeof(StartupAttribute), true)
                ? 0
                : type.GetCustomAttribute<StartupAttribute>(true).Order;
        }
    }
}