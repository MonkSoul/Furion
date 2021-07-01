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
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 应用中间件拓展类（由框架内部调用）
    /// </summary>
    [SuppressSniffer]
    public static class AppApplicationBuilderExtensions
    {
        /// <summary>
        /// 注入基础中间件（带Swagger）
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routePrefix">空字符串将为首页</param>
        /// <param name="swaggerConfigure"></param>
        /// <param name="swaggerUIConfigure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseInject(this IApplicationBuilder app, string routePrefix = default, Action<SwaggerOptions> swaggerConfigure = null, Action<SwaggerUIOptions> swaggerUIConfigure = null)
        {
            app.UseSpecificationDocuments(routePrefix, swaggerConfigure, swaggerUIConfigure);
            return app;
        }

        /// <summary>
        /// 注入基础中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseInjectBase(this IApplicationBuilder app)
        {
            return app;
        }

        /// <summary>
        /// 添加应用中间件
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="configure">应用配置</param>
        /// <returns>应用构建器</returns>
        internal static IApplicationBuilder UseApp(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null)
        {
            // 启用 MiniProfiler组件
            if (App.Settings.InjectMiniProfiler == true)
            {
                app.UseMiniProfiler();
            }

            // 调用自定义服务
            configure?.Invoke(app);
            return app;
        }
    }
}