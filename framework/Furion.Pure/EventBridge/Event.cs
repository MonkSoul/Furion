// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.IPCChannel;
using System;
using System.Threading.Tasks;

namespace Furion.EventBridge
{
    /// <summary>
    /// 事件总线静态类
    /// </summary>
    [SuppressSniffer]
    public static class Event
    {
        /// <summary>
        /// 发出消息
        /// </summary>
        /// <param name="eventCombineId">分类名:事件Id</param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static async Task EmitAsync(string eventCombineId, object payload = default)
        {
            var eventCombines = eventCombineId?.Split(':', System.StringSplitOptions.RemoveEmptyEntries);
            if (eventCombines == null || eventCombines.Length <= 1) throw new InvalidCastException("Is not a valid event combination id.");

            await ChannelContext<EventPayload, EventDispatcher>.BoundedChannel.Writer.WriteAsync(new EventPayload(eventCombines[0], eventCombines[1], payload));
        }

        /// <summary>
        /// 发出消息
        /// </summary>
        /// <param name="eventCombineId">分类名:事件Id</param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static void Emit(string eventCombineId, object payload = default)
        {
            EmitAsync(eventCombineId, payload).GetAwaiter().GetResult();
        }
    }
}
