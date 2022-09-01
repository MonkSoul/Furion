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

using Furion.JsonSerialization;
using System.Text;

namespace Furion.RemoteRequest.Extensions;

/// <summary>
/// 远程请求字符串拓展
/// </summary>
[SuppressSniffer]
public static class RemoteRequestStringExtensions
{
    /// <summary>
    /// 设置 URL 模板
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="templates"></param>
    /// <returns></returns>
    public static HttpRequestPart SetTemplates(this string requestUrl, IDictionary<string, object> templates)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetTemplates(templates);
    }

    /// <summary>
    /// 设置 URL 模板
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="templates"></param>
    /// <returns></returns>
    public static HttpRequestPart SetTemplates(this string requestUrl, object templates)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetTemplates(templates);
    }

    /// <summary>
    /// 设置请求方法
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="httpMethod"></param>
    /// <returns></returns>
    public static HttpRequestPart SetHttpMethod(this string requestUrl, HttpMethod httpMethod)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetHttpMethod(httpMethod);
    }

    /// <summary>
    /// 设置请求报文头
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static HttpRequestPart SetHeaders(this string requestUrl, IDictionary<string, object> headers)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetHeaders(headers);
    }

    /// <summary>
    /// 设置请求报文头
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static HttpRequestPart SetHeaders(this string requestUrl, object headers)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetHeaders(headers);
    }

    /// <summary>
    /// 设置 URL 参数
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="queries"></param>
    /// <returns></returns>
    public static HttpRequestPart SetQueries(this string requestUrl, IDictionary<string, object> queries)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetQueries(queries);
    }

    /// <summary>
    /// 设置 URL 参数
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="queries"></param>
    /// <returns></returns>
    public static HttpRequestPart SetQueries(this string requestUrl, object queries)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetQueries(queries);
    }

    /// <summary>
    /// 设置客户端分类名
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static HttpRequestPart SetClient(this string requestUrl, string name)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetClient(name);
    }

    /// <summary>
    /// 设置客户端提供者
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="clientProvider"></param>
    /// <returns></returns>
    public static HttpRequestPart SetClient(this string requestUrl, Func<HttpClient> clientProvider)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetClient(clientProvider);
    }

    /// <summary>
    /// 设置 Body 内容
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="body"></param>
    /// <param name="contentType"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static HttpRequestPart SetBody(this string requestUrl, object body, string contentType = default, Encoding encoding = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetBody(body, contentType, encoding);
    }

    /// <summary>
    /// 设置内容类型
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public static HttpRequestPart SetContentType(this string requestUrl, string contentType)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetContentType(contentType);
    }

    /// <summary>
    /// 设置内容编码
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static HttpRequestPart SetContentEncoding(this string requestUrl, Encoding encoding)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetContentEncoding(encoding);
    }

    /// <summary>
    /// 设置 Body  Bytes
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="bytesData"></param>
    /// <returns></returns>
    public static HttpRequestPart SetBodyBytes(this string requestUrl, params (string Name, byte[] Bytes, string FileName)[] bytesData)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetBodyBytes(bytesData);
    }

    /// <summary>
    /// 设置超时时间（秒）
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static HttpRequestPart SetClientTimeout(this string requestUrl, long timeout)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetClientTimeout(timeout);
    }

    /// <summary>
    /// 设置 JSON 序列化提供器
    /// </summary>
    /// <typeparam name="TJsonSerializationProvider"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public static HttpRequestPart SetJsonSerialization<TJsonSerializationProvider>(this string requestUrl, object jsonSerializerOptions = default)
        where TJsonSerializationProvider : IJsonSerializerProvider
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetJsonSerialization<TJsonSerializationProvider>(jsonSerializerOptions);
    }

    /// <summary>
    /// 设置 JSON 序列化提供器
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="providerType"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public static HttpRequestPart SetJsonSerialization(this string requestUrl, Type providerType, object jsonSerializerOptions = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetJsonSerialization(providerType, jsonSerializerOptions);
    }

    /// <summary>
    /// 是否启用验证状态
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="enabled"></param>
    /// <param name="includeNull"></param>
    /// <returns></returns>
    public static HttpRequestPart SetValidationState(this string requestUrl, bool enabled, bool includeNull = true)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetValidationState(enabled, includeNull);
    }

    /// <summary>
    /// 设置请求作用域
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static HttpRequestPart SetRequestScoped(this string requestUrl, IServiceProvider serviceProvider)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetRequestScoped(serviceProvider);
    }

    /// <summary>
    /// 构建请求对象拦截器
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static HttpRequestPart OnRequesting(this string requestUrl, Action<HttpClient, HttpRequestMessage> action)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).OnRequesting(action);
    }

    /// <summary>
    /// 创建客户端对象拦截器
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static HttpRequestPart OnClientCreating(this string requestUrl, Action<HttpClient> action)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).OnClientCreating(action);
    }

    /// <summary>
    /// 请求成功拦截器
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static HttpRequestPart OnResponsing(this string requestUrl, Action<HttpClient, HttpResponseMessage> action)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).OnResponsing(action);
    }

    /// <summary>
    /// 请求异常拦截器
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static HttpRequestPart OnException(this string requestUrl, Action<HttpClient, HttpResponseMessage, string> action)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).OnException(action);
    }

    /// <summary>
    /// 配置重试策略
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="numRetries"></param>
    /// <param name="retryTimeout">每次延迟时间（毫秒）</param>
    /// <returns></returns>
    public static HttpRequestPart SetRetryPolicy(this string requestUrl, int numRetries, int retryTimeout = 1000)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SetRetryPolicy(numRetries, retryTimeout);
    }

    /// <summary>
    /// 启用 Gzip 压缩反压缩支持
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="supportGZip"></param>
    /// <returns></returns>
    public static HttpRequestPart WithGZip(this string requestUrl, bool supportGZip = true)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).WithGZip(supportGZip);
    }

    /// <summary>
    /// 启用对 Url 进行 Uri.EscapeDataString
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="encodeUrl"></param>
    /// <returns></returns>
    public static HttpRequestPart WithEncodeUrl(this string requestUrl, bool encodeUrl = true)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).WithEncodeUrl(encodeUrl);
    }

    /// <summary>
    /// 发送 GET 请求返回 T 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<T> GetAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).GetAsAsync<T>(cancellationToken);
    }

    /// <summary>
    /// 发送 GET 请求返回 Stream
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Stream> GetAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).GetAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 GET 请求返回 String
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<string> GetAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).GetAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 GET 请求返回 ByteArray
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<byte[]> GetAsByteArrayAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).GetAsByteArrayAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 GET 请求
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> GetAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).GetAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求返回 T 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<T> PostAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PostAsAsync<T>(cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求返回 Stream
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Stream> PostAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PostAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求返回 String
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<string> PostAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PostAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求返回 ByteArray
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<byte[]> PostAsByteArrayAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PostAsByteArrayAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> PostAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PostAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PUT 请求返回 T 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<T> PutAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PutAsAsync<T>(cancellationToken);
    }

    /// <summary>
    /// 发送 PUT 请求返回 Stream
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Stream> PutAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PutAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PUT 请求返回 String
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<string> PutAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PutAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PUT 请求返回 ByteArray
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<byte[]> PutAsByteArrayAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PutAsByteArrayAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PUT 请求
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> PutAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PutAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 DELETE 请求返回 T 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<T> DeleteAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).DeleteAsAsync<T>(cancellationToken);
    }

    /// <summary>
    /// 发送 DELETE 请求返回 Stream
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Stream> DeleteAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).DeleteAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 DELETE 请求返回 String
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<string> DeleteAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).DeleteAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 DELETE 请求返回 ByteArray
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<byte[]> DeleteAsByteArrayAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).DeleteAsByteArrayAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 DELETE 请求
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> DeleteAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).DeleteAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PATCH 请求返回 T 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<T> PatchAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PatchAsAsync<T>(cancellationToken);
    }

    /// <summary>
    /// 发送 PATCH 请求返回 Stream
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Stream> PatchAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PatchAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PATCH 请求返回 String
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<string> PatchAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PatchAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PATCH 请求返回 ByteArray
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<byte[]> PatchAsByteArrayAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PatchAsByteArrayAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PATCH 请求
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> PatchAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).PatchAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 HEAD 请求返回 T 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<T> HeadAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).HeadAsAsync<T>(cancellationToken);
    }

    /// <summary>
    /// 发送 HEAD 请求返回 Stream
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Stream> HeadAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).HeadAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 HEAD 请求返回 String
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<string> HeadAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).HeadAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 HEAD 请求返回 ByteArray
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<byte[]> HeadAsByteArrayAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).HeadAsByteArrayAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 HEAD 请求
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> HeadAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).HeadAsync(cancellationToken);
    }

    /// <summary>
    /// 发送请求返回 T 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<T> SendAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SendAsAsync<T>(cancellationToken);
    }

    /// <summary>
    /// 发送请求返回 Stream
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<Stream> SendAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SendAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送请求返回 String
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<string> SendAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SendAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// 发送请求返回 ByteArray
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<byte[]> SendAsByteArrayAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SendAsByteArrayAsync(cancellationToken);
    }

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> SendAsync(this string requestUrl, CancellationToken cancellationToken = default)
    {
        return HttpRequestPart.Default().SetRequestUrl(requestUrl).SendAsync(cancellationToken);
    }
}