// MIT License
//
// Copyright (c) 2020-present 百小僧, Baiqian Co.,Ltd and Contributors
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

namespace Furion.Schedule;

/// <summary>
/// 命名转换器
/// </summary>
/// <remarks>用于生成持久化 SQL 语句</remarks>
[SuppressSniffer]
public enum NamingConventions
{
    /// <summary>
    /// 驼峰命名法
    /// </summary>
    /// <remarks>第一个单词首字母小写</remarks>
    CamelCase = 0,

    /// <summary>
    /// 帕斯卡命名法
    /// </summary>
    /// <remarks>每一个单词首字母大写</remarks>
    Pascal = 1,

    /// <summary>
    /// 下划线命名法
    /// </summary>
    /// <remarks>每次单词使用下划线连接且首字母都是小写</remarks>
    UnderScoreCase = 2
}