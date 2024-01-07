// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业执行记录事件参数
/// </summary>
[SuppressSniffer]
public sealed class JobExecutionRecordEventArgs : EventArgs
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="timeline">作业触发器运行记录</param>
    public JobExecutionRecordEventArgs(TriggerTimeline timeline)
    {
        Timeline = timeline;
    }

    /// <summary>
    /// 作业触发器运行记录
    /// </summary>
    public TriggerTimeline Timeline { get; }
}