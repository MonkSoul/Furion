using Fur.ApplicationBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.SwaggerDoc.Extensions.Services
{
    /// <summary>
    /// Swagger 服务拓展类
    /// </summary>
    public static class SwaggerDocServiceExtensions
    {
        #region Swagger 服务拓展方法 + public static IServiceCollection AddFurSwaggerGen(this IServiceCollection services, IConfiguration configuration)

        /// <summary>
        /// Swagger 服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurSwaggerGen(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options => SwaggerDocConfigure.Initialize(options));

            if (AppGlobal.GlobalSettings.SwaggerDocOptions.EnableMiniProfiler)
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/fur-swagger-profiler";
                    // 这里需要配置权限
                })
                    .AddEntityFramework();
            }

            return services;
        }

        #endregion
    }
}