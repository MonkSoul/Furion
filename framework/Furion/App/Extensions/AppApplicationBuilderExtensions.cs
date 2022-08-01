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

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 应用中间件拓展类（由框架内部调用）
/// </summary>
[SuppressSniffer]
public static class AppApplicationBuilderExtensions
{
    /// <summary>
    /// 注入基础中间件（带Swagger）
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routePrefix">空字符串将为首页</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInject(this IApplicationBuilder app, string routePrefix = default, Action<InjectConfigureOptions> configure = null)
    {
        // 载入中间件配置选项
        var configureOptions = new InjectConfigureOptions();
        configure?.Invoke(configureOptions);

        app.UseSpecificationDocuments(routePrefix, configureOptions?.SpecificationDocumentConfigure);

        return app;
    }

    /// <summary>
    /// 注入基础中间件
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInjectBase(this IApplicationBuilder app)
    {
        return app;
    }

    /// <summary>
    /// 解决 .NET6 WebApplication 模式下二级虚拟目录错误问题
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder MapRouteControllers(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }

    /// <summary>
    /// 添加应用中间件
    /// </summary>
    /// <param name="app">应用构建器</param>
    /// <param name="configure">应用配置</param>
    /// <returns>应用构建器</returns>
    internal static IApplicationBuilder UseApp(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null)
    {
        // 调用自定义服务
        configure?.Invoke(app);
        return app;
    }
}