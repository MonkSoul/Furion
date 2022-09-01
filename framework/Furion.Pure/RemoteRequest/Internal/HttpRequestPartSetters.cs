﻿// MIT License
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

using Furion.ClayObject.Extensions;
using Furion.JsonSerialization;
using Furion.Templates.Extensions;
using System.Text;

namespace Furion.RemoteRequest;

/// <summary>
/// HttpClient 对象组装部件
/// </summary>
public sealed partial class HttpRequestPart
{
    /// <summary>
    /// 设置请求地址
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <returns></returns>
    public HttpRequestPart SetRequestUrl(string requestUrl)
    {
        if (string.IsNullOrWhiteSpace(requestUrl)) return this;

        // 解决配置 BaseAddress 客户端后，地址首位斜杆问题
        requestUrl = requestUrl.StartsWith("/") ? requestUrl[1..] : requestUrl;

        // 支持读取配置渲染
        RequestUrl = requestUrl.Render();
        return this;
    }

    /// <summary>
    /// 设置 URL 模板
    /// </summary>
    /// <param name="templates"></param>
    /// <returns></returns>
    public HttpRequestPart SetTemplates(IDictionary<string, object> templates)
    {
        if (templates != null) Templates = templates;
        return this;
    }

    /// <summary>
    /// 设置 URL 模板
    /// </summary>
    /// <param name="templates"></param>
    /// <returns></returns>
    public HttpRequestPart SetTemplates(object templates)
    {
        return SetTemplates(templates.ToDictionary());
    }

    /// <summary>
    /// 设置请求方法
    /// </summary>
    /// <param name="httpMethod"></param>
    /// <returns></returns>
    public HttpRequestPart SetHttpMethod(HttpMethod httpMethod)
    {
        if (httpMethod != null) Method = httpMethod;
        return this;
    }

    /// <summary>
    /// 设置请求报文头
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    public HttpRequestPart SetHeaders(IDictionary<string, object> headers)
    {
        if (headers != null) Headers = headers;
        return this;
    }

    /// <summary>
    /// 设置请求报文头
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    public HttpRequestPart SetHeaders(object headers)
    {
        return SetHeaders(headers.ToDictionary());
    }

    /// <summary>
    /// 设置 URL 参数
    /// </summary>
    /// <param name="queries"></param>
    /// <returns></returns>
    public HttpRequestPart SetQueries(IDictionary<string, object> queries)
    {
        if (queries != null) Queries = queries;
        return this;
    }

    /// <summary>
    /// 设置 URL 参数
    /// </summary>
    /// <param name="queries"></param>
    /// <returns></returns>
    public HttpRequestPart SetQueries(object queries)
    {
        return SetQueries(queries.ToDictionary());
    }

    /// <summary>
    /// 设置客户端分类名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public HttpRequestPart SetClient(string name)
    {
        if (!string.IsNullOrWhiteSpace(name)) ClientName = name;
        return this;
    }

    /// <summary>
    /// 设置客户端提供者
    /// </summary>
    /// <param name="clientProvider"></param>
    /// <returns></returns>
    public HttpRequestPart SetClient(Func<HttpClient> clientProvider)
    {
        ClientProvider = clientProvider;
        return this;
    }

    /// <summary>
    /// 设置内容类型
    /// </summary>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public HttpRequestPart SetContentType(string contentType)
    {
        if (!string.IsNullOrWhiteSpace(contentType)) ContentType = contentType;
        return this;
    }

    /// <summary>
    /// 设置内容编码
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public HttpRequestPart SetContentEncoding(Encoding encoding)
    {
        if (encoding != null) ContentEncoding = encoding;
        return this;
    }

    /// <summary>
    /// 设置 Body 内容
    /// </summary>
    /// <param name="body"></param>
    /// <param name="contentType"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public HttpRequestPart SetBody(object body, string contentType = default, Encoding encoding = default)
    {
        if (body != null) Body = body;
        SetContentType(contentType).SetContentEncoding(encoding);

        return this;
    }

