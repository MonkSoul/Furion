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
    [SkipScan]
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