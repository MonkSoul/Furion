using Fur.DependencyInjection;
using System;

namespace Fur.MessageCenter
{
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