// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 内置时间调度器
    /// </summary>
    [SuppressSniffer]
    public sealed class SpareTimer : Timer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workerName"></param>
        internal SpareTimer(string workerName = default) : base()
        {
            WorkerName = workerName ?? Guid.NewGuid().ToString("N");

            // 记录当前定时器
            if (!SpareTime.WorkerRecords.TryAdd(WorkerName, new WorkerRecord
            {
                Timer = this
            })) throw new InvalidOperationException($"The worker name `{WorkerName}` is exist.");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="workerName"></param>
        internal SpareTimer(double interval, string workerName = default) : base(interval)
        {
            WorkerName = workerName ?? Guid.NewGuid().ToString("N");

            // 记录当前定时器
            if (!SpareTime.WorkerRecords.TryAdd(WorkerName, new WorkerRecord
            {
                Interlocked = 0,
                Tally = 0,
                Timer = this
            })) throw new InvalidOperationException($"The worker name `{WorkerName}` is exist."); ;
        }

        /// <summary>
        /// 当前任务名
        /// </summary>
        public string WorkerName { get; private set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public SpareTimeTypes Type { get; internal set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public SpareTimeStatus Status { get; internal set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        public SpareTimeExecuteTypes ExecuteType { get; internal set; } = SpareTimeExecuteTypes.Parallel;

        /// <summary>
        /// 异常信息
        /// </summary>
        public Dictionary<long, Exception> Exception { get; internal set; } = new Dictionary<long, Exception>();

        /// <summary>
        /// 任务执行计数
        /// </summary>
        public long Tally { get; internal set; } = 0;
    }
}