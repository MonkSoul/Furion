using Fur.ApplicationSystem;
using Fur.ObjectMapper.Mappers;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Fur.ObjectMapper.Extensions
{
    /// <summary>
    /// 对象映射拓展类
    /// </summary>
    public static class ObjectMapperServiceExtensions
    {
        #region 对象映射拓展方法 + public static IServiceCollection AddFurObjectMapper(this IServiceCollection services)

        /// <summary>
        /// 对象映射拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurObjectMapper(this IServiceCollection services)
        {
            services.AddScoped<IMapper, ServiceMapper>();

            TypeAdapterConfig.GlobalSettings.Scan(ApplicationGlobal.ApplicationInfo.Assemblies.Select(a => a.Assembly).ToArray());
            TypeAdapterConfig.GlobalSettings.Default
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                .PreserveReference(true);

            return services;
        }

        #endregion 对象映射拓展方法 + public static IServiceCollection AddFurObjectMapper(this IServiceCollection services)
    }
}