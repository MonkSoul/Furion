using Fur.DependencyInjection;
using Fur.SpecificationDocument;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 规范化接口服务拓展类
    /// </summary>
    [SkipScan]
    public static class SpecificationDocumentServiceCollectionExtensions
    {
        /// <summary>
        /// 添加规范化文档服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">自定义配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSpecificationDocuments(this IServiceCollection services, Action<SwaggerGenOptions> configure = null)
        {
            // 添加配置
            services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();

            // 添加Swagger生成器服务
            services.AddSwaggerGen(options => SpecificationDocumentBuilder.BuildGen(options, configure));

            return services;
        }

        /// <summary>
        /// 添加规范化文档服务
        /// </summary>
        /// <param name="mvcBuilder">Mvc 构建器</param>
        /// <param name="configure">自定义配置</param>
        /// <returns>服务集合</returns>
        public static IMvcBuilder AddSpecificationDocuments(this IMvcBuilder mvcBuilder, Action<SwaggerGenOptions> configure = null)
        {
            var services = mvcBuilder.Services;

            // 添加配置
            services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();

            // 添加Swagger生成器服务
            services.AddSwaggerGen(options => SpecificationDocumentBuilder.BuildGen(options, configure));

            return mvcBuilder;
        }
    }
}