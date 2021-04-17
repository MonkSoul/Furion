using Furion.DependencyInjection;
using System;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 配置定时任务特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SpareTimeAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="workerName"></param>
        public SpareTimeAttribute(double interval, string workerName = default)
        {
            Interval = interval;
            Type = SpareTimeTypes.Interval;
            WorkerName = workerName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cronExpression"></param>
        /// <param name="workName"></param>
        public SpareTimeAttribute(string cronExpression, string workName = default)
        {
            CronExpression = cronExpression;
            Type = SpareTimeTypes.Cron;
            WorkerName = workName;
        }

        /// <summary>
        /// 间隔
        /// </summary>
        public double Interval { get; private set; }

        /// <summary>
        /// Cron 表达式
        /// </summary>
        public string CronExpression { get; private set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string WorkerName { get; private set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        internal SpareTimeTypes Type { get; private set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 只执行一次
        /// </summary>
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        public bool StartNow { get; set; } = false;

        /// <summary>
        /// Cron 表达式格式化方式
        /// </summary>
        public CronFormat CronFormat { get; set; } = CronFormat.Standard;
    }
}