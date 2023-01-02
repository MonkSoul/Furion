﻿// MIT License
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

using System.ComponentModel;

namespace Furion.RemoteRequest;

/// <summary>
/// 拦截类型
/// </summary>
[SuppressSniffer]
public enum InterceptorTypes
{
    /// <summary>
    /// 创建 HttpClient 拦截
    /// </summary>
    [Description("创建 HttpClient 拦截")]
    Initiate,

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    [Description("HttpClient 拦截")]
    Client,

    /// <summary>
    /// HttpRequestMessage 拦截
    /// </summary>
    [Description("HttpRequestMessage 拦截")]
    Request,

    /// <summary>
    /// HttpResponseMessage 拦截
    /// </summary>
    [Description("HttpResponseMessage 拦截")]
    Response,

    /// <summary>
    /// 异常拦截
    /// </summary>
    [Description("异常拦截")]
    Exception
}