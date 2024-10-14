// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Diagnostics;
using System.Threading.Channels;

namespace Furion.HttpRemote;

/// <summary>
///     文件上传管理器
/// </summary>
internal sealed class FileUploadManager
{
    /// <inheritdoc cref="HttpFileUploadBuilder" />
    internal readonly HttpFileUploadBuilder _httpFileUploadBuilder;

    /// <inheritdoc cref="IHttpRemoteService" />
    internal readonly IHttpRemoteService _httpRemoteService;

    /// <summary>
    ///     文件传输进度信息的通道
    /// </summary>
    internal readonly Channel<FileTransferProgress> _progressChannel;

    /// <summary>
    ///     <inheritdoc cref="FileUploadManager" />
    /// </summary>
    /// <param name="httpRemoteService">
    ///     <see cref="IHttpRemoteService" />
    /// </param>
    /// <param name="httpFileUploadBuilder">
    ///     <see cref="HttpFileUploadBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    internal FileUploadManager(IHttpRemoteService httpRemoteService, HttpFileUploadBuilder httpFileUploadBuilder,
        Action<HttpRequestBuilder>? configure = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteService);
        ArgumentNullException.ThrowIfNull(httpFileUploadBuilder);

        _httpRemoteService = httpRemoteService;
        _httpFileUploadBuilder = httpFileUploadBuilder;

        // 初始化文件传输进度信息的通道
        _progressChannel = Channel.CreateUnbounded<FileTransferProgress>();

        // 解析 IHttpFileTransferEventHandler 事件处理程序
        FileTransferEventHandler = (httpFileUploadBuilder.FileTransferEventHandlerType is not null
            ? httpRemoteService.ServiceProvider.GetService(httpFileUploadBuilder.FileTransferEventHandlerType)
            : null) as IHttpFileTransferEventHandler;

        // 构建 HttpRequestBuilder 实例
        RequestBuilder = httpFileUploadBuilder.Build(_httpRemoteService.RemoteOptions, _progressChannel, configure);
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
    ///     开始上传
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    internal HttpResponseMessage Start(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage;

        // 创建关联的取消标识
        using var progressCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化进度报告任务
        var reportProgressTask = ReportProgressAsync(progressCancellationTokenSource.Token);

        // 处理文件传输开始
        HandleTransferStarted();

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // 发送 HTTP 远程请求
            httpResponseMessage = _httpRemoteService.Send(RequestBuilder, cancellationToken);

            // 计算文件传输总花费时间
            var duration = stopwatch.ElapsedMilliseconds;

            // 处理文件传输完成
            HandleTransferCompleted(duration);
        }
        catch (Exception e)
        {
            // 处理文件传输失败
            HandleTransferFailed(e);

            throw;
        }
        finally
        {
            // 停止计时
            stopwatch.Stop();

            // 关闭通道
            _progressChannel.Writer.Complete();

            // 等待进度报告任务完成
            progressCancellationTokenSource.Cancel();
            reportProgressTask.Wait(cancellationToken);
        }

        return httpResponseMessage;
    }

    /// <summary>
    ///     开始上传
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" />
    /// </returns>
    internal async Task<HttpResponseMessage> StartAsync(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage;

        // 创建关联的取消标识
        using var progressCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化进度报告任务
        var reportProgressTask = ReportProgressAsync(progressCancellationTokenSource.Token);

        // 处理文件传输开始
        HandleTransferStarted();

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // 发送 HTTP 远程请求
            httpResponseMessage = await _httpRemoteService.SendAsync(RequestBuilder, cancellationToken);

            // 计算文件传输总花费时间
            var duration = stopwatch.ElapsedMilliseconds;

            // 处理文件传输完成
            HandleTransferCompleted(duration);
        }
        catch (Exception e)
        {
            // 处理文件传输失败
            HandleTransferFailed(e);

            throw;
        }
        finally
        {
            // 停止计时
            stopwatch.Stop();

            // 关闭通道
            _progressChannel.Writer.Complete();

            // 等待进度报告任务完成
            await progressCancellationTokenSource.CancelAsync();
            await reportProgressTask;
        }

        return httpResponseMessage;
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
        if (_httpFileUploadBuilder.OnProgressChanged is null && FileTransferEventHandler is null)
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
                    await Task.Delay(_httpFileUploadBuilder.ProgressInterval, cancellationToken);
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

        _httpFileUploadBuilder.OnTransferStarted.TryInvoke();
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

        _httpFileUploadBuilder.OnTransferCompleted.TryInvoke(duration);
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

        _httpFileUploadBuilder.OnTransferFailed.TryInvoke(e);
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

        await _httpFileUploadBuilder.OnProgressChanged.TryInvokeAsync(fileTransferProgress);
    }
}