using Fur.ApplicationSystem.Models;
using Fur.AttachController.Conventions;
using Fur.AttachController.Options;
using Fur.AttachController.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Fur.AttachController.Extensions
{
    /// <summary>
    /// 附加控制器服务拓展
    /// </summary>
    public static class AttachControllerServiceExtensions
    {
        #region 附加控制器服务拓展方法 +/* public static IServiceCollection AddFurAttachControllers(this IServiceCollection services)

        /// <summary>
        /// 附加控制器服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurAttachControllers(this IServiceCollection services, IConfiguration configuration)
        {
            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddFurAttachControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            var attactControllerOptions = configuration.GetSection($"{nameof(FurSettings)}:{nameof(AttactControllerOptions)}");
            services.AddOptions<AttactControllerOptions>().Bind(attactControllerOptions).ValidateDataAnnotations();

            partManager.FeatureProviders.Add(new AttachControllerFeatureProvider());
            services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new AttachControllerModelConvention(attactControllerOptions.Get<AttactControllerOptions>()));
            });

            return services;
        }

        #endregion 附加控制器服务拓展方法 +/* public static IServiceCollection AddFurAttachControllers(this IServiceCollection services)
    }
}