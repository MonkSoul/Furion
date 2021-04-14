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
        /// <param name="workName"></param>
        public SpareTimeAttribute(double interval, string workName = default)
        {
            Interval = interval;
            Type = SpareTimeTypes.Interval;
            WorkName = workName;
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
            WorkName = workName;
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
        public string WorkName { get; private set; }

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
    }
}