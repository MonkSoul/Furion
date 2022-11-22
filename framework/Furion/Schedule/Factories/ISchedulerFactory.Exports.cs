// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace Furion.Schedule;

/// <summary>
/// 作业计划工厂服务
/// </summary>
public partial interface ISchedulerFactory
{
    /// <summary>
    /// 查找所有作业
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{IScheduler}"/></returns>
    IEnumerable<IScheduler> GetJobs(string group = default);

    /// <summary>
    /// 查找所有作业并转换成 <see cref="SchedulerModel"/>
    /// </summary>
    /// <param name="group">作业组名称</param>
    /// <returns><see cref="IEnumerable{SchedulerModel}"/></returns>
    IEnumerable<SchedulerModel> GetJobsOfModels(string group = default);

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
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    void AddJob(SchedulerBuilder schedulerBuilder);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob(JobBuilder jobBuilder, TriggerBuilder[] triggerBuilders, out IScheduler scheduler);

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
    /// <remarks><see cref="ScheduleResult"/></remarks>
    ScheduleResult TryAddJob<TJob>(TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
        where TJob : class, IJob;

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
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob<TJob>(string jobId, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
        where TJob : class, IJob;

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
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob<TJob>(string jobId, bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
        where TJob : class, IJob;

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
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddJob<TJob>(bool concurrent, TriggerBuilder[] triggerBuilders, out IScheduler scheduler)
        where TJob : class, IJob;

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <param name="concurrent">是否采用并发执行</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void AddJob<TJob>(bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob;

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    /// <param name="scheduler">新的作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryUpdateJob(SchedulerBuilder schedulerBuilder, out IScheduler scheduler);

    /// <summary>
    /// 更新作业
    /// </summary>
    /// <param name="schedulerBuilder">作业计划构建器</param>
    void UpdateJob(SchedulerBuilder schedulerBuilder);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryRemoveJob(string jobId, out IScheduler scheduler);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    void RemoveJob(string jobId);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="scheduler">作业计划</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryRemoveJob(IScheduler scheduler);

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="scheduler">作业计划</param>
    void RemoveJob(IScheduler scheduler);

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
}