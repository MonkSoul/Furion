using Fur.ApplicationSystem.Models;
using Fur.SwaggerGen.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling.Storage;
using System;

namespace Fur.SwaggerGen.Extensions
{
    /// <summary>
    /// Swagger 服务拓展类
    /// </summary>
    public static class SwaggerGenServiceExtensions
    {
        #region Swagger 服务拓展方法 +/* public static IServiceCollection AddFurSwaggerGen(this IServiceCollection services, IConfiguration configuration)

        /// <summary>
        /// Swagger 服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurSwaggerGen(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerOptionsConfiguration = configuration.GetSection($"{nameof(FurSettings)}:{nameof(SwaggerOptions)}");
            services.AddOptions<SwaggerOptions>().Bind(swaggerOptionsConfiguration).ValidateDataAnnotations();

            var swaggerOptions = swaggerOptionsConfiguration.Get<SwaggerOptions>();
            SwaggerConfigure.SetSwaggerOptions(swaggerOptions);
            services.AddSwaggerGen(options => SwaggerConfigure.Initialize(options));

            if (swaggerOptions.EnableMiniProfiler)
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";
                    (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
                })
                    .AddEntityFramework();
            }

            return services;
        }

        #endregion Swagger 服务拓展方法 +/* public static IServiceCollection AddFurSwaggerGen(this IServiceCollection services, IConfiguration configuration)
    }
}