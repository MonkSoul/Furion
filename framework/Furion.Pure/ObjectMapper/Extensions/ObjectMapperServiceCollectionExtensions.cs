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
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 对象映射拓展类
    /// </summary>
    [SuppressSniffer]
    public static class ObjectMapperServiceCollectionExtensions
    {
        /// <summary>
        /// 添加对象映射
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns></returns>
        public static IServiceCollection AddObjectMapper(this IServiceCollection services)
        {
            // 判断是否安装了 Mapster 程序集
            var objectMapperAssembly = App.Assemblies.FirstOrDefault(u => u.GetName().Name.Equals(AppExtra.OBJECTMAPPER_MAPSTER));
            if (objectMapperAssembly != null)
            {
                // 加载 ObjectMapper 拓展类型和拓展方法
                var objectMapperServiceCollectionExtensionsType = objectMapperAssembly.GetType($"Microsoft.Extensions.DependencyInjection.ObjectMapperServiceCollectionExtensions");
                var addObjectMapperMethod = objectMapperServiceCollectionExtensionsType
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .First(u => u.Name == "AddObjectMapper");

                return addObjectMapperMethod.Invoke(null, new object[] { services, App.Assemblies.ToArray() }) as IServiceCollection;
            }

            return services;
        }
    }
}