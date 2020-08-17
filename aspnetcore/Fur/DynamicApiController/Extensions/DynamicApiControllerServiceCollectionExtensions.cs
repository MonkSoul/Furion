using Fur.DynamicApiController;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///动态接口控制器拓展类
    /// </summary>
    public static class DynamicApiControllerServiceCollectionExtensions
    {
        /// <summary>
        /// 添加动态接口控制器
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="providersOrConventions">控制器特性提供器或应用模型转换器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddDynamicApiControllers(this IMvcBuilder mvcBuilder, params object[] providersOrConventions)
        {
            var services = mvcBuilder.Services;
            // 添加配置
            services.AddAppOptions<DynamicApiControllerSettingsOptions>();

            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddDynamicApiControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            // 添加控制器特性提供器
            var providers = providersOrConventions.Where(u => typeof(IApplicationFeatureProvider).IsAssignableFrom(u.GetType()));
            if (!providers.Any()) partManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());
            else
            {
                foreach (var provider in providers)
                {
                    partManager.FeatureProviders.Add(provider as IApplicationFeatureProvider);
                }
            }

            // 添加应用模型转换器
            var conventions = providersOrConventions.Where(u => typeof(IApplicationModelConvention).IsAssignableFrom(u.GetType()));
            mvcBuilder.AddMvcOptions(options =>
            {
                if (!providers.Any()) options.Conventions.Add(new DynamicApiControllerApplicationModelConvention());
                else
                {
                    foreach (var convention in conventions)
                    {
                        options.Conventions.Add(convention as IApplicationModelConvention);
                    }
                }
            });

            // 支持 JArray/JObject
            mvcBuilder.AddNewtonsoftJson();

            return mvcBuilder;
        }
    }
}