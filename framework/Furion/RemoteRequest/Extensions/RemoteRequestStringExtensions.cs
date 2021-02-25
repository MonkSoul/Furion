using Furion.DependencyInjection;
using Furion.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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
        /// 发送 Get 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetAsync(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Get, null, HttpContentTypeOptions.JsonStringContent, default, headers, clientName, requestInterceptor, httpClientInterceptor, JsonNamingPolicyOptions.CamelCase, cancellationToken);
        }

        /// <summary>
        /// 发送 Get 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="deserResultOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> GetAsAsync<T>(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonSerializerOptions deserResultOptions = null, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Get, null, HttpContentTypeOptions.JsonStringContent, default, headers, clientName, requestInterceptor, httpClientInterceptor, JsonNamingPolicyOptions.CamelCase, deserResultOptions, cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> HeadAsync(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Head, null, HttpContentTypeOptions.JsonStringContent, default, headers, clientName, requestInterceptor, httpClientInterceptor, JsonNamingPolicyOptions.CamelCase, cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="deserResultOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> HeadAsAsync<T>(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonSerializerOptions deserResultOptions = null, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Head, null, HttpContentTypeOptions.JsonStringContent, default, headers, clientName, requestInterceptor, httpClientInterceptor, JsonNamingPolicyOptions.CamelCase, deserResultOptions, cancellationToken);
        }

        /// <summary>
        /// 发送 Post 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Post, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Post 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="deserResultOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PostAsAsync<T>(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, JsonSerializerOptions deserResultOptions = null, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Post, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, deserResultOptions, cancellationToken);
        }

        /// <summary>
        /// 发送 Put 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutAsync(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Put, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Put 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="deserResultOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PutAsAsync<T>(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, JsonSerializerOptions deserResultOptions = null, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Put, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, deserResultOptions, cancellationToken);
        }

        /// <summary>
        /// 发送 Delete 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Delete, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Delete 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="deserResultOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> DeleteAsAsync<T>(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, JsonSerializerOptions deserResultOptions = null, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Delete, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, deserResultOptions, cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PatchAsync(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Patch, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="deserResultOptions">自定义反序列化结果的配置</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PatchAsAsync<T>(this string requestUri, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, JsonSerializerOptions deserResultOptions = null, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Patch, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, deserResultOptions, cancellationToken);
        }

        /// <summary>
        /// 发送 Http 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="deserResultOptions">自定义反序列化结果的配置</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> SendAsAsync<T>(this string requestUri, HttpMethod httpMethod = default, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, JsonSerializerOptions deserResultOptions = null, CancellationToken cancellationToken = default)
        {
            var response = await requestUri.SendAsync(httpMethod, bodyArgs, httpContentType, contentType, headers, clientName, requestInterceptor, httpClientInterceptor, propertyNamingPolicy, cancellationToken);

            // 读取流
            using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

            // 反序列化结果
            var result = await JsonSerializer.DeserializeAsync<T>(responseStream, deserResultOptions ??= JsonSerializerUtility.GetDefaultJsonSerializerOptions(), cancellationToken);
            return result;
        }

        /// <summary>
        /// 发送 Http 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="httpContentType"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="requestInterceptor"></param>
        /// <param name="httpClientInterceptor"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(this string requestUri, HttpMethod httpMethod = default, object bodyArgs = null, HttpContentTypeOptions httpContentType = HttpContentTypeOptions.JsonStringContent, string contentType = "application/json", Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> requestInterceptor = default, Action<HttpClient> httpClientInterceptor = default, JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            // 检查 method
            if (httpMethod == null) throw new ArgumentNullException(nameof(httpMethod));

            // 创建请求对象
            var request = new HttpRequestMessage(httpMethod, requestUri);

            // 设置请求报文头
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // 设置请求报文参数，排除Get和Head请求
            if (httpMethod != HttpMethod.Get && httpMethod != HttpMethod.Head && bodyArgs != null)
            {
                Penetrates.SetHttpRequestBody(request, bodyArgs, httpContentType, propertyNamingPolicy, contentType);
            }

            // 请求之前拦截
            requestInterceptor?.Invoke(request);

            // 打印请求地址
            App.PrintToMiniProfiler(Penetrates.MiniProfilerCategory, "Beginning", $"{request.Method} {request.RequestUri}");

            // 创建 HttpClient 对象
            var clientFactory = App.GetService<IHttpClientFactory>();
            if (clientFactory == null) throw new InvalidOperationException("Please register for RemoteRequest service first: services.AddRemoteRequest();");

            var httpClient = string.IsNullOrEmpty(clientName)
                                        ? clientFactory.CreateClient()
                                        : clientFactory.CreateClient(clientName);

            // 拦截客户端
            httpClientInterceptor?.Invoke(httpClient);

            // 发送请求
            var response = await httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode) return response;
            else throw (await Penetrates.CreateRequestException(response));
        }
    }
}