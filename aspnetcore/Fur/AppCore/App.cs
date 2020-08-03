using Fur.AppCore.Attributes;
using Fur.AppCore.Inflations;
using Fur.AppCore.Options;
using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Entities;
using Fur.DatabaseAccessor.Entities.Configurations;
using Fur.DatabaseAccessor.MultipleTenants.Entities;
using Fur.DatabaseAccessor.MultipleTenants.Options;
using Fur.Linq.Extensions;
using Fur.MirrorController.Attributes;
using Fur.MirrorController.Dependencies;
using Fur.TypeExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Fur.AppCore
{
    /// <summary>
    /// 应用核心类
    /// </summary>
    [NonInflated]
    public static class App
    {
        /// <summary>
        /// Fur 框架配置选项
        /// </summary>
        public static AppOptions AppOptions;

        /// <summary>
        /// 应用包装器
        /// </summary>
        internal static ApplicationInflation Inflations;

        /// <summary>
        /// 多租户配置选项
        /// </summary>
        internal static FurMultipleTenantOptions MultipleTenantOptions { get; set; } = FurMultipleTenantOptions.None;

        /// <summary>
        /// 是否支持多租户
        /// </summary>
        internal static bool SupportedMultipleTenant { get; set; } = false;

        /// <summary>
        /// 是否支持性能分析
        /// </summary>
        internal static bool SupportedMiniProfiler { get; set; } = false;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static App()
        {
            Inflations ??= GetApplicationInflations();
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
        /// 获取应用解决方案中所有的包装器集合
        /// </summary>
        /// <returns><see cref="Inflations"/></returns>
        private static ApplicationInflation GetApplicationInflations()
        {
            // 避免重复读取
            if (Inflations != null) return Inflations;

            var applicationAssemblies = GetApplicationAssembliesWithoutNuget();

            // 组装应用包装器
            var applicationWrapper = new ApplicationInflation
            {
                // 创建程序集包装器
                Assemblies = applicationAssemblies
                .Select(a => new AssemblyInflation()
                {
                    ThisAssembly = a,
                    Name = a.GetName().Name,
                    FullName = a.FullName,

                    // 创建类型包装器
                    SubClassTypes = a.GetTypes()
                    .Where(t => t.IsPublic && !t.IsInterface && !t.IsEnum && !t.IsDefined(typeof(NonInflatedAttribute), false))
                    .Select(t => new TypeInflation()
                    {
                        ThisAssembly = a,
                        ThisType = t,
                        Name = t.Name,
                        FullName = t.FullName,
                        IsGenericType = t.IsGenericType,
                        IsControllerType = IsControllerType(t),
                        GenericArgumentTypes = t.IsGenericType ? t.GetGenericArguments() : null,
                        CustomAttributes = t.GetCustomAttributes(),
                        SwaggerGroups = GetControllerTypeSwaggerGroups(t),
                        IsStaticType = t.IsAbstract && t.IsSealed,
                        CanBeNew = !t.IsAbstract,
                        IsDbEntityRelevanceType = !t.IsAbstract && (typeof(IDbEntityBase).IsAssignableFrom(t) || typeof(IDbEntityConfigure).IsAssignableFrom(t)),
                        IsTenantType = t == typeof(Tenant),

                        // 创建属性包装器
                        SubPropertis = t.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                        .Where(p => p.DeclaringType == t && !p.IsDefined(typeof(NonInflatedAttribute), false))
                        .Select(p => new PropertyInflation()
                        {
                            Name = p.Name,
                            ThisAssembly = a,
                            ThisDeclareType = t,
                            PropertyType = p.PropertyType,
                            CustomAttributes = p.GetCustomAttributes()
                        }),

                        // 创建方法包装器
                        SubMethods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)
                        .Where(m => m.DeclaringType == t && !m.IsDefined(typeof(NonInflatedAttribute), false))
                        .Select(m => new MethodInflation()
                        {
                            ThisAssembly = a,
                            ThisDeclareType = t,
                            ThisMethod = m,
                            Name = m.Name,
                            CustomAttributes = m.GetCustomAttributes(),
                            ReturnType = m.ReturnType,
                            IsControllerActionMethod = IsControllerActionMethod(m),
                            SwaggerGroups = GetControllerActionSwaggerGroups(m),
                            IsStaticMethod = m.IsStatic,
                            IsDbFunctionMethod = t.IsAbstract && t.IsSealed && m.IsStatic && m.IsDefined(typeof(DbFunctionAttribute), false),
                            SubParameters = m.GetParameters(),
                        })
                    })
                })
            };

            // 读取所有程序集下的公开类型包装器集合
            applicationWrapper.ClassTypes = applicationWrapper.Assemblies.SelectMany(u => u.SubClassTypes);

            // 读取所有程序集下的所有方法包装器集合
            applicationWrapper.Methods = applicationWrapper.ClassTypes.SelectMany(u => u.SubMethods);

            return applicationWrapper;
        }

        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="exceptControllerBase">是否排除 MVC <see cref="ControllerBase"/> 类型</param>
        /// <returns>bool</returns>
        internal static bool IsControllerType(Type type, bool exceptControllerBase = false)
        {
            // 必须是公开非抽象类、非泛型类、非接口类型
            if (!type.IsPublic || type.IsAbstract || type.IsGenericType || type.IsInterface || type.IsEnum) return false;

            // 判断是否是控制器类型，且 [ApiExplorerSettings].IgnoreApi!=true
            if (!exceptControllerBase)
            {
                var apiExplorerSettingsAttribute = type.GetDeepAttribute<ApiExplorerSettingsAttribute>();
                if (typeof(ControllerBase).IsAssignableFrom(type) && (apiExplorerSettingsAttribute == null || apiExplorerSettingsAttribute.IgnoreApi != true)) return true;
            }

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (type.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && type.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            // 是否是镜面控制器类型，继承 IAttachControllerDependency
            if (typeof(IMirrorControllerModel).IsAssignableFrom(type) && (!type.IsDefined(typeof(MirrorControllerAttribute), true) || type.GetDeepAttribute<MirrorControllerAttribute>().Enabled != false)) return true;

            return false;
        }

        /// <summary>
        /// 判断是否是控制器 Action 方法
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>bool</returns>
        internal static bool IsControllerActionMethod(MethodInfo method)
        {
            // 方法所在类必须是一个控制器类型
            if (!IsControllerType(method.DeclaringType)) return false;

            // 必须是公开的，非抽象类，非静态方法，非泛型方法
            if (!method.IsPublic || method.IsAbstract || method.IsStatic || method.IsGenericMethod) return false;

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (method.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && method.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            return true;
        }

        /// <summary>
        /// 获取控制器类型 Swagger 接口文档分组
        /// </summary>
        /// <param name="controllerType">控制器类型</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerTypeSwaggerGroups(Type controllerType)
        {
            // 如果不是控制器类型，返回 null
            if (!IsControllerType(controllerType)) return default;

            var defaultSwaggerGroups = new string[] { "Default" };

            if (!controllerType.IsDefined(typeof(MirrorControllerAttribute), true))
                return defaultSwaggerGroups;

            var mirrorControllerAttribute = controllerType.GetDeepAttribute<MirrorControllerAttribute>();
            if (mirrorControllerAttribute.SwaggerGroups == null || !mirrorControllerAttribute.SwaggerGroups.Any())
                return defaultSwaggerGroups;

            return mirrorControllerAttribute.SwaggerGroups;
        }

        /// <summary>
        /// 获取控制器 Action Swagger 接口文档分组
        /// </summary>
        /// <param name="controllerAction">控制器Action</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerActionSwaggerGroups(MethodInfo controllerAction)
        {
            // 如果不是控制器Action类型，返回 null
            if (!IsControllerActionMethod(controllerAction)) return default;

            if (!controllerAction.IsDefined(typeof(MirrorActionAttribute), true))
                return GetControllerTypeSwaggerGroups(controllerAction.DeclaringType);

            var attachActionAttribute = controllerAction.GetCustomAttribute<MirrorActionAttribute>();
            if (attachActionAttribute.SwaggerGroups == null || !attachActionAttribute.SwaggerGroups.Any())
                return GetControllerTypeSwaggerGroups(controllerAction.DeclaringType);

            return attachActionAttribute.SwaggerGroups;
        }
    }
}