// MIT License
//
// Copyright (c) 2020-2023 百小僧, Baiqian Co.,Ltd and Contributors
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

namespace Furion.RemoteRequest;

/// <summary>
/// 远程请求失败事件类
/// </summary>
[SuppressSniffer]
public sealed class HttpRequestFaildedEventArgs : EventArgs
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    /// <param name="exception"></param>
    public HttpRequestFaildedEventArgs(HttpRequestMessage request, HttpResponseMessage response, Exception exception)
    {
        Request = request;
        Response = response;
        Exception = exception;
    }

    /// <summary>
    /// 请求对象
    /// </summary>
    public HttpRequestMessage Request { get; internal set; }

    /// <summary>
    /// 响应对象
    /// </summary>
    public HttpResponseMessage Response { get; internal set; }

    /// <summary>
    /// 异常对象
    /// </summary>
    public Exception Exception { get; internal set; }
}