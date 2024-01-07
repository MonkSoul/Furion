// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Threading.Channels;

namespace Furion.EventBus;

/// <summary>
/// 内存通道事件源存储器（默认实现）
/// </summary>
/// <remarks>
/// <para>顾名思义，这里指的是事件消息存储中心，提供读写能力</para>
/// <para>默认实现为内存中的 <see cref="System.Threading.Channels.Channel"/>，可自由更换存储介质，如 Kafka，SQL Server 等</para>
/// </remarks>
internal sealed partial class ChannelEventSourceStorer : IEventSourceStorer
{
    /// <summary>
    /// 内存通道事件源存储器
    /// </summary>
    private readonly Channel<IEventSource> _channel;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="capacity">管道最多能够处理多少消息，超过该容量进入等待写入</param>
    public ChannelEventSourceStorer(int capacity)
    {
        // 配置通道，设置超出默认容量后进入等待
        var boundedChannelOptions = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        // 创建有限容量通道
        _channel = Channel.CreateBounded<IEventSource>(boundedChannelOptions);
    }

    /// <summary>
    /// 将事件源写入存储器
    /// </summary>
    /// <param name="eventSource">事件源对象</param>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask WriteAsync(IEventSource eventSource, CancellationToken cancellationToken)
    {
        // 空检查
        if (eventSource == default)
        {
            throw new ArgumentNullException(nameof(eventSource));
        }

        // 写入存储器
        await _channel.Writer.WriteAsync(eventSource, cancellationToken);
    }

    /// <summary>
    /// 从存储器中读取一条事件源
    /// </summary>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns>事件源对象</returns>
    public async ValueTask<IEventSource> ReadAsync(CancellationToken cancellationToken)
    {
        // 读取一条事件源
        var eventSource = await _channel.Reader.ReadAsync(cancellationToken);
        return eventSource;
    }
}