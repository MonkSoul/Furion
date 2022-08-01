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

using Microsoft.CodeAnalysis;
using System.Runtime.Serialization;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎模板编译异常类
/// </summary>
[SuppressSniffer]
public class ViewEngineTemplateException : ViewEngineException
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ViewEngineTemplateException()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ViewEngineTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="innerException"></param>
    public ViewEngineTemplateException(Exception innerException) : base(null, innerException)
    {
    }

    /// <summary>
    /// 错误信息
    /// </summary>
    public List<Diagnostic> Errors { get; set; }

    /// <summary>
    /// 生成的代码
    /// </summary>
    public string GeneratedCode { get; set; }

    /// <summary>
    /// 重写异常消息
    /// </summary>
    public override string Message => $"Unable to compile template: {string.Join("\n", Errors.Where(w => w.IsWarningAsError || w.Severity == DiagnosticSeverity.Error))}";
}