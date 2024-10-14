// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="HttpRequestMessage" /> 构建器
/// </summary>
public sealed partial class HttpRequestBuilder
{
    /// <summary>
    ///     创建 <c>GET</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Get(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Get, requestUri, configure);

    /// <summary>
    ///     创建 <c>GET</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Get(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Get, requestUri, configure);

    /// <summary>
    ///     创建 <c>PUT</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Put(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Put, requestUri, configure);

    /// <summary>
    ///     创建 <c>PUT</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Put(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Put, requestUri, configure);

    /// <summary>
    ///     创建 <c>POST</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Post(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Post, requestUri, configure);

    /// <summary>
    ///     创建 <c>POST</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Post(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Post, requestUri, configure);

    /// <summary>
    ///     创建 <c>DELETE</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Delete(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Delete, requestUri, configure);

    /// <summary>
    ///     创建 <c>DELETE</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Delete(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Delete, requestUri, configure);

    /// <summary>
    ///     创建 <c>HEAD</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Head(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Head, requestUri, configure);

    /// <summary>
    ///     创建 <c>HEAD</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Head(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Head, requestUri, configure);

    /// <summary>
    ///     创建 <c>OPTIONS</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Options(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Options, requestUri, configure);

    /// <summary>
    ///     创建 <c>OPTIONS</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Options(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Options, requestUri, configure);

    /// <summary>
    ///     创建 <c>TRACE</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Trace(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Trace, requestUri, configure);

    /// <summary>
    ///     创建 <c>TRACE</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Trace(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Trace, requestUri, configure);

    /// <summary>
    ///     创建 <c>PATCH</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Patch(string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Patch, requestUri, configure);

