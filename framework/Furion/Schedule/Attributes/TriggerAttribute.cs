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
/// 作业触发器特性基类
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class TriggerAttribute : Attribute
{
    /// <summary>
    /// 私有开始时间
    /// </summary>
    private string _startTime;

    /// <summary>
    /// 私有结束时间
    /// </summary>
    private string _endTime;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="triggerType">作业触发器类型</param>
    /// <param name="args">作业触发器参数</param>
    public TriggerAttribute(Type triggerType, params object[] args)
    {
        RuntimeTriggerType = triggerType;
        RuntimeTriggerArgs = args;
    }

    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    public string TriggerId { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 起始时间
    /// </summary>
    public string StartTime
    {
        get { return _startTime; }
        set
        {
            _startTime = value;

            // 解析运行时开始时间
            if (string.IsNullOrWhiteSpace(value)) RuntimeStartTime = null;
            else RuntimeStartTime = Convert.ToDateTime(value);
        }
    }

    /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime
    {
        get { return _endTime; }
        set
        {
            _endTime = value;

            // 解析运行时结束时间
            if (string.IsNullOrWhiteSpace(value)) RuntimeEndTime = null;
            else RuntimeEndTime = Convert.ToDateTime(value);
        }
    }

    /// <summary>
    /// 最大触发次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    public long MaxNumberOfRuns { get; set; }

    /// <summary>
    /// 最大出错次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    public long MaxNumberOfErrors { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int NumRetries { get; set; } = 0;

    /// <summary>
    /// 重试间隔时间
    /// </summary>
    /// <remarks>默认1000毫秒</remarks>
    public int RetryTimeout { get; set; } = 1000;

    /// <summary>
    /// 是否立即启动
    /// </summary>
    public bool StartNow { get; set; } = true;

    /// <summary>
    /// 是否启动时执行一次
    /// </summary>
    public bool RunOnStart { get; set; } = false;

    /// <summary>
    /// 是否在启动时重置最大触发次数等于一次的作业
    /// </summary>
    /// <remarks>解决因持久化数据已完成一次触发但启动时不再执行的问题</remarks>
    public bool ResetOnlyOnce { get; set; } = true;

    /// <summary>
    /// 作业触发器运行时起始时间
    /// </summary>
    internal DateTime? RuntimeStartTime { get; set; }

    /// <summary>
    /// 作业触发器运行时结束时间
    /// </summary>
    internal DateTime? RuntimeEndTime { get; set; }

    /// <summary>
    /// 作业触发器运行时类型
    /// </summary>
    internal Type RuntimeTriggerType { get; set; }

    /// <summary>
    /// 作业触发器运行时参数
    /// </summary>
    internal object[] RuntimeTriggerArgs { get; set; }
}