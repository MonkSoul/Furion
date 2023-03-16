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
/// 作业触发器运行记录
/// </summary>
[SuppressSniffer]
public sealed class TriggerTimeline
{
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
    internal DateTime CreatedTime { get; set; }
}