    /// <summary>
    /// 设置 Body  Bytes
    /// </summary>
    /// <param name="bytesData"></param>
    /// <returns></returns>
    public HttpRequestPart SetBodyBytes(params (string Name, byte[] Bytes, string FileName)[] bytesData)
    {
        BodyBytes ??= new List<(string Name, byte[] Bytes, string FileName)>();
        if (bytesData != null && bytesData.Length > 0) BodyBytes.AddRange(bytesData);

        return this;
    }

    /// <summary>
    /// 设置超时时间（秒）
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public HttpRequestPart SetClientTimeout(long timeout)
    {
        if (timeout > 0) Timeout = timeout;
        return this;
    }

    /// <summary>
    /// 设置 JSON 序列化提供器
    /// </summary>
    /// <param name="providerType"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public HttpRequestPart SetJsonSerialization(Type providerType, object jsonSerializerOptions = default)
    {
        if (providerType != null) JsonSerializerProvider = providerType;
        if (jsonSerializerOptions != null) JsonSerializerOptions = jsonSerializerOptions;

        return this;
    }

    /// <summary>
    /// 设置 JSON 序列化提供器
    /// </summary>
    /// <typeparam name="TJsonSerializationProvider"></typeparam>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public HttpRequestPart SetJsonSerialization<TJsonSerializationProvider>(object jsonSerializerOptions = default)
        where TJsonSerializationProvider : IJsonSerializerProvider
    {
        return SetJsonSerialization(typeof(TJsonSerializationProvider), jsonSerializerOptions);
    }

    /// <summary>
    /// 是否启用验证状态
    /// </summary>
    /// <param name="enabled"></param>
    /// <param name="includeNull"></param>
    /// <returns></returns>
    public HttpRequestPart SetValidationState(bool enabled = true, bool includeNull = true)
    {
        ValidationState = (enabled, includeNull);
        return this;
    }

    /// <summary>
    /// 构建请求对象拦截器
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public HttpRequestPart OnRequesting(Action<HttpClient, HttpRequestMessage> action)
    {
        if (action == null) return this;
        if (!RequestInterceptors.Contains(action)) RequestInterceptors.Add(action);

        return this;
    }

    /// <summary>
    /// 创建客户端对象拦截器
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public HttpRequestPart OnClientCreating(Action<HttpClient> action)
    {
        if (action == null) return this;
        if (!HttpClientInterceptors.Contains(action)) HttpClientInterceptors.Add(action);

        return this;
    }

    /// <summary>
    /// 请求成功拦截器
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public HttpRequestPart OnResponsing(Action<HttpClient, HttpResponseMessage> action)
    {
        if (action == null) return this;
        if (!ResponseInterceptors.Contains(action)) ResponseInterceptors.Add(action);

        return this;
    }

    /// <summary>
    /// 请求异常拦截器
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public HttpRequestPart OnException(Action<HttpClient, HttpResponseMessage, string> action)
    {
        if (action == null) return this;
        if (!ExceptionInterceptors.Contains(action)) ExceptionInterceptors.Add(action);

        return this;
    }

    /// <summary>
    /// 设置请求作用域
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public HttpRequestPart SetRequestScoped(IServiceProvider serviceProvider)
    {
        if (serviceProvider != null) RequestScoped = serviceProvider;
        return this;
    }

    /// <summary>
    /// 配置重试策略
    /// </summary>
    /// <param name="numRetries"></param>
    /// <param name="retryTimeout">每次延迟时间（毫秒）</param>
    /// <returns></returns>
    public HttpRequestPart SetRetryPolicy(int numRetries, int retryTimeout = 1000)
    {
        RetryPolicy = (numRetries, retryTimeout);
        return this;
    }

    /// <summary>
    /// 启用 Gzip 压缩反压缩支持
    /// </summary>
    /// <param name="supportGzip"></param>
    /// <returns></returns>
    public HttpRequestPart WithGZip(bool supportGzip = true)
    {
        SupportGZip = supportGzip;
        return this;
    }

    /// <summary>
    /// 启用对 Url 进行 Uri.EscapeDataString
    /// </summary>
    /// <param name="encodeUrl"></param>
    /// <returns></returns>
    public HttpRequestPart WithEncodeUrl(bool encodeUrl = true)
    {
        EncodeUrl = encodeUrl;
        return this;
    }
}