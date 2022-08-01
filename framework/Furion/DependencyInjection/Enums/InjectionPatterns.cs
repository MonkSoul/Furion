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

using System.ComponentModel;

namespace Furion.DependencyInjection;

/// <summary>
/// 注册范围
/// </summary>
[SuppressSniffer]
public enum InjectionPatterns
{
    /// <summary>
    /// 只注册自己
    /// </summary>
    [Description("只注册自己")]
    Self,

    /// <summary>
    /// 第一个接口
    /// </summary>
    [Description("只注册第一个接口")]
    FirstInterface,

    /// <summary>
    /// 自己和第一个接口，默认值
    /// </summary>
    [Description("自己和第一个接口")]
    SelfWithFirstInterface,

    /// <summary>
    /// 所有接口
    /// </summary>
    [Description("所有接口")]
    ImplementedInterfaces,

    /// <summary>
    /// 注册自己包括所有接口
    /// </summary>
    [Description("自己包括所有接口")]
    All
}