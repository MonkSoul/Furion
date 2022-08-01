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
/// 异常元数据
/// </summary>
[SuppressSniffer]
public sealed class ExceptionMetadata
{
    /// <summary>
    /// 状态码
    /// </summary>
    public int StatusCode { get; internal set; }

    /// <summary>
    /// 错误码
    /// </summary>
    public object ErrorCode { get; internal set; }

    /// <summary>
    /// 错误码（没被复写过的 ErrorCode ）
    /// </summary>
    public object OriginErrorCode { get; internal set; }

    /// <summary>
    /// 错误对象（信息）
    /// </summary>
    public object Errors { get; internal set; }
}