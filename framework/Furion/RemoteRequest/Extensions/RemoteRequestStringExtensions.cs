using Furion.DependencyInjection;
using Furion.JsonSerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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
        /// 设置 URL 模板
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="templates"></param>
        /// <returns></returns>
        public static HttpClientPart SetTemplates(this string requestUrl, Dictionary<string, object> templates)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetTemplates(templates);
        }

        /// <summary>
        /// 设置 URL 模板
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="templates"></param>
        /// <returns></returns>
        public static HttpClientPart SetTemplates(this string requestUrl, object templates)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetTemplates(templates);
        }

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
        public static HttpClientPart SetHeaders(this string requestUrl, Dictionary<string, object> headers)
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
        public static HttpClientPart SetQueries(this string requestUrl, Dictionary<string, object> queries)
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
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static HttpClientPart SetBody(this string requestUrl, object body, string contentType = default, Encoding encoding = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetBody(body, contentType, encoding);
        }

        /// <summary>
        /// 设置 Body 内容
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static HttpClientPart SetBody(this string requestUrl, Dictionary<string, object> body, string contentType = default, Encoding encoding = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetBody(body, contentType, encoding);
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
        /// 设置内容编码
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static HttpClientPart SetContentEncoding(this string requestUrl, Encoding encoding)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetContentEncoding(encoding);
        }

        /// <summary>
        /// 设置 Body  Bytes
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public static HttpClientPart SetBodyBytes(this string requestUrl, params (string Name, byte[] Bytes, string FileName)[] bytesData)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetBodyBytes(bytesData);
        }

        /// <summary>
        /// 设置 Body  Bytes
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public static HttpClientPart SetBodyBytes(this string requestUrl, List<(string Name, byte[] Bytes, string FileName)> bytesData)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetBodyBytes(bytesData);
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <typeparam name="TJsonSerializationProvider"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public static HttpClientPart SetJsonSerialization<TJsonSerializationProvider>(this string requestUrl, object jsonSerializerOptions = default)
            where TJsonSerializationProvider : IJsonSerializerProvider
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetJsonSerialization<TJsonSerializationProvider>(jsonSerializerOptions);
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="providerType"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public static HttpClientPart SetJsonSerialization(this string requestUrl, Type providerType, object jsonSerializerOptions = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SetJsonSerialization(providerType, jsonSerializerOptions);
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
        /// 发送 GET 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<T> GetAsAsync<T>(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).GetAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求返回 Stream
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Stream> GetAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).GetAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求返回 String
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> GetAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).GetAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> GetAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).GetAsync(cancellationToken);
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
            return new HttpClientPart().SetRequestUrl(requestUrl).PostAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 Stream
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Stream> PostAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PostAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 String
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> PostAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PostAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PostAsync(cancellationToken);
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
            return new HttpClientPart().SetRequestUrl(requestUrl).PutAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 Stream
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Stream> PutAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PutAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 String
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> PutAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PutAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PutAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PutAsync(cancellationToken);
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
            return new HttpClientPart().SetRequestUrl(requestUrl).DeleteAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 Stream
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Stream> DeleteAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).DeleteAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 String
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> DeleteAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).DeleteAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> DeleteAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).DeleteAsync(cancellationToken);
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
            return new HttpClientPart().SetRequestUrl(requestUrl).PatchAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求返回 Stream
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Stream> PatchAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PatchAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求返回 String
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> PatchAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PatchAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PatchAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).PatchAsync(cancellationToken);
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
            return new HttpClientPart().SetRequestUrl(requestUrl).HeadAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求返回 Stream
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Stream> HeadAsStreamAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).HeadAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求返回 String
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> HeadAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).HeadAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> HeadAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).HeadAsync(cancellationToken);
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
        /// 发送请求返回 String
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> SendAsStringAsync(this string requestUrl, CancellationToken cancellationToken = default)
        {
            return new HttpClientPart().SetRequestUrl(requestUrl).SendAsStringAsync(cancellationToken);
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