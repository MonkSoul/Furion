using Furion.DependencyInjection;
using System;
using System.ComponentModel;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// Cron 表达式支持类型
    /// </summary>
    [SkipScan, Flags]
    public enum CronFormat
    {
        /// <summary>
        /// 只有 5 个字符：分钟，小时，月/天，天，周/天
        /// </summary>
        [Description("只有 5 个字符：分钟，小时，月/天，天，周/天")]
        Standard = 0,

        /// <summary>
        /// 支持秒解析，也就是 6 个字符
        /// </summary>
        [Description("支持秒解析，也就是 6 个字符")]
        IncludeSeconds = 1
    }
}