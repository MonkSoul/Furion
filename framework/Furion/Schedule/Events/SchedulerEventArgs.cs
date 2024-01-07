// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业计划事件参数
/// </summary>
[SuppressSniffer]
public sealed class SchedulerEventArgs : EventArgs
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    public SchedulerEventArgs(JobDetail jobDetail)
    {
        JobDetail = jobDetail;
    }

    /// <summary>
    /// 作业信息
    /// </summary>
    public JobDetail JobDetail { get; }
}