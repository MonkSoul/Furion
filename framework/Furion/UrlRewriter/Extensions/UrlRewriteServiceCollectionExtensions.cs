using Furion.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        /// <returns></returns>
        public static IServiceCollection AddUrlRewrite(this IServiceCollection services)
        {
            // 注入url转发的httpclient
            services.AddHttpClient<RewriteProxyHttpClient>()
                .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler()
                {
                    AllowAutoRedirect = false,
                    MaxConnectionsPerServer = int.MaxValue,
                    UseCookies = false,
                });

            // 添加URL转发器
            services.AddSingleton<IUrlRewriter, UrlRewriteProxy>();

            // 绑定配置
            ConfigureUrlRewriteOptions(services);

            return services;
        }

        private static void ConfigureUrlRewriteOptions(IServiceCollection services)
        {
            // 获取配置节点
            var urlRewreteOptions = services.BuildServiceProvider()
                        .GetService<IConfiguration>()
                        .GetSection("UrlRewriteSettings");

            // 配置验证
            services.AddOptions<UrlRewriteSettingsOptions>()
                        .Bind(urlRewreteOptions)
                        .ValidateDataAnnotations();
        }
    }
}