// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;

namespace Furion.EventBridge
{
    /// <summary>
    /// 事件元数据
    /// </summary>
    [SuppressSniffer]
    public class EventIdMetadata : EventMetadata
    {
        /// <summary>
        /// 事件 Id
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 负载数据（进行序列化存储）
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string PayloadAssemblyName { get; set; }

        /// <summary>
        /// 处理程序名称
        /// </summary>
        public string PayloadTypeFullName { get; set; }
    }
}
