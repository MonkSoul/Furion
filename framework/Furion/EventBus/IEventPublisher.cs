// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.EventBus;

/// <summary>
/// 事件发布服务依赖接口
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task PublishAsync(IEventSource eventSource);

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task PublishDelayAsync(IEventSource eventSource, long delay);

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns></returns>
    Task PublishAsync(string eventId, object payload = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns></returns>
    Task PublishAsync(Enum eventId, object payload = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task PublishDelayAsync(string eventId, long delay, object payload = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <param name="payload">事件承载（携带）数据</param>
    /// <param name="cancellationToken"> 取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task PublishDelayAsync(Enum eventId, long delay, object payload = default, CancellationToken cancellationToken = default);
}