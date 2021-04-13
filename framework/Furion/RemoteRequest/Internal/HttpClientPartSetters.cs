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
        /// 设置 URL 模板
        /// </summary>
        /// <param name="templates"></param>
        /// <returns></returns>
        public HttpClientPart SetTemplates(Dictionary<string, object> templates)
        {
            Templates = templates;
            return this;
        }

        /// <summary>
        /// 设置 URL 模板
        /// </summary>
        /// <param name="templates"></param>
        /// <returns></returns>
        public HttpClientPart SetTemplates(object templates)
        {
            Templates = templates.ToDictionary<object>();
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
        public HttpClientPart SetHeaders(Dictionary<string, object> headers)
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
            Headers = headers.ToDictionary<object>();
            return this;
        }

        /// <summary>
        /// 设置 URL 参数
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        public HttpClientPart SetQueries(Dictionary<string, object> queries)
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
            Queries = queries.ToDictionary<object>();
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

            if (!string.IsNullOrWhiteSpace(contentType)) ContentType = contentType;
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
        public HttpClientPart SetBody(Dictionary<string, object> body, string contentType = default, Encoding encoding = default)
        {
            Body = body;

            if (!string.IsNullOrWhiteSpace(contentType)) ContentType = contentType;
            if (encoding != null) ContentEncoding = encoding;

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
        /// 设置 Body  Bytes
        /// </summary>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public HttpClientPart SetBodyBytes(params (string Name, byte[] Bytes, string FileName)[] bytesData)
        {
            BodyBytes ??= new List<(string Name, byte[] Bytes, string FileName)>();
            if (bytesData != null && bytesData.Length > 0) BodyBytes.AddRange(bytesData);

            return this;
        }

        /// <summary>
        /// 设置 Body  Bytes
        /// </summary>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public HttpClientPart SetBodyBytes(List<(string Name, byte[] Bytes, string FileName)> bytesData)
        {
            return SetBodyBytes(bytesData?.ToArray());
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <typeparam name="TJsonSerializationProvider"></typeparam>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public HttpClientPart SetJsonSerialization<TJsonSerializationProvider>(object jsonSerializerOptions = default)
            where TJsonSerializationProvider : IJsonSerializerProvider
        {
            JsonSerialization = (typeof(TJsonSerializationProvider), jsonSerializerOptions);
            return this;
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <param name="providerType"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public HttpClientPart SetJsonSerialization(Type providerType, object jsonSerializerOptions = default)
        {
            JsonSerialization = (providerType, jsonSerializerOptions);
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
            RequestInterceptors ??= new List<Action<HttpRequestMessage>>();
            if (action != null && !RequestInterceptors.Contains(action)) RequestInterceptors.Add(action);
            return this;
        }

        /// <summary>
        /// 创建客户端对象拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart OnClientCreating(Action<HttpClient> action)
        {
            HttpClientInterceptors ??= new List<Action<HttpClient>>();
            if (action != null && !HttpClientInterceptors.Contains(action)) HttpClientInterceptors.Add(action);
            return this;
        }

        /// <summary>
        /// 请求成功拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart OnResponsing(Action<HttpResponseMessage> action)
        {
            ResponseInterceptors ??= new List<Action<HttpResponseMessage>>();
            if (action != null && !ResponseInterceptors.Contains(action)) ResponseInterceptors.Add(action);
            return this;
        }

        /// <summary>
        /// 请求异常拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart OnException(Action<HttpResponseMessage, string> action)
        {
            ExceptionInterceptors ??= new List<Action<HttpResponseMessage, string>>();
            if (action != null && !ExceptionInterceptors.Contains(action)) ExceptionInterceptors.Add(action);
            return this;
        }
    }
}