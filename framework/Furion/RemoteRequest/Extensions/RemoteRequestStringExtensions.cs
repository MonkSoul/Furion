using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "httpRequest";

        /// <summary>
        /// 发送 Get 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetAsync(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Get, null, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Get 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> GetAsAsync<T>(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Get, null, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> HeadAsync(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Head, null, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> HeadAsAsync<T>(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Head, null, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Post 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Post, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Post 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PostAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Post, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Put 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Put, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Put 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PutAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Put, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Delete 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Delete, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Delete 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> DeleteAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Delete, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PatchAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Patch, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PatchAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Patch, bodyArgs, headers, clientName, interceptor, cancellationToken);
        }

        /// <summary>
        /// 发送 Http 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> SendAsAsync<T>(this string requestUri, HttpMethod httpMethod = default, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            var response = await requestUri.SendAsync(httpMethod, bodyArgs, headers, clientName, interceptor, cancellationToken);

            // 读取流
            using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

            // 反序列化结果
            var result = await JsonSerializer.DeserializeAsync<T>(responseStream, JsonSerializerUtility.GetDefaultJsonSerializerOptions(), cancellationToken);
            return result;
        }

        /// <summary>
        /// 发送 Http 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(this string requestUri, HttpMethod httpMethod = default, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, CancellationToken cancellationToken = default)
        {
            // 检查 Url 地址是否有效
            var result = requestUri.TryValidate(ValidationTypes.Url);
            if (!result.IsValid) throw new InvalidOperationException($"{requestUri} is not a valid url address.");

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
                var bodyJson = JsonSerializerUtility.Serialize(bodyArgs);
                request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                // 打印请求地址
                App.PrintToMiniProfiler(MiniProfilerCategory, "Body", bodyJson);
            }

            // 请求之前拦截
            interceptor?.Invoke(request);

            // 打印请求地址
            App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning", $"{request.Method} {request.RequestUri.AbsoluteUri}");

            // 创建 HttpClient 对象
            var clientFactory = App.GetService<IHttpClientFactory>();
            if (clientFactory == null) throw new ArgumentNullException("Please register for RemoteRequest service first: services.AddRemoteRequest();");

            var httpClient = string.IsNullOrEmpty(clientName)
                                        ? clientFactory.CreateClient()
                                        : clientFactory.CreateClient(clientName);

            // 发送请求
            var response = await httpClient.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                // 读取错误数据
                var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

                // 打印失败消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", errorMessage, isError: true);

                // 抛出请求异常
                throw new HttpRequestException(errorMessage);
            }
        }
    }
}