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

using Fur;
using Fur.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 应用中间件拓展类（由框架内部调用）
    /// </summary>
    [SkipScan]
    public static class AppApplicationBuilderExtensions
    {
        /// <summary>
        /// 注入基础中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routePrefix"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseInject(this IApplicationBuilder app, string routePrefix = default)
        {
            app.UseSpecificationDocuments(routePrefix);
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