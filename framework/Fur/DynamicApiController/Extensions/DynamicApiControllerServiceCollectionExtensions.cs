using Fur.DynamicApiController;
using Fur.FriendlyException;
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
        /// 添加动态接口控制器服务
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddDynamicApiControllers(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;
            // 添加配置
            services.AddAppOptions<DynamicApiControllerSettingsOptions>();

            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager)).ImplementationInstance as ApplicationPartManager
                ?? throw Oops.Oh($"`{nameof(AddDynamicApiControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.", typeof(InvalidOperationException));

            // 添加控制器特性提供器
            partManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());

            // 注册动态 Api 控制器应用模型转换器
            var serviceProvider = services
                .AddSingleton<DynamicApiControllerApplicationModelConvention>()
                .BuildServiceProvider();

            // 添加应用模型转换器
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Conventions.Add(serviceProvider.GetService<DynamicApiControllerApplicationModelConvention>());
            });

            // 支持 JArray/JObject
            mvcBuilder.AddNewtonsoftJson();

            return mvcBuilder;
        }
    }
}