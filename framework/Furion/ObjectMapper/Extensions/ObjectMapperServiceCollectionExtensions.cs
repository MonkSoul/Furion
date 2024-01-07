// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;
using Furion.Reflection;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 对象映射拓展类
/// </summary>
[SuppressSniffer]
public static class ObjectMapperServiceCollectionExtensions
{
    /// <summary>
    /// 对象映射程序集名称
    /// </summary>
    public const string ASSEMBLY_NAME = "Furion.Extras.ObjectMapper.Mapster";

    /// <summary>
    /// 添加对象映射
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns></returns>
    public static IServiceCollection AddObjectMapper(this IServiceCollection services)
    {
        // 判断是否安装了 Mapster 程序集
        var objectMapperAssembly = App.Assemblies.FirstOrDefault(u => u.GetName().Name.Equals(ASSEMBLY_NAME));
        if (objectMapperAssembly != null)
        {
            // 加载 ObjectMapper 拓展类型和拓展方法
            var objectMapperServiceCollectionExtensionsType = Reflect.GetType(objectMapperAssembly, $"Microsoft.Extensions.DependencyInjection.ObjectMapperServiceCollectionExtensions");
            var addObjectMapperMethod = objectMapperServiceCollectionExtensionsType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(u => u.Name == "AddObjectMapper");

            return addObjectMapperMethod.Invoke(null, new object[] { services, App.Assemblies.ToArray() }) as IServiceCollection;
        }

        return services;
    }
}