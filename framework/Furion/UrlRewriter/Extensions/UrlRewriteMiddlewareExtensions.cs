using Furion.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发中间件拓展
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
            // 提供配置
            var options = builder.ApplicationServices.GetService<IOptions<UrlRewriteSettingsOptions>>().Value ?? new UrlRewriteSettingsOptions();
            optionsBuilder?.Invoke(options);

            builder.UseMiddleware<UrlRewriteProxyMiddleware>(options);

            return builder;
        }
    }
}