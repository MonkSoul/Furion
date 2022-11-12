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
/// 作业调度计划
/// </summary>
internal sealed partial class Scheduler
{
    /// <summary>
    /// 获取作业调度计划构建器
    /// </summary>
    /// <returns><see cref="Scheduler"/></returns>
    public SchedulerBuilder GetBuilder()
    {
        return SchedulerBuilder.From(this);
    }

    /// <summary>
    /// 启动作业
    /// </summary>
    public void Start()
    {
        var changeCount = 0;
        foreach (var (_, jobTrigger) in JobTriggers)
        {
            if (jobTrigger.Status != TriggerStatus.Pause) continue;

            jobTrigger.SetStatus(TriggerStatus.Ready);
            jobTrigger.IncrementNextRunTime();
            changeCount++;

            // 记录执行信息并通知作业持久化器
            Factory?.Record(JobDetail, jobTrigger);
        }

        // 通知作业调度服务强制刷新
        if (changeCount > 0) Factory.ForceRefresh();
    }

    /// <summary>
    /// 暂停作业
    /// </summary>
    public void Pause()
    {
        var changeCount = 0;
        foreach (var (_, jobTrigger) in JobTriggers)
        {
            jobTrigger.SetStatus(TriggerStatus.Pause);
            changeCount++;

            // 记录执行信息并通知作业持久化器
            Factory?.Record(JobDetail, jobTrigger);
        }

        // 通知作业调度服务强制刷新
        if (changeCount > 0) Factory.ForceRefresh();
    }

    /// <summary>
    /// 启动作业触发器
    /// </summary>
    public void StartTrigger(string jobTriggerId)
    {
        var jobTrigger = JobTriggers.SingleOrDefault(u => u.Key == jobTriggerId).Value;
        if (jobTrigger == default || jobTrigger.Status != TriggerStatus.Pause) return;

        jobTrigger.SetStatus(TriggerStatus.Ready);
        jobTrigger.IncrementNextRunTime();

        // 通知作业调度服务强制刷新
        Factory.ForceRefresh();

        // 记录执行信息并通知作业持久化器
        Factory?.Record(JobDetail, jobTrigger);
    }

    /// <summary>
    /// 暂停作业触发器
    /// </summary>
    public void PauseTrigger(string jobTriggerId)
    {
        var jobTrigger = JobTriggers.SingleOrDefault(u => u.Key == jobTriggerId).Value;
        if (jobTrigger == default) return;

        jobTrigger.SetStatus(TriggerStatus.Pause);

        // 通知作业调度服务强制刷新
        Factory.ForceRefresh();

        // 记录执行信息并通知作业持久化器
        Factory?.Record(JobDetail, jobTrigger);
    }

    /// <summary>
    /// 强制触发持久化操作
    /// </summary>
    public void ForcePersist()
    {
        foreach (var (_, jobTrigger) in JobTriggers)
        {
            // 记录执行信息并通知作业持久化器
            Factory?.Record(JobDetail, jobTrigger);
        }
    }

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="jobTriggerBuilder">作业触发器构建器</param>
    public void AddTrigger(JobTriggerBuilder jobTriggerBuilder)
    {
        // 配置默认 TriggerId
        if (string.IsNullOrWhiteSpace(jobTriggerBuilder.TriggerId))
        {
            jobTriggerBuilder.SetTriggerId($"{JobDetail.JobId}_trigger{JobTriggers.Count + 1}");
        }

        var jobTrigger = jobTriggerBuilder.Build(JobDetail.JobId);
        var succeed = JobTriggers.TryAdd(jobTrigger.TriggerId, jobTrigger);

        // 作业触发器 Id 唯一检查
        if (!succeed) throw new InvalidOperationException($"The TriggerId of <{jobTrigger.TriggerId}> already exists.");

        // 通知作业调度服务强制刷新
        Factory.ForceRefresh();

        // 记录执行信息并通知作业持久化器
        Factory?.Record(JobDetail, jobTrigger, PersistenceBehavior.AppendTrigger);
    }

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="jobTriggerId"></param>
    public void RemoveTrigger(string jobTriggerId)
    {
        var succeed = JobTriggers.TryGetValue(jobTriggerId, out var jobTrigger);
        if (succeed)
        {
            JobTriggers.Remove(jobTriggerId);

            // 记录执行信息并通知作业持久化器
            Factory?.Record(JobDetail, jobTrigger, PersistenceBehavior.RemoveTrigger);

            // 通知作业调度服务强制刷新
            Factory.ForceRefresh();
        }
    }
}