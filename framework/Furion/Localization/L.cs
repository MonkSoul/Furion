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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Furion.Localization;

/// <summary>
/// 全局多语言静态类
/// </summary>
[SuppressSniffer]
public static class L
{
    /// <summary>
    /// String 多语言
    /// </summary>
    public static IStringLocalizer @Text => App.GetService<IStringLocalizerFactory>(App.RootServices)?.Create();

    /// <summary>
    /// Html 多语言
    /// </summary>
    public static IHtmlLocalizer @Html => App.GetService<IHtmlLocalizerFactory>(App.RootServices)?.Create();

    /// <summary>
    /// 设置多语言区域
    /// </summary>
    /// <param name="culture"></param>
    public static void SetCulture(string culture)
    {
        var httpContext = App.HttpContext;
        if (httpContext == null) return;

        httpContext.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
    }

    /// <summary>
    /// 获取当前选择的语言
    /// </summary>
    /// <returns></returns>
    public static RequestCulture GetSelectCulture()
    {
        var httpContext = App.HttpContext;
        if (httpContext == null) return default;

        // 获取请求特性
        var requestCulture = httpContext.Features.Get<IRequestCultureFeature>();
        return requestCulture.RequestCulture;
    }

    /// <summary>
    /// 获取系统提供的语言列表
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetCultures()
    {
        var httpContext = App.HttpContext;
        if (httpContext == null) return new Dictionary<string, string>();

        // 获取请求本地特性选项
        var locOptions = httpContext.RequestServices.GetService<IOptions<RequestLocalizationOptions>>().Value;

        // 获取语言符号和名称
        var cultureItems = locOptions.SupportedUICultures
            .ToDictionary(u => u.Name, u => u.DisplayName);

        return cultureItems;
    }
}