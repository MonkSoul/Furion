// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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