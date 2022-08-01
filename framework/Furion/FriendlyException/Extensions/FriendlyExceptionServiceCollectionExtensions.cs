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

using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 友好异常服务拓展类
/// </summary>
[SuppressSniffer]
public static class FriendlyExceptionServiceCollectionExtensions
{
    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <typeparam name="TErrorCodeTypeProvider">异常错误码提供器</typeparam>
    /// <param name="mvcBuilder">Mvc构建器</param>
    /// <param name="configure">是否启用全局异常过滤器</param>
    /// <returns></returns>
    public static IMvcBuilder AddFriendlyException<TErrorCodeTypeProvider>(this IMvcBuilder mvcBuilder, Action<FriendlyExceptionServiceOptions> configure = null)
        where TErrorCodeTypeProvider : class, IErrorCodeTypeProvider
    {
        mvcBuilder.Services.AddFriendlyException<TErrorCodeTypeProvider>(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <typeparam name="TErrorCodeTypeProvider">异常错误码提供器</typeparam>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddFriendlyException<TErrorCodeTypeProvider>(this IServiceCollection services, Action<FriendlyExceptionServiceOptions> configure = null)
        where TErrorCodeTypeProvider : class, IErrorCodeTypeProvider
    {
        // 添加全局异常过滤器
        services.AddFriendlyException(configure);

        // 单例注册异常状态码提供器
        services.AddSingleton<IErrorCodeTypeProvider, TErrorCodeTypeProvider>();

        return services;
    }

    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <param name="mvcBuilder">Mvc构建器</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddFriendlyException(this IMvcBuilder mvcBuilder, Action<FriendlyExceptionServiceOptions> configure = null)
    {
        mvcBuilder.Services.AddFriendlyException(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddFriendlyException(this IServiceCollection services, Action<FriendlyExceptionServiceOptions> configure = null)
    {
        // 添加友好异常配置文件支持
        services.AddConfigurableOptions<FriendlyExceptionSettingsOptions>();

        // 添加异常配置文件支持
        services.AddConfigurableOptions<ErrorCodeMessageSettingsOptions>();

        // 载入服务配置选项
        var configureOptions = new FriendlyExceptionServiceOptions();
        configure?.Invoke(configureOptions);

        // 添加全局异常过滤器
        if (configureOptions.EnabledGlobalFriendlyException)
            services.AddMvcFilter<FriendlyExceptionFilter>();

        return services;
    }
}