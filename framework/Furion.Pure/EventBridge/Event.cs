// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.JsonSerialization;
using System;
using System.Reflection;
using System.Runtime.Loader;
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

            // 调用事件存储提供器
            var eventStoreProvider = App.GetService<IEventStoreProvider>();
            var eventMetadata = await eventStoreProvider?.GetEventAsync(eventCombines[0]);
            if (eventMetadata == null) return;

            // 添加事件
            await eventStoreProvider.AppendEventIdAsync(new EventIdMetadata
            {
                AssemblyName = eventMetadata.AssemblyName,
                Category = eventMetadata.Category,
                CreatedTime = DateTimeOffset.UtcNow,
                TypeFullName = eventMetadata.TypeFullName,
                EventId = eventCombines[1],
                Payload = payload == null ? null : (payload.GetType().IsValueType ? payload.ToString() : JSON.Serialize(payload)),
                PayloadAssemblyName = payload == null ? typeof(object).Assembly.GetName().Name : payload.GetType().Assembly.GetName().Name,
                PayloadTypeFullName = payload == null ? typeof(object).FullName : payload.GetType().FullName,
            });
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

        /// <summary>
        /// 反序列化承载是数据
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <returns></returns>
        public static object DeserializePayload(EventIdMetadata eventIdMetadata)
        {
            object payload = null;

            // 反序列化承载数据
            if (eventIdMetadata.Payload != null)
            {
                // 加载程序集
                var payloadAssembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(eventIdMetadata.PayloadAssemblyName));
                var payloadType = payloadAssembly.GetType(eventIdMetadata.PayloadTypeFullName);

                // 转换承载数据为具体值
                if (payloadType.IsValueType) payload = eventIdMetadata.Payload.ChangeType(payloadType);
                else payload = typeof(JSON).GetMethod("Deserialize").MakeGenericMethod(payloadType).Invoke(null, new object[] { eventIdMetadata.Payload, null, null });
            }

            return payload;
        }
    }
}
