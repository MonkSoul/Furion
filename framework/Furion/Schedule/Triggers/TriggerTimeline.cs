// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text.Json.Serialization;

namespace Furion.Schedule;

/// <summary>
/// 作业触发器运行记录
/// </summary>
[SuppressSniffer]
public sealed class TriggerTimeline
{
    /// <summary>
    /// 作业 Id
    /// </summary>
    [JsonInclude]
    public string JobId { get; internal set; }

    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    [JsonInclude]
    public string TriggerId { get; internal set; }

    /// <summary>
    /// 当前运行次数
    /// </summary>
    [JsonInclude]
    public long NumberOfRuns { get; internal set; }

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
    /// 作业触发器状态
    /// </summary>
    [JsonInclude]
    public TriggerStatus Status { get; internal set; }

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
    /// 新增时间
    /// </summary>
    [JsonInclude]
    public DateTime CreatedTime { get; internal set; }
}