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
/// 作业调度计划接口
/// </summary>
public interface IScheduler
{
    /// <summary>
    /// 将作业调度计划转换为构建器
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    SchedulerBuilder ToBuilder();

    /// <summary>
    /// 获取作业信息构建器
    /// </summary>
    /// <returns><see cref="JobBuilder"/></returns>
    JobBuilder GetDetailBuilder();

    /// <summary>
    /// 获取作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    TriggerBuilder GetTriggerBuilder(string triggerId);

    /// <summary>
    /// 启动作业
    /// </summary>
    void Start();

    /// <summary>
    /// 暂停作业
    /// </summary>
    void Pause();

    /// <summary>
    /// 启动作业单个触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    void StartTrigger(string triggerId);

    /// <summary>
    /// 暂停作业单个触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    void PauseTrigger(string triggerId);

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryGetTrigger(string triggerId, out JobTrigger trigger);

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="JobTrigger"/></returns>
    JobTrigger GetTrigger(string triggerId);

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddTrigger(TriggerBuilder triggerBuilder, out JobTrigger trigger);

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    void AddTrigger(TriggerBuilder triggerBuilder);

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryUpdateTrigger(TriggerBuilder triggerBuilder, out JobTrigger trigger);

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    void UpdateTrigger(TriggerBuilder triggerBuilder);

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryRemoveTrigger(string triggerId, out JobTrigger trigger);

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    void RemoveTrigger(string triggerId);

    /// <summary>
    /// 强制触发持久化记录
    /// </summary>
    void Persist();

    /// <summary>
    /// 检查作业触发器是否存在
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="bool"/></returns>
    bool ContainsTrigger(string triggerId);

    /// <summary>
    /// 将当前作业调度计划从调度器中删除
    /// </summary>
    /// <param name="scheduler">作业调度计划</param>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    ScheduleResult TryRemove(out IScheduler scheduler);

    /// <summary>
    /// 将当前作业调度计划从调度器中删除
    /// </summary>
    void Remove();
}