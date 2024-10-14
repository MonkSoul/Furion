// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     <inheritdoc cref="IHttpRemoteService" />
/// </summary>
internal sealed partial class HttpRemoteService
{
    /// <inheritdoc />
    public void DownloadFile(string? requestUri, string? destinationPath, Action<HttpRequestBuilder>? configure = null,
        Func<FileTransferProgress, Task>? onProgressChanged = null,
        FileExistsBehavior fileExistsBehavior = FileExistsBehavior.CreateNew,
        CancellationToken cancellationToken = default) =>
        Send(
            HttpRequestBuilder.DownloadFile(requestUri, destinationPath, onProgressChanged)
                .SetFileExistsBehavior(fileExistsBehavior), configure, cancellationToken);

    /// <inheritdoc />
    public Task DownloadFileAsync(string? requestUri, string? destinationPath,
        Action<HttpRequestBuilder>? configure = null, Func<FileTransferProgress, Task>? onProgressChanged = null,
        FileExistsBehavior fileExistsBehavior = FileExistsBehavior.CreateNew,
        CancellationToken cancellationToken = default) =>
        SendAsync(
            HttpRequestBuilder.DownloadFile(requestUri, destinationPath, onProgressChanged)
                .SetFileExistsBehavior(fileExistsBehavior), configure,
            cancellationToken);

    /// <inheritdoc />
    public void Send(HttpFileDownloadBuilder httpFileDownloadBuilder, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new FileDownloadManager(this, httpFileDownloadBuilder, configure).Start(cancellationToken);

    /// <inheritdoc />
    public Task SendAsync(HttpFileDownloadBuilder httpFileDownloadBuilder, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new FileDownloadManager(this, httpFileDownloadBuilder, configure).StartAsync(cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage UploadFile(string? requestUri, string fileFullName, string name = "file",
        Action<HttpRequestBuilder>? configure = null, Func<FileTransferProgress, Task>? onProgressChanged = null,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.UploadFile(requestUri, fileFullName, name, onProgressChanged), configure,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> UploadFileAsync(string? requestUri, string fileFullName, string name = "file",
        Action<HttpRequestBuilder>? configure = null, Func<FileTransferProgress, Task>? onProgressChanged = null,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.UploadFile(requestUri, fileFullName, name, onProgressChanged), configure,
            cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Send(HttpFileUploadBuilder httpFileUploadBuilder,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new FileUploadManager(this, httpFileUploadBuilder, configure).Start(cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> SendAsync(HttpFileUploadBuilder httpFileUploadBuilder,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new FileUploadManager(this, httpFileUploadBuilder, configure).StartAsync(cancellationToken);

    /// <inheritdoc />
    public void ServerSentEvents(string? requestUri, Func<ServerSentEventsData, Task> onMessage,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.ServerSentEvents(requestUri, onMessage), configure, cancellationToken);

    /// <inheritdoc />
    public Task ServerSentEventsAsync(string? requestUri, Func<ServerSentEventsData, Task> onMessage,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.ServerSentEvents(requestUri, onMessage), configure, cancellationToken);

    /// <inheritdoc />
    public void Send(HttpServerSentEventsBuilder httpServerSentEventsBuilder,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new ServerSentEventsManager(this, httpServerSentEventsBuilder, configure).Start(cancellationToken);

    /// <inheritdoc />
    public Task SendAsync(HttpServerSentEventsBuilder httpServerSentEventsBuilder,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new ServerSentEventsManager(this, httpServerSentEventsBuilder, configure).StartAsync(cancellationToken);

    /// <inheritdoc />
    public StressTestHarnessResult StressTestHarness(string? requestUri, int numberOfRequests = 100,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.StressTestHarness(requestUri).SetNumberOfRequests(numberOfRequests), configure,
            cancellationToken);

    /// <inheritdoc />
    public Task<StressTestHarnessResult> StressTestHarnessAsync(string? requestUri, int numberOfRequests = 100,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.StressTestHarness(requestUri).SetNumberOfRequests(numberOfRequests), configure,
            cancellationToken);

    /// <inheritdoc />
    public StressTestHarnessResult Send(HttpStressTestHarnessBuilder httpStressTestHarnessBuilder,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new StressTestHarnessManager(this, httpStressTestHarnessBuilder, configure).Start(cancellationToken);

    /// <inheritdoc />
    public Task<StressTestHarnessResult> SendAsync(HttpStressTestHarnessBuilder httpStressTestHarnessBuilder,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new StressTestHarnessManager(this, httpStressTestHarnessBuilder, configure).StartAsync(cancellationToken);

    /// <inheritdoc />
    public void LongPolling(string? requestUri, Func<HttpResponseMessage, Task> onDataReceived,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.LongPolling(requestUri, onDataReceived), configure, cancellationToken);

    /// <inheritdoc />
    public Task LongPollingAsync(string? requestUri, Func<HttpResponseMessage, Task> onDataReceived,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.LongPolling(requestUri, onDataReceived), configure, cancellationToken);

    /// <inheritdoc />
    public void Send(HttpLongPollingBuilder httpLongPollingBuilder, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new LongPollingManager(this, httpLongPollingBuilder, configure).Start(cancellationToken);

    /// <inheritdoc />
    public Task SendAsync(HttpLongPollingBuilder httpLongPollingBuilder, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        new LongPollingManager(this, httpLongPollingBuilder, configure).StartAsync(cancellationToken);
}