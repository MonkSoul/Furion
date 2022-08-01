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

namespace Furion.FriendlyException;

/// <summary>
/// 异常复写特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class IfExceptionAttribute : Attribute
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public IfExceptionAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="errorCode">错误编码</param>
    /// <param name="args">格式化参数</param>
    public IfExceptionAttribute(object errorCode, params object[] args)
    {
        ErrorCode = errorCode;
        Args = args;
    }

    /// <summary>
    /// 捕获特定异常类型异常（用于全局异常捕获）
    /// </summary>
    /// <param name="exceptionType"></param>
    public IfExceptionAttribute(Type exceptionType)
    {
        ExceptionType = exceptionType;
    }

    /// <summary>
    /// 错误编码
    /// </summary>
    public object ErrorCode { get; set; }

    /// <summary>
    /// 异常类型
    /// </summary>
    public Type ExceptionType { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// 格式化参数
    /// </summary>
    public object[] Args { get; set; }
}