// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using System;

namespace Furion.EventBus
{
    /// <summary>
    /// 订阅消息特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SubscribeMessageAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageId"></param>
        public SubscribeMessageAttribute(string messageId)
        {
            MessageId = messageId;
        }

        /// <summary>
        /// 消息Id
        /// </summary>
        public string MessageId { get; set; }
    }
}