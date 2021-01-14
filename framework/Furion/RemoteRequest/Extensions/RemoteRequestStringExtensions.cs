using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        /// <param name="contentType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetAsync(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Get, null, headers, clientName, interceptor, contentType, JsonNamingPolicyOptions.CamelCase, cancellationToken);
        }

        /// <summary>
        /// 发送 Get 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> GetAsAsync<T>(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Get, null, headers, clientName, interceptor, contentType, JsonNamingPolicyOptions.CamelCase, cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> HeadAsync(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Head, null, headers, clientName, interceptor, contentType, JsonNamingPolicyOptions.CamelCase, cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> HeadAsAsync<T>(this string requestUri, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Head, null, headers, clientName, interceptor, contentType, JsonNamingPolicyOptions.CamelCase, cancellationToken);
        }

        /// <summary>
        /// 发送 Post 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Post, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Post 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PostAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Post, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Put 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Put, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Put 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PutAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Put, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Delete 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Delete, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Delete 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> DeleteAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Delete, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PatchAsync(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsync(HttpMethod.Patch, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="headers"></param>
        /// <param name="clientName"></param>
        /// <param name="interceptor"></param>
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> PatchAsAsync<T>(this string requestUri, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            return await requestUri.SendAsAsync<T>(HttpMethod.Patch, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);
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
        /// <param name="contentType"></param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> SendAsAsync<T>(this string requestUri, HttpMethod httpMethod = default, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
        {
            var response = await requestUri.SendAsync(httpMethod, bodyArgs, headers, clientName, interceptor, contentType, propertyNamingPolicy, cancellationToken);

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
        /// <param name="contentType">contentType</param>
        /// <param name="propertyNamingPolicy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(this string requestUri, HttpMethod httpMethod = default, object bodyArgs = null, Dictionary<string, string> headers = default, string clientName = default, Action<HttpRequestMessage> interceptor = default, string contentType = "application/json", JsonNamingPolicyOptions propertyNamingPolicy = JsonNamingPolicyOptions.CamelCase, CancellationToken cancellationToken = default)
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
                string body;

                // 处理 json 类型
                if (contentType.Contains("json"))
                {
                    // 配置 Json 命名策略
                    var jsonSerializerOptions = JsonSerializerUtility.GetDefaultJsonSerializerOptions();
                    jsonSerializerOptions.PropertyNamingPolicy = propertyNamingPolicy switch
                    {
                        JsonNamingPolicyOptions.CamelCase => JsonNamingPolicy.CamelCase,
                        JsonNamingPolicyOptions.Null => null,
                        _ => null
                    };

                    body = JsonSerializerUtility.Serialize(bodyArgs, jsonSerializerOptions);
                }
                // 处理 xml 类型
                else if (contentType.Contains("xml"))
                {
                    var xmlSerializer = new XmlSerializer(bodyArgs.GetType());
                    var buffer = new StringBuilder();

                    using var writer = new StringWriter(buffer);
                    xmlSerializer.Serialize(writer, bodyArgs);

                    body = buffer.ToString();
                }
                // 其他类型
                else body = bodyArgs.ToString();

                if (!string.IsNullOrEmpty(body))
                {
                    var httpContent = new StringContent(body, Encoding.UTF8);

                    // 设置内容类型
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    request.Content = httpContent;

                    // 打印请求地址
                    App.PrintToMiniProfiler(MiniProfilerCategory, "Body", body);
                }
            }

            // 请求之前拦截
            interceptor?.Invoke(request);

            // 打印请求地址
            App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning", $"{request.Method} {request.RequestUri}");

            // 创建 HttpClient 对象
            var clientFactory = App.GetService<IHttpClientFactory>();
            if (clientFactory == null) throw new InvalidOperationException("Please register for RemoteRequest service first: services.AddRemoteRequest();");

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