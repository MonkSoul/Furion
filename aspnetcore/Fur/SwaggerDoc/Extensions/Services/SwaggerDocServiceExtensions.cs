using Fur.AppCore;
using Fur.AppCore.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.SwaggerDoc.Extensions.Services
{
    /// <summary>
    /// Swagger 服务拓展类
    /// </summary>
    [NonWrapper]
    public static class SwaggerDocServiceExtensions
    {
        /// <summary>
        /// Swagger 服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => SwaggerDocConfigure.Initialize(options));

            if (App.FurOptions.FurSwaggerDocOptions.EnableMiniProfiler)
            {
                App.SupportedMiniProfiler = true;

                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/fur-swagger-profiler";
                    // 这里需要配置权限
                })
                    .AddEntityFramework();
            }

            return services;
        }
    }
}