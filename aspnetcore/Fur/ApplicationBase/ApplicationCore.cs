using Fur.ApplicationBase.Attributes;
using Fur.ApplicationBase.Wrappers;
using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using Fur.Extensions;
using Fur.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Fur.ApplicationBase
{
    /// <summary>
    /// 应用核心类
    /// <code>static</code>
    /// <para>功能列表：</para>
    /// <list type="number">
    /// <item>
    /// <description>启动时扫描解决方案中所有程序集并创建对应的包装类对象</description>
    /// </item>
    /// <item>
    /// <description>定义解决方案全局可调用的公共属性或方法</description>
    /// </item>
    /// </list>
    /// </summary>
    public static class ApplicationCore
    {
        /// <summary>
        /// 应用包装器
        /// </summary>
        public static ApplicationWrapper ApplicationWrapper = GetApplicationWrappers();

        #region 获取类型的包装类型 + public static TypeWrapper GetTypeWrapper(Type type)
        /// <summary>
        /// 获取类型的包装类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns><see cref="TypeWrapper"/></returns>
        public static TypeWrapper GetTypeWrapper(Type type)
            => ApplicationWrapper.PublicClassTypeWrappers.FirstOrDefault(u => u.Type == type);
        #endregion

        #region 获取方法的包装类型 + public static MethodWrapper GetMethodWrapper(MethodInfo method)
        /// <summary>
        /// 获取方法的包装类型
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns><see cref="MethodWrapper"/></returns>
        public static MethodWrapper GetMethodWrapper(MethodInfo method)
            => ApplicationWrapper.PublicMethodWrappers.FirstOrDefault(u => u.Method == method);
        #endregion

        #region 获取公开类型自定义特性 + public static TAttribute GetPublicClassTypeCustomAttribute<TAttribute>(Type type) where TAttribute : Attribute
        /// <summary>
        /// 获取公开类型自定义特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>{TAttribute}</returns>
        public static TAttribute GetPublicClassTypeCustomAttribute<TAttribute>(Type type) where TAttribute : Attribute
            => GetTypeWrapper(type).CustomAttributes.FirstOrDefault(u => u is TAttribute) as TAttribute;
        #endregion

        #region 获取公开方法自定义特性 + public static TAttribute GetPublicMethodCustomAttribute<TAttribute>(MethodInfo method) where TAttribute : Attribute
        /// <summary>
        /// 获取公开方法自定义特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="method">方法</param>
        /// <returns>{TAttribute}</returns>
        public static TAttribute GetPublicMethodCustomAttribute<TAttribute>(MethodInfo method) where TAttribute : Attribute
            => GetMethodWrapper(method).CustomAttributes.FirstOrDefault(u => u is TAttribute) as TAttribute;
        #endregion


        #region 判断是否是控制器类型 + internal static bool IsControllerType(TypeInfo typeInfo, bool exceptControllerBase = false)
        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="typeInfo">类型</param>
        /// <param name="exceptControllerBase">是否排除 MVC <see cref="ControllerBase"/> 类型</param>
        /// <returns>bool</returns>
        internal static bool IsControllerType(TypeInfo typeInfo, bool exceptControllerBase = false)
        {
            // 必须是公开非抽象类、非泛型类、非接口类型
            if (!typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType || typeInfo.IsInterface) return false;

            // 判断是否是控制器类型，且 [ApiExplorerSettings].IgnoreApi!=true
            if (!exceptControllerBase)
            {
                var apiExplorerSettingsAttribute = typeInfo.GetDeepAttribute<ApiExplorerSettingsAttribute>();
                if (typeof(ControllerBase).IsAssignableFrom(typeInfo) && (apiExplorerSettingsAttribute == null || apiExplorerSettingsAttribute.IgnoreApi != true)) return true;
            }

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (typeInfo.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && typeInfo.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            // 是否是附加控制器类型，且 [AttachController].Attach!=false，且继承 IAttachControllerDependency 接口
            var attachControllerAttribute = typeInfo.GetDeepAttribute<AttachControllerAttribute>();
            if (attachControllerAttribute != null && attachControllerAttribute.Attach != false && typeof(IAttachControllerDependency).IsAssignableFrom(typeInfo)) return true;

            return false;
        }
        #endregion

        #region 判断是否是控制器类型 + internal static bool IsControllerType(Type type, bool exceptControllerBase = false)
        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="exceptControllerBase">除 MVC <see cref="ControllerBase"/> 类型</param>
        /// <returns>bool</returns>
        internal static bool IsControllerType(Type type, bool exceptControllerBase = false)
            => IsControllerType(type.GetTypeInfo(), exceptControllerBase);
        #endregion

        #region 判断是否是控制器 Action 类型 + internal static bool IsControllerActionType(MethodInfo method)
        /// <summary>
        /// 判断是否是控制器 Action 类型
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>bool</returns>
        internal static bool IsControllerActionType(MethodInfo method)
        {
            // 方法所在类必须是一个控制器类型
            if (!IsControllerType(method.DeclaringType)) return false;

            // 必须是公开的，非抽象类，非静态方法
            if (!method.IsPublic || method.IsAbstract || method.IsStatic) return false;

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (method.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && method.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            return true;
        }
        #endregion


        #region 获取应用所有程序集 + private static IEnumerable<Assembly> GetApplicationAssembliesWithoutNuget(string namespacePrefix = nameof(Fur))
        /// <summary>
        /// 获取应用所有程序集
        /// <para>不包括Nuget/MyGet等第三方安装的包</para>
        /// </summary>
        /// <param name="namespacePrefix">命名空间前缀，默认为：<see cref="Fur"/></param>
        /// <returns> IEnumerable<Assembly></returns>
        private static IEnumerable<Assembly> GetApplicationAssembliesWithoutNuget(string namespacePrefix = nameof(Fur))
        {
            var dependencyConext = DependencyContext.Default;

            return dependencyConext.CompileLibraries
                .Where(u => !u.Serviceable && u.Type != "package")
                .WhereIf(namespacePrefix.HasValue(), u => u.Name.StartsWith(namespacePrefix))
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));
        }
        #endregion

        #region 获取应用解决方案中所有的包装器集合 + private static ApplicationWrapper GetApplicationWrappers()
        /// <summary>
        /// 获取应用解决方案中所有的包装器集合
        /// </summary>
        /// <returns><see cref="ApplicationWrapper"/></returns>
        private static ApplicationWrapper GetApplicationWrappers()
        {
            // 避免重复读取
            if (ApplicationWrapper != null) return ApplicationWrapper;

            var applicationAssemblies = GetApplicationAssembliesWithoutNuget();

            // 组装应用包装器
            var applicationWrapper = new ApplicationWrapper
            {
                // 创建程序集包装器
                AssemblyWrappers = applicationAssemblies
                .Select(a => new AssemblyWrapper()
                {
                    Assembly = a,
                    Name = a.GetName().Name,
                    FullName = a.FullName,

                    // 创建类型包装器
                    PublicClassTypes = a.GetTypes()
                    .Where(t => !t.IsInterface && t.IsPublic && !t.IsDefined(typeof(NonWrapperAttribute)))
                    .Select(t => new TypeWrapper()
                    {
                        ThisAssembly = a,
                        Type = t,
                        IsGenericType = t.IsGenericType,
                        IsControllerType = IsControllerType(t),
                        GenericArgumentTypes = t.IsGenericType ? t.GetGenericArguments() : null,
                        CustomAttributes = t.GetCustomAttributes(),
                        SwaggerGroups = GetControllerTypeSwaggerGroups(t),
                        IsStaticType = (t.IsAbstract && t.IsSealed),
                        CanBeNew = !t.IsAbstract,

                        // 创建包装属性器
                        PublicPropertis = t.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                        .Where(p => p.DeclaringType == t && !p.IsDefined(typeof(NonWrapperAttribute)))
                        .Select(p => new PropertyWrapper()
                        {
                            Name = p.Name,
                            ThisAssembly = a,
                            ThisDeclareType = t,
                            Type = p.PropertyType,
                            CustomAttributes = p.GetCustomAttributes()
                        }),

                        // 创建方法包装器
                        PublicMethods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)
                        .Where(m => m.DeclaringType == t && !m.IsDefined(typeof(NonWrapperAttribute)))
                        .Select(m => new MethodWrapper()
                        {
                            ThisAssembly = a,
                            ThisDeclareType = t,
                            Method = m,
                            Name = m.Name,
                            CustomAttributes = m.GetCustomAttributes(),
                            ReturnType = m.ReturnType,
                            IsControllerActionType = IsControllerActionType(m),
                            SwaggerGroups = GetControllerActionSwaggerGroups(m),

                            // 创建参数包装器
                            Parameters = m.GetParameters()
                            .Where(p => !p.IsDefined(typeof(NonWrapperAttribute)))
                            .Select(p => new ParameterWrapper()
                            {
                                ThisAssembly = a,
                                ThisDeclareType = t,
                                ThisMethod = m,
                                Name = p.Name,
                                Type = p.ParameterType,
                                CustomAttributes = p.GetCustomAttributes()
                            }),
                            IsStaticMethod = m.IsStatic,
                        })
                    })
                })
            };

            // 读取所有程序集下的公开类型包装器集合
            applicationWrapper.PublicClassTypeWrappers = applicationWrapper.AssemblyWrappers.SelectMany(u => u.PublicClassTypes);

            // 读取所有程序集下的所有方法包装器集合
            applicationWrapper.PublicMethodWrappers = applicationWrapper.PublicClassTypeWrappers.SelectMany(u => u.PublicMethods);

            return applicationWrapper;
        }
        #endregion

        #region 获取控制器类型 Swagger 接口文档分组 + private static string[] GetControllerTypeSwaggerGroups(Type controllerType)
        /// <summary>
        /// 获取控制器类型 Swagger 接口文档分组
        /// </summary>
        /// <param name="controllerType">控制器类型</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerTypeSwaggerGroups(Type controllerType)
        {
            // 如果不是控制器类型，返回 null
            if (!IsControllerType(controllerType)) return null;

            var defaultSwaggerGroups = new string[] { "Default" };

            if (!controllerType.IsDefined(typeof(AttachControllerAttribute), true)) return defaultSwaggerGroups;

            var attachControllerAttribute = controllerType.GetDeepAttribute<AttachControllerAttribute>();
            if (attachControllerAttribute.SwaggerGroups == null || !attachControllerAttribute.SwaggerGroups.Any()) return defaultSwaggerGroups;

            return attachControllerAttribute.SwaggerGroups;
        }
        #endregion

        #region 获取控制器 Action Swagger 接口文档分组 + private static string[] GetControllerActionSwaggerGroups(MethodInfo controllerAction)
        /// <summary>
        /// 获取控制器 Action Swagger 接口文档分组
        /// </summary>
        /// <param name="controllerAction">控制器Action</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerActionSwaggerGroups(MethodInfo controllerAction)
        {
            // 如果不是控制器Action类型，返回 null
            if (!IsControllerActionType(controllerAction)) return null;

            if (!controllerAction.IsDefined(typeof(AttachActionAttribute), true)) return GetControllerTypeSwaggerGroups(controllerAction.DeclaringType);

            var attachActionAttribute = controllerAction.GetCustomAttribute<AttachActionAttribute>();
            if (attachActionAttribute.SwaggerGroups == null || !attachActionAttribute.SwaggerGroups.Any()) return GetControllerTypeSwaggerGroups(controllerAction.DeclaringType);

            return attachActionAttribute.SwaggerGroups;
        }
        #endregion
    }
}