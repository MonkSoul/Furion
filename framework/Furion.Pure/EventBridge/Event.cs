// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.DistributedIDGenerator;
using Furion.Extensions;
using Furion.IPCChannel;
using Furion.JsonSerialization;
using Furion.Reflection;
using System;
using System.Reflection;
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
        /// 发射消息
        /// </summary>
        /// <param name="eventCombineId">分类名:事件Id</param>
        /// <param name="payload"></param>
        public static void Emit(string eventCombineId, object payload = default)
        {
            EmitAsync(eventCombineId, payload).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 发射消息
        /// </summary>
        /// <param name="eventCombineId">分类名:事件Id</param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static async Task EmitAsync(string eventCombineId, object payload = default)
        {
            var eventCombines = eventCombineId?.Split(':', System.StringSplitOptions.RemoveEmptyEntries);
            if (eventCombines == null || eventCombines.Length <= 1) throw new InvalidCastException("Is not a valid event combination id.");

            // 解析注册的事件存储提供器
            var eventStoreProvider = App.GetService<IEventStoreProvider>();

            // 获取事件处理程序元数据
            var eventHandlerMetadata = await eventStoreProvider?.GetEventHandlerAsync(eventCombines[0]);
            if (eventHandlerMetadata == null) return;

            var nonPayload = payload == null;
            var payloadType = payload?.GetType();
            // 追加一条事件消息
            await eventStoreProvider.AppendEventMessageAsync(new EventMessageMetadata
            {
                AssemblyName = eventHandlerMetadata.AssemblyName,
                Category = eventHandlerMetadata.Category,
                CreatedTime = DateTimeOffset.UtcNow,
                TypeFullName = eventHandlerMetadata.TypeFullName,
                EventId = eventCombines[1],
                MessageId = IDGen.NextID().ToString(),
                Payload = nonPayload ? default : (payloadType.IsValueType ? payload.ToString() : JSON.Serialize(payload)),
                PayloadAssemblyName = nonPayload ? default : Reflect.GetAssemblyName(payloadType),
                PayloadTypeFullName = nonPayload ? default : payloadType.FullName,
            });
        }

        /// <summary>
        /// 发射消息
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <param name="eventId"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static Task EmitAsync<TEventHandler>(string eventId, object payload = default)
            where TEventHandler : class, IEventHandler
        {
            return EmitAsync($"{GetEventHandlerCategory(typeof(TEventHandler))}:{eventId}", payload);
        }

        /// <summary>
        /// 发射消息
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <param name="eventId"></param>
        /// <param name="payload"></param>
        public static void Emit<TEventHandler>(string eventId, object payload = default)
            where TEventHandler : class, IEventHandler
        {
            EmitAsync<TEventHandler>(eventId, payload).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 发射消息
        /// </summary>
        /// <param name="eventMessageMetadata"></param>
        /// <returns></returns>
        public static async Task EmitAsync(EventMessageMetadata eventMessageMetadata)
        {
            // 反射创建承载数据
            var payload = DeserializePayload(eventMessageMetadata);
            await ChannelContext<EventMessage, EventDispatcher>.BoundedChannel.Writer.WriteAsync(new EventMessage(eventMessageMetadata.Category, eventMessageMetadata.EventId, eventMessageMetadata.MessageId, payload));
        }

        /// <summary>
        /// 反序列化承载数据
        /// </summary>
        /// <param name="eventMessageMetadata"></param>
        /// <returns></returns>
        public static object DeserializePayload(EventMessageMetadata eventMessageMetadata)
        {
            object payload = null;

            // 反序列化承载数据
            if (eventMessageMetadata.Payload != null)
            {
                // 获取承载数据运行时类型
                var payloadType = Reflect.GetType(eventMessageMetadata.PayloadAssemblyName, eventMessageMetadata.PayloadTypeFullName);

                // 转换承载数据为具体值
                if (payloadType.IsValueType) payload = eventMessageMetadata.Payload.ChangeType(payloadType);
                else payload = typeof(JSON).GetMethod("Deserialize").MakeGenericMethod(payloadType)
                                                                         .Invoke(null, new object[] { eventMessageMetadata.Payload, null, null });
            }

            return payload;
        }

        /// <summary>
        /// 获取事件处理程序分类名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static string GetEventHandlerCategory(Type type)
        {
            // 如果定义了 [EventHandler] 特性，使用 Category，否则使用类型名（默认去除 EventHandler）结尾
            var defaultCategory = type.Name.ClearStringAffixes(1, "EventHandler");
            var eventCategory = type.IsDefined(typeof(EventHandlerAttribute), false)
                                                 ? type.GetCustomAttribute<EventHandlerAttribute>(false).Category ?? defaultCategory
                                                 : defaultCategory;
            return eventCategory;
        }
    }
}