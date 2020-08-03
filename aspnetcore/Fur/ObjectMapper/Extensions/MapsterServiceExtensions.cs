using Fur.AppCore;
using Fur.AppCore.Attributes;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Fur.ObjectMapper.Extensions
{
    /// <summary>
    /// 对象映射拓展类
    /// </summary>
    [NonInflated]
    public static class MapsterServiceExtensions
    {
        /// <summary>
        /// 对象映射拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurObjectMapper(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(App.Wrapper.AssemblyWrappers.Select(a => a.ThisAssembly).ToArray());
            TypeAdapterConfig.GlobalSettings.Default
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                .PreserveReference(true);

            return services;
        }
    }
}