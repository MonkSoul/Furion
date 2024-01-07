// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业调度持久化器
/// </summary>
public interface IJobPersistence
{
    /// <summary>
    /// 作业调度器预加载服务
    /// </summary>
    /// <returns><see cref="IEnumerable{SchedulerBuilder}"/></returns>
    IEnumerable<SchedulerBuilder> Preload();

    /// <summary>
    /// 作业计划初始化通知
    /// </summary>
    /// <param name="builder">作业计划构建器</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    SchedulerBuilder OnLoading(SchedulerBuilder builder);

    /// <summary>
    /// 作业信息更改通知
    /// </summary>
    /// <param name="context">作业信息持久化上下文</param>
    void OnChanged(PersistenceContext context);

    /// <summary>
    /// 作业触发器更改通知
    /// </summary>
    /// <param name="context">作业触发器持久化上下文</param>
    void OnTriggerChanged(PersistenceTriggerContext context);

    /// <summary>
    /// 作业触发记录通知
    /// </summary>
    /// <param name="timeline">作业触发器运行记录</param>
    void OnExecutionRecord(TriggerTimeline timeline);
}