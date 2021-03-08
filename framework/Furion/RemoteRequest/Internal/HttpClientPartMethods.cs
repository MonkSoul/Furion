using Furion.Extensions;
using Furion.JsonSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
            HttpMethod = httpMethod;
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
        /// <returns></returns>
        public HttpClientPart SetValidationState(bool enabled)
        {
            this.ValidationState = enabled;
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

        /// <summary>
        /// 发送请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendAsAsync<T>(CancellationToken cancellationToken = default)
        {
            var response = await SendAsync(cancellationToken);

            // 如果配置了异常拦截器，且请求不成功，则返回 T 默认值
            if (ExceptionInspector != null && !response.IsSuccessStatusCode) return default;

            // 读取响应流
            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            // 解析 Json 序列化提供器
            var jsonSerializer = App.GetService(JsonSerializationProvider.ProviderType ?? typeof(SystemTextJsonSerializerProvider)) as IJsonSerializerProvider;

            // 反序列化流
            var result = await jsonSerializer.DeserializeAsync<T>(stream, JsonSerializationProvider.JsonSerializerOptions, cancellationToken);
            return result;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(CancellationToken cancellationToken = default)
        {
            // 检查是否配置了请求方法
            if (HttpMethod == null) throw new NullReferenceException(nameof(HttpMethod));

            // 检查请求地址
            if (string.IsNullOrEmpty(RequestUrl)) throw new NullReferenceException(RequestUrl);

            var finalRequestUrl = RequestUrl;

            // 拼接查询参数
            if (Queries != null && Queries.Count > 0)
            {
                var urlParameters = Queries.Select(u => u.Key + "=" + u.Value);
                finalRequestUrl += $"?{string.Join("&", urlParameters)}";
            }

            // 构建请求对象
            var request = new HttpRequestMessage(HttpMethod, finalRequestUrl);

            // 设置请求报文头
            if (Headers != null && Headers.Count > 0)
            {
                foreach (var header in Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // 配置 Body 内容

            // 配置请求拦截
            RequestInspector?.Invoke(request);

            // 创建客户端请求工厂
            var clientFactory = App.GetService<IHttpClientFactory>();

            // 创建 HttpClient 对象
            var httpClient = string.IsNullOrEmpty(ClientName)
                                        ? clientFactory.CreateClient()
                                        : clientFactory.CreateClient(ClientName);

            // 配置 HttpClient 拦截
            HttpClientInspector?.Invoke(httpClient);

            // 发送请求
            var response = await httpClient.SendAsync(request, cancellationToken);

            // 请求成功
            if (response.IsSuccessStatusCode)
            {
                // 调用成功拦截器
                ResponseInspector?.Invoke(response);
            }
            // 请求异常
            else
            {
                // 读取错误消息
                var errors = await response.Content.ReadAsStringAsync(cancellationToken);

                // 抛出异常
                if (ExceptionInspector == null) throw new HttpRequestException(errors);
                // 调用异常拦截器
                else ExceptionInspector?.Invoke(response, errors);
            }

            return response;
        }
    }
}