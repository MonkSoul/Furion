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
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Channels;

namespace Furion.HttpRemote;

/// <summary>
///     Server-Sent Events 管理器
/// </summary>
/// <remarks>参考文献：https://developer.mozilla.org/zh-CN/docs/Web/API/Server-sent_events/Using_server-sent_events。</remarks>
internal sealed class ServerSentEventsManager
{
    /// <inheritdoc cref="IHttpRemoteService" />
    internal readonly IHttpRemoteService _httpRemoteService;

    /// <inheritdoc cref="HttpServerSentEventsBuilder" />
    internal readonly HttpServerSentEventsBuilder _httpServerSentEventsBuilder;

    /// <summary>
    ///     事件消息传输的通道
    /// </summary>
    internal readonly Channel<ServerSentEventsData> _messageChannel;

    /// <summary>
    ///     <inheritdoc cref="ServerSentEventsManager" />
    /// </summary>
    /// <param name="httpRemoteService">
    ///     <see cref="IHttpRemoteService" />
    /// </param>
    /// <param name="httpServerSentEventsBuilder">
    ///     <see cref="HttpServerSentEventsBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    internal ServerSentEventsManager(IHttpRemoteService httpRemoteService,
        HttpServerSentEventsBuilder httpServerSentEventsBuilder,
        Action<HttpRequestBuilder>? configure = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteService);
        ArgumentNullException.ThrowIfNull(httpServerSentEventsBuilder);

        _httpRemoteService = httpRemoteService;
        _httpServerSentEventsBuilder = httpServerSentEventsBuilder;
        CurrentRetryInterval = httpServerSentEventsBuilder.DefaultRetryInterval;
        CurrentRetries = 0;

        // 初始化事件消息传输的通道
        _messageChannel = Channel.CreateUnbounded<ServerSentEventsData>();

        // 解析 IHttpServerSentEventsEventHandler 事件处理程序
        ServerSentEventsEventHandler = (httpServerSentEventsBuilder.ServerSentEventsEventHandlerType is not null
            ? httpRemoteService.ServiceProvider.GetService(httpServerSentEventsBuilder.ServerSentEventsEventHandlerType)
            : null) as IHttpServerSentEventsEventHandler;

        // 构建 HttpRequestBuilder 实例
        RequestBuilder = httpServerSentEventsBuilder.Build(_httpRemoteService.RemoteOptions, configure);
    }

    /// <summary>
    ///     当前重新连接的时间（毫秒）
    /// </summary>
    internal int CurrentRetryInterval { get; private set; }

    /// <summary>
    ///     当前重试次数
    /// </summary>
    internal int CurrentRetries { get; private set; }

    /// <summary>
    ///     <inheritdoc cref="HttpRequestBuilder" />
    /// </summary>
    internal HttpRequestBuilder RequestBuilder { get; }

    /// <summary>
    ///     <inheritdoc cref="IHttpServerSentEventsEventHandler" />
    /// </summary>
    internal IHttpServerSentEventsEventHandler? ServerSentEventsEventHandler { get; }

    /// <summary>
    ///     开始接收
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal void Start(CancellationToken cancellationToken = default)
    {
        // 创建关联的取消标识
        using var messageCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化接收事件消息任务
        var receiveDataTask = ReceiveDataAsync(messageCancellationTokenSource.Token);

        // 处理与事件源的连接打开
        HandleOpen();

        // 声明取消接收标识
        var isCancelled = false;

        try
        {
            // 发送 HTTP 远程请求
            var httpResponseMessage = _httpRemoteService.Send(RequestBuilder,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            // 获取 HTTP 响应体中的内容流
            using var contentStream = httpResponseMessage.Content.ReadAsStream(cancellationToken);

            // 初始化 StreamReader 实例
            using var reader = new StreamReader(contentStream, Encoding.UTF8);

            // 声明 ServerSentEventsData 变量
            ServerSentEventsData? serverSentEventsData = null;

            // 循环读取数据直到取消请求或读取完毕
            while (!cancellationToken.IsCancellationRequested && reader.ReadLine() is { } line)
            {
                // 尝试解析事件消息行文本
                if (!TryParseEventLine(line, ref serverSentEventsData))
                {
                    continue;
                }

                // 检查是否已经收集了一个完整的事件
                if (!IsEventComplete(serverSentEventsData))
                {
                    continue;
                }

                // 重置当前重试次数
                CurrentRetries = 0;

                // 发送事件数据到通道
                _messageChannel.Writer.TryWrite(serverSentEventsData);

                // 重置 ServerSentEventsData 实例，等待下一个事件
                serverSentEventsData = null;
            }
        }
        catch (Exception e) when (cancellationToken.IsCancellationRequested || e is OperationCanceledException)
        {
            // 标识客户端中止事件消息接收
            isCancelled = true;

            throw;
        }
        catch (Exception e)
        {
            // 处理与事件源的连接错误
            HandleError(e);

            // 检查是否达到了最大当前重试次数
            if (CurrentRetries < _httpServerSentEventsBuilder.MaxRetries)
            {
                // 重新开始接收
                Retry(cancellationToken);
            }
            else
            {
                throw new AggregateException(
                    $"Failed to establish Server-Sent Events connection after {_httpServerSentEventsBuilder.MaxRetries} attempts.",
                    e);
            }
        }
        finally
        {
            if (isCancelled)
            {
                // 关闭通道
                _messageChannel.Writer.Complete();
            }

            // 等待接收事件消息任务完成
            messageCancellationTokenSource.Cancel();
            receiveDataTask.Wait(cancellationToken);
        }
    }

    /// <summary>
    ///     开始接收
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task StartAsync(CancellationToken cancellationToken = default)
    {
        // 创建关联的取消标识
        using var messageCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 初始化接收事件消息任务
        var receiveDataTask = ReceiveDataAsync(messageCancellationTokenSource.Token);

        // 处理与事件源的连接打开
        HandleOpen();

        // 声明取消接收标识
        var isCancelled = false;

        try
        {
            // 发送 HTTP 远程请求
            var httpResponseMessage = await _httpRemoteService.SendAsync(RequestBuilder,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            // 获取 HTTP 响应体中的内容流
            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);

            // 初始化 StreamReader 实例
            using var reader = new StreamReader(contentStream, Encoding.UTF8);

            // 声明 ServerSentEventsData 变量
            ServerSentEventsData? serverSentEventsData = null;

            // 循环读取数据直到取消请求或读取完毕
            while (!cancellationToken.IsCancellationRequested &&
                   await reader.ReadLineAsync(cancellationToken) is { } line)
            {
                // 尝试解析事件消息行文本
                if (!TryParseEventLine(line, ref serverSentEventsData))
                {
                    continue;
                }

                // 检查是否已经收集了一个完整的事件
                if (!IsEventComplete(serverSentEventsData))
                {
                    continue;
                }

                // 重置当前重试次数
                CurrentRetries = 0;

                // 发送事件数据到通道
                await _messageChannel.Writer.WriteAsync(serverSentEventsData, cancellationToken);

                // 重置 ServerSentEventsData 实例，等待下一个事件
                serverSentEventsData = null;
            }
        }
        catch (Exception e) when (cancellationToken.IsCancellationRequested || e is OperationCanceledException)
        {
            // 标识客户端中止事件消息接收
            isCancelled = true;

            throw;
        }
        catch (Exception e)
        {
            // 处理与事件源的连接错误
            HandleError(e);

            // 检查是否达到了最大当前重试次数
            if (CurrentRetries < _httpServerSentEventsBuilder.MaxRetries)
            {
                // 重新开始接收
                await RetryAsync(cancellationToken);
            }
            else
            {
                throw new AggregateException(
                    $"Failed to establish Server-Sent Events connection after {_httpServerSentEventsBuilder.MaxRetries} attempts.",
                    e);
            }
        }
        finally
        {
            if (isCancelled)
            {
                // 关闭通道
                _messageChannel.Writer.Complete();
            }

            // 等待接收事件消息任务完成
            await messageCancellationTokenSource.CancelAsync();
            await receiveDataTask;
        }
    }

    /// <summary>
    ///     重新开始接收
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal void Retry(CancellationToken cancellationToken = default)
    {
        // 递增当前重试次数
        CurrentRetries++;

        // 根据配置的重新连接的间隔时间延迟重新开始接收
        Task.Delay(CurrentRetryInterval, cancellationToken).Wait(cancellationToken);

        // 重新开始接收
        Start(cancellationToken);
    }

    /// <summary>
    ///     重新开始接收
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task RetryAsync(CancellationToken cancellationToken = default)
    {
        // 递增当前重试次数
        CurrentRetries++;

        // 根据配置的重新连接的间隔时间延迟重新开始接收
        await Task.Delay(CurrentRetryInterval, cancellationToken);

        // 重新开始接收
        await StartAsync(cancellationToken);
    }

    /// <summary>
    ///     检查是否已经收集了一个完整的事件
    /// </summary>
    /// <param name="serverSentEventsData">
    ///     <see cref="ServerSentEventsData" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal static bool IsEventComplete(ServerSentEventsData serverSentEventsData)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(serverSentEventsData);

        return serverSentEventsData.Data.Length > 0;
    }

    /// <summary>
    ///     尝试解析事件消息行文本
    /// </summary>
    /// <param name="line"></param>
    /// <param name="serverSentEventsData">
    ///     <see cref="ServerSentEventsData" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal bool TryParseEventLine(string line, [NotNullWhen(true)] ref ServerSentEventsData? serverSentEventsData)
    {
        // 空检查（忽略空白行和注释行）
        if (string.IsNullOrWhiteSpace(line) || line.StartsWith(':'))
        {
            return false;
        }

        // 初始化 ServerSentEventsData 实例
        serverSentEventsData ??= new ServerSentEventsData();

        // 采用冒号对行文本进行分割
        var parts = line.Split(':');
        var key = parts[0].Trim();

        // 如果一行文本中不包含冒号，则整行文本会被解析成为字段名，其字段值为空
        var value = parts.Length > 1 ? parts[1].Trim() : string.Empty;

        switch (key)
        {
            case "event":
                serverSentEventsData.Event = value;
                break;
            case "data":
                serverSentEventsData.AppendData(value);
                break;
            case "id":
                serverSentEventsData.Id = value;
                break;
            case "retry":
                CurrentRetryInterval = serverSentEventsData.Retry = int.TryParse(value, out var retryInterval)
                    ? retryInterval
                    : _httpServerSentEventsBuilder.DefaultRetryInterval;
                break;
            // 所有其他的字段名都会被忽略
            default:
                serverSentEventsData = null;
                return false;
        }

        return true;
    }

    /// <summary>
    ///     接收事件消息任务
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task ReceiveDataAsync(CancellationToken cancellationToken)
    {
        // 空检查
        if (_httpServerSentEventsBuilder.OnMessage is null && ServerSentEventsEventHandler is null)
        {
            return;
        }

        try
        {
            // 从事件消息传输的通道中读取所有的事件消息
            await foreach (var serverSentEventsData in _messageChannel.Reader.ReadAllAsync(cancellationToken))
            {
                try
                {
                    // 处理服务器发送的事件消息
                    await HandleMessageReceivedAsync(serverSentEventsData);
                }
                // 捕获当通道关闭或操作被取消时的异常
                catch (Exception e) when (cancellationToken.IsCancellationRequested ||
                                          e is ChannelClosedException or OperationCanceledException)
                {
                    // 处理服务器发送的事件消息
                    await HandleMessageReceivedAsync(serverSentEventsData);

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
    ///     处理与事件源的连接打开
    /// </summary>
    internal void HandleOpen()
    {
        // 空检查
        if (ServerSentEventsEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(ServerSentEventsEventHandler.OnOpen);
        }

        _httpServerSentEventsBuilder.OnOpen.TryInvoke();
    }

    /// <summary>
    ///     处理与事件源的连接错误
    /// </summary>
    /// <param name="e">
    ///     <see cref="Exception" />
    /// </param>
    internal void HandleError(Exception e)
    {
        // 空检查
        if (ServerSentEventsEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(ServerSentEventsEventHandler.OnError, e);
        }

        _httpServerSentEventsBuilder.OnError.TryInvoke(e);
    }

    /// <summary>
    ///     处理服务器发送的事件消息
    /// </summary>
    /// <param name="serverSentEventsData">
    ///     <see cref="ServerSentEventsData" />
    /// </param>
    internal async Task HandleMessageReceivedAsync(ServerSentEventsData serverSentEventsData)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(serverSentEventsData);

        // 空检查
        if (ServerSentEventsEventHandler is not null)
        {
            await DelegateExtensions.TryInvokeAsync(ServerSentEventsEventHandler.OnMessageAsync, serverSentEventsData);
        }

        await _httpServerSentEventsBuilder.OnMessage.TryInvokeAsync(serverSentEventsData);
    }
}