// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.RemoteRequest;
using System.Net;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 远程请求服务拓展类
/// </summary>
[SuppressSniffer]
public static class RemoteRequestServiceCollectionExtensions
{
    /// <summary>
    /// 注册远程请求
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <param name="inludeDefaultHttpClient">是否包含默认客户端</param>
    /// <returns></returns>
    public static IServiceCollection AddRemoteRequest(this IServiceCollection services, Action<IServiceCollection> configure = null, bool inludeDefaultHttpClient = true)
    {
        // 注册远程请求代理接口
        services.AddDispatchProxyForInterface<HttpDispatchProxy, IHttpDispatchProxy>(typeof(ISingleton));

        // 注册默认请求客户端
        if (inludeDefaultHttpClient)
        {
            services.AddHttpClient(string.Empty)
                // 忽略 SSL 不安全检查，或 https 不安全或 https 证书有误
                .ConfigurePrimaryHttpMessageHandler(u => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
                })
                // 设置客户端生存期为 5 分钟
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
        }

        // 注册其他客户端
        configure?.Invoke(services);

        return services;
    }

    /// <summary>
    /// 忽略所有请求证书
    /// </summary>
    /// <remarks>慎用</remarks>
    public static void ApproveAllCerts(this IServiceCollection _)
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
        {
            return true;
        };
    }
}