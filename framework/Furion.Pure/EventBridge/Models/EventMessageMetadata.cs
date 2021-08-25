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
    /// 承载事件传输元数据
    /// </summary>
    [SuppressSniffer]
    public class EventMessageMetadata : EventHandlerMetadata
    {
        /// <summary>
        /// 只允许程序集内创建
        /// </summary>
        internal EventMessageMetadata()
        {
        }

        /// <summary>
        /// 事件 Id
        /// </summary>
        public string EventId { get; internal set; }

        /// <summary>
        /// 承载数据值（进行序列化存储）
        /// </summary>
        public string Payload { get; internal set; }

        /// <summary>
        /// 承载数据程序集名称
        /// </summary>
        public string PayloadAssemblyName { get; internal set; }

        /// <summary>
        /// 承载数据类型完整限定名
        /// </summary>
        public string PayloadTypeFullName { get; internal set; }
    }
}
