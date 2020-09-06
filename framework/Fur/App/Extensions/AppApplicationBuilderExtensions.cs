// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 应用中间件拓展类
    /// </summary>
    public static class AppApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加应用中间件
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="configure">应用配置</param>
        /// <returns>应用构建器</returns>
        public static IApplicationBuilder UseApp(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null)
        {
            // 启用 MiniProfiler组件
            if (App.Settings.InjectMiniProfiler == true)
            {
                app.UseMiniProfiler();
            }

            configure?.Invoke(app);
            return app;
        }
    }
}