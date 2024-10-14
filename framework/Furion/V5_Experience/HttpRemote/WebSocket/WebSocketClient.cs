// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Net.WebSockets;
using System.Text;

namespace Furion.HttpRemote;

/// <summary>
///     WebSocket 客户端
/// </summary>
public sealed partial class WebSocketClient : IDisposable
{
    /// <inheritdoc cref="ClientWebSocket" />
    internal ClientWebSocket? _clientWebSocket;

    /// <summary>
    ///     取消接收服务器消息标记
    /// </summary>
    internal CancellationTokenSource? _messageCancellationTokenSource;

    /// <summary>
    ///     接收服务器消息任务
    /// </summary>
    internal Task? _receiveTask;

    /// <summary>
    ///     <inheritdoc cref="WebSocketClient" />
    /// </summary>
    /// <param name="serverUri">服务器地址</param>
    public WebSocketClient(string serverUri)
        : this(new WebSocketClientOptions(serverUri))
    {
    }

    /// <summary>
    ///     <inheritdoc cref="WebSocketClient" />
    /// </summary>
    /// <param name="serverUri">服务器地址</param>
    public WebSocketClient(Uri serverUri)
        : this(new WebSocketClientOptions(serverUri))
    {
    }

    /// <summary>
    ///     <inheritdoc cref="WebSocketClient" />
    /// </summary>
    /// <param name="options">
    ///     <see cref="WebSocketClientOptions" />
    /// </param>
    public WebSocketClient(WebSocketClientOptions options)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(options);

