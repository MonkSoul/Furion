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
/// 作业调度计划工厂服务
/// </summary>
public partial interface ISchedulerFactory
{
    /// <summary>
    /// 查找所有作业调度计划
    /// </summary>
    /// <returns></returns>
    IEnumerable<IScheduler> GetJobs();

    /// <summary>
    /// 获取作业调度计划
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    IScheduler GetJob(string jobId);

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="schedulerBuilder">作业调度程序构建器</param>
    void AddJob(SchedulerBuilder schedulerBuilder);

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
    void AddJob<TJob>(params TriggerBuilder[] triggerBuilders)
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
    void AddJob<TJob>(string jobId, bool concurrent, params TriggerBuilder[] triggerBuilders)
        where TJob : class, IJob;

    /// <summary>
    /// 删除作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    void RemoveJob(string jobId);

    /// <summary>
    /// 检查作业是否存在
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="scheduler">作业调度计划</param>
    bool ContainsJob(string jobId, out IScheduler scheduler);
}