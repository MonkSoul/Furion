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

namespace Furion.RemoteRequest;

/// <summary>
/// 配置Body参数
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Parameter)]
public class BodyAttribute : ParameterBaseAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BodyAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="contentType"></param>
    public BodyAttribute(string contentType)
    {
        ContentType = contentType;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="contentType"></param>
    /// <param name="encoding"></param>
    public BodyAttribute(string contentType, string encoding)
    {
        ContentType = contentType;
        Encoding = encoding;
    }

    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; } = "application/json";

    /// <summary>
    /// 内容编码
    /// </summary>
    public string Encoding { get; set; } = "UTF-8";
}