// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 远程请求服务
/// </summary>
public partial interface IHttpRemoteService
{
    /// <summary>
    ///     下载文件
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="destinationPath">文件保存的目标路径</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <param name="fileExistsBehavior">
    ///     <see cref="FileExistsBehavior" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    void DownloadFile(string? requestUri, string? destinationPath, Action<HttpRequestBuilder>? configure = null,
        Func<FileTransferProgress, Task>? onProgressChanged = null,
        FileExistsBehavior fileExistsBehavior = FileExistsBehavior.CreateNew,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     下载文件
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="destinationPath">文件保存的目标路径</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <param name="fileExistsBehavior">
    ///     <see cref="FileExistsBehavior" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task DownloadFileAsync(string? requestUri, string? destinationPath, Action<HttpRequestBuilder>? configure = null,
        Func<FileTransferProgress, Task>? onProgressChanged = null,
        FileExistsBehavior fileExistsBehavior = FileExistsBehavior.CreateNew,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     下载文件
    /// </summary>
    /// <param name="httpFileDownloadBuilder">
    ///     <see cref="HttpFileDownloadBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    void Send(HttpFileDownloadBuilder httpFileDownloadBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     下载文件
    /// </summary>
    /// <param name="httpFileDownloadBuilder">
    ///     <see cref="HttpFileDownloadBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task SendAsync(HttpFileDownloadBuilder httpFileDownloadBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="name">表单名称；默认值为 <c>file</c>。</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage UploadFile(string? requestUri, string fileFullName, string name = "file",
        Action<HttpRequestBuilder>? configure = null, Func<FileTransferProgress, Task>? onProgressChanged = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="name">表单名称；默认值为 <c>file</c>。</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="onProgressChanged">用于传输进度发生变化时执行的委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" />
    /// </returns>
    Task<HttpResponseMessage> UploadFileAsync(string? requestUri, string fileFullName, string name = "file",
        Action<HttpRequestBuilder>? configure = null, Func<FileTransferProgress, Task>? onProgressChanged = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="httpFileUploadBuilder">
    ///     <see cref="HttpFileUploadBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Send(HttpFileUploadBuilder httpFileUploadBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="httpFileUploadBuilder">
    ///     <see cref="HttpFileUploadBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" />
    /// </returns>
    Task<HttpResponseMessage> SendAsync(HttpFileUploadBuilder httpFileUploadBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 Server-Sent Events 请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onMessage">用于在从事件源接收到数据时的操作</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    void ServerSentEvents(string? requestUri, Func<ServerSentEventsData, Task> onMessage,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 Server-Sent Events 请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onMessage">用于在从事件源接收到数据时的操作</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task ServerSentEventsAsync(string? requestUri, Func<ServerSentEventsData, Task> onMessage,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 Server-Sent Events 请求
    /// </summary>
    /// <param name="httpServerSentEventsBuilder">
    ///     <see cref="HttpServerSentEventsBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    void Send(HttpServerSentEventsBuilder httpServerSentEventsBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 Server-Sent Events 请求
    /// </summary>
    /// <param name="httpServerSentEventsBuilder">
    ///     <see cref="HttpServerSentEventsBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task SendAsync(HttpServerSentEventsBuilder httpServerSentEventsBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     压力测试
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="numberOfRequests">并发请求数量，默认值为：100。</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="StressTestHarnessResult" />
    /// </returns>
    StressTestHarnessResult StressTestHarness(string? requestUri, int numberOfRequests = 100,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     压力测试
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="numberOfRequests">并发请求数量，默认值为：100。</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" />
    /// </returns>
    Task<StressTestHarnessResult> StressTestHarnessAsync(string? requestUri, int numberOfRequests = 100,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     压力测试
    /// </summary>
    /// <param name="httpStressTestHarnessBuilder">
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="StressTestHarnessResult" />
    /// </returns>
    StressTestHarnessResult Send(HttpStressTestHarnessBuilder httpStressTestHarnessBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     压力测试
    /// </summary>
    /// <param name="httpStressTestHarnessBuilder">
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" />
    /// </returns>
    Task<StressTestHarnessResult> SendAsync(HttpStressTestHarnessBuilder httpStressTestHarnessBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送长轮询请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onDataReceived">用于在长轮询时接收到数据时的操作</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    void LongPolling(string? requestUri, Func<HttpResponseMessage, Task> onDataReceived,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送长轮询请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="onDataReceived">用于在长轮询时接收到数据时的操作</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task LongPollingAsync(string? requestUri, Func<HttpResponseMessage, Task> onDataReceived,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送长轮询请求
    /// </summary>
    /// <param name="httpLongPollingBuilder">
    ///     <see cref="HttpLongPollingBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    void Send(HttpLongPollingBuilder httpLongPollingBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送长轮询请求
    /// </summary>
    /// <param name="httpLongPollingBuilder">
    ///     <see cref="HttpLongPollingBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task SendAsync(HttpLongPollingBuilder httpLongPollingBuilder,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default);
}