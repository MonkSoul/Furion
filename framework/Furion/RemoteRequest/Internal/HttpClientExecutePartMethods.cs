using Furion.DataValidation;
using Furion.Extensions;
using Furion.JsonSerialization;
using Furion.Templates.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpClient 对象组装部件
    /// </summary>
    public sealed partial class HttpClientExecutePart
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "httpclient";

        /// <summary>
        /// 发送 GET 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> GetAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> GetAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> GetAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> PostAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> PostAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> PostAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PostAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> PutAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> PutAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> PutAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PutAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> DeleteAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> DeleteAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> DeleteAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> PatchAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> PatchAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> PatchAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PatchAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> HeadAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> HeadAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> HeadAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> HeadAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendAsAsync<T>(CancellationToken cancellationToken = default)
        {
            // 如果 T 是 HttpRequestMessage 类型，则返回
            if (typeof(T) == typeof(HttpRequestMessage))
            {
                var httpResponseMessage = await SendAsync(cancellationToken);
                return (T)(object)httpResponseMessage;
            }
            if (typeof(T) == typeof(string))
            {
                var str = await SendAsStringAsync(cancellationToken);
                return (T)(object)str;
            }

            // 解析 Json 序列化提供器
            var jsonSerializer = App.GetService(JsonSerialization.ProviderType ?? typeof(SystemTextJsonSerializerProvider), RequestScoped) as IJsonSerializerProvider;

            // 读取流内容
            var stream = await SendAsStreamAsync(cancellationToken);

            // 如果 T 是 Stream 类型，则返回
            if (typeof(T) == typeof(Stream)) return (T)(object)stream;

            using var streamReader = new StreamReader(stream);
            var text = await streamReader.ReadToEndAsync();
            // 释放流
            await stream.DisposeAsync();

            // 反序列化流
            var result = jsonSerializer.Deserialize<T>(text, JsonSerialization.JsonSerializerOptions);
            return result;
        }

        /// <summary>
        /// 发送请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Stream> SendAsStreamAsync(CancellationToken cancellationToken = default)
        {
            var response = await SendAsync(cancellationToken);

            // 如果配置了异常拦截器，且请求不成功，则返回 T 默认值
            if (ExceptionInterceptors != null && ExceptionInterceptors.Count > 0 && !response.IsSuccessStatusCode) return default;

            // 读取响应流
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return stream;
        }

        /// <summary>
        /// 发送请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> SendAsStringAsync(CancellationToken cancellationToken = default)
        {
            var response = await SendAsync(cancellationToken);

            // 如果配置了异常拦截器，且请求不成功，则返回 T 默认值
            if (ExceptionInterceptors != null && ExceptionInterceptors.Count > 0 && !response.IsSuccessStatusCode) return default;

            // 读取响应报文
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return content;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(CancellationToken cancellationToken = default)
        {
            // 检查是否配置了请求方法
            if (Method == null) throw new NullReferenceException(nameof(Method));

            // 检查请求地址
            if (string.IsNullOrWhiteSpace(RequestUrl)) throw new NullReferenceException(RequestUrl);

            // 处理模板问题
            RequestUrl = RequestUrl.Render(Templates, true);

            // 构建请求对象
            var request = new HttpRequestMessage(Method, RequestUrl);
            request.AppendQueries(Queries);

            // 设置请求报文头
            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    if (header.Value != null) request.Headers.Add(header.Key, header.Value.ToString());
                }
            }

            // 验证模型参数（只作用于 body 类型）
            if (ValidationState.Enabled)
            {
                // 判断是否启用 Null 验证且 body 值为 null
                if (ValidationState.IncludeNull && Body == null) throw new InvalidOperationException($"{nameof(Body)} can not be null.");

                // 验证模型
                Body?.Validate();
            }

            // 设置 HttpContent
            SetHttpContent(request);

            // 配置请求拦截
            RequestInterceptors.ForEach(u =>
            {
                u?.Invoke(request);
            });

            // 创建客户端请求工厂
            var clientFactory = App.GetService<IHttpClientFactory>(RequestScoped);
            if (clientFactory == null) throw new InvalidOperationException("please add `services.AddRemoteRequest()` in Startup.cs.");

            // 创建 HttpClient 对象
            using var httpClient = string.IsNullOrWhiteSpace(ClientName)
                                         ? clientFactory.CreateClient()
                                         : clientFactory.CreateClient(ClientName);

            // 配置 HttpClient 拦截
            HttpClientInterceptors.ForEach(u =>
            {
                u?.Invoke(httpClient);
            });

            // 打印发送请求
            App.PrintToMiniProfiler(MiniProfilerCategory, "Sending", $"[{Method}] {httpClient.BaseAddress?.OriginalString}{request.RequestUri?.OriginalString}");

            // 发送请求
            var response = await httpClient.SendAsync(request, cancellationToken);

            // 请求成功
            if (response.IsSuccessStatusCode)
            {
                // 打印成功请求
                App.PrintToMiniProfiler(MiniProfilerCategory, "Succeeded", $"[StatusCode: {response.StatusCode}] Succeeded");

                // 调用成功拦截器
                ResponseInterceptors.ForEach(u =>
                {
                    u?.Invoke(response);
                });
            }
            // 请求异常
            else
            {
                // 读取错误消息
                var errors = await response.Content.ReadAsStringAsync(cancellationToken);

                // 打印失败请求
                App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", $"[StatusCode: {response.StatusCode}] {errors}", true);

                // 抛出异常
                if (ExceptionInterceptors == null || ExceptionInterceptors.Count == 0) throw new HttpRequestException(errors);
                // 调用异常拦截器
                else ExceptionInterceptors.ForEach(u =>
                {
                    u?.Invoke(response, errors);
                });
            }

            return response;
        }

        /// <summary>
        /// 设置 HttpContent
        /// </summary>
        /// <param name="request"></param>
        private void SetHttpContent(HttpRequestMessage request)
        {
            // GET/HEAD 请求不支持设置 Body 请求
            if (Body == null || Method == HttpMethod.Get || Method == HttpMethod.Head) return;

            HttpContent httpContent = null;

            // 处理各种 Body 类型
            switch (ContentType)
            {
                case "multipart/form-data":
                    var multipartFormDataContent = new MultipartFormDataContent();

                    // 添加 Bytes 类型
                    foreach (var (Name, Bytes, FileName) in BodyBytes)
                    {
                        if (string.IsNullOrWhiteSpace(FileName))
                            multipartFormDataContent.Add(new ByteArrayContent(Bytes), Name);
                        else
                            multipartFormDataContent.Add(new ByteArrayContent(Bytes), Name, FileName);
                    }

                    // 处理其他类型
                    var dic = ConvertBodyToDictionary();
                    if (dic == null || dic.Count == 0)
                    {
                        foreach (var (key, value) in dic)
                        {
                            multipartFormDataContent.Add(new StringContent(value, ContentEncoding), string.Format("\"{0}\"", key));
                        }
                    }

                    if (multipartFormDataContent.Any()) httpContent = multipartFormDataContent;

                    break;

                case "application/json":
                case "text/json":
                case "application/*+json":
                    httpContent = new StringContent(SerializerObject(Body), ContentEncoding);
                    break;

                case "application/x-www-form-urlencoded":
                    // 解析字典
                    var keyValues = ConvertBodyToDictionary();

                    if (keyValues == null || keyValues.Count == 0) return;

                    httpContent = new FormUrlEncodedContent(keyValues);
                    break;
            }

            if (httpContent != null)
            {
                // 设置内容类型
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

                // 设置 HttpContent
                request.Content = httpContent;
            }
        }

        /// <summary>
        /// 转换 Body 为 字典类型
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> ConvertBodyToDictionary()
        {
            Dictionary<string, string> keyValues = null;

            // 处理各种情况
            if (Body is Dictionary<string, string> dic) keyValues = dic;
            else if (Body is Dictionary<string, object> dicObj) keyValues = dicObj.ToDictionary(u => u.Key, u => SerializerObject(u.Value));
            else keyValues = Body.ToDictionary<string>();
            return keyValues;
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private string SerializerObject(object body)
        {
            // 解析序列化工具
            var jsonSerializer = App.GetService(JsonSerialization.ProviderType, RequestScoped) as IJsonSerializerProvider;
            return jsonSerializer.Serialize(body, JsonSerialization.JsonSerializerOptions);
        }
    }
}