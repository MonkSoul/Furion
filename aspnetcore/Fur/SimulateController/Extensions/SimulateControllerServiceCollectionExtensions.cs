using Fur.SimulateController;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 模刻控制器拓展类
    /// </summary>
    public static class SimulateControllerServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模刻控制器
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="providersOrConventions">提供器或转换器集合</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddSimulateControllers(this IMvcBuilder mvcBuilder, params object[] providersOrConventions)
        {
            var services = mvcBuilder.Services;
            // 添加配置
            services.AddAppOptions<SimulateSettingsOptions>();

            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddSimulateControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            // 添加模刻控制器提供器
            var providers = providersOrConventions.Where(u => typeof(IApplicationFeatureProvider).IsAssignableFrom(u.GetType()));
            if (!providers.Any()) partManager.FeatureProviders.Add(new SimulateControllerFeatureProvider());
            else
            {
                foreach (var provider in providers)
                {
                    partManager.FeatureProviders.Add(provider as IApplicationFeatureProvider);
                }
            }

            // 添加特性应用转换器
            var conventions = providersOrConventions.Where(u => typeof(IApplicationModelConvention).IsAssignableFrom(u.GetType()));
            mvcBuilder.AddMvcOptions(options =>
            {
                if (!providers.Any()) options.Conventions.Add(new SimulateApplicationModelConvention());
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