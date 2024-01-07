// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业执行后上下文
/// </summary>
[SuppressSniffer]
public sealed class JobExecutedContext : JobExecutionContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="occurrenceTime">作业计划触发时间</param>
    /// <param name="runId">当前作业触发器触发的唯一标识</param>
    /// <param name="serviceProvider">服务提供器</param>
    internal JobExecutedContext(JobDetail jobDetail
        , Trigger trigger
        , DateTime occurrenceTime
        , Guid runId
        , IServiceProvider serviceProvider)
        : base(jobDetail, trigger, occurrenceTime, runId, serviceProvider)
    {
    }

    /// <summary>
    /// 执行后时间
    /// </summary>
    public DateTime ExecutedTime { get; internal set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public Exception Exception { get; internal set; }
}