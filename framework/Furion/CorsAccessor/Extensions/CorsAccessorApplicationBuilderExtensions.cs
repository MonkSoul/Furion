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

using Furion.CorsAccessor;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 跨域中间件拓展
/// </summary>
[SuppressSniffer]
public static class CorsAccessorApplicationBuilderExtensions
{
    /// <summary>
    /// 添加跨域中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="corsPolicyBuilderHandler"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCorsAccessor(this IApplicationBuilder app, Action<CorsPolicyBuilder> corsPolicyBuilderHandler = default)
    {
        // 获取选项
        var corsAccessorSettings = app.ApplicationServices.GetService<IOptions<CorsAccessorSettingsOptions>>().Value;

        // 判断是否启用 SignalR 跨域支持
        if (corsAccessorSettings.SignalRSupport == false)
        {
            // 配置跨域中间件
            _ = corsPolicyBuilderHandler == null
                   ? app.UseCors(corsAccessorSettings.PolicyName)
                   : app.UseCors(corsPolicyBuilderHandler);
        }
        else
        {
            // 配置跨域中间件
            app.UseCors(builder =>
            {
                // 设置跨域策略
                Penetrates.SetCorsPolicy(builder, corsAccessorSettings, true);

                // 添加自定义配置
                corsPolicyBuilderHandler?.Invoke(builder);
            });
        }

        // 添加压缩缓存
        app.UseResponseCaching();

        return app;
    }
}