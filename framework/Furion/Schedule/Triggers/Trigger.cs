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

using System.Text.Json.Serialization;

namespace Furion.Schedule;

/// <summary>
/// 作业触发器基类
/// </summary>
[SuppressSniffer]
public partial class Trigger
{
    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    [JsonInclude]
    public string TriggerId { get; internal set; }

    /// <summary>
    /// 作业 Id
    /// </summary>
    [JsonInclude]
    public string JobId { get; internal set; }

    /// <summary>
    /// 作业触发器类型
    /// </summary>
    /// <remarks>存储的是类型的 FullName</remarks>
    [JsonInclude]
    public string TriggerType { get; internal set; }

    /// <summary>
    /// 作业触发器类型所在程序集
    /// </summary>
    /// <remarks>存储的是程序集 Name</remarks>
    [JsonInclude]
    public string AssemblyName { get; internal set; }

    /// <summary>
    /// 作业触发器参数
    /// </summary>
    /// <remarks>运行时将反序列化为 object[] 类型并作为构造函数参数</remarks>
    [JsonInclude]
    public string Args { get; internal set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    [JsonInclude]
    public string Description { get; internal set; }

    /// <summary>
    /// 作业触发器状态
    /// </summary>
    [JsonInclude]
    public TriggerStatus Status { get; internal set; } = TriggerStatus.Ready;

    /// <summary>
    /// 起始时间
    /// </summary>
    [JsonInclude]
    public DateTime? StartTime { get; internal set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [JsonInclude]
    public DateTime? EndTime { get; internal set; }

    /// <summary>
    /// 最近运行时间
    /// </summary>
    [JsonInclude]
    public DateTime? LastRunTime { get; internal set; }

    /// <summary>
    /// 下一次运行时间
    /// </summary>
    [JsonInclude]
    public DateTime? NextRunTime { get; internal set; }

    /// <summary>
    /// 触发次数
    /// </summary>
    [JsonInclude]
    public long NumberOfRuns { get; internal set; }

    /// <summary>
    /// 最大触发次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    [JsonInclude]
    public long MaxNumberOfRuns { get; internal set; }

    /// <summary>
    /// 出错次数
    /// </summary>
    [JsonInclude]
    public long NumberOfErrors { get; internal set; }

    /// <summary>
    /// 最大出错次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    [JsonInclude]
    public long MaxNumberOfErrors { get; internal set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    [JsonInclude]
    public int NumRetries { get; internal set; } = 0;

    /// <summary>
    /// 重试间隔时间
    /// </summary>
    /// <remarks>默认1000毫秒</remarks>
    [JsonInclude]
    public int RetryTimeout { get; internal set; } = 1000;

    /// <summary>
    /// 是否立即启动
    /// </summary>
    [JsonInclude]
    public bool StartNow { get; internal set; } = true;

    /// <summary>
    /// 是否启动时执行一次
    /// </summary>
    [JsonInclude]
    public bool RunOnStart { get; internal set; } = false;

    /// <summary>
    /// 是否在启动时重置最大触发次数等于一次的作业
    /// </summary>
    /// <remarks>解决因持久化数据已完成一次触发但启动时不再执行的问题</remarks>
    [JsonInclude]
    public bool ResetOnlyOnce { get; internal set; } = true;

    /// <summary>
    /// 本次执行结果
    /// </summary>
    [JsonInclude]
    public string Result { get; internal set; }

    /// <summary>
    /// 本次执行耗时
    /// </summary>
    [JsonInclude]
    public long ElapsedTime { get; internal set; }

    /// <summary>
    /// 作业触发器更新时间
    /// </summary>
    [JsonInclude]
    public DateTime? UpdatedTime { get; internal set; }

    /// <summary>
    /// 标记作业触发器持久化行为
    /// </summary>
    internal PersistenceBehavior Behavior { get; set; } = PersistenceBehavior.Appended;

    /// <summary>
    /// 作业触发器运行时类型
    /// </summary>
    internal Type RuntimeTriggerType { get; set; }

    /// <summary>
    /// 作业触发器运行时参数
    /// </summary>
    internal object[] RuntimeTriggerArgs { get; set; }

    /// <summary>
    /// 作业触发器最近运行记录
    /// </summary>
    /// <remarks>默认只保存 10 条</remarks>
    internal Queue<TriggerTimeline> Timelines { get; set; } = new();
}