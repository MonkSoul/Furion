// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Furion.EventBus
{
    /// <summary>
    /// 轻量级消息中心（进程内）
    /// </summary>
    [SuppressSniffer]
    public static class MessageCenter
    {
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageHandlers"></param>
        public static void Subscribe<T>(string messageId, params Action<string, object>[] messageHandlers)
        {
            InternalMessageCenter.Instance.Subscribe<T>(messageId, messageHandlers);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageHandlers"></param>
        public static void Subscribe<T>(string messageId, params Func<string, object, Task>[] messageHandlers)
        {
            InternalMessageCenter.Instance.Subscribe<T>(messageId, messageHandlers);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="payload"></param>
        /// <param name="isSync">是否同步执行</param>
        public static void Send(string messageId, object payload = default, bool isSync = false)
        {
            InternalMessageCenter.Instance.Send(messageId, payload, isSync);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="messageId"></param>
        public static void Unsubscribe(string messageId)
        {
            InternalMessageCenter.Instance.Unsubscribe(messageId);
        }
    }
}