using Furion.DependencyInjection;
using Furion.RemoteRequest;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 远程请求服务拓展类
    /// </summary>
    [SkipScan]
    public static class RemoteRequestServiceCollectionExtensions
    {
        /// <summary>
        /// 注册远程请求
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddRemoteRequest(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            // 注册 Http 代理接口
            services.AddScopedDispatchProxyForInterface<HttpDispatchProxy, IHttpDispatchProxy>();

            // 注册默认请求客户端
            services.AddHttpClient();

            // 注册其他客户端
            configure?.Invoke(services);

            return services;
        }
    }
}