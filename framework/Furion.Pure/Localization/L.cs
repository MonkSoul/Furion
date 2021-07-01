// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Furion.Localization
{
    /// <summary>
    /// 全局多语言静态类
    /// </summary>
    [SuppressSniffer]
    public static class L
    {
        /// <summary>
        /// 语言类型
        /// </summary>
        public static readonly Type LangType;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static L()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            LangType = entryAssembly.GetType($"{entryAssembly.GetName().Name}.Lang");
        }

        /// <summary>
        /// String 多语言
        /// </summary>
        public static IStringLocalizer @Text => LangType == null ? null : App.GetService<IStringLocalizerFactory>()?.Create(LangType);

        /// <summary>
        /// Html 多语言
        /// </summary>
        public static IHtmlLocalizer @Html => LangType == null ? null : App.GetService<IHtmlLocalizerFactory>()?.Create(LangType);

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
}