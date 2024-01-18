// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.EventBus;

/// <summary>
/// 事件源（事件承载对象）依赖接口
/// </summary>
public interface IEventSource
{
    /// <summary>
    /// 事件 Id
    /// </summary>
    string EventId { get; }

    /// <summary>
    /// 事件承载（携带）数据
    /// </summary>
    object Payload { get; }

    /// <summary>
    /// 事件创建时间
    /// </summary>
    DateTime CreatedTime { get; }

    /// <summary>
    /// 取消任务 Token
    /// </summary>
    /// <remarks>用于取消本次消息处理</remarks>
    CancellationToken CancellationToken { get; }

    /// <summary>
    /// 消息是否只消费一次
    /// </summary>
    bool IsConsumOnce { get; }
}