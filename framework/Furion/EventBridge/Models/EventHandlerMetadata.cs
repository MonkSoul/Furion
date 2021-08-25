// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using System;

namespace Furion.EventBridge
{
    /// <summary>
    /// 事件处理程序元数据
    /// </summary>
    [SuppressSniffer]
    public class EventHandlerMetadata
    {
        /// <summary>
        /// 只允许程序集内创建
        /// </summary>
        internal EventHandlerMetadata()
        {
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; internal set; }

        /// <summary>
        /// 处理程序名称
        /// </summary>
        public string TypeFullName { get; internal set; }

        /// <summary>
        /// 分类名
        /// </summary>
        public string Category { get; internal set; }

        /// <summary>
        /// 创建事件
        /// </summary>
        public DateTimeOffset CreatedTime { get; internal set; } = DateTimeOffset.UtcNow;
    }
}
