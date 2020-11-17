using Furion.DependencyInjection;
using System;

namespace Furion.MessageCenter
{
    /// <summary>
    /// 事件消息特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public sealed class EventMessageAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageId"></param>
        public EventMessageAttribute(string messageId)
        {
            MessageId = messageId;
        }

        /// <summary>
        /// 消息Id
        /// </summary>
        public string MessageId { get; set; }
    }
}