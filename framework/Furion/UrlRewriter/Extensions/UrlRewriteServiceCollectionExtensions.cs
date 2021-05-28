using Furion.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发服务拓展
    /// </summary>
    [SkipScan]
    public static class UrlRewriteServiceCollectionExtensions
    {
        /// <summary>
        /// 添加URL转发服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddUrlRewrite(this IServiceCollection services, Action<HttpClientHandler> action = default)
        {
            var httpClientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                MaxConnectionsPerServer = int.MaxValue,
                UseCookies = false,
            };

            action?.Invoke(httpClientHandler);

            // 添加URL转发用的Http客户端
            services.AddHttpClient<RewriteProxyHttpClient>().ConfigurePrimaryHttpMessageHandler(x => httpClientHandler);

            // 添加URL转发器
            services.AddSingleton<IUrlRewriter, UrlRewriteProxy>();

            // 添加URL转发配置选项
            services.AddConfigurableOptions<UrlRewriteSettingsOptions>();

            return services;
        }
    }
}