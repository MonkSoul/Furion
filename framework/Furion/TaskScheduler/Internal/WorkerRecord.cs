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