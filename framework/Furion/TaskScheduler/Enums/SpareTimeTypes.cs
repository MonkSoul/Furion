using Furion.DependencyInjection;
using System.ComponentModel;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 任务类型
    /// </summary>
    [SkipScan]
    public enum SpareTimeTypes
    {
        /// <summary>
        /// 间隔方式
        /// </summary>
        [Description("间隔方式")]
        Interval,

        /// <summary>
        /// Cron 表达式
        /// </summary>
        [Description("Cron 表达式")]
        Cron
    }
}