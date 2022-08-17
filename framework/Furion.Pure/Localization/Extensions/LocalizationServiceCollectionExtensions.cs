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

using Furion;
using Furion.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 多语言服务拓展类
/// </summary>
[SuppressSniffer]
public static class LocalizationServiceCollectionExtensions
{
    /// <summary>
    /// 配置多语言服务
    /// </summary>
    /// <param name="mvcBuilde"></param>
    /// <returns></returns>
    public static IMvcBuilder AddAppLocalization(this IMvcBuilder mvcBuilde)
    {
        var services = mvcBuilde.Services;

        // 添加多语言配置选项
        services.AddConfigurableOptions<LocalizationSettingsOptions>();

        // 获取多语言配置选项
        var localizationSettings = App.GetConfig<LocalizationSettingsOptions>("LocalizationSettings", true);

        // 注册多语言服务
        services.AddLocalization(options =>
        {
            if (!string.IsNullOrWhiteSpace(localizationSettings.ResourcesPath))
                options.ResourcesPath = localizationSettings.ResourcesPath;
        });

        // 配置视图多语言和验证多语言
        mvcBuilde.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                 .AddDataAnnotationsLocalization(options =>
                 {
                     options.DataAnnotationLocalizerProvider = (type, factory) =>
                         factory.Create(localizationSettings.LanguageFilePrefix, localizationSettings.AssemblyName);
                 });

        // 注册请求多语言配置选项
        services.Configure((Action<RequestLocalizationOptions>)(options =>
        {
            Penetrates.SetRequestLocalization(options, localizationSettings);
        }));

        // 处理多语言在 Razor 视图中文乱码问题
        services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

        return mvcBuilde;
    }
}