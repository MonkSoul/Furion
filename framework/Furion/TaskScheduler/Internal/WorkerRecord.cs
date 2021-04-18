namespace Furion.TaskScheduler
{
    /// <summary>
    /// 记录任务数据
    /// </summary>
    internal sealed class WorkerRecord
    {
        /// <summary>
        /// 定时器
        /// </summary>
        internal SpareTimer Timer { get; set; }

        /// <summary>
        /// 任务执行计数
        /// </summary>
        internal long Tally { get; set; } = 0;

        /// <summary>
        /// 解决定时器重入问题
        /// </summary>
        internal long Interlocked { get; set; } = 0;

        /// <summary>
        /// Cron 表达式实际执行计数
        /// </summary>
        internal long CronActualTally { get; set; } = 0;

        /// <summary>
        /// 是否上一个已完成
        /// </summary>
        internal bool IsCompleteOfPrev { get; set; } = true;
    }
}