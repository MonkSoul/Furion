using Fur.Linq;
using Fur.Models.ApplicationSystem;
using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Fur.ApplicationSystem
{
    /// <summary>
    /// 程序启动预热类
    /// </summary>
    public sealed class ApplicationPrepare
    {
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public static IEnumerable<ApplicationAssemblyInfo> ApplicationAssemblies = GetApplicationAssemblyInfos();

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
                    GenericArguments = t.IsGenericType ? t.GetGenericArguments() : null,
                    CustomAttributes = t.GetCustomAttributes(),
                    PublicInstanceMethods = t.GetMethods().Where(m => m.DeclaringType == t).Select(m => new ApplicationMethodInfo()
                    {
                        CustomAttributes = m.GetCustomAttributes(),
                        Method = m
                    })
                })
            });
        }
        #endregion
    }
}
