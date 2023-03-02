// MIT License
//
// Copyright (c) 2020-present 百小僧, 百签科技（广东）有限公司 and Contributors
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

using System.Collections.Generic;

namespace Furion.Tools.CommandLine;

/// <summary>
/// 参数模型
/// </summary>
public sealed class ArgumentModel
{
    /// <summary>
    /// 参数字典
    /// </summary>
    public Dictionary<string, object> ArgumentDictionary { get; internal set; }

    /// <summary>
    /// 参数键值对
    /// </summary>
    public List<KeyValuePair<string, string>> ArgumentList { get; internal set; }

    /// <summary>
    /// 参数命令
    /// </summary>
    public string CommandLineString { get; internal set; }

    /// <summary>
    /// 操作符列表
    /// </summary>
    public List<string> OperandList { get; internal set; }
}
