using Fur.DependencyInjection;
using Fur.RemoteRequest;

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
        /// <returns></returns>
        public static IServiceCollection AddRemoteRequest(this IServiceCollection services)
        {
            // 注册 Http 代理接口
            services.AddScopedDispatchProxyForInterface<HttpDispatchProxy, IHttpDispatchProxy>();

            // 注册默认请求客户端
            services.AddHttpClient();

            return services;
        }
    }
}