// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Net.WebSockets;

namespace Furion.HttpRemote;

/// <summary>
///     WebSocket 客户端配置选项
/// </summary>
public sealed class WebSocketClientOptions
{
    /// <summary>
    ///     <inheritdoc cref="WebSocketClientOptions" />
    /// </summary>
    /// <param name="serverUri">服务器地址</param>
    public WebSocketClientOptions(string serverUri)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(serverUri);

        ServerUri = new Uri(serverUri);
    }

    /// <summary>
    ///     <inheritdoc cref="WebSocketClientOptions" />
    /// </summary>
    /// <param name="serverUri">服务器地址</param>
    public WebSocketClientOptions(Uri serverUri)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(serverUri);

        ServerUri = serverUri;
    }

    /// <summary>
    ///     服务器地址
    /// </summary>
    public Uri ServerUri { get; }

    /// <summary>
    ///     重新连接的间隔时间（毫秒）
    /// </summary>
    /// <remarks>默认值为 2 秒。</remarks>
    public TimeSpan ReconnectInterval { get; set; } = TimeSpan.FromSeconds(2);

    /// <summary>
    ///     最大重连次数
    /// </summary>
    /// <remarks>默认最大重连次数为 10。</remarks>
    public int MaxReconnectRetries { get; set; } = 10;

    /// <summary>
    ///     超时时间
    /// </summary>
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    ///     接收服务器新消息缓冲区大小
    /// </summary>
    /// <remarks>默认值为：4096。</remarks>
    public int ReceiveBufferSize { get; set; } = 1024 * 4;

    /// <summary>
    ///     用于配置 <see cref="ConfigureClientWebSocketOptions" /> 的操作
    /// </summary>
    public Action<ClientWebSocketOptions>? ConfigureClientWebSocketOptions { get; set; }
}