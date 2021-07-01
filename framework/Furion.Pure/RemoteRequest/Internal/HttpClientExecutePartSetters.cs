// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.ClayObject.Extensions;
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
    public sealed partial class HttpClientExecutePart
    {
        /// <summary>
        /// 设置请求地址
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetRequestUrl(string requestUrl)
        {
            if (!string.IsNullOrWhiteSpace(requestUrl) && requestUrl.StartsWith("/"))
            {
                requestUrl = requestUrl[1..];
            }

            RequestUrl = requestUrl;
            return this;
        }

        /// <summary>
        /// 设置 URL 模板
        /// </summary>
        /// <param name="templates"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetTemplates(IDictionary<string, object> templates)
        {
            Templates = templates;
            return this;
        }

        /// <summary>
        /// 设置 URL 模板
        /// </summary>
        /// <param name="templates"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetTemplates(object templates)
        {
            if (templates == null) return this;

            Templates = templates.ToDictionary();
            return this;
        }

        /// <summary>
        /// 设置请求方法
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetHttpMethod(HttpMethod httpMethod)
        {
            Method = httpMethod;
            return this;
        }

        /// <summary>
        /// 设置请求报文头
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetHeaders(IDictionary<string, object> headers)
        {
            Headers = headers;
            return this;
        }

        /// <summary>
        /// 设置请求报文头
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetHeaders(object headers)
        {
            if (headers == null) return this;

            Headers = headers.ToDictionary();
            return this;
        }

        /// <summary>
        /// 设置 URL 参数
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetQueries(IDictionary<string, object> queries)
        {
            Queries = queries;
            return this;
        }

        /// <summary>
        /// 设置 URL 参数
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetQueries(object queries)
        {
            if (queries == null) return this;

            Queries = queries.ToDictionary();
            return this;
        }

        /// <summary>
        /// 设置客户端分类名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetClient(string name)
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
        public HttpClientExecutePart SetBody(object body, string contentType = default, Encoding encoding = default)
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
        public HttpClientExecutePart SetBody(IDictionary<string, object> body, string contentType = default, Encoding encoding = default)
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
        public HttpClientExecutePart SetContentType(string contentType)
        {
            ContentType = contentType;
            return this;
        }

        /// <summary>
        /// 设置内容编码
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetContentEncoding(Encoding encoding)
        {
            ContentEncoding = encoding;
            return this;
        }

        /// <summary>
        /// 设置 Body  Bytes
        /// </summary>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetBodyBytes(params (string Name, byte[] Bytes, string FileName)[] bytesData)
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
        public HttpClientExecutePart SetBodyBytes(List<(string Name, byte[] Bytes, string FileName)> bytesData)
        {
            return SetBodyBytes(bytesData?.ToArray());
        }

        /// <summary>
        /// 设置超时时间（分钟）
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetTimeout(long timeout)
        {
            Timeout = timeout;
            return this;
        }

        /// <summary>
        /// 设置 JSON 序列化提供器
        /// </summary>
        /// <typeparam name="TJsonSerializationProvider"></typeparam>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetJsonSerialization<TJsonSerializationProvider>(object jsonSerializerOptions = default)
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
        public HttpClientExecutePart SetJsonSerialization(Type providerType, object jsonSerializerOptions = default)
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
        public HttpClientExecutePart SetValidationState(bool enabled = true, bool includeNull = true)
        {
            ValidationState = (enabled, includeNull);
            return this;
        }

        /// <summary>
        /// 构建请求对象拦截器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientExecutePart OnRequesting(Action<HttpRequestMessage> action)
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
        public HttpClientExecutePart OnClientCreating(Action<HttpClient> action)
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
        public HttpClientExecutePart OnResponsing(Action<HttpResponseMessage> action)
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
        public HttpClientExecutePart OnException(Action<HttpResponseMessage, string> action)
        {
            ExceptionInterceptors ??= new List<Action<HttpResponseMessage, string>>();
            if (action != null && !ExceptionInterceptors.Contains(action)) ExceptionInterceptors.Add(action);
            return this;
        }

        /// <summary>
        /// 设置请求作用域
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public HttpClientExecutePart SetRequestScoped(IServiceProvider serviceProvider)
        {
            RequestScoped = serviceProvider;
            return this;
        }
    }
}