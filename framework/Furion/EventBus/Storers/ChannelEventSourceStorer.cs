// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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