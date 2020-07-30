using Fur.MirrorController.Conventions;
using Fur.MirrorController.Providers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Fur.MirrorController.Extensions.MvcBuilders
{
    /// <summary>
    /// 镜面控制器Mvc构建器拓展
    /// </summary>
    public static class MirrorControllerMvcBuilderExtensions
    {
        /// <summary>
        /// 镜面控制器Mvc构建器拓展方法
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的Mvc构建器</returns>
        public static IMvcBuilder AddFurMirrorControllers(this IMvcBuilder mvcBuilder)
        {
            var partManager = mvcBuilder.Services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddFurMirrorControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            partManager.FeatureProviders.Add(new MirrorControllerFeatureProvider());
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Conventions.Add(new MirrorControllerModelConvention());
            });

            mvcBuilder.AddNewtonsoftJson();

            return mvcBuilder;
        }
    }
}