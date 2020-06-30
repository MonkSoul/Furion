using Fur.ApplicationSystem.Models;
using Fur.SwaggerGen.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            var attactControllerOptions = configuration.GetSection($"{nameof(FurSettings)}:{nameof(SwaggerOptions)}");
            services.AddOptions<SwaggerOptions>().Bind(attactControllerOptions).ValidateDataAnnotations();

            SwaggerConfigure.SetSwaggerOptions(attactControllerOptions.Get<SwaggerOptions>());
            services.AddSwaggerGen(options => SwaggerConfigure.Initialize(options));

            return services;
        }
        #endregion
    }
}
