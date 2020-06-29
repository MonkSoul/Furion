using Fur.AttachController.Conventions;
using Fur.AttachController.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
        #region 附加控制器服务拓展方法 +/* public static IServiceCollection AddAttachControllers(this IServiceCollection services)
        /// <summary>
        /// 附加控制器服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddAttachControllers(this IServiceCollection services)
        {
            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddAttachControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            partManager.FeatureProviders.Add(new AttachControllerFeatureProvider());
            services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new AttachControllerModelConvention());
            });

            return services;
        }
        #endregion
    }
}