    /// <summary>
    ///     创建 <c>PATCH</c> 请求的 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Patch(Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        Create(HttpMethod.Patch, requestUri, configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Create(HttpMethod httpMethod, Uri? requestUri) => new(httpMethod, requestUri);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Create(HttpMethod httpMethod, string? requestUri) =>
        Create(httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute));

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Create(string httpMethod, string? requestUri)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(httpMethod);

        return Create(Helpers.ParseHttpMethod(httpMethod), requestUri);
    }

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Create(string httpMethod, Uri? requestUri)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(httpMethod);

        return Create(Helpers.ParseHttpMethod(httpMethod), requestUri);
    }

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Create(HttpMethod httpMethod, Uri? requestUri,
        Action<HttpRequestBuilder>? configure)
    {
        // 初始化 HttpRequestBuilder 实例
        var httpRequestBuilder = Create(httpMethod, requestUri);

        // 调用自定义配置委托
        configure?.Invoke(httpRequestBuilder);

        return httpRequestBuilder;
    }

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Create(HttpMethod httpMethod, string? requestUri,
        Action<HttpRequestBuilder>? configure) =>
        Create(httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder Create(string httpMethod, string? requestUri,
        Action<HttpRequestBuilder>? configure)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(httpMethod);

        return Create(Helpers.ParseHttpMethod(httpMethod), requestUri, configure);
    }

    /// <summary>
    ///     创建 <see cref="HttpFileDownloadBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="destinationPath">文件保存的目标路径</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <returns>
    ///     <see cref="HttpFileDownloadBuilder" />
    /// </returns>
    public static HttpFileDownloadBuilder DownloadFile(HttpMethod httpMethod, Uri? requestUri, string? destinationPath,
        Func<FileTransferProgress, Task>? onProgressChanged = null)
    {
        var httpFileDownloadBuilder =
            new HttpFileDownloadBuilder(httpMethod, requestUri).SetDestinationPath(destinationPath);

        // 空检查
        if (onProgressChanged is not null)
        {
            httpFileDownloadBuilder.SetOnProgressChanged(onProgressChanged);
        }

        return httpFileDownloadBuilder;
    }

    /// <summary>
    ///     创建 <see cref="HttpFileDownloadBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="destinationPath">文件保存的目标路径</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <returns>
    ///     <see cref="HttpFileDownloadBuilder" />
    /// </returns>
    public static HttpFileDownloadBuilder DownloadFile(Uri? requestUri, string? destinationPath,
        Func<FileTransferProgress, Task>? onProgressChanged = null) =>
        DownloadFile(HttpMethod.Get, requestUri, destinationPath, onProgressChanged);

    /// <summary>
    ///     创建 <see cref="HttpFileDownloadBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="destinationPath">文件保存的目标路径</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <returns>
    ///     <see cref="HttpFileDownloadBuilder" />
    /// </returns>
    public static HttpFileDownloadBuilder DownloadFile(string? requestUri, string? destinationPath,
        Func<FileTransferProgress, Task>? onProgressChanged = null) =>
        DownloadFile(HttpMethod.Get,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute),
            destinationPath, onProgressChanged);

    /// <summary>
    ///     创建 <see cref="HttpFileUploadBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="name">表单名称；默认值为 <c>file</c>。</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public static HttpFileUploadBuilder UploadFile(HttpMethod httpMethod, Uri? requestUri, string fileFullName,
        string name = "file", Func<FileTransferProgress, Task>? onProgressChanged = null)
    {
        var httpFileUploadBuilder = new HttpFileUploadBuilder(httpMethod, requestUri, fileFullName, name);

        // 空检查
        if (onProgressChanged is not null)
        {
            httpFileUploadBuilder.SetOnProgressChanged(onProgressChanged);
        }

        return httpFileUploadBuilder;
    }

    /// <summary>
    ///     创建 <see cref="HttpFileUploadBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="name">表单名称；默认值为 <c>file</c>。</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public static HttpFileUploadBuilder UploadFile(Uri? requestUri, string fileFullName, string name = "file",
        Func<FileTransferProgress, Task>? onProgressChanged = null) =>
        UploadFile(HttpMethod.Post, requestUri, fileFullName, name, onProgressChanged);

    /// <summary>
    ///     创建 <see cref="HttpFileUploadBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="name">表单名称；默认值为 <c>file</c>。</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public static HttpFileUploadBuilder UploadFile(string? requestUri, string fileFullName, string name = "file",
        Func<FileTransferProgress, Task>? onProgressChanged = null) =>
        UploadFile(HttpMethod.Post,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute),
            fileFullName, name, onProgressChanged);

    /// <summary>
    ///     创建 <see cref="HttpServerSentEventsBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onMessage">用于在从事件源接收到数据时的操作</param>
    /// <returns>
    ///     <see cref="HttpServerSentEventsBuilder" />
    /// </returns>
    public static HttpServerSentEventsBuilder ServerSentEvents(Uri? requestUri,
        Func<ServerSentEventsData, Task> onMessage) =>
        new HttpServerSentEventsBuilder(requestUri).SetOnMessage(onMessage);

    /// <summary>
    ///     创建 <see cref="HttpServerSentEventsBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onMessage">用于在从事件源接收到数据时的操作</param>
    /// <returns>
    ///     <see cref="HttpServerSentEventsBuilder" />
    /// </returns>
    public static HttpServerSentEventsBuilder ServerSentEvents(string? requestUri,
        Func<ServerSentEventsData, Task> onMessage) =>
        ServerSentEvents(string.IsNullOrWhiteSpace(requestUri)
            ? null
            : new Uri(requestUri, UriKind.RelativeOrAbsolute), onMessage);

    /// <summary>
    ///     创建 <see cref="HttpStressTestHarnessBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <returns>
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </returns>
    public static HttpStressTestHarnessBuilder StressTestHarness(HttpMethod httpMethod, Uri? requestUri) =>
        new(httpMethod, requestUri);

    /// <summary>
    ///     创建 <see cref="HttpStressTestHarnessBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <returns>
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </returns>
    public static HttpStressTestHarnessBuilder StressTestHarness(Uri? requestUri) =>
        StressTestHarness(HttpMethod.Get, requestUri);

    /// <summary>
    ///     创建 <see cref="HttpStressTestHarnessBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <returns>
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </returns>
    public static HttpStressTestHarnessBuilder StressTestHarness(string? requestUri) =>
        StressTestHarness(HttpMethod.Get,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute));

    /// <summary>
    ///     创建 <see cref="HttpLongPollingBuilder" /> 构建器
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onDataReceived">用于在长轮询时接收到数据时的操作</param>
    /// <returns>
    ///     <see cref="HttpLongPollingBuilder" />
    /// </returns>
    public static HttpLongPollingBuilder
        LongPolling(HttpMethod httpMethod, Uri? requestUri, Func<HttpResponseMessage, Task> onDataReceived) =>
        new HttpLongPollingBuilder(httpMethod, requestUri).SetOnDataReceived(onDataReceived);

    /// <summary>
    ///     创建 <see cref="HttpLongPollingBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onDataReceived">用于在长轮询时接收到数据时的操作</param>
    /// <returns>
    ///     <see cref="HttpLongPollingBuilder" />
    /// </returns>
    public static HttpLongPollingBuilder LongPolling(Uri? requestUri, Func<HttpResponseMessage, Task> onDataReceived) =>
        LongPolling(HttpMethod.Get, requestUri, onDataReceived);

    /// <summary>
    ///     创建 <see cref="HttpLongPollingBuilder" /> 构建器
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onDataReceived">用于在长轮询时接收到数据时的操作</param>
    /// <returns>
    ///     <see cref="HttpLongPollingBuilder" />
    /// </returns>
    public static HttpLongPollingBuilder
        LongPolling(string? requestUri, Func<HttpResponseMessage, Task> onDataReceived) =>
        LongPolling(HttpMethod.Get,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute),
            onDataReceived);
}