        Options = options;
    }

    /// <inheritdoc cref="WebSocketState" />
    public WebSocketState? State => _clientWebSocket?.State;

    /// <summary>
    ///     <see cref="WebSocketClientOptions" />
    /// </summary>
    internal WebSocketClientOptions Options { get; }

    /// <summary>
    ///     当前重连次数
    /// </summary>
    internal int CurrentReconnectRetries { get; private set; }

    /// <inheritdoc />
    public void Dispose()
    {
        // 释放 ClientWebSocket 实例
        _clientWebSocket?.Dispose();
        _clientWebSocket = null;

        // 等待接收服务器消息任务完成
        _messageCancellationTokenSource?.Cancel();
        _messageCancellationTokenSource?.Dispose();
        _messageCancellationTokenSource = null;

        _receiveTask?.Wait();
        _receiveTask = null;
    }

    /// <summary>
    ///     连接到服务器
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        // 初始化 ClientWebSocket 实例
        _clientWebSocket ??= new ClientWebSocket();

        // 调用用于配置 ConfigureClientWebSocketOptions 的操作
        Options.ConfigureClientWebSocketOptions?.Invoke(_clientWebSocket.Options);

        // 检查连接是否处于正在连接或打开状态，如果是则跳过
        if (State is WebSocketState.Connecting or WebSocketState.Open)
        {
            if (State == WebSocketState.Open)
            {
                // 重置当前重连次数
                CurrentReconnectRetries = 0;
            }

            return;
        }

        // 创建关联的超时 Token 标识
        using var timeoutCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 设置连接超时时间控制
        if (Options.Timeout is not null && Options.Timeout.Value != TimeSpan.Zero)
        {
            timeoutCancellationTokenSource.CancelAfter(Options.Timeout.Value);
        }

        // 触发开始连接事件
        var onConnecting = OnConnecting;
        onConnecting.TryInvoke();

        try
        {
            // 连接到服务器
            await _clientWebSocket.ConnectAsync(Options.ServerUri, timeoutCancellationTokenSource.Token);

            // 重置当前重连次数
            CurrentReconnectRetries = 0;

            // 触发连接成功事件
            var onConnected = OnConnected;
            onConnected.TryInvoke();
        }
        catch (Exception e) when (cancellationToken.IsCancellationRequested || e is OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // 释放 WebSocketClient 实例
            Dispose();

            // 输出调试事件
            Debugging.Error(e.Message);

            // 检查是否达到了最大重连次数
            if (CurrentReconnectRetries < Options.MaxReconnectRetries)
            {
                // 触发开始重新连接事件
                var onReconnecting = OnReconnecting;
                onReconnecting.TryInvoke();

                // 重新连接到服务器
                await ReconnectAsync(cancellationToken);
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    ///     重新连接到服务器
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task ReconnectAsync(CancellationToken cancellationToken = default)
    {
        // 递增当前重连次数
        CurrentReconnectRetries++;

        // 根据配置的重连的间隔时间延迟重新开始连接
        await Task.Delay(Options.ReconnectInterval, cancellationToken);

        // 重新连接到服务器
        await ConnectAsync(cancellationToken);

        // 触发重新连接成功事件
        var onReconnected = OnReconnected;
        onReconnected.TryInvoke();
    }

    /// <summary>
    ///     开始监听服务器消息
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    public Task ListeningAsync(CancellationToken cancellationToken = default)
    {
        // 检查连接是否处于打开状态
        if (State == WebSocketState.Open)
        {
            // 初始化接收服务器消息任务
            _receiveTask ??= ReceiveAsync(cancellationToken);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    ///     等待接收服务器消息
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    public async Task WaitToReceiveAsync(CancellationToken cancellationToken = default)
    {
        // 检查连接是否处于打开状态
        if (State != WebSocketState.Open)
        {
            return;
        }

        // 空检查
        if (_receiveTask is not null)
        {
            await _receiveTask;
        }
        else
        {
            await ReceiveAsync(cancellationToken);
        }
    }

    /// <summary>
    ///     接收服务器消息
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal async Task ReceiveAsync(CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(_clientWebSocket);

        // 创建关联的取消接收服务器消息 Token 标识
        _messageCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 触发开始接收消息事件
        var onStartedReceiving = OnStartedReceiving;
        onStartedReceiving.TryInvoke();

        // 初始化缓冲区大小
        var buffer = new byte[Options.ReceiveBufferSize];

        try
        {
            // 循环读取服务器消息直到取消请求或连接处于非打开状态
            while (!cancellationToken.IsCancellationRequested && State == WebSocketState.Open)
            {
                try
                {
                    // 获取接收到的数据
                    var receiveResult =
                        await _clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                    // 如果接收到关闭帧，则退出循环
                    if (receiveResult.MessageType == WebSocketMessageType.Close || receiveResult.CloseStatus.HasValue)
                    {
                        break;
                    }

                    // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                    switch (receiveResult.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            // 解码接收到的文本消息
                            var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);

                            // 初始化 WebSocketReceiveResult<TResult> 实例
                            var textReceiveResult = new WebSocketReceiveResult<string>(receiveResult.Count,
                                receiveResult.MessageType,
                                receiveResult.EndOfMessage, receiveResult.CloseStatus,
                                receiveResult.CloseStatusDescription) { Result = message };

                            // 触发接收文本消息事件
                            var onReceived = OnReceived;
                            onReceived.TryInvoke(textReceiveResult);
                            break;
                        case WebSocketMessageType.Binary:
                            // 将接收到的数据从原始缓冲区复制到新创建的字节数组中
                            var bytes = new byte[receiveResult.Count];
                            Buffer.BlockCopy(buffer, 0, bytes, 0, receiveResult.Count);

                            // 初始化 WebSocketReceiveResult<TResult> 实例
                            var binaryReceiveResult = new WebSocketReceiveResult<byte[]>(receiveResult.Count,
                                receiveResult.MessageType,
                                receiveResult.EndOfMessage, receiveResult.CloseStatus,
                                receiveResult.CloseStatusDescription) { Result = bytes };

                            // 触发接收二进制消息事件
                            var onBinaryReceived = OnBinaryReceived;
                            onBinaryReceived.TryInvoke(binaryReceiveResult);
                            break;
                    }

                    // 如果这是消息的最后一部分，则清空缓冲区
                    if (receiveResult.EndOfMessage)
                    {
                        Array.Clear(buffer, 0, buffer.Length);
                    }
                }
                catch (Exception e) when (cancellationToken.IsCancellationRequested || e is OperationCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    // 输出调试事件
                    Debugging.Error(e.Message);
                }
            }
        }
        finally
        {
            // 触发停止接收消息事件
            var onStoppedReceiving = OnStoppedReceiving;
            onStoppedReceiving.TryInvoke();

            // 断开连接
            await DisconnectAsync(cancellationToken);
        }
    }

    /// <summary>
    ///     向服务器发送消息
    /// </summary>
    /// <param name="message">字符串消息</param>
    /// <param name="endOfMessage">是否作为消息的最后一部分，默认值为 <c>true</c>。</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    public Task SendAsync(string message, bool endOfMessage = true, CancellationToken cancellationToken = default) =>
        SendAsync(message, WebSocketMessageType.Text, endOfMessage, cancellationToken);

    /// <summary>
    ///     向服务器发送消息
    /// </summary>
    /// <param name="message">字符串消息</param>
    /// <param name="webSocketMessageType">
    ///     <see cref="WebSocketMessageType" />
    /// </param>
    /// <param name="endOfMessage">是否作为消息的最后一部分，默认值为 <c>true</c>。</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    public async Task SendAsync(string message, WebSocketMessageType webSocketMessageType, bool endOfMessage = true,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(message);

        // 检查连接是否处于打开状态
        if (State != WebSocketState.Open)
        {
            return;
        }

        // 空检查
        ArgumentNullException.ThrowIfNull(_clientWebSocket);

        // 将字符串编码为字节数组
        var buffer = Encoding.UTF8.GetBytes(message);

        // 初始化 ArraySegment 实例
        var arraySegment = new ArraySegment<byte>(buffer);

        // 向服务器发送消息
        await _clientWebSocket.SendAsync(arraySegment, webSocketMessageType, endOfMessage, cancellationToken);
    }

    /// <summary>
    ///     向服务器发送消息
    /// </summary>
    /// <param name="bytes">二进制消息</param>
    /// <param name="endOfMessage">是否作为消息的最后一部分，默认值为 <c>true</c>。</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    public async Task SendAsync(byte[] bytes, bool endOfMessage = true, CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(bytes);

        // 检查连接是否处于打开状态
        if (State != WebSocketState.Open)
        {
            return;
        }

        // 空检查
        ArgumentNullException.ThrowIfNull(_clientWebSocket);

        // 初始化 ArraySegment 实例
        var arraySegment = new ArraySegment<byte>(bytes);

        // 向服务器发送二进制消息
        await _clientWebSocket.SendAsync(arraySegment, WebSocketMessageType.Binary, endOfMessage, cancellationToken);
    }

    /// <summary>
    ///     断开连接
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    public async Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        // 检查连接是否处于关闭状态
        if (State is null or WebSocketState.CloseSent or WebSocketState.Closed)
        {
            return;
        }

        // 空检查
        ArgumentNullException.ThrowIfNull(_clientWebSocket);

        // 触发开始断开连接事件
        var onDisconnecting = OnDisconnecting;
        onDisconnecting.TryInvoke();

        try
        {
            // 发送关闭帧并断开连接
            await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
        }
        catch (Exception e)
        {
            // 输出调试事件
            Debugging.Error(e.Message);
        }
        finally
        {
            // 释放 WebSocketClient 实例
            Dispose();

            // 重置当前重连次数
            CurrentReconnectRetries = 0;

            // 触发断开连接成功事件
            var onDisconnected = OnDisconnected;
            onDisconnected.TryInvoke();
        }
    }
}