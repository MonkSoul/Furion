// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业计划工厂服务（内部服务）
/// </summary>
public partial interface ISchedulerFactory
{
    /// <summary>
    /// 作业计划变更通知
    /// </summary>
    event EventHandler<SchedulerEventArgs> OnChanged;

    /// <summary>
    /// 作业调度器初始化
    /// </summary>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    Task PreloadAsync(CancellationToken stoppingToken);

    /// <summary>
    /// 查找即将触发的作业
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    IEnumerable<IScheduler> GetCurrentRunJobs(DateTime startAt, string group = default);

    /// <summary>
    /// 查找即将触发的作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    IEnumerable<SchedulerModel> GetCurrentRunJobsOfModels(DateTime startAt, string group = default);

    /// <summary>
    /// 使作业调度器进入休眠状态
    /// </summary>
    /// <param name="startAt">起始时间</param>
    Task SleepAsync(DateTime startAt);

    /// <summary>
    /// 取消作业调度器休眠状态（强制唤醒）
    /// </summary>
    void CancelSleep();

    /// <summary>
    /// 将作业信息运行数据写入持久化
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="behavior">作业持久化行为</param>
    void Shorthand(JobDetail jobDetail, PersistenceBehavior behavior = PersistenceBehavior.Updated);

    /// <summary>
    /// 将作业触发器运行数据写入持久化
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="behavior">作业持久化行为</param>
    void Shorthand(JobDetail jobDetail, Trigger trigger, PersistenceBehavior behavior = PersistenceBehavior.Updated);

    /// <summary>
    /// 创建作业处理程序实例
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="context"><see cref="JobFactoryContext"/> 上下文</param>
    /// <returns><see cref="IJob"/></returns>
    IJob CreateJob(IServiceProvider serviceProvider, JobFactoryContext context);

    /// <summary>
    /// GC 垃圾回收器回收处理
    /// </summary>
    /// <remarks>避免频繁 GC 回收</remarks>
    void GCCollect();
}