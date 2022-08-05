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

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 模型绑定特性
/// </summary>
/// <remarks>供模型绑定使用</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class BindToAttribute : Attribute
{
    /// <summary>
    /// 是否允许空字符串
    /// </summary>
    public bool AllowStringEmpty { get; set; } = false;

    /// <summary>
    /// 模型转换绑定器
    /// </summary>
    public Type ModelConvertBinder { get; set; }

    /// <summary>
    /// 额外数据
    /// </summary>
    public object Extras { get; set; }

    /// <summary>
    /// 完全自定义
    /// </summary>
    /// <remarks>框架内部不做任何处理</remarks>
    public bool Customize { get; set; } = false;
}