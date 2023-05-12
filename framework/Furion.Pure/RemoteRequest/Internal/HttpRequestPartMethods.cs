// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.ClayObject;
using Furion.ClayObject.Extensions;
using Furion.DataValidation;
using Furion.Extensions;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.Templates.Extensions;
using Furion.VirtualFileServer;
using System.IO.Compression;
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
    public Task<(Stream Stream, Encoding Encoding)> GetAsStreamAsync(CancellationToken cancellationToken = default)
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
    public Task<(Stream Stream, Encoding Encoding)> PostAsStreamAsync(CancellationToken cancellationToken = default)
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
    public Task<(Stream Stream, Encoding Encoding)> PutAsStreamAsync(CancellationToken cancellationToken = default)
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
    public Task<(Stream Stream, Encoding Encoding)> DeleteAsStreamAsync(CancellationToken cancellationToken = default)
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
    public Task<(Stream Stream, Encoding Encoding)> PatchAsStreamAsync(CancellationToken cancellationToken = default)
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
    public Task<(Stream Stream, Encoding Encoding)> HeadAsStreamAsync(CancellationToken cancellationToken = default)
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
        var (stream, encoding) = await SendAsStreamAsync(cancellationToken);
        if (stream == null) return default;

        // 如果 T 是 Stream 类型，则返回
        if (typeof(T) == typeof(Stream)) return (T)(object)stream;

        // 判断是否启用 Gzip
        using var streamReader = new StreamReader(
            !SupportGZip
            ? stream
            : new GZipStream(stream, CompressionMode.Decompress), encoding);

        var text = await streamReader.ReadToEndAsync();
        // 释放流
        await stream.DisposeAsync();

        // 如果字符串为空，则返回默认值
        if (string.IsNullOrWhiteSpace(text)) return default;

        // 解析 Json 序列化提供器
        var jsonSerializer = App.GetService(JsonSerializerProvider, RequestScoped ?? App.RootServices) as IJsonSerializerProvider;

        // 反序列化流
        var result = jsonSerializer.Deserialize<T>(text, JsonSerializerOptions);
        return result;
    }

    /// <summary>
    /// 发送请求返回 Stream
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<(Stream Stream, Encoding Encoding)> SendAsStreamAsync(CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(cancellationToken);
        if (response == null || response.Content == null) return default;

        // 获取 charset 编码
        var encoding = GetCharsetEncoding(response);

        // 读取响应流
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        stream.Position = 0;

        return (stream, encoding);
    }

    /// <summary>
    /// 发送请求并将流保存到本地磁盘
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SendToSaveAsync(string path, CancellationToken cancellationToken = default)
    {
        var (stream, _) = await SendAsStreamAsync(cancellationToken);
        await stream.CopyToSaveAsync(path);
    }

    /// <summary>
    /// 发送请求返回 String
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> SendAsStringAsync(CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(cancellationToken);
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
        var response = await SendAsync(cancellationToken);
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

        // 判断命名客户端是否配置了 BaseAddress，且必须以 / 结尾
        var httpClientOriginalString = httpClient.BaseAddress?.OriginalString;
        if (!string.IsNullOrWhiteSpace(httpClientOriginalString) && !httpClientOriginalString.EndsWith("/"))
            throw new InvalidOperationException($"The `{ClientName}` of HttpClient BaseAddress must be end with '/'.");

        // 添加默认 User-Agent
        if (!httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                             "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36 Edg/104.0.1293.47");
        }

        // 配置 HttpClient 拦截
        HttpClientInterceptors.ForEach(u =>
        {
            u?.Invoke(httpClient);
        });

        // 检查请求地址，如果客户端 BaseAddress 没有配置且 RequestUrl 也没配置
        if (string.IsNullOrWhiteSpace(httpClient.BaseAddress?.OriginalString) && string.IsNullOrWhiteSpace(RequestUrl)) throw new NullReferenceException(RequestUrl);

        // 处理模板问题
        RequestUrl = RequestUrl.Render(Templates, EncodeUrl);

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

        // 捕获异常
        Exception exception = default;
        HttpResponseMessage response = default;

        try
        {
            if (RetryPolicy == null) response = await httpClient.SendAsync(request, cancellationToken);
            else
            {
                // 失败重试
                await Retry.InvokeAsync(async () =>
                {
                    // 发送请求
                    response = await httpClient.SendAsync(request, cancellationToken);
                }, RetryPolicy.Value.NumRetries, RetryPolicy.Value.RetryTimeout);
            }
        }
        catch (Exception ex)
        {
            // 触发自定义事件
            if (response != null && OnRequestFailded != null) OnRequestFailded(this, new HttpRequestFaildedEventArgs(request, response, ex));

            exception = ex;
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
            var statusCode = (int)(response?.StatusCode ?? HttpStatusCode.InternalServerError);

            // 打印失败请求
            App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", $"[StatusCode: {statusCode}] {errors}", exception != null);

            // 调用异常拦截器
            if (ExceptionInterceptors != null && ExceptionInterceptors.Count > 0) ExceptionInterceptors.ForEach(u =>
            {
                u?.Invoke(httpClient, response, errors);
            });

            // 抛出请求异常
            if (exception != null) throw exception;
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
        if (Method == HttpMethod.Get || Method == HttpMethod.Head) return;

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
                            multipartFormDataContent.Add(byteArrayContent, $"\"{httpFile.Name}\"");
                        else
                            multipartFormDataContent.Add(byteArrayContent, $"\"{httpFile.Name}\"", $"\"{httpFile.FileName}\"");
                    }

                    // 处理 Stream 文件
                    if (httpFile.FileStream != null)
                    {
                        var streamContent = new StreamContent(httpFile.FileStream, (int)httpFile.FileStream.Length);
                        streamContent.Headers.TryAddWithoutValidation("Content-Type", contentType ?? "application/octet-stream");

                        if (string.IsNullOrWhiteSpace(httpFile.FileName))
                            multipartFormDataContent.Add(streamContent, $"\"{httpFile.Name}\"");
                        else
                            multipartFormDataContent.Add(streamContent, $"\"{httpFile.Name}\"", $"\"{httpFile.FileName}\"");
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
                if (Files.Count > 0 && Files[0].Bytes.Length > 0)
                {
                    httpContent = new ByteArrayContent(Files[0].Bytes);

                    // 设置内容类型
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
                }
                break;

            case "application/json":
            case "text/json":
            case "application/*+json":
                if (Body != null)
                {
                    httpContent = new StringContent(SerializerObject(Body), ContentEncoding);

                    // 设置内容类型
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
                }
                break;

            case "application/x-www-form-urlencoded":
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

                break;

            case "application/xml":
            case "text/xml":
            case "text/html":
            case "text/plain":
                if (Body != null) httpContent = new StringContent(Body.ToString(), ContentEncoding, ContentType);
                break;

            default:
                // 其他类型可通过 HttpRequestMessage 拦截器设置
                break;
        }

        // 设置 HttpContent
        if (httpContent != null) request.Content = httpContent;
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
        return Encoding.GetEncoding(encoding);
    }
}