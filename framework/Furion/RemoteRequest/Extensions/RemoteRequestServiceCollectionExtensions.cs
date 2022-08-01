// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
        services.AddScopedDispatchProxyForInterface<HttpDispatchProxy, IHttpDispatchProxy>();

        // 注册默认请求客户端
        if (inludeDefaultHttpClient)
        {
            services.AddHttpClient(string.Empty)
                // 忽略 SSL 不安全检查，或 https 不安全或 https 证书有误
                .ConfigurePrimaryHttpMessageHandler(u => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
                });
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