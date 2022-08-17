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

using System.Text.RegularExpressions;

namespace Furion.DataValidation;

/// <summary>
/// 验证项元数据
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Field)]
public class ValidationItemMetadataAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="regularExpression">正则表达式</param>
    /// <param name="defaultErrorMessage">失败提示默认消息</param>
    /// <param name="regexOptions">正则表达式匹配选项</param>
    public ValidationItemMetadataAttribute(string regularExpression, string defaultErrorMessage, RegexOptions regexOptions = RegexOptions.None)
    {
        RegularExpression = regularExpression;
        DefaultErrorMessage = defaultErrorMessage;
        RegexOptions = regexOptions;
    }

    /// <summary>
    /// 正则表达式
    /// </summary>
    public string RegularExpression { get; set; }

    /// <summary>
    /// 默认验证失败类型
    /// </summary>
    public string DefaultErrorMessage { get; set; }

    /// <summary>
    /// 正则表达式选项
    /// </summary>
    public RegexOptions RegexOptions { get; set; }
}