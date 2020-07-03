using Fur.ApplicationSystem.Models;
using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using Fur.DependencyInjection.Attributes;
using Fur.Extensions;
using Fur.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Fur.ApplicationSystem
{
    /// <summary>
    /// 程序启动预热类
    /// </summary>
    public sealed class ApplicationGlobal
    {
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public static ApplicationInfo ApplicationInfo = GetApplicationInfo();

        #region 获取类型的应用类型信息 +/*  public static ApplicationTypeInfo GetApplicationTypeInfo(Type type)
        /// <summary>
        /// 获取类型的应用类型信息
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <returns>应用类型信息对象</returns>
        public static ApplicationTypeInfo GetApplicationTypeInfo(Type type)
            => ApplicationInfo.PublicClassTypes.FirstOrDefault(u => u.Type == type);
        #endregion

        #region 获取特定类型的特定方法信息 +/* public static ApplicationMethodInfo GetApplicationMethodInfo(MethodInfo methodInfo)
        /// <summary>
        /// 获取特定类型的特定方法信息
        /// </summary>
        /// <param name="methodInfo">方法对象</param>
        /// <returns>应用方法信息</returns>
        public static ApplicationMethodInfo GetApplicationMethodInfo(MethodInfo methodInfo)
            => ApplicationInfo.PublicInstanceMethods.FirstOrDefault(u => u.Method == methodInfo);
        #endregion

        #region 获取类型指定特性 +/* public static TAttribute GetTypeAttribute<TAttribute>(Type type) where TAttribute : Attribute
        /// <summary>
        /// 获取类型指定特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="type">类型对象</param>
        /// <returns>特性对象</returns>
        public static TAttribute GetTypeAttribute<TAttribute>(Type type) where TAttribute : Attribute
            => GetApplicationTypeInfo(type).CustomAttributes.FirstOrDefault(u => u is TAttribute) as TAttribute;
        #endregion

        #region 获取类型指定特性 +/* public static TAttribute GetMethodAttribute<TAttribute>(MethodInfo methodInfo) where TAttribute : Attribute
        /// <summary>
        /// 获取类型指定特性
        /// </summary>
        /// <typeparam name="TAttribute">泛型特性</typeparam>
        /// <param name="methodInfo">方法对象</param>
        /// <returns>特性对象</returns>
        public static TAttribute GetMethodAttribute<TAttribute>(MethodInfo methodInfo) where TAttribute : Attribute
            => GetApplicationMethodInfo(methodInfo).CustomAttributes.FirstOrDefault(u => u is TAttribute) as TAttribute;
        #endregion

        #region 判断是否是控制器类型 +/* public static bool IsControllerType(TypeInfo typeInfo, bool exceptMvcController = false)
        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="typeInfo">类型对象</param>
        /// <param name="exceptMvcController">是否排除Mvc控制器类型</param>
        /// <returns>是或否</returns>
        public static bool IsControllerType(TypeInfo typeInfo, bool exceptMvcController = false)
        {
            // 1）必须是公开非抽象类、非泛型类、非接口类型
            if (!typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType || typeInfo.IsInterface) return false;

            // 2）判断是否是控制器类型，且 [ApiExplorerSettings].IgnoreApi!=true
            if (!exceptMvcController)
            {
                var apiExplorerSettingsAttribute = typeInfo.GetDeepAttribute<ApiExplorerSettingsAttribute>();
                if (typeof(ControllerBase).IsAssignableFrom(typeInfo) && (apiExplorerSettingsAttribute == null || apiExplorerSettingsAttribute.IgnoreApi != true)) return true;
            }

            // 3）是否是附加控制器类型，且 [AttachController].Attach!=false，且继承 IAttachControllerDependency 接口
            var attachControllerAttribute = typeInfo.GetDeepAttribute<AttachControllerAttribute>();
            if (attachControllerAttribute != null && attachControllerAttribute.Attach != false && typeof(IAttachControllerDependency).IsAssignableFrom(typeInfo)) return true;

            return false;
        }
        #endregion

        #region 判断是否是控制器类型 +/*  public static bool IsControllerType(Type type, bool exceptMvcController = false)
        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <param name="exceptMvcController">是否排除Mvc控制器类型</param>
        /// <returns>是或否</returns>
        public static bool IsControllerType(Type type, bool exceptMvcController = false)
            => IsControllerType(type.GetTypeInfo(), exceptMvcController);
        #endregion

        #region 判断是否是控制器Action类型 +/* public static bool IsControllerActionType(MethodInfo methodInfo)
        /// <summary>
        /// 判断是否是控制器Action类型
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static bool IsControllerActionType(MethodInfo methodInfo)
        {
            // 1）方法所在类必须是一个控制器类型
            if (!IsControllerType(methodInfo.DeclaringType)) return false;

            // 2）必须是公开的，非抽象类，非静态方法
            if (!methodInfo.IsPublic || methodInfo.IsAbstract || methodInfo.IsStatic) return false;

            // 3）定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (methodInfo.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && methodInfo.GetCustomAttribute<ApiExplorerSettingsAttribute>().IgnoreApi) return false;

            return true;
        }
        #endregion

        #region 获取应用程序集，并且不包含Nuget下载 -/* private IEnumerable<Assembly> GetApplicationAssembliesWithoutNuget(string prefix = nameof(Fur))
        /// <summary>
        /// 获取应用程序集，并且不包含Nuget下载
        /// </summary>
        /// <param name="prefix">程序集前缀</param>
        /// <returns>程序集集合</returns>
        internal static IEnumerable<Assembly> GetApplicationAssembliesWithoutNuget(string prefix = nameof(Fur))
        {
            var dependencyConext = DependencyContext.Default;
            return dependencyConext.CompileLibraries
                .Where(u => !u.Serviceable && u.Type != "package")
                .WhereIf(prefix.HasValue(), u => u.Name.StartsWith(prefix))
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));
        }
        #endregion

        #region 获取应用信息类 -/* private static ApplicationInfo GetApplicationInfo()
        /// <summary>
        /// 获取应用信息类
        /// </summary>
        /// <returns>应用信息类</returns>
        private static ApplicationInfo GetApplicationInfo()
        {
            var applicationAssemblies = GetApplicationAssembliesWithoutNuget();
            var applicationInfo = new ApplicationInfo
            {
                Assemblies = applicationAssemblies.Select(a => new ApplicationAssemblyInfo()
                {
                    Assembly = a,
                    Name = a.GetName().Name,
                    FullName = a.FullName,
                    PublicClassTypes = a.GetTypes().Where(t => !t.IsInterface && t.IsPublic && !t.IsDefined(typeof(NotInjectAttribute))).Select(t => new ApplicationTypeInfo()
                    {
                        Assembly = a,
                        Type = t,
                        IsGenericType = t.IsGenericType,
                        IsControllerType = IsControllerType(t),
                        GenericArguments = t.IsGenericType ? t.GetGenericArguments() : null,
                        CustomAttributes = t.GetCustomAttributes(),
                        SwaggerGroups = IsControllerType(t) ? (t.GetCustomAttribute<AttachControllerAttribute>()?.SwaggerGroups ?? new string[] { "Default" }) : null,
                        IsStaticType = (t.IsAbstract && t.IsSealed),
                        CanNewType = !t.IsAbstract,
                        PublicInstanceMethods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).Where(m => m.DeclaringType == t && !m.IsDefined(typeof(NotInjectAttribute))).Select(m => new ApplicationMethodInfo()
                        {
                            Assembly = a,
                            DeclareType = t,
                            Method = m,
                            CustomAttributes = m.GetCustomAttributes(),
                            ReturnType = m.ReturnType,
                            IsControllerActionType = IsControllerActionType(m),
                            SwaggerGroups = IsControllerActionType(m) ? (m.GetCustomAttribute<AttachActionAttribute>()?.SwaggerGroups ?? (t.GetCustomAttribute<AttachControllerAttribute>()?.SwaggerGroups ?? new string[] { "Default" }) ?? new string[] { "Default" }) : null,
                            Parameters = m.GetParameters().Where(p => !p.IsDefined(typeof(NotInjectAttribute))).Select(p => new ApplicationParameterInfo()
                            {
                                Assembly = a,
                                DeclareType = t,
                                Method = m,
                                Name = p.Name,
                                Type = p.ParameterType,
                                CustomAttributes = p.GetCustomAttributes()
                            }),
                            IsStaticType = m.IsStatic,
                        })
                    })
                })
            };
            applicationInfo.PublicClassTypes = applicationInfo.Assemblies.SelectMany(u => u.PublicClassTypes);
            applicationInfo.PublicInstanceMethods = applicationInfo.PublicClassTypes.SelectMany(u => u.PublicInstanceMethods);
            return applicationInfo;
        }
        #endregion
    }
}
