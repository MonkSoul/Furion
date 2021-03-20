using Furion.Extensions;
using Furion.JsonSerialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpClient 对象组装部件
    /// </summary>
    public sealed partial class HttpClientPart
    {
        /// <summary>
        /// 设置请求地址
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public HttpClientPart SetRequestUrl(string requestUrl)
        {
            RequestUrl = requestUrl;
            return this;
        }

        /// <summary>
        /// 设置请求方法
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public HttpClientPart SetHttpMethod(HttpMethod httpMethod)
        {
            Method = httpMethod;
            return this;
        }

        /// <summary>
        /// 设置请求报文头
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpClientPart SetHeaders(Dictionary<string, string> headers)
        {
            Headers = headers;
            return this;
        }

        /// <summary>
        /// 设置请求报文头
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpClientPart SetHeaders(object headers)
        {
            Headers = headers.ToDictionary<string>();
            return this;
        }

        /// <summary>
        /// 设置 URL 参数
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        public HttpClientPart SetQueries(Dictionary<string, string> queries)
        {
            Queries = queries;
            return this;
        }

        /// <summary>
        /// 设置 URL 参数
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        public HttpClientPart SetQueries(object queries)
        {
            Queries = queries.ToDictionary<string>();
            return this;
        }

        /// <summary>
        /// 设置客户端分类名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HttpClientPart SetClient(string name)
        {
            ClientName = name;
            return this;
        }

        /// <summary>
        /// 设置 Body 内容
        /// </summary>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public HttpClientPart SetBody(object body, string contentType = default, Encoding encoding = default)
        {
            Body = body;

            if (!string.IsNullOrEmpty(contentType)) ContentType = contentType;
            ContentEncoding = encoding ?? Encoding.UTF8;

            return this;
        }

        /// <summary>
        /// 设置内容类型
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public HttpClientPart SetContentType(string contentType)
        {
            ContentType = contentType;
            return this;
        }

        /// <summary>
        /// 设置内容编码
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public HttpClientPart SetContentEncoding(Encoding encoding)
        {
            ContentEncoding = encoding;
            return this;
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <typeparam name="TJsonSerializationProvider"></typeparam>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public HttpClientPart SetJsonSerializationProvider<TJsonSerializationProvider>(object jsonSerializerOptions)
            where TJsonSerializationProvider : IJsonSerializerProvider
        {
            JsonSerializationProvider = (typeof(TJsonSerializationProvider), jsonSerializerOptions);
            return this;
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <param name="jsonSerializationProvider"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public HttpClientPart SetJsonSerializationProvider(Type jsonSerializationProvider, object jsonSerializerOptions)
        {
            JsonSerializationProvider = (jsonSerializationProvider, jsonSerializerOptions);
            return this;
        }

        /// <summary>
        /// 是否启用验证状态
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="includeNull"></param>
        /// <returns></returns>
        public HttpClientPart SetValidationState(bool enabled = true, bool includeNull = true)
        {
            ValidationState = (enabled, includeNull);
            return this;
        }

        /// <summary>
        /// 构建请求对象拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart OnRequesting(Action<HttpRequestMessage> action)
        {
            RequestInspector = action;
            return this;
        }

        /// <summary>
        /// 创建客户端对象拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart OnClientCreating(Action<HttpClient> action)
        {
            HttpClientInspector = action;
            return this;
        }

        /// <summary>
        /// 请求成功拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart OnResponsing(Action<HttpResponseMessage> action)
        {
            ResponseInspector = action;
            return this;
        }

        /// <summary>
        /// 请求异常拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart OnException(Action<HttpResponseMessage, string> action)
        {
            ExceptionInspector = action;
            return this;
        }
    }
}