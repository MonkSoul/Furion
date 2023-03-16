// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace Furion.Schedule;

/// <summary>
/// 作业计划工厂服务
/// </summary>
public partial interface ISchedulerFactory : IDisposable
{
    /// <summary>
    /// 查找所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <param name="active">是否是有效的作业</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    IEnumerable<IScheduler> GetJobs(string group = default, bool active = false);

    /// <summary>
    /// 查找所有作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <param name="active">是否是有效的作业</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    IEnumerable<SchedulerModel> GetJobsOfModels(string group = default, bool active = false);

    /// <summary>
    /// 查找下一批触发的作业
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    IEnumerable<IScheduler> GetNextRunJobs(DateTime startAt, string group = default);

    /// <summary>
    /// 查找下一批触发的作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    IEnumerable<SchedulerModel> GetNextRunJobsOfModels(DateTime startAt, string group = default);

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryGetJob(string jobId, out IScheduler scheduler);

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="IScheduler"/></returns>
    IScheduler GetJob(string jobId);

    /// <summary>
    /// 保存作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TrySaveJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 保存作业
    /// </summary>
    /// <param name="schedulerBuilders">作业计划构建器集合</param>
    void SaveJob(params SchedulerBuilder[] schedulerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilders">作业计划构建器集合</param>
    void AddJob(params SchedulerBuilder[] schedulerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(JobBuilder jobBuilder, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(JobBuilder jobBuilder, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    ScheduleResult TryAddJob<TJob>(TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    ScheduleResult TryAddJob(Type jobType, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob<TJob>(params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Type jobType, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob<TJob>(string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(Type jobType, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob<TJob>(string jobId, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Type jobType, string jobId, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob<TJob>(string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(Type jobType, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob<TJob>(string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Type jobType, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob<TJob>(bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(Type jobType, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob<TJob>(bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobType"><see cref="IJob"/> 实现类型</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Type jobType, bool concurrent, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="dynamicExecuteAsync">运行时动态作业执行逻辑</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob(Func<JobExecutingContext, CancellationToken, Task> dynamicExecuteAsync, bool concurrent, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob(Action<HttpJobMessage> buildMessage, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, params TriggerBuilder[] triggerBuilders)
       where TJob : class, IJob;

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
       where TJob : class, IJob;

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, params TriggerBuilder[] triggerBuilders)
       where TJob : class, IJob;

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
       where TJob : class, IJob;

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
       where TJob : class, IJob;

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="jobId">作业 ID</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
       where TJob : class, IJob;

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob(Action<HttpJobMessage> buildMessage, bool concurrent, params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob(Action<HttpJobMessage> buildMessage, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="ScheduleOptionsBuilder"/></returns>
    void AddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, bool concurrent, params TriggerBuilder[] triggerBuilders)
       where TJob : class, IJob;

    /// <summary>
    /// 添加 HTTP 作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="buildMessage">构建 HTTP 作业消息委托</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddHttpJob<TJob>(Action<HttpJobMessage> buildMessage, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler, bool immediately = true)
       where TJob : class, IJob;

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">新的作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryUpdateJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilders">作业计划构建器集合</param>
    void UpdateJob(params SchedulerBuilder[] schedulerBuilders);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryRemoveJob(string jobId, out IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobIds">作业 Id 集合</param>
    void RemoveJob(params string[] jobIds);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="scheduler">作业计划</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryRemoveJob(IScheduler scheduler, bool immediately = true);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="schedulers">作业计划集合</param>
    void RemoveJob(params IScheduler[] schedulers);

    /// <summary>
    /// 检查作业是否存在
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="bool"/></returns>
    bool ContainsJob(string jobId, string group = default);

    /// <summary>
    /// 启动所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    void StartAll(string group = default);

    /// <summary>
    /// 暂停所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    void PauseAll(string group = default);

    /// <summary>
    /// 删除所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    void RemoveAll(string group = default);

    /// <summary>
    /// 强制触发所有作业持久化记录
    /// </summary>
    /// <param name="group">作业组名称</param>
    void PersistAll(string group = default);

    /// <summary>
    /// 校对所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    void CollateAll(string group = default);

    /// <summary>
    /// 立即执行作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryRunJob(string jobId);

    /// <summary>
    /// 立即执行作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    void RunJob(string jobId);
}