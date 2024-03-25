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
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    Task<IEnumerable<SchedulerBuilder>> PreloadAsync(CancellationToken stoppingToken);

    /// <summary>
    /// 作业计划初始化通知
    /// </summary>
    /// <param name="builder">作业计划构建器</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    Task<SchedulerBuilder> OnLoadingAsync(SchedulerBuilder builder, CancellationToken stoppingToken);

    /// <summary>
    /// 作业信息更改通知
    /// </summary>
    /// <param name="context">作业信息持久化上下文</param>
    /// <returns><see cref="Task"/></returns>
    Task OnChangedAsync(PersistenceContext context);

    /// <summary>
    /// 作业触发器更改通知
    /// </summary>
    /// <param name="context">作业触发器持久化上下文</param>
    /// <returns><see cref="Task"/></returns>
    Task OnTriggerChangedAsync(PersistenceTriggerContext context);

    /// <summary>
    /// 作业触发记录通知
    /// </summary>
    /// <param name="timeline">作业触发器运行记录</param>
    /// <returns><see cref="Task"/></returns>
    Task OnExecutionRecordAsync(TriggerTimeline timeline);
}