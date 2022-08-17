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

namespace Furion.Options;

/// <summary>
/// 选项构建器特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class OptionsBuilderAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public OptionsBuilderAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sectionKey">配置节点</param>
    public OptionsBuilderAttribute(string sectionKey)
    {
        SectionKey = sectionKey;
    }

    /// <summary>
    /// 配置节点
    /// </summary>
    public string SectionKey { get; set; }

    /// <summary>
    /// 未知配置节点抛异常
    /// </summary>
    public bool ErrorOnUnknownConfiguration { get; set; }

    /// <summary>
    /// 绑定非公开属性
    /// </summary>
    public bool BindNonPublicProperties { get; set; }

    /// <summary>
    /// 启用验证特性支持
    /// </summary>
    public bool ValidateDataAnnotations { get; set; }

    /// <summary>
    /// 验证选项类型
    /// </summary>
    public Type[] ValidateOptionsTypes { get; set; }
}