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
    /// 启动作业
    /// </summary>
    public void Start()
    {
        var changeCount = 0;
        foreach (var (_, trigger) in Triggers)
        {
            if (trigger.Status != TriggerStatus.Pause) continue;

            trigger.SetStatus(TriggerStatus.Ready);
            trigger.GetNextRunTime();
            changeCount++;

            // 记录执行信息并通知作业持久化器
            Factory?.Shorthand(JobDetail, trigger);
        }

        // 通知作业调度服务强制刷新
        if (changeCount > 0) Factory.CancelSleep();
    }

    /// <summary>
    /// 暂停作业
    /// </summary>
    public void Pause()
    {
        var changeCount = 0;
        foreach (var (_, trigger) in Triggers)
        {
            trigger.SetStatus(TriggerStatus.Pause);
            changeCount++;

            // 记录执行信息并通知作业持久化器
            Factory?.Shorthand(JobDetail, trigger);
        }

        // 通知作业调度服务强制刷新
        if (changeCount > 0) Factory.CancelSleep();
    }

    /// <summary>
    /// 启动作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    public void StartTrigger(string triggerId)
    {
        var trigger = Triggers.SingleOrDefault(u => u.Key == triggerId).Value;
        if (trigger == default || trigger.Status != TriggerStatus.Pause) return;

        trigger.SetStatus(TriggerStatus.Ready);
        trigger.GetNextRunTime();

        // 通知作业调度服务强制刷新
        Factory.CancelSleep();

        // 记录执行信息并通知作业持久化器
        Factory?.Shorthand(JobDetail, trigger);
    }

    /// <summary>
    /// 暂停作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    public void PauseTrigger(string triggerId)
    {
        var trigger = Triggers.SingleOrDefault(u => u.Key == triggerId).Value;
        if (trigger == default) return;

        trigger.SetStatus(TriggerStatus.Pause);

        // 通知作业调度服务强制刷新
        Factory.CancelSleep();

        // 记录执行信息并通知作业持久化器
        Factory?.Shorthand(JobDetail, trigger);
    }

    /// <summary>
    /// 强制触发持久化操作
    /// </summary>
    public void ForcePersist()
    {
        foreach (var (_, trigger) in Triggers)
        {
            // 记录执行信息并通知作业持久化器
            Factory?.Shorthand(JobDetail, trigger);
        }
    }

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    public void AddTrigger(TriggerBuilder triggerBuilder)
    {
        // 配置默认 TriggerId
        if (string.IsNullOrWhiteSpace(triggerBuilder.TriggerId))
        {
            triggerBuilder.SetTriggerId($"{JobDetail.JobId}_trigger{Triggers.Count + 1}");
        }

        var trigger = triggerBuilder.Build(JobDetail.JobId);
        var succeed = Triggers.TryAdd(trigger.TriggerId, trigger);

        // 作业触发器 Id 唯一检查
        if (!succeed) throw new InvalidOperationException($"The TriggerId of <{trigger.TriggerId}> already exists.");

        // 通知作业调度服务强制刷新
        Factory.CancelSleep();

        // 记录执行信息并通知作业持久化器
        Factory?.Shorthand(JobDetail, trigger, PersistenceBehavior.AppendTrigger);
    }

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    public void RemoveTrigger(string triggerId)
    {
        var succeed = Triggers.TryGetValue(triggerId, out var trigger);
        if (succeed)
        {
            Triggers.Remove(triggerId);

            // 记录执行信息并通知作业持久化器
            Factory?.Shorthand(JobDetail, trigger, PersistenceBehavior.RemoveTrigger);

            // 通知作业调度服务强制刷新
            Factory.CancelSleep();
        }
    }
}