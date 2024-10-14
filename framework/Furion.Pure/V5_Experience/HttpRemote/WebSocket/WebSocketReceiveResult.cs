// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Net.WebSockets;

namespace Furion.HttpRemote;

/// <summary>
///     WebSocket 接收到的数据结果
/// </summary>
/// <typeparam name="TResult">结果的目标类型</typeparam>
public sealed class WebSocketReceiveResult<TResult> : WebSocketReceiveResult
{
    /// <inheritdoc />
    public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
        : base(count, messageType, endOfMessage)
    {
    }

    /// <inheritdoc />
    public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage,
        WebSocketCloseStatus? closeStatus, string? closeStatusDescription)
        : base(count, messageType, endOfMessage,
            closeStatus, closeStatusDescription)
    {
    }

    /// <summary>
    ///     <typeparamref name="TResult" />
    /// </summary>
    public TResult Result { get; internal init; } = default!;
}