using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发中间件
    /// </summary>
    [SkipScan]
    public class UrlRewriteProxyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UrlRewriteSettingsOptions _urlRewriteOption;

        /// <summary>
        /// URL转发中间件
        /// </summary>
        /// <param name="next"></param>
        /// <param name="urlRewriteOption"></param>
        public UrlRewriteProxyMiddleware(RequestDelegate next, UrlRewriteSettingsOptions urlRewriteOption)
        {
            _next = next;
            _urlRewriteOption = urlRewriteOption;
        }

        /// <summary>
        /// 通过中间件,拦截访问,检测前缀,并转发
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (!UrlRewriterContext.IsSkipUrlRewrite(context, out IUrlRewriter urlRewriter, _urlRewriteOption))
            {
                var matchResult = UrlRewriterContext.MatchRewrite(context, _urlRewriteOption);
                if (matchResult?.IsMatch == true)
                {
                    await urlRewriter.RewriteUri(context, matchResult.Prefix, matchResult.ToHost);
                    return;
                }
            }

            await _next(context);
        }
    }
}