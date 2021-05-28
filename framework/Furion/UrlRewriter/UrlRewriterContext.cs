using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发上下文
    /// </summary>
    internal class UrlRewriterContext
    {
        internal static bool IsSkipUrlRewrite(HttpContext context, out IUrlRewriter urlRewriter, UrlRewriteSettingsOptions urlRewriteOption)
        {
            // 判断是否跳过URL转发
            var isSkip = !urlRewriteOption.UrlRewriteEnable;

            urlRewriter = isSkip ? null : App.GetService<IUrlRewriter>();
            return urlRewriter == null || isSkip;
        }

        /// <summary>
        /// 匹配是否满足转发规则
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="urlRewriteOption"></param>
        /// <returns></returns>
        internal static UrlRewriteMatchResult MatchRewrite(HttpContext httpContext, UrlRewriteSettingsOptions urlRewriteOption)
        {
            if (string.IsNullOrEmpty(httpContext.Request.Path)) return default;

            if (urlRewriteOption.UrlRewriteRules?.Length == 0) return default;

            // 查找符合条件的转发规则
            var item = urlRewriteOption.UrlRewriteRules.FirstOrDefault(x => !string.IsNullOrEmpty(x[0]) && httpContext.Request.Path.StartsWithSegments(x[0]));

            return item == null ? default : new UrlRewriteMatchResult { IsMatch = true, Prefix = item[0], ToHost = item[1] };
        }
    }
}