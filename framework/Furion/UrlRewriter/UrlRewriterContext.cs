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

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL 转发上下文
    /// </summary>
    internal class UrlRewriterContext
    {
        /// <summary>
        /// 判断是否跳过 Url 重写
        /// </summary>
        /// <param name="context"></param>
        /// <param name="urlRewriterOption"></param>
        /// <param name="urlRewriter"></param>
        /// <returns></returns>
        internal static bool IsSkipUrlRewriter(HttpContext context, UrlRewriterSettingsOptions urlRewriterOption, out IUrlRewriterProxy urlRewriter)
        {
            // 判断是否跳过URL转发
            var isSkip = urlRewriterOption.Enabled != true;

            urlRewriter = isSkip ? null : context.RequestServices.GetService<IUrlRewriterProxy>();
            return urlRewriter == null || isSkip;
        }

        /// <summary>
        /// 匹配是否满足转发规则
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="urlRewriterOption"></param>
        /// <returns></returns>
        internal static UrlRewriterMatchResult TryMatchUrlRewriter(HttpContext httpContext, UrlRewriterSettingsOptions urlRewriterOption)
        {
            if (string.IsNullOrWhiteSpace(httpContext.Request.Path)) return default;

            if (urlRewriterOption?.Rules?.Length == 0) return default;

            // 查找符合条件的转发规则
            var item = urlRewriterOption.Rules.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x[0]) && httpContext.Request.Path.StartsWithSegments(x[0]));

            return item == null ? default : new UrlRewriterMatchResult { IsMatch = true, Prefix = item[0], TargetHost = item[1] };
        }
    }
}