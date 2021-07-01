// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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