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

using System;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数定义特性
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ArgumentAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="shortName">短参数名</param>
    /// <param name="longName">长参数名</param>
    /// <param name="helpText">帮助文本</param>
    public ArgumentAttribute(char shortName, string longName, string helpText = null)
    {
        ShortName = shortName;
        LongName = longName;
        HelpText = helpText;
    }

    /// <summary>
    /// 帮助文本
    /// </summary>
    public string HelpText { get; set; }

    /// <summary>
    /// 长参数名
    /// </summary>
    public string LongName { get; set; }

    /// <summary>
    /// 短参数名
    /// </summary>
    public char ShortName { get; set; }
}
