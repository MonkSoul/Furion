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

namespace System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// 配置表名称前后缀
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TableFixsAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="prefix"></param>
    public TableFixsAttribute(string prefix)
    {
        Prefix = prefix;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    public TableFixsAttribute(string prefix, string suffix)
        : this(prefix)
    {
        Suffix = suffix;
    }

    /// <summary>
    /// 前缀
    /// </summary>
    /// <remarks>前缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string Prefix { get; set; }

    /// <summary>
    /// 后缀
    /// </summary>
    /// <remarks>后缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string Suffix { get; set; }
}