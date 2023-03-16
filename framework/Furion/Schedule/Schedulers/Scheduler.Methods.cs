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
/// 作业计划
/// </summary>
internal sealed partial class Scheduler
{
    /// <summary>
    /// 返回可公开访问的作业计划模型
    /// </summary>
    /// <remarks>常用于接口返回或序列化操作</remarks>
    /// <returns><see cref="SchedulerModel"/></returns>
    public SchedulerModel GetModel()
    {
        return new SchedulerModel
        {
            JobDetail = JobDetail,
            Triggers = Triggers.Values.ToArray()
        };
    }

    /// <summary>
    /// 获取作业计划构建器
    /// </summary>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public SchedulerBuilder GetBuilder()
    {
        return SchedulerBuilder.From(this);
    }

    /// <summary>
    /// 获取作业信息构建器
    /// </summary>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder GetJobBuilder()
    {
        return GetBuilder().GetJobBuilder();
    }

    /// <summary>
    /// 获取作业触发器构建器集合
    /// </summary>
    /// <returns><see cref="List{TriggerBuilder}"/></returns>
    public IReadOnlyList<TriggerBuilder> GetTriggerBuilders()
    {
        return GetBuilder().GetTriggerBuilders();
    }

    /// <summary>
    /// 获取作业触发器构建器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder GetTriggerBuilder(string triggerId)
    {
        return GetBuilder().GetTriggerBuilder(triggerId);
    }

    /// <summary>
    /// 查找作业信息
    /// </summary>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail GetJobDetail()
    {
        return JobDetail;
    }

    /// <summary>
    /// 查找作业触发器集合
    /// </summary>
    /// <returns><see cref="IEnumerable{Trigger}"/></returns>
    public IEnumerable<Trigger> GetTriggers()
    {
        return Triggers.Values;
    }

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryGetTrigger(string triggerId, out Trigger trigger)
    {
        return InternalTryGetTrigger(triggerId, out trigger);
    }

    /// <summary>
    /// 查找作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="Trigger"/></returns>
    public Trigger GetTrigger(string triggerId)
    {
        _ = TryGetTrigger(triggerId, out var trigger);
        return trigger;
    }

    /// <summary>
    /// 保存作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TrySaveTrigger(TriggerBuilder triggerBuilder, out Trigger trigger, bool immediately = true)
    {
        // 空检查
        if (triggerBuilder == null) throw new ArgumentNullException(nameof(triggerBuilder));

        // 解析作业触发器构建器状态
        var isAppended = triggerBuilder.Behavior == PersistenceBehavior.Appended;
        var isUpdated = triggerBuilder.Behavior == PersistenceBehavior.Updated;
        var isRemoved = triggerBuilder.Behavior == PersistenceBehavior.Removed;

        // 原始作业触发器
        Trigger originTrigger = default;

        // 获取作业触发器 Id
        var triggerId = triggerBuilder.TriggerId;

        // 检查更新和删除作业触发器时是否正确配置作业触发器 Id 及检查作业触发器是否存在
        if (isUpdated || isRemoved)
        {
            // 查找作业触发器
            var scheduleResult = InternalTryGetTrigger(triggerId, out originTrigger, true);
            if (scheduleResult != ScheduleResult.Succeed)
            {
                trigger = default;
                return scheduleResult;
            }
        }

        // 构建新的作业触发器
        var newTrigger = triggerBuilder.Build(JobId);
        triggerId = newTrigger.TriggerId;

        // 获取作业触发器构建器
        var schedulerBuilder = GetBuilder();
        if (isAppended) schedulerBuilder.AddTriggerBuilder(triggerBuilder);
        else if (isUpdated) schedulerBuilder.UpdateTriggerBuilder(triggerBuilder);
        else if (isRemoved) schedulerBuilder.RemoveTriggerBuilder(triggerId);
        else { }

        // 更新作业
        if (Factory.TryUpdateJob(schedulerBuilder, out _, immediately) != ScheduleResult.Succeed)
        {
            trigger = null;
            return ScheduleResult.Failed;
        }

        if (isRemoved)
        {
            trigger = originTrigger;

            // 刷新作业
            Reload();
            return ScheduleResult.Succeed;
        }
        else
        {
            // 刷新作业
            Reload();
            return InternalTryGetTrigger(triggerId, out trigger, true);
        }
    }

