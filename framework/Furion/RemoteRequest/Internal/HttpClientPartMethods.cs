using Furion.DataValidation;
using Furion.JsonSerialization;
using System;
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
    public sealed partial class HttpClientPart
    {
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
            // 解析 Json 序列化提供器
            var jsonSerializer = App.GetService(JsonSerialization.ProviderType ?? typeof(SystemTextJsonSerializerProvider)) as IJsonSerializerProvider;

            // 读取流内容并转换成字符串
            var stream = await SendAsStreamAsync(cancellationToken);
            using var streamReader = new StreamReader(stream);
            var text = await streamReader.ReadToEndAsync();

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
            if (ExceptionInspector != null && !response.IsSuccessStatusCode) return default;

            // 读取响应流
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return stream;
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
            if (string.IsNullOrEmpty(RequestUrl)) throw new NullReferenceException(RequestUrl);

            var finalRequestUrl = RequestUrl;

            // 拼接查询参数
            if (Queries != null && Queries.Count > 0)
            {
                var urlParameters = Queries.Select(u => u.Key + "=" + u.Value);
                finalRequestUrl += $"?{string.Join("&", urlParameters)}";
            }

            // 构建请求对象
            var request = new HttpRequestMessage(Method, finalRequestUrl);

            // 设置请求报文头
            if (Headers != null && Headers.Count > 0)
            {
                foreach (var header in Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
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
            RequestInspector?.Invoke(request);

            // 创建客户端请求工厂
            var clientFactory = App.GetService<IHttpClientFactory>();

            // 创建 HttpClient 对象
            using var httpClient = string.IsNullOrEmpty(ClientName)
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
                    break;

                case "application/json":
                case "text/json":
                case "application/*+json":
                    // 解析序列化工具
                    var jsonSerializer = App.GetService(JsonSerialization.ProviderType) as IJsonSerializerProvider;

                    // 序列化
                    httpContent = new StringContent(jsonSerializer.Serialize(Body, JsonSerialization.JsonSerializerOptions), ContentEncoding);
                    break;

                case "application/x-www-form-urlencoded":
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
    }
}