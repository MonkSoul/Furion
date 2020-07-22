using Fur.ApplicationBase;
using Fur.MirrorController.Conventions;
using Fur.MirrorController.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Fur.MirrorController.Extensions.Services
{
    /// <summary>
    /// 镜面控制器服务拓展
    /// </summary>
    public static class MirrorControllerServiceExtensions
    {
        #region 镜面控制器服务拓展方法 + public static IServiceCollection AddFurMirrorControllers(this IServiceCollection services, IConfiguration configuration)

        /// <summary>
        /// 镜面控制器服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurMirrorControllers(this IServiceCollection services, IConfiguration configuration)
        {
            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddFurMirrorControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            partManager.FeatureProviders.Add(new MirrorControllerFeatureProvider());
            services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new MirrorControllerModelConvention(ApplicationCore.GlobalSettings.MirrorControllerOptions));
            });

            return services;
        }

        #endregion 镜面控制器服务拓展方法 + public static IServiceCollection AddFurMirrorControllers(this IServiceCollection services, IConfiguration configuration)
    }
}