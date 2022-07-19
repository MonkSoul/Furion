// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.EventBus;

/// <summary>
/// 基于内存通道事件发布者（默认实现）
/// </summary>
internal sealed partial class ChannelEventPublisher : IEventPublisher
{
    /// <summary>
    /// 事件源存储器
    /// </summary>
    private readonly IEventSourceStorer _eventSourceStorer;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventSourceStorer">事件源存储器</param>
    public ChannelEventPublisher(IEventSourceStorer eventSourceStorer)
    {
        _eventSourceStorer = eventSourceStorer;
    }

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public async Task PublishAsync(IEventSource eventSource)
    {
        await _eventSourceStorer.WriteAsync(eventSource, eventSource.CancellationToken);
    }

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public Task PublishDelayAsync(IEventSource eventSource, long delay)
    {
        // 创建新线程
        Task.Factory.StartNew(async () =>
        {
            // 延迟 delay 毫秒
            await Task.Delay(TimeSpan.FromMilliseconds(delay), eventSource.CancellationToken);

            await _eventSourceStorer.WriteAsync(eventSource, eventSource.CancellationToken);
        }, eventSource.CancellationToken);

        return Task.CompletedTask;
    }

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns></returns>
    public async Task PublishAsync(string eventId, object payload = default, CancellationToken cancellationToken = default)
    {
        await PublishAsync(new ChannelEventSource(eventId, payload, cancellationToken));
    }

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns></returns>
    public async Task PublishAsync(Enum eventId, object payload = default, CancellationToken cancellationToken = default)
    {
        await PublishAsync(new ChannelEventSource(eventId, payload, cancellationToken));
    }

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public async Task PublishDelayAsync(string eventId, long delay, object payload = default, CancellationToken cancellationToken = default)
    {
        await PublishDelayAsync(new ChannelEventSource(eventId, payload, cancellationToken), delay);
    }

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public async Task PublishDelayAsync(Enum eventId, long delay, object payload = default, CancellationToken cancellationToken = default)
    {
        await PublishDelayAsync(new ChannelEventSource(eventId, payload, cancellationToken), delay);
    }
}