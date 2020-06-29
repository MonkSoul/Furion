using Fur.ApplicationSystem.Models;
using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using Fur.Extensions;
using Fur.Linq;
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
        public static IEnumerable<ApplicationAssemblyInfo> ApplicationAssemblies = GetApplicationAssemblyInfos();

        #region 判断是否是控制器类型 +/* public static bool IsControllerType(TypeInfo typeInfo)
        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="typeInfo">类型对象</param>
        /// <returns>是或否</returns>
        public static bool IsControllerType(TypeInfo typeInfo)
        {
            // 1）不能是非公开、抽象类、泛型类、接口
            if (!typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType || typeInfo.IsInterface) return false;

            // 2）必须贴 [AttachController] 特性，且 [AttachController].Attach != false，且需继承 IAttachControllerDependency 接口
            var attachControllerAttribute = typeInfo.GetDeepAttribute<AttachControllerAttribute>();
            if (attachControllerAttribute == null || attachControllerAttribute.Attach == false || !typeof(IAttachControllerDependency).IsAssignableFrom(typeInfo.AsType())) return false;

            // 3）贴了 [ApiExplorerSettings] 特性，且 [ApiExplorerSettings].IgnoreApi != true
            var apiExplorerSettingsAttribute = typeInfo.GetDeepAttribute<ApiExplorerSettingsAttribute>();
            if (apiExplorerSettingsAttribute != null && apiExplorerSettingsAttribute.IgnoreApi == true) return false;

            return true;
        }
        #endregion

        #region 判断是否是控制器类型 +/* public static bool IsControllerType(Type type)
        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <returns>是或否</returns>
        public static bool IsControllerType(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            // 1）不能是非公开、抽象类、泛型类、接口
            if (!typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType || typeInfo.IsInterface) return false;

            // 2）必须贴 [AttachController] 特性，且 [AttachController].Attach != false
            var attachControllerAttribute = typeInfo.GetDeepAttribute<AttachControllerAttribute>();
            if (attachControllerAttribute == null || attachControllerAttribute.Attach == false) return false;

            // 3）贴了 [ApiExplorerSettings] 特性，且 [ApiExplorerSettings].IgnoreApi != true
            var apiExplorerSettingsAttribute = typeInfo.GetDeepAttribute<ApiExplorerSettingsAttribute>();
            if (apiExplorerSettingsAttribute != null && apiExplorerSettingsAttribute.IgnoreApi == true) return false;

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
                .WhereIf(!string.IsNullOrEmpty(prefix), u => u.Name.StartsWith(prefix))
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));
        }
        #endregion

        #region 获取应用程序集信息 -/* private static IEnumerable<ApplicationAssemblyInfo> GetApplicationAssemblyInfos()
        /// <summary>
        /// 获取应用程序集信息
        /// </summary>
        /// <returns>程序集集合对象</returns>
        private static IEnumerable<ApplicationAssemblyInfo> GetApplicationAssemblyInfos()
        {
            var applicationAssemblies = GetApplicationAssembliesWithoutNuget();
            return applicationAssemblies.Select(a => new ApplicationAssemblyInfo()
            {
                Assembly = a,
                Name = a.GetName().Name,
                FullName = a.FullName,
                PublicClassTypes = a.GetTypes().Where(t => !t.IsInterface && !t.IsAbstract && t.IsPublic).Select(t => new ApplicationTypeInfo()
                {
                    Type = t,
                    IsGenericType = t.IsGenericType,
                    IsControllerType = IsControllerType(t),
                    GenericArguments = t.IsGenericType ? t.GetGenericArguments() : null,
                    CustomAttributes = t.GetCustomAttributes(),
                    PublicInstanceMethods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => m.DeclaringType == t).Select(m => new ApplicationMethodInfo()
                    {
                        CustomAttributes = m.GetCustomAttributes(),
                        Method = m,
                        ReturnType = m.ReturnType,
                        Parameters = m.GetParameters().Select(p => new ApplicationParameterInfo()
                        {
                            Name = p.Name,
                            Type = p.ParameterType,
                            CustomAttributes = p.GetCustomAttributes()
                        })
                    })
                })
            });
        }
        #endregion
    }
}
