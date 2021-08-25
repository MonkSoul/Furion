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
    /// 任务消息元数据
    /// </summary>
    [SuppressSniffer]
    public sealed class TaskMessageMetadata
    {
        /// <summary>
        /// 只允许程序集内创建
        /// </summary>
        internal TaskMessageMetadata()
        {
        }

        /// <summary>
        /// 任务 Id
        /// </summary>
        public string TaskId { get; internal set; }

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

        /// <summary>
        /// 分类名
        /// </summary>
        public string Category { get; internal set; }

        /// <summary>
        /// 创建事件
        /// </summary>
        public DateTimeOffset CreatedTime { get; internal set; } = DateTimeOffset.UtcNow;

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
        /// 总执行次数（含成功、失败）
        /// </summary>
        public long TotalTally { get; internal set; }
    }
}
