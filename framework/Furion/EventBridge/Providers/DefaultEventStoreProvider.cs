// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.IPCChannel;
using Furion.JsonSerialization;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Furion.EventBridge
{
    /// <summary>
    /// 默认事件总线存储提供器
    /// </summary>
    [SuppressSniffer]
    public sealed class DefaultEventStoreProvider : IEventStoreProvider
    {
        /// <summary>
        /// 事件信息存储
        /// </summary>
        private static readonly ConcurrentDictionary<string, EventMetadata> EventStore;

        /// <summary>
        /// 事件 Id 信息存储
        /// </summary>
        private static readonly ConcurrentDictionary<string, EventIdMetadata> EventIdStore;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DefaultEventStoreProvider()
        {
            EventStore = new();
            EventIdStore = new();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public Task RegisterEventAsync(EventMetadata eventMetadata)
        {
            if (Debugger.IsAttached) Console.WriteLine(nameof(RegisterEventAsync));

            EventStore.TryAdd(eventMetadata.Category, eventMetadata);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取事件信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task<EventMetadata> GetEventAsync(string category)
        {
            if (Debugger.IsAttached) Console.WriteLine(nameof(GetEventAsync));

            var isSet = EventStore.TryGetValue($"{category}", out var value);
            var eventMetadata = isSet ? value : default;
            return Task.FromResult(eventMetadata);
        }

        /// <summary>
        /// 添加事件（等待触发）
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <returns></returns>
        public Task AppendEventIdAsync(EventIdMetadata eventIdMetadata)
        {
            if (Debugger.IsAttached) Console.WriteLine(nameof(AppendEventIdAsync));

            EventIdStore.TryAdd($"{eventIdMetadata.Category}:{eventIdMetadata.EventId}", eventIdMetadata);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取单个事件
        /// </summary>
        /// <param name="category"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Task<EventIdMetadata> GetEventIdAsync(string category, string eventId)
        {
            if (Debugger.IsAttached) Console.WriteLine(nameof(GetEventIdAsync));

            var isSet = EventIdStore.TryGetValue($"{category}:{eventId}", out var value);
            var eventIdMetadata = isSet ? value : default;
            return Task.FromResult(eventIdMetadata);
        }

        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <returns></returns>
        public Task ExecuteEventIdSuccessfully(EventIdMetadata eventIdMetadata)
        {
            if (Debugger.IsAttached) Console.WriteLine(nameof(ExecuteEventIdSuccessfully));

            return Task.CompletedTask;
        }

        /// <summary>
        /// 执行失败
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public Task ExecuteEventIdFaildAsync(EventIdMetadata eventIdMetadata, Exception ex)
        {
            if (Debugger.IsAttached) Console.WriteLine(nameof(ExecuteEventIdFaildAsync));

            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取失败的事件
        /// </summary>
        /// <returns></returns>
        public Task<EventIdMetadata[]> GetFailedEventIdsAsync()
        {
            if (Debugger.IsAttached) Console.WriteLine(nameof(GetFailedEventIdsAsync));

            return Task.FromResult(Array.Empty<EventIdMetadata>());
        }

        /// <summary>
        /// 重新发送失败事件
        /// </summary>
        /// <returns></returns>
        public async Task RetryEventIdsAsync()
        {
            var eventIdMetadatas = await GetFailedEventIdsAsync();
            if (eventIdMetadatas == null || eventIdMetadatas.Length == 0) return;

            foreach (var eventIdMetadata in eventIdMetadatas)
            {
                var payload = DeserializePayload(eventIdMetadata);
                await ChannelContext<EventPayload, EventDispatcher>.BoundedChannel.Writer.WriteAsync(new EventPayload(eventIdMetadata.Category, eventIdMetadata.EventId, payload));
            }
        }

        /// <summary>
        /// 反序列化承载是数据
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <returns></returns>
        private static object DeserializePayload(EventIdMetadata eventIdMetadata)
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
