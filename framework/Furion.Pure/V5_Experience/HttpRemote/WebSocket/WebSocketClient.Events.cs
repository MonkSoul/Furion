// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     WebSocket 客户端
/// </summary>
public sealed partial class WebSocketClient
{
    /// <summary>
    ///     开始连接时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? Connecting;

    /// <summary>
    ///     连接成功时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? Connected;

    /// <summary>
    ///     开始重新连接时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? Reconnecting;

    /// <summary>
    ///     重新连接成功时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? Reconnected;

    /// <summary>
    ///     开始断开连接时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? Disconnecting;

    /// <summary>
    ///     断开连接成功时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? Disconnected;

    /// <summary>
    ///     开始接收消息时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? StartedReceiving;

    /// <summary>
    ///     停止接收消息时触发事件
    /// </summary>
    public event EventHandler<EventArgs>? StoppedReceiving;

    /// <summary>
    ///     接收文本消息事件
    /// </summary>
    public event EventHandler<WebSocketReceiveResult<string>>? Received;

    /// <summary>
    ///     接收二进制消息事件
    /// </summary>
    public event EventHandler<WebSocketReceiveResult<byte[]>>? BinaryReceived;

    /// <summary>
    ///     触发开始连接事件
    /// </summary>
    internal void OnConnecting() => Connecting?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发连接成功事件
    /// </summary>
    internal void OnConnected() => Connected?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发开始重新连接事件
    /// </summary>
    internal void OnReconnecting() => Reconnecting?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发重新连接成功事件
    /// </summary>
    internal void OnReconnected() => Reconnected?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发开始断开连接事件
    /// </summary>
    internal void OnDisconnecting() => Disconnecting?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发断开连接成功事件
    /// </summary>
    internal void OnDisconnected() => Disconnected?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发开始接收消息事件
    /// </summary>
    internal void OnStartedReceiving() => StartedReceiving?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发停止接收消息事件
    /// </summary>
    internal void OnStoppedReceiving() => StoppedReceiving?.Invoke(this, EventArgs.Empty);

    /// <summary>
    ///     触发接收文本消息事件
    /// </summary>
    /// <param name="receiveResult">
    ///     <see cref="WebSocketReceiveResult{TResult}" />
    /// </param>
    internal void OnReceived(WebSocketReceiveResult<string> receiveResult) => Received?.Invoke(this, receiveResult);

    /// <summary>
    ///     触发接收二进制消息事件
    /// </summary>
    /// <param name="receiveResult">
    ///     <see cref="WebSocketReceiveResult{TResult}" />
    /// </param>
    internal void OnBinaryReceived(WebSocketReceiveResult<byte[]> receiveResult) =>
        BinaryReceived?.Invoke(this, receiveResult);
}