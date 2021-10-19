// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;

namespace Furion.EventBridge
{
    /// <summary>
    /// 事件消息传输对象
    /// </summary>
    [SuppressSniffer]
    public sealed class EventMessage : EventMessage<object>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="category"></param>
        /// <param name="eventId"></param>
        /// <param name="messageId"></param>
        /// <param name="payload"></param>
        public EventMessage(string category, string eventId, string messageId, object payload = default)
            : base(category, eventId, messageId, payload)
        {
        }
    }

    /// <summary>
    /// 事件消息传输对象
    /// </summary>
    [SuppressSniffer]
    public class EventMessage<TPayload>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="category"></param>
        /// <param name="eventId"></param>
        /// <param name="messageId"></param>
        /// <param name="payload"></param>
        public EventMessage(string category, string eventId, string messageId, TPayload payload = default)
        {
            Category = category;
            EventId = eventId;
            MessageId = messageId;
            Payload = payload;
        }

        /// <summary>
        /// 事件唯一 Id
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 消息唯一标识
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 事件类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public TPayload Payload { get; set; }
    }
}