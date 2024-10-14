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

using Furion.Extensions;
using System.Diagnostics;
using System.Threading.Channels;

namespace Furion.HttpRemote;

/// <summary>
///     文件下载管理器
/// </summary>
internal sealed class FileDownloadManager
{
    /// <inheritdoc cref="HttpFileDownloadBuilder" />
    internal readonly HttpFileDownloadBuilder _httpFileDownloadBuilder;

    /// <inheritdoc cref="IHttpRemoteService" />
    internal readonly IHttpRemoteService _httpRemoteService;

    /// <summary>
    ///     文件传输进度信息的通道
    /// </summary>
    internal readonly Channel<FileTransferProgress> _progressChannel;

    /// <summary>
    ///     <inheritdoc cref="FileDownloadManager" />
    /// </summary>
    /// <param name="httpRemoteService">
    ///     <see cref="IHttpRemoteService" />
    /// </param>
    /// <param name="httpFileDownloadBuilder">
    ///     <see cref="HttpFileDownloadBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    internal FileDownloadManager(IHttpRemoteService httpRemoteService, HttpFileDownloadBuilder httpFileDownloadBuilder,
        Action<HttpRequestBuilder>? configure = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteService);
        ArgumentNullException.ThrowIfNull(httpFileDownloadBuilder);

        _httpRemoteService = httpRemoteService;
        _httpFileDownloadBuilder = httpFileDownloadBuilder;

        // 初始化文件传输进度信息的通道
        _progressChannel = Channel.CreateUnbounded<FileTransferProgress>();

        // 解析 IHttpFileTransferEventHandler 事件处理程序
        FileTransferEventHandler = (httpFileDownloadBuilder.FileTransferEventHandlerType is not null
            ? httpRemoteService.ServiceProvider.GetService(httpFileDownloadBuilder.FileTransferEventHandlerType)
            : null) as IHttpFileTransferEventHandler;

        // 构建 HttpRequestBuilder 实例
        RequestBuilder = httpFileDownloadBuilder.Build(_httpRemoteService.RemoteOptions, configure);
    }

    /// <summary>
    ///     <inheritdoc cref="HttpRequestBuilder" />
    /// </summary>
    internal HttpRequestBuilder RequestBuilder { get; }

    /// <summary>
    ///     <inheritdoc cref="IHttpFileTransferEventHandler" />
    /// </summary>
    internal IHttpFileTransferEventHandler? FileTransferEventHandler { get; }

    /// <summary>
    ///     开始下载
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal void Start(CancellationToken cancellationToken = default)
    {
        // 创建关联的取消标识
        using var progressCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化进度报告任务
        var reportProgressTask = ReportProgressAsync(progressCancellationTokenSource.Token);

        // 处理文件传输开始
        HandleTransferStarted();

        // 初始化读取数据的缓冲区和记录进度所需的变量
        var bufferSize = _httpFileDownloadBuilder.BufferSize;
        var buffer = new byte[bufferSize];
        var bytesReceived = 0L;

        // 获取临时文件路径
        var tempDestinationPath = Path.GetTempFileName();

        // 声明 FileStream 变量
        FileStream? fileStream = null;

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // 发送 HTTP 远程请求
            var httpResponseMessage = _httpRemoteService.Send(RequestBuilder,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            // 根据文件是否存在及配置的行为来决定是否应继续进行文件下载
            if (!ShouldContinueWithDownload(httpResponseMessage, out var destinationPath))
            {
                return;
            }

            // 初始化 FileTransferProgress 实例
            var fileTransferProgress =
                new FileTransferProgress(destinationPath, httpResponseMessage.Content.Headers.ContentLength ?? -1);

            // 初始化 FileStream 实例，使用文件流创建文件，设置写入模式，并允许其他进程同时读取文件
            fileStream = new FileStream(tempDestinationPath, FileMode.Create, FileAccess.Write, FileShare.Read,
                bufferSize);

            // 获取 HTTP 响应体中的内容流
            using var contentStream = httpResponseMessage.Content.ReadAsStream(cancellationToken);

            // 循环读取数据直到取消请求或读取完毕
            int numBytesRead;
            while (!cancellationToken.IsCancellationRequested &&
                   (numBytesRead = contentStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // 将读取的数据写入文件
                fileStream.Write(buffer, 0, numBytesRead);

                // 更新文件传输进度
                bytesReceived += numBytesRead;
                fileTransferProgress.UpdateProgress(bytesReceived, stopwatch.Elapsed);

                // 发送文件传输进度到通道
                _progressChannel.Writer.TryWrite(fileTransferProgress);
            }

            // 移动临时文件至文件保存的目标路径
            MoveTempFileToDestinationPath(fileStream, tempDestinationPath, destinationPath);

            // 计算文件传输总花费时间
            var duration = stopwatch.ElapsedMilliseconds;

            // 处理文件传输完成
            HandleTransferCompleted(duration);
        }
        catch (Exception e)
        {
            // 清理临时文件
            fileStream?.Close();
            if (File.Exists(tempDestinationPath))
            {
                File.Delete(tempDestinationPath);
            }

            // 处理文件传输失败
            HandleTransferFailed(e);

            throw;
        }
        finally
        {
            // 释放 FileStream 实例
            fileStream?.Dispose();

            // 停止计时
            stopwatch.Stop();

            // 关闭通道
            _progressChannel.Writer.Complete();

            // 等待进度报告任务完成
            progressCancellationTokenSource.Cancel();
            reportProgressTask.Wait(cancellationToken);
        }
    }

    /// <summary>
    ///     开始下载
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task StartAsync(CancellationToken cancellationToken = default)
    {
        // 创建关联的取消标识
        using var progressCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化进度报告任务
        var reportProgressTask = ReportProgressAsync(progressCancellationTokenSource.Token);

        // 处理文件传输开始
        HandleTransferStarted();

        // 初始化读取数据的缓冲区和记录进度所需的变量
        var bufferSize = _httpFileDownloadBuilder.BufferSize;
        var buffer = new byte[bufferSize];
        var bytesReceived = 0L;

        // 获取临时文件路径
        var tempDestinationPath = Path.GetTempFileName();

        // 声明 FileStream 变量
        FileStream? fileStream = null;

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // 发送 HTTP 远程请求
            var httpResponseMessage = await _httpRemoteService.SendAsync(RequestBuilder,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            // 根据文件是否存在及配置的行为来决定是否应继续进行文件下载
            if (!ShouldContinueWithDownload(httpResponseMessage, out var destinationPath))
            {
                return;
            }

            // 初始化 FileTransferProgress 实例
            var fileTransferProgress =
                new FileTransferProgress(destinationPath, httpResponseMessage.Content.Headers.ContentLength ?? -1);

            // 初始化 FileStream 实例，使用文件流创建文件，设置写入模式，并允许其他进程同时读取文件
            fileStream = new FileStream(tempDestinationPath, FileMode.Create, FileAccess.Write,
                FileShare.Read,
                bufferSize, true);

            // 获取 HTTP 响应体中的内容流
            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);

            // 循环读取数据直到取消请求或读取完毕
            int numBytesRead;
            while (!cancellationToken.IsCancellationRequested &&
                   (numBytesRead = await contentStream.ReadAsync(buffer, cancellationToken)) > 0)
            {
                // 将读取的数据写入文件
                await fileStream.WriteAsync(buffer.AsMemory(0, numBytesRead), cancellationToken);

                // 更新文件传输进度
                bytesReceived += numBytesRead;
                fileTransferProgress.UpdateProgress(bytesReceived, stopwatch.Elapsed);

                // 发送文件传输进度到通道
                await _progressChannel.Writer.WriteAsync(fileTransferProgress, cancellationToken);
            }

            // 移动临时文件至文件保存的目标路径
            MoveTempFileToDestinationPath(fileStream, tempDestinationPath, destinationPath);

            // 计算文件传输总花费时间
            var duration = stopwatch.ElapsedMilliseconds;

            // 处理文件传输完成
            HandleTransferCompleted(duration);
        }
        catch (Exception e)
        {
            // 清理临时文件
            fileStream?.Close();
            if (File.Exists(tempDestinationPath))
            {
                File.Delete(tempDestinationPath);
            }

            // 处理文件传输失败
            HandleTransferFailed(e);

            throw;
        }
        finally
        {
            // 释放 FileStream 实例
            if (fileStream is not null)
            {
                await fileStream.DisposeAsync();
            }

            // 停止计时
            stopwatch.Stop();

            // 关闭通道
            _progressChannel.Writer.Complete();

            // 等待进度报告任务完成
            await progressCancellationTokenSource.CancelAsync();
            await reportProgressTask;
        }
    }

    /// <summary>
    ///     文件传输进度报告任务
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task ReportProgressAsync(CancellationToken cancellationToken)
    {
        // 空检查
        if (_httpFileDownloadBuilder.OnProgressChanged is null && FileTransferEventHandler is null)
        {
            return;
        }

        try
        {
            // 从进度通道中读取所有的进度信息
            await foreach (var fileTransferProgress in _progressChannel.Reader.ReadAllAsync(cancellationToken))
            {
                try
                {
                    // 处理文件传输进度变化
                    await HandleProgressChangedAsync(fileTransferProgress);

                    // 根据配置的进度更新（通知）的间隔时间延迟进度报告
                    await Task.Delay(_httpFileDownloadBuilder.ProgressInterval, cancellationToken);
                }
                // 捕获当通道关闭或操作被取消时的异常
                catch (Exception e) when (cancellationToken.IsCancellationRequested ||
                                          e is ChannelClosedException or OperationCanceledException)
                {
                    // 处理文件传输进度变化
                    await HandleProgressChangedAsync(fileTransferProgress);

                    break;
                }
                catch (Exception e)
                {
                    // 输出调试事件
                    Debugging.Error(e.Message);
                }
            }
        }
        catch (Exception e) when (cancellationToken.IsCancellationRequested || e is OperationCanceledException)
        {
            // 任务被取消
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
    }

    /// <summary>
    ///     处理文件传输开始
    /// </summary>
    internal void HandleTransferStarted()
    {
        // 空检查
        if (FileTransferEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(FileTransferEventHandler.OnTransferStarted);
        }

        _httpFileDownloadBuilder.OnTransferStarted.TryInvoke();
    }

    /// <summary>
    ///     处理文件传输完成
    /// </summary>
    /// <param name="duration">文件传输总花费时间</param>
    internal void HandleTransferCompleted(long duration)
    {
        // 空检查
        if (FileTransferEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(FileTransferEventHandler.OnTransferCompleted, duration);
        }

        _httpFileDownloadBuilder.OnTransferCompleted.TryInvoke(duration);
    }

    /// <summary>
    ///     处理文件传输失败
    /// </summary>
    /// <param name="e">
    ///     <see cref="Exception" />
    /// </param>
    internal void HandleTransferFailed(Exception e)
    {
        // 空检查
        if (FileTransferEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(FileTransferEventHandler.OnTransferFailed, e);
        }

        _httpFileDownloadBuilder.OnTransferFailed.TryInvoke(e);
    }

    /// <summary>
    ///     处理文件传输进度变化
    /// </summary>
    /// <param name="fileTransferProgress">
    ///     <see cref="FileTransferProgress" />
    /// </param>
    internal async Task HandleProgressChangedAsync(FileTransferProgress fileTransferProgress)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(fileTransferProgress);

        // 空检查
        if (FileTransferEventHandler is not null)
        {
            await DelegateExtensions.TryInvokeAsync(FileTransferEventHandler.OnProgressChangedAsync,
                fileTransferProgress);
        }

        await _httpFileDownloadBuilder.OnProgressChanged.TryInvokeAsync(fileTransferProgress);
    }

    /// <summary>
    ///     根据文件是否存在及配置的行为来决定是否应继续进行文件下载
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="destinationPath">文件保存的目标路径</param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    internal bool ShouldContinueWithDownload(HttpResponseMessage httpResponseMessage, out string destinationPath)
    {
        // 生成完整的文件存储路径
        destinationPath = Path.GetFullPath(Path.Combine(
            Path.GetDirectoryName(_httpFileDownloadBuilder.DestinationPath) ?? string.Empty,
            GetFileName(httpResponseMessage)));

        // 检查文件是否存在
        if (!File.Exists(destinationPath))
        {
            return true;
        }

        // 检查文件存在时的行为
        switch (_httpFileDownloadBuilder.FileExistsBehavior)
        {
            case FileExistsBehavior.CreateNew:
                throw new InvalidOperationException($"The destination path `{destinationPath}` already exists.");
            case FileExistsBehavior.Skip:
                // 输出调试事件
                Debugging.File(
                    $"The destination path `{destinationPath}` already exists; skipping the file download operation.");
                return false;
            case FileExistsBehavior.Overwrite:
            default:
                break;
        }

        return true;
    }

    /// <summary>
    ///     获取文件下载名
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal string GetFileName(HttpResponseMessage httpResponseMessage)
    {
        // 获取文件下载保存的文件名
        var fileName = Path.GetFileName(_httpFileDownloadBuilder.DestinationPath);

        // 空检查
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            return fileName;
        }

        // 尝试从响应标头 Content-Disposition 中解析
        var contentDisposition = httpResponseMessage.Content.Headers.ContentDisposition;
        if (!string.IsNullOrWhiteSpace(contentDisposition?.FileNameStar))
        {
            // 使用 UTF-8 解码文件名
            fileName = Uri.UnescapeDataString(contentDisposition.FileNameStar);
        }
        else if (!string.IsNullOrWhiteSpace(contentDisposition?.FileName))
        {
            // 使用 UTF-8 解码文件名
            fileName = Uri.UnescapeDataString(contentDisposition.FileName);
        }

        // 空检查
        if (string.IsNullOrWhiteSpace(fileName))
        {
            // 尝试从原始的请求地址中解析
            fileName = Helpers.GetFileNameFromUri(httpResponseMessage.RequestMessage?.RequestUri);
        }

        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        return fileName;
    }

    /// <summary>
    ///     移动临时文件至文件保存的目标路径
    /// </summary>
    /// <param name="fileStream">
    ///     <see cref="FileStream" />
    /// </param>
    /// <param name="tempDestinationPath">临时文件路径</param>
    /// <param name="destinationPath">文件保存的目标路径</param>
    internal static void MoveTempFileToDestinationPath(FileStream fileStream, string tempDestinationPath,
        string destinationPath)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(fileStream);
        ArgumentException.ThrowIfNullOrWhiteSpace(tempDestinationPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationPath);

        // 检查临时文件是否存在
        if (!File.Exists(tempDestinationPath))
        {
            throw new FileNotFoundException($"The temp destination path `{tempDestinationPath}` does not exist.");
        }

        // 获取文件保存的目标目录
        var destinationDirectory = Path.GetDirectoryName(destinationPath);

        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationDirectory);

        // 如果目录不存在则创建
        if (!Directory.Exists(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }

        // 如果下载成功，则移动临时文件到文件保存的目标路径（文件存在则替换）
        fileStream.Close();
        File.Move(tempDestinationPath, destinationPath, true);
    }
}