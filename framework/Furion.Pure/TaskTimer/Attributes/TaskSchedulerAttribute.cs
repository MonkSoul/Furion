// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using System;

namespace Furion.TaskTimer
{
    /// <summary>
    /// 任务调度配置特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class TaskSchedulerAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskSchedulerAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="taskId"></param>
        public TaskSchedulerAttribute(string taskId)
        {
            TaskId = taskId;
        }


        /// <summary>
        /// 任务 Id
        /// </summary>
        public string TaskId { get; internal set; }

        /// <summary>
        /// 调度类型
        /// </summary>
        public TaskSchedulerTypes SchedulerType { get; internal set; }

        /// <summary>
        /// 调度值
        /// </summary>
        /// <remarks>
        /// <see cref="TaskSchedulerTypes"/>
        ///     TaskSchedulerTypes.Interval：整数值
        ///     TaskSchedulerTypes.CronExpression：Cron 表达式
        /// </remarks>
        public string SchedulerValue { get; internal set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        public TaskInvokeTypes InvokeType { get; internal set; }

        /// <summary>
        /// 执行超时时间
        /// </summary>
        public int InvokeTimeout { get; internal set; }

        /// <summary>
        /// 任务绝对过期时间（毫秒）
        /// </summary>
        public int AbsoluteExpiration { get; internal set; }
    }
}
