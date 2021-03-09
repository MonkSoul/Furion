using Furion.DependencyInjection;
using Furion.JsonSerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.RemoteRequest.Extensions
{
    /// <summary>
    /// 远程请求字符串拓展
    /// </summary>
    [SkipScan]
    public static class RemoteRequestStringExtensions
    {
        /// <summary>
        /// 设置请求方法
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public static HttpClientPart SetHttpMethod(this string requestUrl, HttpMethod httpMethod)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetHttpMethod(httpMethod);
        }

        /// <summary>
        /// 设置请求报文头
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpClientPart SetHeaders(this string requestUrl, Dictionary<string, string> headers)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetHeaders(headers);
        }

        /// <summary>
        /// 设置请求报文头
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpClientPart SetHeaders(this string requestUrl, object headers)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetHeaders(headers);
        }

        /// <summary>
        /// 设置 URL 参数
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="queries"></param>
        /// <returns></returns>
        public static HttpClientPart SetQueries(this string requestUrl, Dictionary<string, string> queries)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetQueries(queries);
        }

        /// <summary>
        /// 设置 URL 参数
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="queries"></param>
        /// <returns></returns>
        public static HttpClientPart SetQueries(this string requestUrl, object queries)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetQueries(queries);
        }

        /// <summary>
        /// 设置客户端分类名
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpClientPart SetClient(this string requestUrl, string name)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetClient(name);
        }

        /// <summary>
        /// 设置 Body 内容
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static HttpClientPart SetBody(this string requestUrl, object body, string contentType = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetBody(body, contentType);
        }

        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static HttpClientPart SetContentType(this string requestUrl, string contentType)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetContentType(contentType);
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <typeparam name="TJsonSerializationProvider"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public static HttpClientPart SetJsonSerializationProvider<TJsonSerializationProvider>(this string requestUrl, object jsonSerializerOptions)
            where TJsonSerializationProvider : IJsonSerializerProvider
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetJsonSerializationProvider<TJsonSerializationProvider>(jsonSerializerOptions);
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="jsonSerializationProvider"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public static HttpClientPart SetJsonSerializationProvider(this string requestUrl, Type jsonSerializationProvider, object jsonSerializerOptions)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetJsonSerializationProvider(jsonSerializationProvider, jsonSerializerOptions);
        }

        /// <summary>
        /// 是否启用验证状态
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public static HttpClientPart SetValidationState(this string requestUrl, bool enabled)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetValidationState(enabled);
        }

        /// <summary>
        /// 构建请求对象拦截器
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static HttpClientPart OnRequesting(this string requestUrl, Action<HttpRequestMessage> action)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).OnRequesting(action);
        }

        /// <summary>
        /// 创建客户端对象拦截器
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static HttpClientPart OnClientCreating(this string requestUrl, Action<HttpClient> action)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).OnClientCreating(action);
        }

        /// <summary>
        /// 请求成功拦截器
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static HttpClientPart OnResponsing(this string requestUrl, Action<HttpResponseMessage> action)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).OnResponsing(action);
        }

        /// <summary>
        /// 请求异常拦截器
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static HttpClientPart OnException(this string requestUrl, Action<HttpResponseMessage, string> action)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).OnException(action);
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
            return new HttpClientPart().SetRequestUrl(requestUrl).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送请求返回 Stream
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Stream> SendAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> SendAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SendAsync(cancellationToken);
        }
    }
}