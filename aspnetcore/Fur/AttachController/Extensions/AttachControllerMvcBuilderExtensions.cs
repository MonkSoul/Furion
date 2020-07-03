using Fur.ApplicationSystem.Models;
using Fur.AttachController.Conventions;
using Fur.AttachController.Options;
using Fur.AttachController.Providers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
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
        #region 附加控制器Mvc构建器拓展方法 +/* public static IMvcBuilder AddFurAttachControllers(this IMvcBuilder mvcBuilder)

        /// <summary>
        /// 附加控制器Mvc构建器拓展方法
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的Mvc构建器</returns>
        public static IMvcBuilder AddFurAttachControllers(this IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            var partManager = mvcBuilder.Services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddFurAttachControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            var attactControllerOptions = configuration.GetSection($"{nameof(FurSettings)}:{nameof(AttactControllerOptions)}");
            mvcBuilder.Services.AddOptions<AttactControllerOptions>().Bind(attactControllerOptions).ValidateDataAnnotations();

            partManager.FeatureProviders.Add(new AttachControllerFeatureProvider());
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Conventions.Add(new AttachControllerModelConvention(attactControllerOptions.Get<AttactControllerOptions>()));
            });

            mvcBuilder.AddNewtonsoftJson();

            return mvcBuilder;
        }

        #endregion 附加控制器Mvc构建器拓展方法 +/* public static IMvcBuilder AddFurAttachControllers(this IMvcBuilder mvcBuilder)
    }
}