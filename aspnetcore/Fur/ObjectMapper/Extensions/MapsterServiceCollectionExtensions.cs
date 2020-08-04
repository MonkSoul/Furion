using Fur.AppCore;
using Fur.AppCore.Attributes;
using Mapster;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 对象映射拓展类
    /// </summary>
    [NonInflated]
    public static class MapsterServiceCollectionExtensions
    {
        /// <summary>
        /// 对象映射拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurObjectMapper(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(App.Inflations.Assemblies.Select(a => a.ThisAssembly).ToArray());
            TypeAdapterConfig.GlobalSettings.Default
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                .PreserveReference(true);

            return services;
        }
    }
}