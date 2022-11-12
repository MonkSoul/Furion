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

namespace Furion.Scheduler;

/// <summary>
/// 作业调度计划
/// </summary>
public sealed partial class JobScheduler
{
    /// <summary>
    /// 获取作业调度计划构建器
    /// </summary>
    /// <returns><see cref="JobScheduler"/></returns>
    public JobSchedulerBuilder GetBuilder()
    {
        return JobSchedulerBuilder.From(this);
    }

    /// <summary>
    /// 启动作业
    /// </summary>
    public void Start()
    {
        var changeCount = 0;
        foreach (var (_, jobTrigger) in JobTriggers)
        {
            if (jobTrigger.Status != JobTriggerStatus.Pause) continue;

            jobTrigger.SetStatus(JobTriggerStatus.Ready);
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
            jobTrigger.SetStatus(JobTriggerStatus.Pause);
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
        if (jobTrigger == default || jobTrigger.Status != JobTriggerStatus.Pause) return;

        jobTrigger.SetStatus(JobTriggerStatus.Ready);
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

        jobTrigger.SetStatus(JobTriggerStatus.Pause);

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
}