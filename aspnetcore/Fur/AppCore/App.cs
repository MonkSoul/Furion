using Fur.DatabaseAccessor.Options;
using Fur.Extensions;
using Fur.Linq.Extensions;
using Fur.MirrorController.Dependencies;
using Fur.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Fur
{
    /// <summary>
    /// 应用核心类
    /// </summary>
    public static class App
    {
        /// <summary>
        /// Fur 框架配置选项
        /// </summary>
        public static AppOptions Settings;

        /// <summary>
        /// 配置
        /// </summary>
        public static IConfiguration Configuration;

        /// <summary>
        /// 环境
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment;

        /// <summary>
        /// 应用包装器
        /// </summary>
        internal static IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 多租户配置选项
        /// </summary>
        internal static FurMultipleTenantOptions MultipleTenantOptions { get; set; }

        /// <summary>
        /// 是否支持多租户
        /// </summary>
        internal static bool SupportedMultipleTenant { get; set; }

        /// <summary>
        /// 是否支持性能分析
        /// </summary>
        internal static bool SupportedMiniProfiler { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        static App()
        {
            Assemblies = GetApplicationAssembliesWithoutNuget();
            MultipleTenantOptions = FurMultipleTenantOptions.None;
            SupportedMultipleTenant = false;
            SupportedMiniProfiler = false;

            _controllerTypeCache = new ConcurrentDictionary<Type, bool>();
            _controllerActionCache = new ConcurrentDictionary<MethodInfo, bool>();
        }

        /// <summary>
        /// 获取应用所有程序集
        /// </summary>
        /// <remarks>
        /// <para>只包含项目程序集或Fur官方发布的Nuget包</para>
        /// </remarks>
        /// <param name="namespacePrefix">命名空间前缀，默认为：<see cref="Fur"/></param>
        /// <returns> IEnumerable<Assembly></returns>
        private static IEnumerable<Assembly> GetApplicationAssembliesWithoutNuget(string namespacePrefix = nameof(Fur))
        {
            var dependencyConext = DependencyContext.Default;

            return dependencyConext.CompileLibraries
                .Where(u => !u.Serviceable && u.Name != "Fur.Database.Migrations" && (u.Type != "package" || u.Name.StartsWith(nameof(Fur))))
                .WhereIf(namespacePrefix.HasValue(), u => u.Name.StartsWith(namespacePrefix))
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));
        }

        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="exceptControllerBase">是否排除 MVC <see cref="ControllerBase"/> 类型</param>
        /// <returns>bool</returns>
        internal static bool IsControllerType(Type type, bool exceptControllerBase = false)
        {
            var isCached = _controllerTypeCache.TryGetValue(type, out bool isControllerType);
            if (isCached) return isControllerType;

            isControllerType = IsControllerTypeCore(type, exceptControllerBase);
            _controllerTypeCache.TryAdd(type, isControllerType);
            return isControllerType;
        }

        /// <summary>
        /// 控制器类型缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<Type, bool> _controllerTypeCache;

        /// <summary>
        /// 判断是否是控制器类型核心代码
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="exceptControllerBase">是否排除 MVC <see cref="ControllerBase"/> 类型</param>
        /// <returns>bool</returns>
        private static bool IsControllerTypeCore(Type type, bool exceptControllerBase = false)
        {
            // 必须是公开非抽象类、非泛型类、非接口类型
            if (!type.IsPublic || type.IsAbstract || type.IsGenericType || type.IsInterface || type.IsEnum) return false;

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (type.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && type.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            // 判断是否是控制器类型，且 [ApiExplorerSettings].IgnoreApi!=true
            if (!exceptControllerBase && typeof(ControllerBase).IsAssignableFrom(type)) return true;

            // 是否是镜面控制器类型，继承 IAttachControllerDependency
            if (typeof(IMirrorControllerModel).IsAssignableFrom(type)) return true;

            return false;
        }

        /// <summary>
        /// 判断是否是控制器 Action 方法
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>bool</returns>
        internal static bool IsControllerActionMethod(MethodInfo method)
        {
            var isCached = _controllerActionCache.TryGetValue(method, out bool isControllerAction);
            if (isCached) return isControllerAction;

            isControllerAction = IsControllerActionMethodCore(method);
            _controllerActionCache.TryAdd(method, isControllerAction);
            return isControllerAction;
        }

        /// <summary>
        /// 控制器Action缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<MethodInfo, bool> _controllerActionCache;

        /// <summary>
        /// 判断是否是控制器 Action 方法核心代码
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>bool</returns>
        private static bool IsControllerActionMethodCore(MethodInfo method)
        {
            // 方法所在类必须是一个控制器类型
            if (!IsControllerType(method.DeclaringType)) return false;

            // 必须是公开的，非抽象类，非静态方法，非泛型方法
            if (!method.IsPublic || method.IsAbstract || method.IsStatic || method.IsGenericMethod) return false;

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (method.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && method.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            return true;
        }
    }
}