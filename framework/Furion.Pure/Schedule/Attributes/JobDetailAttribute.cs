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
/// 配置作业信息特性
/// </summary>
/// <remarks>仅限 <see cref="IJob"/> 实现类使用</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class JobDetailAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    public JobDetailAttribute(string jobId)
    {
        JobId = jobId;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="description">作业描述</param>
    public JobDetailAttribute(string jobId, string description)
        : this(jobId)
    {
        Description = description;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="concurrent">并行/串行</param>
    public JobDetailAttribute(string jobId, bool concurrent)
        : this(jobId)
    {
        Concurrent = concurrent;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    ///  <param name="concurrent">并行/串行</param>
    /// <param name="description">作业描述</param>
    public JobDetailAttribute(string jobId, bool concurrent, string description)
        : this(jobId, concurrent)
    {
        Description = description;
    }

    /// <summary>
    /// 作业 Id
    /// </summary>
    [JsonInclude]
    public string JobId { get; set; }

    /// <summary>
    /// 作业组名称
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 是否采用并行执行
    /// </summary>
    /// <remarks>如果设置为 false，那么使用串行执行</remarks>
    public bool Concurrent { get; set; } = true;
}