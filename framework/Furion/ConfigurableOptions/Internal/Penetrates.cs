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

using System.Reflection;

namespace Furion.ConfigurableOptions;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 获取选项配置
    /// </summary>
    /// <param name="optionsType">选项类型</param>
    /// <returns></returns>
    internal static (OptionsSettingsAttribute, string) GetOptionsConfiguration(Type optionsType)
    {
        var optionsSettings = optionsType.GetCustomAttribute<OptionsSettingsAttribute>(false);

        // 默认后缀
        var defaultStuffx = nameof(Options);

        return (optionsSettings, optionsSettings switch
        {
            // // 没有贴 [OptionsSettings]，如果选项类以 `Options` 结尾，则移除，否则返回类名称
            null => optionsType.Name.EndsWith(defaultStuffx) ? optionsType.Name[0..^defaultStuffx.Length] : optionsType.Name,
            // 如果贴有 [OptionsSettings] 特性，但未指定 Path 参数，则直接返回类名，否则返回 Path
            _ => optionsSettings != null && string.IsNullOrWhiteSpace(optionsSettings.Path) ? optionsType.Name : optionsSettings.Path,
        });
    }

    /// <summary>
    /// 在主机启动时获取选项
    /// </summary>
    /// <remarks>解决 v4.5.2+ 历史版本升级问题</remarks>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    internal static TOptions GetOptionsOnStarting<TOptions>()
        where TOptions : class, new()
    {
        if (App.RootServices == null && typeof(IConfigurableOptions).IsAssignableFrom(typeof(TOptions)))
        {
            var (_, path) = GetOptionsConfiguration(typeof(TOptions));
            return App.GetConfig<TOptions>(path, true);
        }

        return null;
    }
}