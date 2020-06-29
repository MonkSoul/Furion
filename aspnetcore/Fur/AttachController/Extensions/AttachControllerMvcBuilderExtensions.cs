using Fur.AttachController.Conventions;
using Fur.AttachController.Providers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Fur.AttachController.Extensions
{
    /// <summary>
    /// 附加控制器Mvc构建器拓展
    /// </summary>
    public static class AttachControllerMvcBuilderExtensions
    {
        #region 附加控制器Mvc构建器拓展方法 +/* public static IMvcBuilder AddAttachControllers(this IMvcBuilder mvcBuilder)
        /// <summary>
        /// 附加控制器Mvc构建器拓展方法
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>新的Mvc构建器</returns>
        public static IMvcBuilder AddAttachControllers(this IMvcBuilder mvcBuilder)
        {
            var partManager = mvcBuilder.Services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddAttachControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            partManager.FeatureProviders.Add(new AttachControllerFeatureProvider());
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Conventions.Add(new AttachControllerModelConvention());
            });

            return mvcBuilder;
        }
        #endregion
    }
}
