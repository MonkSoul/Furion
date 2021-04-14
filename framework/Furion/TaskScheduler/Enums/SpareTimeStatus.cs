using System.ComponentModel;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum SpareTimeStatus
    {
        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Running,

        /// <summary>
        /// 已停止
        /// </summary>
        [Description("已停止")]
        Stopped,

        /// <summary>
        /// 任务已取消或没有该任务
        /// </summary>
        [Description("任务已取消或没有该任务")]
        CanceledOrNone
    }
}