// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Threading.Channels;

namespace Furion.HttpRemote;

/// <summary>
///     长轮询管理器
/// </summary>
internal sealed class LongPollingManager
{
    /// <summary>
    ///     数据接收传输的通道
    /// </summary>
    internal readonly Channel<HttpResponseMessage> _dataChannel;

    /// <inheritdoc cref="HttpLongPollingBuilder" />
    internal readonly HttpLongPollingBuilder _httpLongPollingBuilder;

    /// <inheritdoc cref="IHttpRemoteService" />
    internal readonly IHttpRemoteService _httpRemoteService;

    /// <summary>
    ///     <inheritdoc cref="LongPollingManager" />
    /// </summary>
    /// <param name="httpRemoteService">
    ///     <see cref="IHttpRemoteService" />
    /// </param>
    /// <param name="httpLongPollingBuilder">
    ///     <see cref="HttpLongPollingBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    internal LongPollingManager(IHttpRemoteService httpRemoteService, HttpLongPollingBuilder httpLongPollingBuilder,
        Action<HttpRequestBuilder>? configure = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteService);
        ArgumentNullException.ThrowIfNull(httpLongPollingBuilder);

        _httpRemoteService = httpRemoteService;
        _httpLongPollingBuilder = httpLongPollingBuilder;

        // 初始化数据接收传输的通道
        _dataChannel = Channel.CreateUnbounded<HttpResponseMessage>();

        // 解析 IHttpLongPollingEventHandler 事件处理程序
        LongPollingEventHandler = (httpLongPollingBuilder.LongPollingEventHandlerType is not null
            ? httpRemoteService.ServiceProvider.GetService(httpLongPollingBuilder.LongPollingEventHandlerType)
            : null) as IHttpLongPollingEventHandler;

        // 构建 HttpRequestBuilder 实例
        RequestBuilder = httpLongPollingBuilder.Build(_httpRemoteService.RemoteOptions, configure);
    }

    /// <summary>
    ///     当前重试次数
    /// </summary>
    internal int CurrentRetries { get; private set; }

    /// <summary>
    ///     <inheritdoc cref="HttpRequestBuilder" />
    /// </summary>
    internal HttpRequestBuilder RequestBuilder { get; }

    /// <summary>
    ///     <inheritdoc cref="IHttpLongPollingEventHandler" />
    /// </summary>
    internal IHttpLongPollingEventHandler? LongPollingEventHandler { get; }

    /// <summary>
    ///     开始请求
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal void Start(CancellationToken cancellationToken = default)
    {
        // 创建关联的取消标识
        using var dataCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化接收数据任务
        var receiveDataTask = ReceiveDataAsync(dataCancellationTokenSource.Token);

        while (true)
        {
            try
            {
                // 发送 HTTP 远程请求
                var httpResponseMessage = _httpRemoteService.Send(RequestBuilder, cancellationToken);

                // 检查是否应该终止长轮询
                if (ShouldTerminatePolling(httpResponseMessage))
                {
                    break;
                }

                // 检查是否请求成功
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // 重置当前重试次数
                    CurrentRetries = 0;

                    // 发送响应消息对象到通道
                    _dataChannel.Writer.TryWrite(httpResponseMessage);

                    // 进入下一次循环
                    continue;
                }
            }
            catch (Exception e) when (cancellationToken.IsCancellationRequested ||
                                      e is ChannelClosedException or OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                // 输出调试事件
                Debugging.Error(e.Message);

                // 递增当前重试次数
                CurrentRetries++;

                // 检查是否超过了最大连续失败次数
                if (CurrentRetries >= _httpLongPollingBuilder.MaxRetries)
                {
                    throw;
                }
            }

            // 没有新数据时等待指定的时间间隔再重试
            Task.Delay(_httpLongPollingBuilder.PollingInterval, cancellationToken).Wait(cancellationToken);
        }

        // 关闭通道
        _dataChannel.Writer.Complete();

        // 等待接收数据任务完成
        dataCancellationTokenSource.Cancel();
        receiveDataTask.Wait(cancellationToken);
    }

    /// <summary>
    ///     开始请求
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task StartAsync(CancellationToken cancellationToken = default)
    {
        // 创建关联的取消标识
        using var dataCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化接收数据任务
        var receiveDataTask = ReceiveDataAsync(dataCancellationTokenSource.Token);

        while (true)
        {
            try
            {
                // 发送 HTTP 远程请求
                var httpResponseMessage = await _httpRemoteService.SendAsync(RequestBuilder, cancellationToken);

                // 检查是否应该终止长轮询
                if (ShouldTerminatePolling(httpResponseMessage))
                {
                    break;
                }

                // 检查是否请求成功
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // 重置当前重试次数
                    CurrentRetries = 0;

                    // 发送响应消息对象到通道
                    await _dataChannel.Writer.WriteAsync(httpResponseMessage, cancellationToken);

                    // 进入下一次循环
                    continue;
                }
            }
            catch (Exception e) when (cancellationToken.IsCancellationRequested ||
                                      e is ChannelClosedException or OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                // 输出调试事件
                Debugging.Error(e.Message);

                // 递增当前重试次数
                CurrentRetries++;

                // 检查是否超过了最大连续失败次数
                if (CurrentRetries >= _httpLongPollingBuilder.MaxRetries)
                {
                    throw;
                }
            }

            // 没有新数据时等待指定的时间间隔再重试
            await Task.Delay(_httpLongPollingBuilder.PollingInterval, cancellationToken);
        }

        // 关闭通道
        _dataChannel.Writer.Complete();

        // 等待接收数据任务完成
        await dataCancellationTokenSource.CancelAsync();
        await receiveDataTask;
    }

    /// <summary>
    ///     检查是否应该终止长轮询
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal bool ShouldTerminatePolling(HttpResponseMessage httpResponseMessage)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpResponseMessage);

        // 检查响应标头中是否存在长轮询结束符
        if (httpResponseMessage.Headers.TryGetValues(Constants.X_END_OF_STREAM_HEADER, out _))
        {
            return true;
        }

        // 如果响应状态码不是成功的，则递增当前重试次数
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            CurrentRetries++;
        }

        return CurrentRetries >= _httpLongPollingBuilder.MaxRetries;
    }

    /// <summary>
    ///     接收数据任务
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task ReceiveDataAsync(CancellationToken cancellationToken)
    {
        // 空检查
        if (_httpLongPollingBuilder.OnDataReceived is null && LongPollingEventHandler is null)
        {
            return;
        }

        try
        {
            // 从数据接收传输的通道中读取所有的数据
            await foreach (var httpResponseMessage in _dataChannel.Reader.ReadAllAsync(cancellationToken))
            {
                try
                {
                    // 处理服务器发送的数据
                    await HandleDataReceivedAsync(httpResponseMessage);
                }
                // 捕获当通道关闭或操作被取消时的异常
                catch (Exception e) when (cancellationToken.IsCancellationRequested ||
                                          e is ChannelClosedException or OperationCanceledException)
                {
                    // 处理服务器发送的数据
                    await HandleDataReceivedAsync(httpResponseMessage);

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
    ///     处理服务器发送的数据
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    internal async Task HandleDataReceivedAsync(HttpResponseMessage httpResponseMessage)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpResponseMessage);

        // 空检查
        if (LongPollingEventHandler is not null)
        {
            await DelegateExtensions.TryInvokeAsync(LongPollingEventHandler.OnDataReceivedAsync, httpResponseMessage);
        }

        await _httpLongPollingBuilder.OnDataReceived.TryInvokeAsync(httpResponseMessage);
    }
}