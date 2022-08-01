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
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// WebApplication 拓展
/// </summary>
public static class AppWebApplicationBuilderExtensions
{
#if !NET5_0
    /// <summary>
    /// Web 应用注入
    /// </summary>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <returns>IWebHostBuilder</returns>
    public static WebApplicationBuilder Inject(this WebApplicationBuilder webApplicationBuilder)
    {
        // 为了兼容 .NET 5 无缝升级至 .NET 6，故传递 WebHost 和 Host
        InternalApp.WebHostEnvironment = webApplicationBuilder.Environment;
        InternalApp.ConfigureApplication(webApplicationBuilder.WebHost, webApplicationBuilder.Host);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册单个组件
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddComponent<TComponent>(this WebApplicationBuilder webApplicationBuilder, object options = default)
        where TComponent : class, IServiceComponent, new()
    {
        webApplicationBuilder.Services.AddComponent<TComponent>(options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IServiceComponent"/></typeparam>
    /// <typeparam name="TComponentOptions">组件参数</typeparam>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static WebApplicationBuilder AddComponent<TComponent, TComponentOptions>(this WebApplicationBuilder webApplicationBuilder, TComponentOptions options = default)
        where TComponent : class, IServiceComponent, new()
    {
        webApplicationBuilder.Services.AddComponent<TComponent, TComponentOptions>(options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static WebApplicationBuilder AddComponent(this WebApplicationBuilder webApplicationBuilder, Type componentType, object options = default)
    {
        webApplicationBuilder.Services.AddComponent(componentType, options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 解决 .NET6 WebApplication 模式下二级虚拟目录错误问题
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseVirtualPath(this WebApplication app, Action<IApplicationBuilder> configuration)
    {
        if (!string.IsNullOrWhiteSpace(App.Settings.VirtualPath))
        {
            return app.Map(App.Settings.VirtualPath, configuration);
        }

        configuration(app);
        return app;
    }
#endif
}