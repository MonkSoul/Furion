// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text.Json.Serialization;

namespace Furion.Schedule;

/// <summary>
/// 作业计划模型
/// </summary>
/// <remarks>常用于接口返回或序列化操作</remarks>
[SuppressSniffer]
public sealed class SchedulerModel
{
    /// <summary>
    /// 作业信息
    /// </summary>
    [JsonInclude]
    public JobDetail JobDetail { get; internal set; }

    /// <summary>
    /// 作业触发器
    /// </summary>
    [JsonInclude]
    public Trigger[] Triggers { get; internal set; }
}