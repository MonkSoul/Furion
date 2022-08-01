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

using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Furion.CorsAccessor;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 默认跨域导出响应头 Key
    /// </summary>
    /// <remarks>解决 ajax，XMLHttpRequest，axios 不能获取请求头问题</remarks>
    private static readonly string[] _defaultExposedHeaders = new[]
    {
        "access-token",
        "x-access-token"
    };

    /// <summary>
    /// 设置跨域策略
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="corsAccessorSettings"></param>
    /// <param name="isMiddleware"></param>
    internal static void SetCorsPolicy(CorsPolicyBuilder builder, CorsAccessorSettingsOptions corsAccessorSettings, bool isMiddleware = false)
    {
        // 判断是否设置了来源，因为 AllowAnyOrigin 不能和 AllowCredentials一起公用
        var isNotSetOrigins = corsAccessorSettings.WithOrigins == null || corsAccessorSettings.WithOrigins.Length == 0;

        // https://docs.microsoft.com/zh-cn/aspnet/core/signalr/security?view=aspnetcore-6.0
        var isSupportSignarlR = isMiddleware && corsAccessorSettings.SignalRSupport == true;

        // 设置总是允许跨域源配置
        builder.SetIsOriginAllowed(_ => true);

        // 如果没有配置来源，则允许所有来源
        if (isNotSetOrigins)
        {
            // 解决 SignarlR  不能配置允许所有源问题
            if (!isSupportSignarlR) builder.AllowAnyOrigin();
        }
        else builder.WithOrigins(corsAccessorSettings.WithOrigins)
                    .SetIsOriginAllowedToAllowWildcardSubdomains();

        // 如果没有配置请求标头，则允许所有表头，包含处理 SignarlR 情况
        if ((corsAccessorSettings.WithHeaders == null || corsAccessorSettings.WithHeaders.Length == 0) || isSupportSignarlR) builder.AllowAnyHeader();
        else builder.WithHeaders(corsAccessorSettings.WithHeaders);

        // 如果没有配置任何请求谓词，则允许所有请求谓词
        if (corsAccessorSettings.WithMethods == null || corsAccessorSettings.WithMethods.Length == 0) builder.AllowAnyMethod();
        else
        {
            // 解决 SignarlR 必须允许 GET POST 问题
            if (isSupportSignarlR)
            {
                builder.WithMethods(corsAccessorSettings.WithMethods.Concat(new[] { "GET", "POST" }).Distinct(StringComparer.OrdinalIgnoreCase).ToArray());
            }
            else builder.WithMethods(corsAccessorSettings.WithMethods);
        }

        // 配置跨域凭据，包含处理 SignarlR 情况
        if ((corsAccessorSettings.AllowCredentials == true && !isNotSetOrigins) || isSupportSignarlR) builder.AllowCredentials();

        // 配置响应头，如果前端不能获取自定义的 header 信息，必须配置该项，默认配置了 access-token 和 x-access-token，可取消默认行为
        IEnumerable<string> exposedHeaders = corsAccessorSettings.FixedClientToken == true
            ? _defaultExposedHeaders
            : Array.Empty<string>();
        if (corsAccessorSettings.WithExposedHeaders != null && corsAccessorSettings.WithExposedHeaders.Length > 0)
        {
            exposedHeaders = exposedHeaders.Concat(corsAccessorSettings.WithExposedHeaders).Distinct(StringComparer.OrdinalIgnoreCase);
        }

        if (exposedHeaders.Any()) builder.WithExposedHeaders(exposedHeaders.ToArray());

        // 设置预检过期时间，如果不设置默认为 24小时
        builder.SetPreflightMaxAge(TimeSpan.FromSeconds(corsAccessorSettings.SetPreflightMaxAge ?? 24 * 60 * 60));
    }
}