// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 策略授权服务拓展类
/// </summary>
[SuppressSniffer]
public static class AuthorizationServiceCollectionExtensions
{
    /// <summary>
    /// 添加策略授权服务
    /// </summary>
    /// <typeparam name="TAuthorizationHandler">策略授权处理程序</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="configure">自定义配置</param>
    /// <param name="enableGlobalAuthorize">是否启用全局授权</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddAppAuthorization<TAuthorizationHandler>(this IServiceCollection services, Action<IServiceCollection> configure = null, bool enableGlobalAuthorize = false)
        where TAuthorizationHandler : class, IAuthorizationHandler
    {
        // 注册授权策略提供器
        services.TryAddSingleton<IAuthorizationPolicyProvider, AppAuthorizationPolicyProvider>();

        // 注册策略授权处理程序
        services.TryAddSingleton<IAuthorizationHandler, TAuthorizationHandler>();

        //启用全局授权
        if (enableGlobalAuthorize)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
        }

        configure?.Invoke(services);
        return services;
    }
}