// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.ClayObject;
using Furion.ClayObject.Extensions;
using Furion.DataValidation;
using Furion.Extensions;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.Templates.Extensions;
using Furion.VirtualFileServer;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Furion.RemoteRequest;

/// <summary>
/// HttpClient 对象组装部件
/// </summary>
public sealed partial class HttpRequestPart
{
    /// <summary>
    /// 请求失败事件
    /// </summary>
    public event EventHandler<HttpRequestFaildedEventArgs> OnRequestFailded;

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
    public Task<(Stream Stream, Encoding Encoding, HttpResponseMessage Response)> GetAsStreamAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Get).SendAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 GET 请求并将 Stream 保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task GetToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Get).SendToSaveAsync(path, cancellationToken);
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
    /// 发送 GET 请求返回 ByteArray
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<byte[]> GetAsByteArrayAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Get).SendAsByteArrayAsync(cancellationToken);
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
    public Task<(Stream Stream, Encoding Encoding, HttpResponseMessage Response)> PostAsStreamAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Post).SendAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求并将 Stream 保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task PostToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Post).SendToSaveAsync(path, cancellationToken);
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
    /// 发送 POST 请求返回 ByteArray
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<byte[]> PostAsByteArrayAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Post).SendAsByteArrayAsync(cancellationToken);
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
    public Task<(Stream Stream, Encoding Encoding, HttpResponseMessage Response)> PutAsStreamAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Put).SendAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PUT 请求并将 Stream 保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task PutToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Put).SendToSaveAsync(path, cancellationToken);
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
    /// 发送 PUT 请求返回 ByteArray
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<byte[]> PutAsByteArrayAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Put).SendAsByteArrayAsync(cancellationToken);
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
    public Task<(Stream Stream, Encoding Encoding, HttpResponseMessage Response)> DeleteAsStreamAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Delete).SendAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 DELETE 请求并将 Stream 保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task DeleteToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Delete).SendToSaveAsync(path, cancellationToken);
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
    /// 发送 DELETE 请求返回 ByteArray
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<byte[]> DeleteAsByteArrayAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Delete).SendAsByteArrayAsync(cancellationToken);
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
    public Task<(Stream Stream, Encoding Encoding, HttpResponseMessage Response)> PatchAsStreamAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Patch).SendAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 PATCH 请求并将 Stream 保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task PatchToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Patch).SendToSaveAsync(path, cancellationToken);
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
    /// 发送 Patch 请求返回 ByteArray
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<byte[]> PatchAsByteArrayAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Patch).SendAsByteArrayAsync(cancellationToken);
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
    public Task<(Stream Stream, Encoding Encoding, HttpResponseMessage Response)> HeadAsStreamAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Head).SendAsStreamAsync(cancellationToken);
    }

    /// <summary>
    /// 发送 HEAD 请求并将 Stream 保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task HeadToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Head).SendToSaveAsync(path, cancellationToken);
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
    /// 发送 Head 请求返回 ByteArray
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<byte[]> HeadAsByteArrayAsync(CancellationToken cancellationToken = default)
    {
        return SetHttpMethod(HttpMethod.Head).SendAsByteArrayAsync(cancellationToken);
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
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(HttpResponseModel<>))
        {
            // 反射调用
            var typeOfTArg = typeof(T).GetGenericArguments()[0];
            var genericMethod = GetType().GetMethod(nameof(SendAsHttpResponseModelAsync)).MakeGenericMethod(typeOfTArg);
            var task = genericMethod.Invoke(this, new object[] { cancellationToken }) as Task;

            var taskCompletionSource = new TaskCompletionSource<object>();
            _ = task.ContinueWith(t =>
            {
                // 异步执行失败处理
                if (t.IsFaulted)
                {
                    taskCompletionSource.TrySetException(t.Exception);
                }
                // 异步被取消处理
                else if (t.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                // 异步成功返回处理
                else
                {
                    taskCompletionSource.TrySetResult(((dynamic)t).Result);
                }
            }, cancellationToken);

            var httpResposeModel = await taskCompletionSource.Task;
            return (T)httpResposeModel;
        }

        // 如果 T 是 HttpResponseMessage 类型，则返回
        if (typeof(T) == typeof(HttpResponseMessage))
        {
            var httpResponseMessage = await SendAsync(cancellationToken);
            return (T)(object)httpResponseMessage;
        }
        // 处理字符串类型
        if (typeof(T) == typeof(string))
        {
            var str = await SendAsStringAsync(cancellationToken);
            return (T)(object)str;
        }
        // 处理 byte[] 数组
        if (typeof(T) == typeof(byte[]))
        {
            var byteArray = await SendAsByteArrayAsync(cancellationToken);
            return (T)(object)byteArray;
        }

        // 读取流内容
        var (stream, encoding, response) = await SendAsStreamAsync(cancellationToken);
        if (stream == null) return default;

        // 处理 Stream 类型
        if (typeof(T) == typeof(Stream)) return (T)(object)stream;

        // 判断是否启用 Gzip
        using var streamReader = new StreamReader(
            !SupportGZip
            ? stream
            : new GZipStream(stream, CompressionMode.Decompress), encoding);

        var text = await streamReader.ReadToEndAsync();
        // 释放流
        await stream.DisposeAsync();
        response?.Dispose();

        // 如果字符串为空，则返回默认值
        if (string.IsNullOrWhiteSpace(text)) return default;

        // 解析 Json 序列化提供器
        var jsonSerializer = App.GetService(JsonSerializerProvider, RequestScoped ?? App.RootServices) as IJsonSerializerProvider;

        // 反序列化流
        var result = jsonSerializer.Deserialize<T>(text, JsonSerializerOptions);
        return result;
    }

    /// <summary>
    /// 发送请求返回 HttpResponseModel{T} 对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<HttpResponseModel<T>> SendAsHttpResponseModelAsync<T>(CancellationToken cancellationToken = default)
    {
        // 类型 T 不能是自身类型
        if ((typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(HttpResponseModel<>)) || typeof(T) == typeof(HttpResponseMessage))
        {
            throw new InvalidOperationException("Type T cannot be of type HttpResponseModel<> and HttpResponseMessage.");
        }

        var response = await SendAsync(cancellationToken);
        if (response == null) return default;

        // 初始化HTTP 响应模型
        var httpResponseModel = new HttpResponseModel<T>
        {
            Response = response,
        };

        if (response.Content != null)
        {
            // 获取 charset 编码
            var encoding = httpResponseModel.Encoding = GetCharsetEncoding(response);

            // 处理字符串类型
            if (typeof(T) == typeof(string))
            {
                // 读取内容字节流
                var byteArray = await response.Content.ReadAsByteArrayAsync(cancellationToken);

                // 通过指定编码解码
                var str = (encoding.GetString(byteArray));
                httpResponseModel.Result = (T)(object)str;
            }
            // 处理 byte[] 数组
            else if (typeof(T) == typeof(byte[]))
            {
                // 读取响应报文
                var byteArray = await response.Content.ReadAsByteArrayAsync(cancellationToken);
                httpResponseModel.Result = (T)(object)byteArray;
            }
            else
            {
                // 读取响应流
                var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                stream.Position = 0;

                if (typeof(T) == typeof(Stream))
                {
                    httpResponseModel.Result = (T)(object)stream;
                }
                else
                {
                    // 复制流，实现 HttpResponse 可以重复读
                    var newStream = await CopyStreamAsync(stream);
                    response.Content = new StreamContent(newStream);
                    stream.Position = 0;

                    // 判断是否启用 Gzip
                    using var streamReader = new StreamReader(
                        !SupportGZip
                        ? stream
                        : new GZipStream(stream, CompressionMode.Decompress), encoding);

                    var text = await streamReader.ReadToEndAsync();

                    // 释放原始流
                    await stream.DisposeAsync();

                    // 如果字符串为空，则返回默认值
                    if (string.IsNullOrWhiteSpace(text)) return default;

                    // 解析 Json 序列化提供器
                    var jsonSerializer = App.GetService(JsonSerializerProvider, RequestScoped ?? App.RootServices) as IJsonSerializerProvider;

                    // 反序列化流
                    var result = jsonSerializer.Deserialize<T>(text, JsonSerializerOptions);
                    httpResponseModel.Result = (T)(object)result;
                }
            }
        }

        return httpResponseModel;
    }

    /// <summary>
    /// 发送请求返回 Stream
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<(Stream Stream, Encoding Encoding, HttpResponseMessage Response)> SendAsStreamAsync(CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(cancellationToken);
        if (response == null || response.Content == null) return (null, null, response);

        // 获取 charset 编码
        var encoding = GetCharsetEncoding(response);

        // 读取响应流
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        stream.Position = 0;

        return (stream, encoding, response);
    }

    /// <summary>
    /// 发送请求并将流保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SendToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        var (stream, _, response) = await SendAsStreamAsync(cancellationToken);

        if (stream != null)
        {
            await stream.CopyToSaveAsync(path);
            // 释放流
            await stream.DisposeAsync();
        }

        response?.Dispose();
    }

    /// <summary>
    /// 发送请求返回 String
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> SendAsStringAsync(CancellationToken cancellationToken = default)
    {
        using var response = await SendAsync(cancellationToken);
        if (response == null || response.Content == null) return default;

        // 获取 charset 编码
        var encoding = GetCharsetEncoding(response);

        // 读取内容字节流
        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);

        // 通过指定编码解码
        return encoding.GetString(content);
    }

    /// <summary>
    /// 发送请求返回 ByteArray
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<byte[]> SendAsByteArrayAsync(CancellationToken cancellationToken = default)
    {
        using var response = await SendAsync(cancellationToken);
        if (response == null || response.Content == null) return default;

        // 读取响应报文
        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);
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

        // 创建客户端请求工厂
        var clientFactory = App.GetService<IHttpClientFactory>(RequestScoped ?? App.RootServices)
            ?? throw new InvalidOperationException("Please add `services.AddRemoteRequest()` in Startup.cs.");

        // 创建 HttpClient 对象，这里支持自定义
        var clientName = ClientName ?? string.Empty;
        // 这里不需要 using 释放：https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0#httpclient-and-lifetime-management
        // 默认生存期为两分钟，两分钟后会自动释放
        var httpClient = ClientProvider?.Invoke() ?? (
                               string.IsNullOrWhiteSpace(clientName)
                                ? clientFactory.CreateClient()
                                : clientFactory.CreateClient(clientName));

        // 添加默认 User-Agent
        if (!httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                             "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36 Edg/104.0.1293.47");
        }

        // 处理 [BaseAddress] 特性
        if (!string.IsNullOrWhiteSpace(BaseAddress))
        {
            httpClient.BaseAddress = new Uri(BaseAddress.TrimEnd('/') + "/");
        }

        // 配置 HttpClient 拦截
        HttpClientInterceptors.ForEach(u =>
        {
            u?.Invoke(httpClient);
        });

        // 判断命名客户端是否配置了 BaseAddress，且必须以 / 结尾
        var httpClientOriginalString = httpClient.BaseAddress?.OriginalString;
        if (!string.IsNullOrWhiteSpace(httpClientOriginalString) && !httpClientOriginalString.EndsWith("/"))
            throw new InvalidOperationException($"The `{ClientName}` of HttpClient BaseAddress must be end with '/'.");

        // 检查请求地址，如果客户端 BaseAddress 没有配置且 RequestUrl 也没配置
        if (string.IsNullOrWhiteSpace(httpClientOriginalString) && string.IsNullOrWhiteSpace(RequestUrl)) throw new NullReferenceException(RequestUrl);

        // 处理模板问题
        RequestUrl = RequestUrl.Render(Templates, EncodeUrl);

        // 捕获异常
        Exception exception = default;
        HttpResponseMessage response = default;

        HttpRequestMessage request = null;
        var logger = App.GetService<ILogger<RemoteRequestService>>();

        try
        {
            if (RetryPolicy == null)
            {
                request?.Dispose();
                request = CreateHttpRequestMessage(httpClient, httpClientOriginalString);
                response = await httpClient.SendAsync(request, cancellationToken);
            }
            else
            {
                // 失败重试
                await Retry.InvokeAsync(async () =>
                {
                    // 发送请求
                    request?.Dispose();
                    request = CreateHttpRequestMessage(httpClient, httpClientOriginalString);
                    response = await httpClient.SendAsync(request, cancellationToken);

                    // 状态码检查（确保异常正确抛出）
                    response.EnsureSuccessStatusCode();
                }, RetryPolicy.Value.NumRetries
                , RetryPolicy.Value.RetryTimeout
                , retryAction: (total, times) =>
                {
                    // 输出重试日志
                    logger.LogWarning("Retrying {times}/{total} times for HTTP request {Method} {RequestUrl}", times, total, Method, RequestUrl);
                });
            }
        }
        catch (Exception ex)
        {
            // 触发自定义事件
            if (response != null) InvokeEvents(new HttpRequestFaildedEventArgs(request, response, ex));

            exception = ex;
        }

        // 检查响应状态码是否为 301,302 或响应头带 Location
        if (response?.StatusCode == HttpStatusCode.MovedPermanently || response?.StatusCode == HttpStatusCode.Found)
        {
            // 获取 Location 头部中的新URL
            var redirectUrl = response.Headers.Location.AbsoluteUri;

            try
            {
                if (RetryPolicy == null) response = await httpClient.GetAsync(redirectUrl, cancellationToken);
                else
                {
                    // 失败重试
                    await Retry.InvokeAsync(async () =>
                    {
                        // 重新发送请求到新的 URL
                        response?.Dispose();
                        response = await httpClient.GetAsync(redirectUrl, cancellationToken);

                        // 状态码检查（确保异常正确抛出）
                        response.EnsureSuccessStatusCode();
                    }, RetryPolicy.Value.NumRetries
                    , RetryPolicy.Value.RetryTimeout
                    , retryAction: (total, times) =>
                    {
                        // 输出重试日志
                        logger.LogWarning("Retrying {times}/{total} times for HTTP request {Method} {redirectUrl}", times, total, HttpMethod.Get, redirectUrl);
                    });
                }
            }
            catch (Exception ex)
            {
                // 触发自定义事件
                if (response != null) InvokeEvents(new HttpRequestFaildedEventArgs(request, response, ex));

                exception = ex;
            }
        }

        // 请求成功
        if (response?.IsSuccessStatusCode == true && exception == default)
        {
            // 打印成功请求
            App.PrintToMiniProfiler(MiniProfilerCategory, "Succeeded", $"[StatusCode: {response.StatusCode}] Succeeded");

            // 调用成功拦截器
            ResponseInterceptors.ForEach(u =>
            {
                u?.Invoke(httpClient, response);
            });
        }
        // 请求异常
        else
        {
            // 读取错误消息
            var errors = exception != null
                ? exception?.Message
                : response?.ReasonPhrase;

            var statusCode = response != null ? (int)response.StatusCode : ParseExceptionMessage(errors);

            // 打印失败请求
            App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", $"[StatusCode: {statusCode}] {errors}", exception != null);

            // 调用异常拦截器
            if (ExceptionInterceptors != null && ExceptionInterceptors.Count > 0)
            {
                response ??= new HttpResponseMessage
                {
                    RequestMessage = request,
                    ReasonPhrase = exception.Message,
                    StatusCode = (HttpStatusCode)statusCode
                };

                ExceptionInterceptors.ForEach(u =>
                {
                    u?.Invoke(httpClient, response, errors);
                });
            }

            // 抛出请求异常
            if (exception != null)
            {
                request?.Dispose();
                response?.Dispose();
                request = null;
                response = null;

                throw exception;
            }
        }

        request?.Dispose();
        return response;
    }

    /// <summary>
    /// 解析常规错误码
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    private static int ParseExceptionMessage(string errors)
    {
        if (errors.Contains("HttpClient.Timeout"))
        {
            return 408;
        }

        return 500;
    }

    /// <summary>
    /// 设置 HttpContent
    /// </summary>
    /// <param name="request"></param>
    private void SetHttpContent(HttpRequestMessage request)
    {
        // GET/HEAD 请求不支持设置 Body 请求（2023.08.02 移除此判断，原因是部分编程语言支持 GET 设置 Body）
        // if (Method == HttpMethod.Get || Method == HttpMethod.Head) return;

        HttpContent httpContent = null;

        // 处理各种 Body 类型
        switch (ContentType)
        {
            case "multipart/form-data":

                var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                var multipartFormDataContent = new MultipartFormDataContent(boundary);

                // 遍历所有上传文件
                foreach (var httpFile in Files)
                {
                    // 获取文件 Content-Type 类型
                    FS.TryGetContentType(httpFile.FileName, out var contentType);

                    // 处理 Bytes 文件
                    if (httpFile.Bytes != null)
                    {
                        var byteArrayContent = new ByteArrayContent(httpFile.Bytes);
                        byteArrayContent.Headers.TryAddWithoutValidation("Content-Type", contentType ?? "application/octet-stream");

                        if (string.IsNullOrWhiteSpace(httpFile.FileName))
                            multipartFormDataContent.Add(byteArrayContent, httpFile.Escape ? Uri.EscapeDataString(httpFile.Name) : httpFile.Name);
                        else
                            multipartFormDataContent.Add(byteArrayContent, httpFile.Escape ? Uri.EscapeDataString(httpFile.Name) : httpFile.Name, httpFile.Escape ? Uri.EscapeDataString(httpFile.FileName) : httpFile.FileName);
                    }

                    // 处理 Stream 文件
                    if (httpFile.FileStream != null)
                    {
                        var streamContent = new StreamContent(httpFile.FileStream, (int)httpFile.FileStream.Length);
                        streamContent.Headers.TryAddWithoutValidation("Content-Type", contentType ?? "application/octet-stream");

                        if (string.IsNullOrWhiteSpace(httpFile.FileName))
                            multipartFormDataContent.Add(streamContent, httpFile.Escape ? Uri.EscapeDataString(httpFile.Name) : httpFile.Name);
                        else
                            multipartFormDataContent.Add(streamContent, httpFile.Escape ? Uri.EscapeDataString(httpFile.Name) : httpFile.Name, httpFile.Escape ? Uri.EscapeDataString(httpFile.FileName) : httpFile.FileName);
                    }
                }

                // 处理其他类型
                var dic = ConvertBodyToDictionary();
                if (dic != null && dic.Count > 0)
                {
                    foreach (var (key, value) in dic)
                    {
                        multipartFormDataContent.Add(new StringContent(value ?? string.Empty, ContentEncoding), string.Format("\"{0}\"", key));
                    }
                }

                // 解决 boundary 带双引号问题
                multipartFormDataContent.Headers.Remove("Content-Type");
                multipartFormDataContent.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);

                // 设置内容类型
                httpContent = multipartFormDataContent;
                break;

            case "application/octet-stream":
            case "application/pdf":
                if (Files.Count > 0 && Files[0].Bytes.Length > 0)
                {
                    httpContent = new ByteArrayContent(Files[0].Bytes);

                    // 设置内容类型
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType)
                    {
                        CharSet = ContentEncoding.BodyName
                    };
                }
                break;

            case "application/json":
            case "text/json":
            case "application/*+json":
            case "application/json-patch+json":
            case "application/ld+json":
            case var _ when ContentType.Contains("+json"):
                if (Body != null)
                {
                    httpContent = new StringContent(SerializerObject(Body), ContentEncoding);

                    // 设置内容类型
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType)
                    {
                        CharSet = ContentEncoding.BodyName
                    };
                }
                break;

            case "application/x-www-form-urlencoded":
                // 如果 Body 是字符串类型，直接传入
                if (Body is string stringBody)
                {
                    httpContent = new StringContent(stringBody, ContentEncoding, ContentType);
                }
                else
                {
                    // 解析字典
                    var keyValues = ConvertBodyToDictionary();

                    if (keyValues == null || keyValues.Count == 0) return;

                    // 设置内容类型
                    if (EncodeUrl)
                    {
                        httpContent = new FormUrlEncodedContent(keyValues);
                    }
                    else
                    {
                        var formData = string.Join('&', keyValues.Select(kv => $"{kv.Key}={kv.Value}"));
                        httpContent = new StringContent(formData, ContentEncoding, ContentType);
                    }
                }

                break;

            case "application/xml":
            case "text/xml":
            case "text/html":
            case "text/plain":
            case var _ when ContentType.StartsWith("text/"):
                if (Body != null) httpContent = new StringContent(Body.ToString(), ContentEncoding, ContentType);
                break;

            default:
                // 其他类型可通过 HttpRequestMessage 拦截器设置
                break;
        }

        // 设置 HttpContent
        if (httpContent != null)
        {
            request.Content = httpContent;

            // 设置请求内容头
            SetContentHeaders(request);
        }
    }

    /// <summary>
    /// 设置请求内容头
    /// </summary>
    /// <param name="request"></param>
    private void SetContentHeaders(HttpRequestMessage request)
    {
        if (ContentHeaders != null && ContentHeaders.Count > 0)
        {
            // 处理 Content-Disposition
            if (ContentHeaders.ContainsKey("Content-Disposition") && ContentHeaders["Content-Disposition"] != null)
            {
                request.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(ContentHeaders["Content-Disposition"].ToString());
            }

            // 处理 Content-Length
            if (ContentHeaders.ContainsKey("Content-Length") && ContentHeaders["Content-Length"] != null)
            {
                request.Content.Headers.ContentLength = (long)ContentHeaders["Content-Length"];
            }

            // 处理 Content-Location
            if (ContentHeaders.ContainsKey("Content-Location") && ContentHeaders["Content-Location"] != null)
            {
                request.Content.Headers.ContentLocation = ContentHeaders["Content-Location"] is string str
                    ? new Uri(str)
                    : (Uri)ContentHeaders["Content-Location"];
            }

            // 处理 Content-MD5
            if (ContentHeaders.ContainsKey("Content-MD5") && ContentHeaders["Content-MD5"] != null)
            {
                request.Content.Headers.ContentMD5 = ContentHeaders["Content-MD5"] is string str
                    ? Convert.FromBase64String(str)
                    : (byte[])ContentHeaders["Content-MD5"];
            }

            // 处理 Content-Range
            if (ContentHeaders.ContainsKey("Content-Range") && ContentHeaders["Content-Range"] != null)
            {
                request.Content.Headers.ContentRange = new ContentRangeHeaderValue((long)ContentHeaders["Content-Range"]);
            }

            // 处理 Content-Type
            if (ContentHeaders.ContainsKey("Content-Type") && ContentHeaders["Content-Type"] != null)
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentHeaders["Content-Type"].ToString());
            }

            // 处理 Expires
            if (ContentHeaders.ContainsKey("Expires") && ContentHeaders["Expires"] != null)
            {
                request.Content.Headers.Expires = ContentHeaders["Expires"].ChangeType<DateTimeOffset>();
            }

            // 处理 LastModified
            if (ContentHeaders.ContainsKey("LastModified") && ContentHeaders["LastModified"] != null)
            {
                request.Content.Headers.LastModified = ContentHeaders["LastModified"].ChangeType<DateTimeOffset>();
            }
        }
    }

    /// <summary>
    /// 转换 Body 为 字典类型
    /// </summary>
    /// <returns></returns>
    private IDictionary<string, string> ConvertBodyToDictionary()
    {
        IDictionary<string, string> keyValues = null;
        if (Body == null) return default;

        // 处理各种情况
        if (Body is IDictionary<string, string> dic) keyValues = dic;
        else if (Body is IDictionary<string, object> dicObj) keyValues = dicObj.ToDictionary(u => u.Key, u => SerializerObject(u.Value));
        else keyValues = Body.ToDictionary().ToDictionary(u => u.Key, u => SerializerObject(u.Value));
        return keyValues;
    }

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    private string SerializerObject(object body)
    {
        if (body == null) return default;
        if (body is string) return body as string;
        if (body.GetType().IsValueType) return body.ToString();
        if (body is Clay clay) return clay.ToString();  // 处理粘土类型

        // 解析序列化工具
        var jsonSerializer = App.GetService(JsonSerializerProvider, RequestScoped ?? App.RootServices) as IJsonSerializerProvider;
        return jsonSerializer.Serialize(body, JsonSerializerOptions);
    }

    /// <summary>
    /// 解析响应报文 charset 编码
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private static Encoding GetCharsetEncoding(HttpResponseMessage response)
    {
        if (response == null) return Encoding.UTF8;

        // 获取 charset
        string charset;

        // 获取响应头的编码格式
        var withContentType = response.Content.Headers.TryGetValues("Content-Type", out var contentTypes);
        if (withContentType)
        {
            charset = contentTypes.First()
                                  .Split(';', StringSplitOptions.RemoveEmptyEntries)
                                  .Where(u => u.Contains("charset", StringComparison.OrdinalIgnoreCase))
                                  .FirstOrDefault() ?? "charset=UTF-8";
        }
        else charset = "charset=UTF-8";

        var encoding = charset.Split('=', StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? "UTF-8";

        // 标准化 charset 名称
        var encodingName = encoding.Equals("utf8", StringComparison.OrdinalIgnoreCase) ? "UTF-8" :
                           encoding.Equals("utf16", StringComparison.OrdinalIgnoreCase) ? "UTF-16" :
                           encoding.Equals("utf32", StringComparison.OrdinalIgnoreCase) ? "UTF-32" :
                           encoding;

        // 获取 Encoding
        try
        {
            return Encoding.GetEncoding(encodingName);
        }
        catch (ArgumentException)
        {
            // 如果无法识别 encodingName，则返回默认的 UTF-8 编码
            return Encoding.UTF8;
        }
    }

    /// <summary>
    /// 触发请求失败事件
    /// </summary>
    /// <param name="args"></param>
    private void InvokeEvents(HttpRequestFaildedEventArgs args)
    {
        try
        {
            OnRequestFailded?.Invoke(this, args);
        }
        catch
        {
        }
    }

    /// <summary>
    /// 创建 <see cref="HttpRequestMessage"/> 对象
    /// </summary>
    /// <remarks>解决重试出现的异常：The request message was already sent. Cannot send the same request message multiple times.</remarks>
    /// <param name="httpClient"></param>
    /// <param name="httpClientOriginalString"></param>
    /// <returns></returns>
    private HttpRequestMessage CreateHttpRequestMessage(HttpClient httpClient, string httpClientOriginalString)
    {
        // 构建请求对象
        var request = new HttpRequestMessage(Method, RequestUrl)
        {
            Version = new Version(HttpVersion)
        };
        request.AppendQueries(Queries, EncodeUrl, IgnoreNullValueQueries);

        // 设置请求报文头
        request.AppendHeaders(Headers);

        // 验证模型参数（只作用于 body 类型）
        if (ValidationState.Enabled)
        {
            // 判断是否启用 Null 验证且 body 值为 null
            if (ValidationState.IncludeNull && Body == null) throw new InvalidOperationException($"The `{nameof(Body)}` can not be null.");

            // 验证模型
            Body?.Validate();
        }

        // 设置 HttpContent
        SetHttpContent(request);

        // 配置请求拦截
        RequestInterceptors.ForEach(u =>
        {
            u?.Invoke(httpClient, request);
        });

        // 打印发送请求
        App.PrintToMiniProfiler(MiniProfilerCategory, "Sending", $"[{Method}] {httpClientOriginalString}{request.RequestUri?.OriginalString}");

        return request;
    }

    /// <summary>
    /// 复制响应流
    /// </summary>
    /// <param name="originalStream"></param>
    /// <returns></returns>
    private static async Task<MemoryStream> CopyStreamAsync(Stream originalStream)
    {
        // 创建一个新的内存流来存储复制的数据
        var memoryStream = new MemoryStream();

        // 创建一个缓冲区来读取和写入数据
        var buffer = new byte[4096];
        int bytesRead;

        // 读取原始流中的数据，直到没有更多数据可读
        while ((bytesRead = await originalStream.ReadAsync(buffer)) != 0)
        {
            // 将读取的数据写入到内存流中
            await memoryStream.WriteAsync(buffer.AsMemory(0, bytesRead));
        }

        // 将内存流的位置重置到开始处，以便可以从中读取数据
        memoryStream.Position = 0;

        // 返回包含复制数据的内存流
        return memoryStream;
    }
}