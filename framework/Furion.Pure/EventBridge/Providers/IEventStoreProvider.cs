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
        /// 注册事件
        /// </summary>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        Task RegisterEventAsync(EventMetadata eventMetadata);

        /// <summary>
        /// 获取事件信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<EventMetadata> GetEventAsync(string category);

        /// <summary>
        /// 添加事件（等待触发）
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <returns></returns>
        Task AppendEventIdAsync(EventIdMetadata eventIdMetadata);

        /// <summary>
        /// 获取事件 Id 信息
        /// </summary>
        /// <param name="category"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task<EventIdMetadata> GetEventIdAsync(string category, string eventId);

        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <returns></returns>
        Task ExecuteEventIdSuccessfully(EventIdMetadata eventIdMetadata);

        /// <summary>
        /// 执行失败
        /// </summary>
        /// <param name="eventIdMetadata"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        Task ExecuteEventIdFaildAsync(EventIdMetadata eventIdMetadata, Exception ex);

        /// <summary>
        /// 获取失败的事件
        /// </summary>
        /// <returns></returns>
        Task<EventIdMetadata[]> GetFailedEventIdsAsync();

        /// <summary>
        /// 重新发送失败事件
        /// </summary>
        /// <returns></returns>
        Task RetryEventIdsAsync();
    }
}