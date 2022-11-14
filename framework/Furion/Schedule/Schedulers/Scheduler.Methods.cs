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
    /// 将作业调度计划转换为构建器
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder ToBuilder()
    {
        return SchedulerBuilder.From(this);
    }

    /// <summary>
    /// 获取作业信息构建器
    /// </summary>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder GetDetailBuilder()
    {
        return JobBuilder.From(JobDetail);
    }

    /// <summary>
    /// 获取作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder GetTriggerBuilder(string triggerId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(triggerId)) throw new ArgumentNullException(nameof(triggerId));

        // 检查作业触发器 Id 是否存在
        var scheduleResult = TryGetTrigger(triggerId, out var internalTrigger);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            return TriggerBuilder.From(internalTrigger);
        }

        return null;
    }

    /// <summary>
    /// 启动作业
    /// </summary>
    public void Start()
    {
        var changeCount = 0;

        // 逐条启用所有作业触发器
        foreach (var (_, trigger) in Triggers)
        {
            trigger.StartNow = true;

            // 如果不是暂停状态，则跳过
            if (trigger.Status != TriggerStatus.Pause) continue;

            trigger.SetStatus(TriggerStatus.Ready);
            trigger.GetNextRunTime();
            changeCount++;

            // 记录作业调度计划状态
            Factory?.Shorthand(JobDetail, trigger);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        if (changeCount > 0) Factory?.CancelSleep();
    }

    /// <summary>
    /// 暂停作业
    /// </summary>
    public void Pause()
    {
        var changeCount = 0;

        // 逐条暂停所有作业触发器
        foreach (var (_, trigger) in Triggers)
        {
            trigger.SetStatus(TriggerStatus.Pause);
            changeCount++;

            // 记录作业调度计划状态
            Factory?.Shorthand(JobDetail, trigger);
        }

        // 取消作业调度器休眠状态（强制唤醒）
        if (changeCount > 0) Factory?.CancelSleep();
    }

    /// <summary>
    /// 启动作业单个触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    public void StartTrigger(string triggerId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(triggerId)) throw new ArgumentNullException(nameof(triggerId));

        var trigger = Triggers.SingleOrDefault(u => u.Key == triggerId).Value;
        if (trigger == default) return;

        trigger.StartNow = true;

        // 如果不是暂停状态，则终止执行
        if (trigger.Status != TriggerStatus.Pause) return;

        trigger.SetStatus(TriggerStatus.Ready);
        trigger.GetNextRunTime();

        // 记录作业调度计划状态
        Factory?.Shorthand(JobDetail, trigger);

        // 取消作业调度器休眠状态（强制唤醒）
        Factory?.CancelSleep();
    }

    /// <summary>
    /// 暂停作业单个触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    public void PauseTrigger(string triggerId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(triggerId)) throw new ArgumentNullException(nameof(triggerId));

        var trigger = Triggers.SingleOrDefault(u => u.Key == triggerId).Value;
        if (trigger == default) return;

        trigger.SetStatus(TriggerStatus.Pause);

        // 记录作业调度计划状态
        Factory?.Shorthand(JobDetail, trigger);

        // 取消作业调度器休眠状态（强制唤醒）
        Factory?.CancelSleep();
    }

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryGetTrigger(string triggerId, out JobTrigger trigger)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(triggerId)) throw new ArgumentNullException(nameof(triggerId));

        var succeed = Triggers.TryGetValue(triggerId, out var internalTrigger);
        trigger = internalTrigger;

        return succeed
            ? ScheduleResult.Succeed
            : ScheduleResult.NotFound;
    }

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="JobTrigger"/></returns>
    public JobTrigger GetTrigger(string triggerId)
    {
        _ = TryGetTrigger(triggerId, out var trigger);
        return trigger;
    }

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddTrigger(TriggerBuilder triggerBuilder, out JobTrigger trigger)
    {
        // 空检查
        if (triggerBuilder == null) throw new ArgumentNullException(nameof(triggerBuilder));

        // 配置默认 TriggerId
        if (string.IsNullOrWhiteSpace(triggerBuilder.TriggerId))
        {
            triggerBuilder.SetTriggerId($"{JobDetail.JobId}_trigger{Triggers.Count + 1}");
        }

        // 构建作业触发器
        var internalTrigger = triggerBuilder.Build(JobDetail.JobId);
        var succeed = Triggers.TryAdd(internalTrigger.TriggerId, internalTrigger);

        if (!succeed)
        {
            trigger = default;
            return ScheduleResult.Fail;
        }

        // 记录作业调度计划状态
        Factory?.Shorthand(JobDetail, internalTrigger, PersistenceBehavior.AppendTrigger);

        // 取消作业调度器休眠状态（强制唤醒）
        Factory?.CancelSleep();

        trigger = internalTrigger;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    public void AddTrigger(TriggerBuilder triggerBuilder)
    {
        _ = TryAddTrigger(triggerBuilder, out _);
    }

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateTrigger(TriggerBuilder triggerBuilder, out JobTrigger trigger)
    {
        // 空检查
        if (triggerBuilder == null) throw new ArgumentNullException(nameof(triggerBuilder));

        var triggerId = triggerBuilder.TriggerId;
        if (string.IsNullOrWhiteSpace(triggerId)) throw new ArgumentException(nameof(triggerId));

        // 检查作业触发器 Id 是否存在
        var scheduleResult = TryGetTrigger(triggerId, out _);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            trigger = default;
            return scheduleResult;
        }

        // 从当前作业触发器中移除
        Triggers.Remove(triggerId);

        // 添加新的作业触发器
        var internalTrigger = triggerBuilder.Build(JobDetail.JobId);
        internalTrigger.NextRunTime = internalTrigger.GetNextRunTime();

        // 记录作业调度计划状态
        Factory?.Shorthand(JobDetail, internalTrigger);

        // 取消作业调度器休眠状态（强制唤醒）
        Factory?.CancelSleep();

        trigger = internalTrigger;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    public void UpdateTrigger(TriggerBuilder triggerBuilder)
    {
        _ = TryUpdateTrigger(triggerBuilder, out _);
    }

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveTrigger(string triggerId, out JobTrigger trigger)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(triggerId)) throw new ArgumentNullException(nameof(triggerId));

        // 检查作业触发器 Id 是否存在
        var scheduleResult = TryGetTrigger(triggerId, out var internalTrigger);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            trigger = default;
            return scheduleResult;
        }

        Triggers.Remove(triggerId);

        // 记录作业调度计划状态
        Factory?.Shorthand(JobDetail, internalTrigger, PersistenceBehavior.RemoveTrigger);

        // 取消作业调度器休眠状态（强制唤醒）
        Factory?.CancelSleep();

        trigger = internalTrigger;
        return ScheduleResult.Succeed;
    }

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    public void RemoveTrigger(string triggerId)
    {
        _ = TryRemoveTrigger(triggerId, out _);
    }

    /// <summary>
    /// 强制触发持久化记录
    /// </summary>
    public void Persist()
    {
        foreach (var (_, trigger) in Triggers)
        {
            // 记录作业调度计划状态
            Factory?.Shorthand(JobDetail, trigger);
        }
    }

    /// <summary>
    /// 检查作业触发器是否存在
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsTrigger(string triggerId)
    {
        return TryGetTrigger(triggerId, out _) == ScheduleResult.Succeed;
    }

    /// <summary>
    /// 将当前作业调度计划从调度器中删除
    /// </summary>
    /// <param name="scheduler">作业调度计划</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryRemove(out IScheduler scheduler)
    {
        scheduler = this;

        var scheduleResult = Factory?.TryRemoveJob(this);
        return scheduleResult == null ? ScheduleResult.Fail : scheduleResult.Value;
    }

    /// <summary>
    /// 将当前作业调度计划从调度器中删除
    /// </summary>
    public void Remove()
    {
        _ = TryRemove(out _);
    }
}