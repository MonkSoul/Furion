// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensitions.EventBus;

namespace Furion.EventBus;

/// <summary>
/// 内存通道事件源（事件承载对象）
/// </summary>
[SuppressSniffer]
public class ChannelEventSource : IEventSource
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ChannelEventSource()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    public ChannelEventSource(string eventId)
    {
        EventId = eventId;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    public ChannelEventSource(string eventId, object payload)
        : this(eventId)
    {
        Payload = payload;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken">取消任务 Token</param>
    public ChannelEventSource(string eventId, object payload, CancellationToken cancellationToken)
        : this(eventId, payload)
    {
        CancellationToken = cancellationToken;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    public ChannelEventSource(Enum eventId)
        : this(eventId.ParseToString())
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    public ChannelEventSource(Enum eventId, object payload)
        : this(eventId.ParseToString(), payload)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken">取消任务 Token</param>
    public ChannelEventSource(Enum eventId, object payload, CancellationToken cancellationToken)
        : this(eventId.ParseToString(), payload, cancellationToken)
    {
    }

    /// <summary>
    /// 事件 Id
    /// </summary>
    public string EventId { get; set; }

    /// <summary>
    /// 事件承载（携带）数据
    /// </summary>
    public object Payload { get; set; }

    /// <summary>
    /// 事件创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// 消息是否只消费一次
    /// </summary>
    public bool IsConsumOnce { get; set; }

    /// <summary>
    /// 取消任务 Token
    /// </summary>
    /// <remarks>用于取消本次消息处理</remarks>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public CancellationToken CancellationToken { get; set; }
}