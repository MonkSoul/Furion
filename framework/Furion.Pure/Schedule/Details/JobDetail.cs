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
/// 作业信息
/// </summary>
[SuppressSniffer]
public partial class JobDetail
{
    /// <summary>
    /// 作业 Id
    /// </summary>
    [JsonInclude]
    public string JobId { get; internal set; }

    /// <summary>
    /// 作业组名称
    /// </summary>
    [JsonInclude]
    public string GroupName { get; internal set; }

    /// <summary>
    /// 作业处理程序类型
    /// </summary>
    /// <remarks>存储的是类型的 FullName</remarks>
    [JsonInclude]
    public string JobType { get; internal set; }

    /// <summary>
    /// 作业处理程序类型所在程序集
    /// </summary>
    /// <remarks>存储的是程序集 Name</remarks>
    [JsonInclude]
    public string AssemblyName { get; internal set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    [JsonInclude]
    public string Description { get; internal set; }

    /// <summary>
    /// 是否采用并行执行
    /// </summary>
    /// <remarks>如果设置为 false，那么使用串行执行</remarks>
    [JsonInclude]
    public bool Concurrent { get; internal set; } = true;

    /// <summary>
    /// 是否扫描 IJob 实现类 [Trigger] 特性触发器
    /// </summary>
    [JsonInclude]
    public bool IncludeAnnotations { get; internal set; } = false;

    /// <summary>
    /// 作业信息额外数据
    /// </summary>
    [JsonInclude]
    public string Properties { get; internal set; } = "{}";

    /// <summary>
    /// 作业更新时间
    /// </summary>
    [JsonInclude]
    public DateTime? UpdatedTime { get; internal set; }

    /// <summary>
    /// 标记其他作业正在执行
    /// </summary>
    /// <remarks>当 <see cref="Concurrent"/> 为 false 时有效，也就是串行执行</remarks>
    internal bool Blocked { get; set; } = false;

    /// <summary>
    /// 作业处理程序运行时类型
    /// </summary>
    internal Type RuntimeJobType { get; set; }

    /// <summary>
    /// 作业信息额外数据运行时实例
    /// </summary>
    internal Dictionary<string, object> RuntimeProperties { get; set; } = new();

    /// <summary>
    /// 运行时动态作业执行逻辑
    /// </summary>
    internal Func<JobExecutingContext, CancellationToken, Task> DynamicExecuteAsync { get; set; }
}