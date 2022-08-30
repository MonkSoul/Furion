// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
        return App.GetService<IEventPublisher>(App.RootServices);
    }

    /// <summary>
    /// 获取事件工厂
    /// </summary>
    /// <returns></returns>
    private static IEventBusFactory GetEventFactory()
    {
        return App.GetService<IEventBusFactory>(App.RootServices);
    }
}