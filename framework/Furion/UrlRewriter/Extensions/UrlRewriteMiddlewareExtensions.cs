// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.9
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.UrlRewriter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// URL 转发中间件拓展
    /// </summary>
    [SkipScan]
    public static class UrlRewriteMiddlewareExtensions
    {
        /// <summary>
        /// 添加URL转发拦截中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUrlRewrite(this IApplicationBuilder builder, Action<UrlRewriteSettingsOptions> optionsBuilder = default)
        {
            // 读取配置选项
            var urlRewriteSettingsOptions = builder.ApplicationServices.GetService<IOptions<UrlRewriteSettingsOptions>>().Value;
            optionsBuilder?.Invoke(urlRewriteSettingsOptions);

            // 注册中间件
            builder.UseMiddleware<UrlRewriteProxyMiddleware>(urlRewriteSettingsOptions);

            return builder;
        }
    }
}