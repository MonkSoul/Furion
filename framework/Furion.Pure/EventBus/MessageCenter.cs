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

using System.Reflection;

namespace Furion.EventBus;

/// <summary>
/// 全局事件总线静态类
/// </summary>
[SuppressSniffer]
public static class MessageCenter
{
    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public static Task PublishAsync(IEventSource eventSource)
    {
        return GetEventPublisher().PublishAsync(eventSource);
    }

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public static Task PublishDelayAsync(IEventSource eventSource, long delay)
    {
        return GetEventPublisher().PublishDelayAsync(eventSource, delay);
    }

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns></returns>
    public static Task PublishAsync(string eventId, object payload = default, CancellationToken cancellationToken = default)
    {
        return GetEventPublisher().PublishAsync(eventId, payload, cancellationToken);
    }

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns></returns>
    public static Task PublishAsync(Enum eventId, object payload = default, CancellationToken cancellationToken = default)
    {
        return GetEventPublisher().PublishAsync(eventId, payload, cancellationToken);
    }

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public static Task PublishDelayAsync(string eventId, long delay, object payload = default, CancellationToken cancellationToken = default)
    {
        return GetEventPublisher().PublishDelayAsync(eventId, delay, payload, cancellationToken);
    }

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public static Task PublishDelayAsync(Enum eventId, long delay, object payload = default, CancellationToken cancellationToken = default)
    {
        return GetEventPublisher().PublishDelayAsync(eventId, delay, payload, cancellationToken);
    }

    /// <summary>
    /// 添加事件订阅者
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="handler">事件订阅委托</param>
    /// <param name="attribute"><see cref="EventSubscribeAttribute"/> 特性对象</param>
    /// <param name="handlerMethod"><see cref="MethodInfo"/> 对象</param>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns></returns>
    public static Task Subscribe(string eventId, Func<EventHandlerExecutingContext, Task> handler, EventSubscribeAttribute attribute = default, MethodInfo handlerMethod = default, CancellationToken cancellationToken = default)
    {
        return GetEventFactory().Subscribe(eventId
            , handler
            , attribute
            , handlerMethod
            , cancellationToken);
    }

    /// <summary>
    /// 删除事件订阅者
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns></returns>
    public static Task Unsubscribe(string eventId, CancellationToken cancellationToken = default)
    {
        return GetEventFactory().Unsubscribe(eventId, cancellationToken);
    }

    /// <summary>
    /// 获取事件发布者
    /// </summary>
    /// <returns></returns>
    private static IEventPublisher GetEventPublisher()
    {
        return App.GetRequiredService<IEventPublisher>(App.RootServices);
    }

    /// <summary>
    /// 获取事件工厂
    /// </summary>
    /// <returns></returns>
    private static IEventBusFactory GetEventFactory()
    {
        return App.GetRequiredService<IEventBusFactory>(App.RootServices);
    }
}