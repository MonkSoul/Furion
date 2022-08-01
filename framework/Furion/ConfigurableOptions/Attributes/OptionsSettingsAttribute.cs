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

namespace Furion.ConfigurableOptions;

/// <summary>
/// 选项配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
public sealed class OptionsSettingsAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public OptionsSettingsAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path">appsetting.json 对应键</param>
    public OptionsSettingsAttribute(string path)
    {
        Path = path;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="postConfigureAll">启动所有实例进行后期配置</param>
    public OptionsSettingsAttribute(bool postConfigureAll)
    {
        PostConfigureAll = postConfigureAll;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path">appsetting.json 对应键</param>
    /// <param name="postConfigureAll">启动所有实例进行后期配置</param>
    public OptionsSettingsAttribute(string path, bool postConfigureAll)
    {
        Path = path;
        PostConfigureAll = postConfigureAll;
    }

    /// <summary>
    /// 对应配置文件中的路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 对所有配置实例进行后期配置
    /// </summary>
    public bool PostConfigureAll { get; set; }
}