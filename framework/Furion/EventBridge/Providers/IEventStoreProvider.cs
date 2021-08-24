// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System;
using System.Threading.Tasks;

namespace Furion.EventBridge
{
    /// <summary>
    /// 事件存储信息提供器
    /// </summary>
    public interface IEventStoreProvider
    {
        /// <summary>
        /// 注册事件处理程序对象
        /// </summary>
        /// <param name="eventHandlerMetadata"></param>
        /// <returns></returns>
        Task RegisterEventHandlerAsync(EventHandlerMetadata eventHandlerMetadata);

        /// <summary>
        /// 根据分类获取事件处理程序对象
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<EventHandlerMetadata> GetEventHandlerAsync(string category);

        /// <summary>
        /// 追加一条事件消息
        /// </summary>
        /// <param name="eventMessageMetadata"></param>
        /// <returns></returns>
        Task AppendEventMessageAsync(EventMessageMetadata eventMessageMetadata);

        /// <summary>
        /// 根据分类及事件Id获取事件消息元数据对象
        /// </summary>
        /// <param name="category"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task<EventMessageMetadata> GetEventMessageAsync(string category, string eventId);

        /// <summary>
        /// 成功执行一条消息
        /// </summary>
        /// <param name="eventMessageMetadata"></param>
        /// <returns></returns>
        Task ExecuteSuccessfullyAsync(EventMessageMetadata eventMessageMetadata);

        /// <summary>
        /// 执行一条消息失败
        /// </summary>
        /// <param name="eventMessageMetadata"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Task ExecuteFaildedAsync(EventMessageMetadata eventMessageMetadata, Exception exception);
    }
}