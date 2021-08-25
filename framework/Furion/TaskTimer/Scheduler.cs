// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Threading.Tasks;

namespace Furion.TaskTimer
{
    /// <summary>
    /// 调度器静态类
    /// </summary>
    public static class Scheduler
    {
        /// <summary>
        /// 创建一个任务
        /// </summary>
        /// <param name="taskMessageMetadata"></param>
        /// <returns></returns>
        public static Task CreateTaskAsync(TaskMessageMetadata taskMessageMetadata)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 开始一个任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static Task StartTaskAsync(string taskId, object payload = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 停止一个任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static Task StopTaskAsync(string taskId)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 取消一个任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static Task CancelTaskAsync(string taskId)
        {
            return Task.CompletedTask;
        }
    }
}
