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