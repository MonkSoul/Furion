// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

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
    }
}