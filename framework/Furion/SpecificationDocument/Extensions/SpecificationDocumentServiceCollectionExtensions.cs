// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion;
using Furion.DependencyInjection;
using Furion.SpecificationDocument;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 规范化接口服务拓展类
    /// </summary>
    [SuppressSniffer]
    public static class SpecificationDocumentServiceCollectionExtensions
    {
        /// <summary>
        /// 添加规范化文档服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="swaggerGenConfigure">自定义配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSpecificationDocuments(this IServiceCollection services, Action<SwaggerGenOptions> swaggerGenConfigure = null)
        {
            // 判断是否启用规范化文档
            if (App.Settings.InjectSpecificationDocument != true) return services;

            // 添加配置
            services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();

            // 添加Swagger生成器服务
            services.AddSwaggerGen(options => SpecificationDocumentBuilder.BuildGen(options, swaggerGenConfigure));

            return services;
        }

        /// <summary>
        /// 添加规范化文档服务
        /// </summary>
        /// <param name="mvcBuilder">Mvc 构建器</param>
        /// <param name="swaggerGenConfigure">自定义配置</param>
        /// <returns>服务集合</returns>
        public static IMvcBuilder AddSpecificationDocuments(this IMvcBuilder mvcBuilder, Action<SwaggerGenOptions> swaggerGenConfigure = null)
        {
            // 判断是否启用规范化文档
            if (App.Settings.InjectSpecificationDocument != true) return mvcBuilder;

            var services = mvcBuilder.Services;

            // 添加配置
            services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();

            // 添加Swagger生成器服务
            services.AddSwaggerGen(options => SpecificationDocumentBuilder.BuildGen(options, swaggerGenConfigure));

            return mvcBuilder;
        }
    }
}