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

using Furion.CorsAccessor;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 跨域中间件拓展
    /// </summary>
    [SuppressSniffer]
    public static class CorsAccessorApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加跨域中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="corsPolicyBuilderHandler"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsAccessor(this IApplicationBuilder app, Action<CorsPolicyBuilder> corsPolicyBuilderHandler = default)
        {
            // 获取选项
            var corsAccessorSettings = app.ApplicationServices.GetService<IOptions<CorsAccessorSettingsOptions>>().Value;

            // 配置跨域中间件
            _ = corsPolicyBuilderHandler == null
                   ? app.UseCors(corsAccessorSettings.PolicyName)
                   : app.UseCors(corsPolicyBuilderHandler);

            // 添加压缩缓存
            app.UseResponseCaching();

            return app;
        }
    }
}