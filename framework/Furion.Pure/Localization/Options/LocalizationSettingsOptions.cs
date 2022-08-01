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

using Furion.ConfigurableOptions;
using Furion.Reflection;
using Microsoft.Extensions.Configuration;

namespace Furion.Localization;

/// <summary>
/// 多语言配置选项
/// </summary>
public sealed class LocalizationSettingsOptions : IConfigurableOptions<LocalizationSettingsOptions>
{
    /// <summary>
    /// 资源路径
    /// </summary>
    public string ResourcesPath { get; set; }

    /// <summary>
    /// 支持的语言列表
    /// </summary>
    public string[] SupportedCultures { get; set; }

    /// <summary>
    /// 默认的语言
    /// </summary>
    public string DefaultCulture { get; set; }

    /// <summary>
    /// 资源文件名前缀
    /// </summary>
    public string LanguageFilePrefix { get; set; }

    /// <summary>
    /// 资源所在程序集名称
    /// </summary>
    public string AssemblyName { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(LocalizationSettingsOptions options, IConfiguration configuration)
    {
        ResourcesPath ??= "Resources";
        SupportedCultures ??= Array.Empty<string>();
        DefaultCulture ??= string.Empty;
        LanguageFilePrefix ??= "Lang";
        AssemblyName ??= Reflect.GetAssemblyName(Reflect.GetEntryAssembly());
    }
}