// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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