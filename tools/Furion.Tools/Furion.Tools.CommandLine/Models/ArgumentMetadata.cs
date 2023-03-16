// MIT License
//
// Copyright © 2020-present 百小僧, 百签科技（广东）有限公司 and Contributors
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

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数元数据
/// </summary>
public sealed class ArgumentMetadata
{
    /// <summary>
    /// 短参数名
    /// </summary>
    public char ShortName { get; internal set; }

    /// <summary>
    /// 长参数名
    /// </summary>
    public string LongName { get; internal set; }

    /// <summary>
    /// 帮助文本
    /// </summary>
    public string HelpText { get; internal set; }

    /// <summary>
    /// 参数值
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// 是否传参
    /// </summary>
    public bool IsTransmission { get; set; }

    /// <summary>
    /// 是否集合
    /// </summary>
    public bool IsCollection { get; internal set; }

    /// <summary>
    /// 属性对象
    /// </summary>
    public PropertyInfo Property { get; internal set; }

    /// <summary>
    /// 是否传入短参数
    /// </summary>
    public bool IsShortName { get; set; }

    /// <summary>
    /// 是否传入长参数
    /// </summary>
    public bool IsLongName { get; set; }
}
