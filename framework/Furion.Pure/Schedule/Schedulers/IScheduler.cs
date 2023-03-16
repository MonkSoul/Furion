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
    /// 更新作业计划信息
    /// </summary>
    /// <param name="jobBuilderAction">作业信息构建器委托</param>
    /// <param name="jobDetail">作业信息</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryUpdateDetail(Action<JobBuilder> jobBuilderAction, out JobDetail jobDetail);

    /// <summary>
    /// 更新作业信息
    /// </summary>
    /// <param name="jobBuilderAction">作业信息构建器委托</param>
    void UpdateDetail(Action<JobBuilder> jobBuilderAction);

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
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="triggerBuilderAction">作业触发器构建器委托</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    ScheduleResult TryUpdateTrigger(string triggerId, Action<TriggerBuilder> triggerBuilderAction, out Trigger trigger);

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="triggerBuilderAction">作业触发器构建器委托</param>
    void UpdateTrigger(string triggerId, Action<TriggerBuilder> triggerBuilderAction);

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

    /// <summary>
    /// 将作业计划转换成可枚举集合
    /// </summary>
    /// <returns><see cref="Dictionary{JobDetail, Trigger}"/></returns>
    Dictionary<JobDetail, Trigger> GetEnumerable();

    /// <summary>
    /// 立即执行作业
    /// </summary>
    void Run();
}