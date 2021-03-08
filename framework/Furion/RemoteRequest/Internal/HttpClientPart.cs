using Furion.Extensions;
using Furion.JsonSerialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpClient 对象组装部件
    /// </summary>
    public sealed class HttpClientPart
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; private set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public HttpMethod HttpMethod { get; private set; }

        /// <summary>
        /// 请求报文头
        /// </summary>
        public Dictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public Dictionary<string, string> Queries { get; private set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; private set; }

        /// <summary>
        /// 请求报文 Body 参数
        /// </summary>
        public object Body { get; private set; }

        /// <summary>
        /// 请求报文 Body 内容类型
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// Json 序列化提供器
        /// </summary>
        public (Type ProviderType, object JsonSerializerOptions) JsonSerializationProvider { get; private set; } = (typeof(SystemTextJsonSerializerProvider), default);

        /// <summary>
        /// 是否启用模型验证
        /// </summary>
        public bool ValidationState { get; private set; }

        /// <summary>
        /// 请求之前（可以配置更多操作）
        /// </summary>
        public Action<HttpRequestMessage> OnRequesting { get; private set; }

        /// <summary>
        /// 请求成功之前，如果返回参数不为空，则作为最终返回值输出
        /// </summary>
        public Func<HttpResponseMessage, object> OnResponsing { get; private set; }

        /// <summary>
        /// 请求异常（如果设置了返回参数，则异常时返回该参数）
        /// </summary>
        public Func<object> OnException { get; private set; }

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
        /// <returns></returns>
        public HttpClientPart SetBody(object body, string contentType = default)
        {
            Body = body;
            if (!string.IsNullOrEmpty(contentType)) ContentType = contentType;
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
        /// <typeparam name="TJsonSerializationProvider"></typeparam>
        /// <param name="jsonSerializationProvider"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        public HttpClientPart SetJsonSerializationProvider<TJsonSerializationProvider>(Type jsonSerializationProvider, object jsonSerializerOptions)
            where TJsonSerializationProvider : IJsonSerializerProvider
        {
            JsonSerializationProvider = (jsonSerializationProvider, jsonSerializerOptions);
            return this;
        }

        /// <summary>
        /// 是否启用验证状态
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public HttpClientPart SetValidationState(bool enabled)
        {
            this.ValidationState = enabled;
            return this;
        }

        /// <summary>
        /// 请求拦截（可以配置更多操作）
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public HttpClientPart SetRequestingInspector(Action<HttpRequestMessage> action)
        {
            OnRequesting = action;
            return this;
        }

        /// <summary>
        /// 请求成功之前，如果返回参数不为空，则作为最终返回值输出
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public HttpClientPart SetResponsingInspector(Func<HttpResponseMessage, object> func)
        {
            OnResponsing = func;
            return this;
        }

        /// <summary>
        /// 请求异常（如果设置了返回参数，则异常时返回该参数）
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public HttpClientPart SetExceptionInspector(Func<object> func)
        {
            OnException = func;
            return this;
        }

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
            HttpMethod = httpMethod;
            return this;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        public Task<HttpResponseMessage> SendAsync()
        {
            // 检查是否配置了请求方法
            if (HttpMethod == null) throw new NullReferenceException(nameof(HttpMethod));

            // 检查请求地址
            if (string.IsNullOrEmpty(RequestUrl)) throw new NullReferenceException(RequestUrl);

            // 构建请求对象
            var request = new HttpRequestMessage(HttpMethod, RequestUrl);

            // 设置请求报文头
            if (Headers != null && Headers.Count > 0)
            {
                foreach (var header in Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            return null;
        }
    }
}