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
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 规范化文档中间件拓展
    /// </summary>
    [SuppressSniffer]
    public static class SpecificationDocumentApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加规范化文档中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routePrefix"></param>
        /// <param name="swaggerConfigure"></param>
        /// <param name="swaggerUIConfigure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSpecificationDocuments(this IApplicationBuilder app, string routePrefix = default, Action<SwaggerOptions> swaggerConfigure = null, Action<SwaggerUIOptions> swaggerUIConfigure = null)
        {
            // 判断是否启用规范化文档
            if (App.Settings.InjectSpecificationDocument != true) return app;

            // 配置 Swagger 全局参数
            app.UseSwagger(options => SpecificationDocumentBuilder.Build(options, swaggerConfigure));

            // 配置 Swagger UI 参数
            app.UseSwaggerUI(options => SpecificationDocumentBuilder.BuildUI(options, routePrefix, swaggerUIConfigure));

            return app;
        }
    }
}