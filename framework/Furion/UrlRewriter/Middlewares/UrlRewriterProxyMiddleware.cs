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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发中间件
    /// </summary>
    [SkipScan]
    public class UrlRewriterProxyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UrlRewriterSettingsOptions _urlRewriterOption;

        /// <summary>
        /// URL转发中间件
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public UrlRewriterProxyMiddleware(RequestDelegate next, IOptions<UrlRewriterSettingsOptions> options)
        {
            _next = next;
            _urlRewriterOption = options.Value;
        }

        /// <summary>
        /// 通过中间件 拦截访问、检测前缀、并转发
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            // 判断是否跳过 Url 重写
            if (!UrlRewriterContext.IsSkipUrlRewriter(context, _urlRewriterOption, out var urlRewriter))
            {
                var matchResult = UrlRewriterContext.TryMatchUrlRewriter(context, _urlRewriterOption);
                if (matchResult?.IsMatch == true)
                {
                    await urlRewriter.RewriteUri(context, matchResult.Prefix, matchResult.TargetHost);
                    return;
                }
            }

            await _next(context);
        }
    }
}