    /// <summary>
    /// 保存作业触发器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    public void SaveTrigger(params TriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (triggerBuilders == null || triggerBuilders.Length == 0) throw new ArgumentNullException(nameof(triggerBuilders));

        // 逐条将作业触发器构建器保存到作业计划中
        foreach (var triggerBuilder in triggerBuilders)
        {
            _ = TrySaveTrigger(triggerBuilder, out var _);
        }
    }

    /// <summary>
    /// 更新作业计划信息
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="jobDetail">作业信息</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateDetail(JobBuilder jobBuilder, out JobDetail jobDetail)
    {
        // 空检查
        if (jobBuilder == null) throw new ArgumentNullException(nameof(jobBuilder));

        // 获取作业信息构建器
        var schedulerBuilder = GetBuilder();
        schedulerBuilder.UpdateJobBuilder(jobBuilder);

        // 更新作业
        var scheduleResult = Factory.TryUpdateJob(schedulerBuilder, out var scheduler);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            jobDetail = null;
            return scheduleResult;
        }

        jobDetail = (scheduler as Scheduler).JobDetail;

        // 刷新作业
        Reload();
        return scheduleResult;
    }

    /// <summary>
    /// 更新作业信息
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    public void UpdateDetail(JobBuilder jobBuilder)
    {
        _ = TryUpdateDetail(jobBuilder, out _);
    }

    /// <summary>
    /// 更新作业计划信息
    /// </summary>
    /// <param name="jobBuilderAction">作业信息构建器委托</param>
    /// <param name="jobDetail">作业信息</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateDetail(Action<JobBuilder> jobBuilderAction, out JobDetail jobDetail)
    {
        var jobBuilder = GetJobBuilder();
        jobBuilderAction?.Invoke(jobBuilder);
        return TryUpdateDetail(jobBuilder, out jobDetail);
    }

    /// <summary>
    /// 更新作业信息
    /// </summary>
    /// <param name="jobBuilderAction">作业信息构建器委托</param>
    public void UpdateDetail(Action<JobBuilder> jobBuilderAction)
    {
        var jobBuilder = GetJobBuilder();
        jobBuilderAction?.Invoke(jobBuilder);
        UpdateDetail(jobBuilder);
    }

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryAddTrigger(TriggerBuilder triggerBuilder, out Trigger trigger)
    {
        return TrySaveTrigger(triggerBuilder?.Appended(), out trigger);
    }

    /// <summary>
    /// 添加作业触发器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器</param>
    public void AddTrigger(params TriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (triggerBuilders == null) throw new ArgumentNullException(nameof(triggerBuilders));

        foreach (var triggerBuilder in triggerBuilders)
        {
            _ = TryAddTrigger(triggerBuilder, out _);
        }
    }

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilder">作业触发器构建器</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateTrigger(TriggerBuilder triggerBuilder, out Trigger trigger)
    {
        return TrySaveTrigger(triggerBuilder?.Updated(), out trigger);
    }

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerBuilders">作业触发器构建器</param>
    public void UpdateTrigger(params TriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (triggerBuilders == null) throw new ArgumentNullException(nameof(triggerBuilders));

        foreach (var triggerBuilder in triggerBuilders)
        {
            _ = TryUpdateTrigger(triggerBuilder, out _);
        }
    }

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="triggerBuilderAction">作业触发器构建器委托</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryUpdateTrigger(string triggerId, Action<TriggerBuilder> triggerBuilderAction, out Trigger trigger)
    {
        var triggerBuilder = GetTriggerBuilder(triggerId);
        triggerBuilderAction?.Invoke(triggerBuilder);
        return TryUpdateTrigger(triggerBuilder, out trigger);
    }

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="triggerBuilderAction">作业触发器构建器委托</param>
    public void UpdateTrigger(string triggerId, Action<TriggerBuilder> triggerBuilderAction)
    {
        var triggerBuilder = GetTriggerBuilder(triggerId);
        triggerBuilderAction?.Invoke(triggerBuilder);
        UpdateTrigger(triggerBuilder);
    }

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    public ScheduleResult TryRemoveTrigger(string triggerId, out Trigger trigger)
    {
        return TrySaveTrigger(TriggerBuilder.Create(triggerId).Removed(), out trigger);
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
    /// 将当前作业计划从调度器中删除
    /// </summary>
    /// <remarks><see cref="ScheduleResult"/></remarks>
    public ScheduleResult TryRemove()
    {
        return Factory.TryRemoveJob(this);
    }

    /// <summary>
    /// 将当前作业计划从调度器中删除
    /// </summary>
    public void Remove()
    {
        _ = TryRemove();
    }

    /// <summary>
    /// 检查作业触发器是否存在
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsTrigger(string triggerId)
    {
        return InternalTryGetTrigger(triggerId, out _) == ScheduleResult.Succeed;
    }

    /// <summary>
    /// 启动作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="bool"/></returns>
    public bool StartTrigger(string triggerId, bool immediately = true)
    {
        var triggerBuilder = GetTriggerBuilder(triggerId);
        if (triggerBuilder != null) triggerBuilder.StartNow = true;
        triggerBuilder?.SetStatus(TriggerStatus.Ready);

        var succeed = TrySaveTrigger(triggerBuilder?.Updated(), out _, immediately) == ScheduleResult.Succeed;
        if (succeed)
        {
            // 输出日志
            Logger.LogInformation("The <{triggerId}> trigger for scheduler of <{JobId}> successfully started to the schedule.", triggerId, JobId);
        }

        return succeed;
    }

    /// <summary>
    /// 暂停作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    /// <returns><see cref="bool"/></returns>
    public bool PauseTrigger(string triggerId, bool immediately = true)
    {
        var triggerBuilder = GetTriggerBuilder(triggerId);
        triggerBuilder?.SetStatus(TriggerStatus.Pause);

        var succeed = TrySaveTrigger(triggerBuilder?.Updated(), out var trigger, immediately) == ScheduleResult.Succeed;
        if (succeed)
        {
            // 记录作业触发器运行信息
            trigger.RecordTimeline();

            // 输出日志
            Logger.LogInformation("The <{triggerId}> trigger for scheduler of <{JobId}> successfully paused to the schedule.", triggerId, JobId);
        }

        return succeed;
    }

    /// <summary>
    /// 强制触发作业持久化记录
    /// </summary>
    public void Persist()
    {
        // 将作业信息运行数据写入持久化
        Factory.Shorthand(JobDetail);

        // 逐条将作业触发器运行数据写入持久化
        foreach (var trigger in Triggers.Values)
        {
            Factory.Shorthand(JobDetail, trigger);
        }

        // 输出日志
        Logger.LogInformation("The scheduler of <{JobId}> successfully persisted to the schedule.", JobId);
    }

    /// <summary>
    /// 启动作业
    /// </summary>
    public void Start()
    {
        // 获取当前时间用来计算触发器下一次触发时间
        var nowTime = Penetrates.GetNowTime(UseUtcTimestamp);

        // 逐条启用所有作业触发器
        foreach (var (_, trigger) in Triggers)
        {
            // 启动内存作业触发器后更新
            trigger.StartNow = true;
            trigger.SetStatus(TriggerStatus.Ready);
            trigger.NextRunTime = trigger.GetNextRunTime(nowTime);
        }

        // 更新作业
        if (Factory.TryUpdateJob(GetBuilder().Updated(), out _, false) != ScheduleResult.Succeed)
        {
            // 输出日志
            Logger.LogWarning("The scheduler of <{JobId}> started failed.", JobId);

            return;
        }

        // 刷新作业
        Reload();

        // 输出日志
        Logger.LogInformation("The scheduler of <{JobId}> successfully started to the schedule.", JobId);

        // 取消作业调度器休眠状态（强制唤醒）
        Factory.CancelSleep();
    }

    /// <summary>
    /// 暂停作业
    /// </summary>
    public void Pause()
    {
        // 逐条暂停所有作业触发器
        foreach (var (_, trigger) in Triggers)
        {
            // 暂停内存作业触发器后更新
            trigger.SetStatus(TriggerStatus.Pause);
        }

        // 更新作业
        if (Factory.TryUpdateJob(GetBuilder().Updated(), out _, false) != ScheduleResult.Succeed)
        {
            // 输出日志
            Logger.LogWarning("The scheduler of <{JobId}> paused failed.", JobId);

            return;
        }

        // 刷新作业
        Reload();

        // 输出日志
        Logger.LogInformation("The scheduler of <{JobId}> successfully paused to the schedule.", JobId);

        // 取消作业调度器休眠状态（强制唤醒）
        Factory.CancelSleep();
    }

    /// <summary>
    /// 校对作业
    /// </summary>
    /// <param name="immediately">是否立即通知作业调度器重新载入</param>
    public void Collate(bool immediately = true)
    {
        if (Factory.TryUpdateJob(GetBuilder().Updated(), out _, immediately) == ScheduleResult.Succeed)
        {
            // 刷新作业
            Reload();

            // 输出日志
            Logger.LogInformation("The scheduler of <{JobId}> successfully collated to the schedule.", JobId);
        }
    }

    /// <summary>
    /// 刷新作业计划
    /// </summary>
    public void Reload()
    {
        if (Factory.TryGetJob(JobId, out var scheduler) != ScheduleResult.Succeed)
        {
            // 输出日志
            Logger.LogWarning("The scheduler of <{JobId}> is not found.", JobId);

            return;
        }

        _ = scheduler.MapTo<Scheduler>(this);
    }

    /// <summary>
    /// 转换成 JSON 字符串
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToJSON(NamingConventions naming = NamingConventions.CamelCase)
    {
        return GetBuilder().ConvertToJSON(naming);
    }

    /// <summary>
    /// 将作业计划转换成可枚举集合
    /// </summary>
    /// <returns><see cref="Dictionary{JobDetail, Trigger}"/></returns>
    public Dictionary<JobDetail, Trigger> GetEnumerable()
    {
        var enumerable = new Dictionary<JobDetail, Trigger>(new RepeatKeyEqualityComparer());
        foreach (var (_, trigger) in Triggers)
        {
            enumerable.Add(JobDetail, trigger);
        }

        return enumerable;
    }

    /// <summary>
    /// 立即执行作业
    /// </summary>
    public void Run()
    {
        Factory?.RunJob(JobId);
    }

    /// <summary>
    /// 内部获取作业触发器
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="showLog">是否显示日志</param>
    /// <returns><see cref="ScheduleResult"/></returns>
    private ScheduleResult InternalTryGetTrigger(string triggerId, out Trigger trigger, bool showLog = false)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(triggerId))
        {
            // 输出日志
            if (showLog) Logger.LogWarning("Empty identity trigger.");

            trigger = default;
            return ScheduleResult.NotIdentify;
        }

        // 查找内存最新的作业信息
        var scheduleResult = Factory.TryGetJob(JobId, out var scheduler);
        if (scheduleResult != ScheduleResult.Succeed)
        {
            // 输出日志
            if (showLog) Logger.LogWarning("The scheduler of <{JobId}> is not found.", JobId);

            trigger = default;
            return ScheduleResult.NotFound;
        }

        // 查找作业
        var succeed = (scheduler as Scheduler).Triggers.TryGetValue(triggerId, out var originTrigger);

        if (!succeed)
        {
            // 输出日志
            if (showLog) Logger.LogWarning("The <{triggerId}> trigger for scheduler of <{JobId}> is not found.", triggerId, JobId);

            trigger = default;
            return ScheduleResult.NotFound;
        }

        trigger = originTrigger;
        return ScheduleResult.Succeed;
    }
}