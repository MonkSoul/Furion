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
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// URL转发服务拓展
    /// </summary>
    [SkipScan]
    public static class UrlRewriterServiceCollectionExtensions
    {
        /// <summary>
        /// 添加URL转发服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureHttpClientHandler"></param>
        /// <returns></returns>
        public static IServiceCollection AddUrlRewriter(this IServiceCollection services, Action<HttpClientHandler> configureHttpClientHandler = default)
        {
            // 注册配置选项
            services.AddConfigurableOptions<UrlRewriterSettingsOptions>();

            // 注册 Url 转发 UrlRewriterHttpClientHandler
            services.AddTransient<UrlRewriterHttpClientHandler>();

            // 添加 Url 转发用的 Http 客户端
            services.AddHttpClient<UrlRewriterProxyHttpClient>()
                    .ConfigurePrimaryHttpMessageHandler(provider =>
                    {
                        // 解析服务
                        var httpClientHandler = provider.GetRequiredService<UrlRewriterHttpClientHandler>();
                        configureHttpClientHandler?.Invoke(httpClientHandler);

                        return httpClientHandler;
                    });

            // 注册 Url 转发器
            services.AddSingleton<IUrlRewriterProxy, UrlRewriterProxy>();

            return services;
        }
    }
}