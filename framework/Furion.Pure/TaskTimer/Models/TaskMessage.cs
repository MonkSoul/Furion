// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;

namespace Furion.TaskTimer
{
    /// <summary>
    /// 任务消息传输对象
    /// </summary>
    [SuppressSniffer]
    public sealed class TaskMessage : TaskMessage<object>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="category"></param>
        /// <param name="taskId"></param>
        /// <param name="payload"></param>
        public TaskMessage(string category, string taskId, object payload = default)
            : base(category, taskId, payload)
        {
        }
    }

    /// <summary>
    /// 任务消息传输对象
    /// </summary>
    [SuppressSniffer]
    public class TaskMessage<TPayload>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="category"></param>
        /// <param name="taskId"></param>
        /// <param name="payload"></param>
        public TaskMessage(string category, string taskId, TPayload payload = default)
        {
            Category = category;
            TaskId = taskId;
            Payload = payload;
        }

        /// <summary>
        /// 任务唯一 Id
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 任务类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 任务承载数据
        /// </summary>
        public TPayload Payload { get; set; }
    }
}
