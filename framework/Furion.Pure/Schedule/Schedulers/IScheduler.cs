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
/// 作业计划接口
/// </summary>
public interface IScheduler
{
    /// <summary>
    /// 返回可公开访问的作业计划模型
    /// </summary>
    /// <remarks>常用于接口返回或序列化操作</remarks>
    /// <returns><see cref="SchedulerModel"/></returns>
    SchedulerModel GetModel();

    /// <summary>
    /// 获取作业计划构建器
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    SchedulerBuilder GetBuilder();

    /// <summary>
    /// 获取作业信息构建器
    /// </summary>
    /// <returns><see cref="JobBuilder"/></returns>
    JobBuilder GetJobBuilder();

    /// <summary>
    /// 获取作业触发器构建器集合
    /// </summary>
    /// <returns><see cref="List{TriggerBuilder}"/></returns>
    IReadOnlyList<TriggerBuilder> GetTriggerBuilders();

    /// <summary>
    /// 获取作业触发器构建器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    TriggerBuilder GetTriggerBuilder(string triggerId);

    /// <summary>
    /// 查找作业信息
    /// </summary>
    /// <returns><see cref="JobDetail"/></returns>
    JobDetail GetJobDetail();

    /// <summary>
    /// 查找作业触发器集合
    /// </summary>
    /// <returns><see cref="IEnumerable{Trigger}"/></returns>
    IEnumerable<Trigger> GetTriggers();

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryGetTrigger(string triggerId, out Trigger trigger);

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="Trigger"/></returns>
    Trigger GetTrigger(string triggerId);

    /// <summary>
    /// 保存作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TrySaveTrigger(TriggerBuilder triggerBuilder, out Trigger trigger, bool immediately = true);

    /// <summary>
    /// 保存作业触发器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    void SaveTrigger(params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 更新作业计划信息
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="jobDetail">作业信息</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryUpdateDetail(JobBuilder jobBuilder, out JobDetail jobDetail);

    /// <summary>
    /// 更新作业信息
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    void UpdateDetail(JobBuilder jobBuilder);

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryAddTrigger(TriggerBuilder triggerBuilder, out Trigger trigger);

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器</param>
    void AddTrigger(params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryUpdateTrigger(TriggerBuilder triggerBuilder, out Trigger trigger);

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器</param>
    void UpdateTrigger(params TriggerBuilder[] triggerBuilders);

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryRemoveTrigger(string triggerId, out Trigger trigger);

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    void RemoveTrigger(string triggerId);

    /// <summary>
    /// 将当前作业计划从调度器中删除
    /// </summary>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    ScheduleResult TryRemove();

    /// <summary>
    /// 将当前作业计划从调度器中删除
    /// </summary>
    void Remove();

    /// <summary>
    /// 检查作业触发器是否存在
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="bool"/></returns>
    bool ContainsTrigger(string triggerId);

    /// <summary>
    /// 启动作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="bool"/></returns>
    bool StartTrigger(string triggerId, bool immediately = true);

    /// <summary>
    /// 暂停作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="bool"/></returns>
    bool PauseTrigger(string triggerId, bool immediately = true);

    /// <summary>
    /// 强制触发作业持久化记录
    /// </summary>
    void Persist();

    /// <summary>
    /// 启动作业
    /// </summary>
    void Start();

    /// <summary>
    /// 暂停作业
    /// </summary>
    void Pause();

    /// <summary>
    /// 校对作业
    /// </summary>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    void Collate(bool immediately = true);

    /// <summary>
    /// 刷新作业计划
    /// </summary>
    void Reload();

    /// <summary>
    /// 转换成 JSON 字符串
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    string ConvertToJSON(NamingConventions naming = NamingConventions.CamelCase);
}