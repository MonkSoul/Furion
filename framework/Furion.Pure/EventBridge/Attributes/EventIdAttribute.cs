// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using System;

namespace Furion.EventBridge
{
    /// <summary>
    /// 事件 Id 配置
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class EventIdAttribute : Attribute
    {
        /// <summary>
        /// 事件 Id
        /// </summary>
        /// <param name="id"></param>
        public EventIdAttribute(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 事件Id
        /// </summary>
        public string Id { get; set; }
    }
}
