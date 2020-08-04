using Fur;

using Mapster;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 对象映射拓展类
    /// </summary>

    public static class MapsterServiceCollectionExtensions
    {
        /// <summary>
        /// 对象映射拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurObjectMapper(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(App.Assemblies.ToArray());
            TypeAdapterConfig.GlobalSettings.Default
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                .PreserveReference(true);

            return services;
        }
    }
}