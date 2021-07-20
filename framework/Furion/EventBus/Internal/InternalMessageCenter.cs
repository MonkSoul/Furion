// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.TaskScheduler;
using Furion.Templates.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Furion.EventBus
{
    /// <summary>
    /// 轻量级消息中心（进程内）
    /// </summary>
    internal sealed class InternalMessageCenter
    {
        /// <summary>
        /// 消息注册队列
        /// </summary>
        private readonly ConcurrentDictionary<string, Func<string, object, Task>[]> MessageHandlerQueues = new();

        /// <summary>
        /// 类型消息 Id 注册表
        /// </summary>
        private readonly ConcurrentBag<string> MessageIdRegisterTable = new();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private InternalMessageCenter()
        {
        }

        /// <summary>
        /// 采用延迟加载设计模式处理单例
        /// </summary>
        private static readonly Lazy<InternalMessageCenter> InstanceLock = new(() => new InternalMessageCenter());

        /// <summary>
        /// 获取消息中心对象
        /// </summary>
        internal static InternalMessageCenter Instance => InstanceLock.Value;

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageHandlers"></param>
        internal void Subscribe<T>(string messageId, params Action<string, object>[] messageHandlers)
        {
            Subscribe(typeof(T), messageId, messageHandlers);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageHandlers"></param>
        internal void Subscribe<T>(string messageId, params Func<string, object, Task>[] messageHandlers)
        {
            Subscribe(typeof(T), messageId, messageHandlers);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="payload"></param>
        /// <param name="isSync">是否同步执行</param>
        internal void Send(string messageId, object payload = null, bool isSync = false)
        {
            // 支持读取配置渲染
            var realMessageId = messageId.Render();

            if (MessageHandlerQueues.TryGetValue(realMessageId, out var messageHandlers))
            {
                foreach (var eventHandler in messageHandlers)
                {
                    if (isSync)
                    {
                        try
                        {
                            eventHandler(realMessageId, payload).GetAwaiter().GetResult();
                        }
                        finally
                        {
                            // 释放未托管对象
                            App.DisposeUnmanagedObjects();
                        }
                    }
                    else
                    {
                        // 采用后台线程执行
                        SpareTime.DoIt(async () =>
                        {
                            await eventHandler(realMessageId, payload);
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="messageId"></param>
        internal void Unsubscribe(string messageId)
        {
            _ = MessageHandlerQueues.TryRemove(messageId, out _);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="messageId"></param>
        /// <param name="messageHandlers"></param>
        internal void Subscribe(Type t, string messageId, params Action<string, object>[] messageHandlers)
        {
            if (messageHandlers == null || messageHandlers.Length == 0) return;

            var handlers = messageHandlers.Select(u =>
             {
                 Func<string, object, Task> handler = async (m, o) =>
                 {
                     u(m, o);
                     await Task.CompletedTask;
                 };
                 return handler;
             });

            Subscribe(t, messageId, handlers.ToArray());
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="messageId"></param>
        /// <param name="messageHandlers"></param>
        internal void Subscribe(Type t, string messageId, params Func<string, object, Task>[] messageHandlers)
        {
            if (messageHandlers == null || messageHandlers.Length == 0) return;

            // 支持读取配置渲染
            var realMessageId = messageId.Render();

            // 判断当前类型是否已经注册过
            var uniqueMessageId = $"{t.FullName}_{realMessageId}";
            if (!MessageIdRegisterTable.Contains(uniqueMessageId))
            {
                MessageIdRegisterTable.Add(uniqueMessageId);
            }

            // 如果没有包含事件Id，则添加
            var isAdded = MessageHandlerQueues.TryAdd(realMessageId, messageHandlers);
            if (!isAdded)
            {
                MessageHandlerQueues[realMessageId] = MessageHandlerQueues[realMessageId].Concat(messageHandlers).ToArray();
            }
        }
    }
}