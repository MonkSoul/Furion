using Furion.DependencyInjection;
using System;
using System.Timers;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 内置时间调度器
    /// </summary>
    [SkipScan]
    public sealed class SpareTimer : Timer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workerName"></param>
        internal SpareTimer(string workerName = default) : base()
        {
            WorkerName = workerName ?? Guid.NewGuid().ToString("N");

            // 存储当前 Worker
            _ = SpareTime.Workers.TryAdd(WorkerName, this);
            _ = SpareTime.WorkerTimes.TryAdd(workerName, 0);
            _ = SpareTime.WorkerLockTimer.TryAdd(workerName, 0);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="workerName"></param>
        internal SpareTimer(double interval, string workerName = default) : base(interval)
        {
            WorkerName = workerName ?? Guid.NewGuid().ToString("N");

            // 存储当前 Worker
            _ = SpareTime.Workers.TryAdd(WorkerName, this);
            _ = SpareTime.WorkerTimes.TryAdd(workerName, 0);
            _ = SpareTime.WorkerLockTimer.TryAdd(workerName, 0);
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
    }
}