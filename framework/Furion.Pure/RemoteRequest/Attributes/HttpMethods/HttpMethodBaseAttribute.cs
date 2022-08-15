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

namespace Furion.RemoteRequest;

/// <summary>
/// 请求方法基类
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class HttpMethodBaseAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="method"></param>
    public HttpMethodBaseAttribute(string requestUrl, HttpMethod method)
    {
        RequestUrl = requestUrl;
        Method = method;
    }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; set; }

    /// <summary>
    /// 请求谓词
    /// </summary>
    public HttpMethod Method { get; set; }

    /// <summary>
    /// 支持 GZip 压缩
    /// </summary>
    public bool WithGZip { get; set; } = false;

    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; } = "application/json";

    /// <summary>
    /// 内容编码
    /// </summary>
    public string Encoding { get; set; } = "UTF-8";

    /// <summary>
    /// 是否对 Url 进行 Uri.EscapeDataString
    /// </summary>
    public bool WithEncodeUrl { get; set; } = true;
}