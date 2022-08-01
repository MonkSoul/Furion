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

using Furion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// ASP.NET Mvc 服务拓展类
/// </summary>
[SuppressSniffer]
public static class AspNetMvcBuilderServiceCollectionExtensions
{
    /// <summary>
    /// 注册 Mvc 过滤器
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    /// <param name="mvcBuilder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddMvcFilter<TFilter>(this IMvcBuilder mvcBuilder, Action<MvcOptions> configure = default)
        where TFilter : IFilterMetadata
    {
        mvcBuilder.Services.AddMvcFilter<TFilter>(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 注册 Mvc 过滤器
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddMvcFilter<TFilter>(this IServiceCollection services, Action<MvcOptions> configure = default)
        where TFilter : IFilterMetadata
    {
        // 非 Web 环境跳过注册
        if (App.WebHostEnvironment == default) return services;

        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<TFilter>();

            // 其他额外配置
            configure?.Invoke(options);
        });

        return services;
    }

    /// <summary>
    /// 注册 Mvc 过滤器
    /// </summary>
    /// <param name="services"></param>
    /// <param name="filter"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddMvcFilter(this IServiceCollection services, IFilterMetadata filter, Action<MvcOptions> configure = default)
    {
        // 非 Web 环境跳过注册
        if (App.WebHostEnvironment == default) return services;

        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add(filter);

            // 其他额外配置
            configure?.Invoke(options);
        });

        return services;
    }
}