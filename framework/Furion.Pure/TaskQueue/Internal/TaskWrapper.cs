// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.TaskQueue;

/// <summary>
/// 任务包装器
/// </summary>
[SuppressSniffer]
public sealed class TaskWrapper
{
    /// <summary>
    /// 任务通道
    /// </summary>
    public string Channel { get; internal set; } = string.Empty;

    /// <summary>
    /// 任务 ID
    /// </summary>
    public object TaskId { get; internal set; }

    /// <summary>
    /// 任务处理委托
    /// </summary>
    public Func<IServiceProvider, CancellationToken, ValueTask> Handler { get; internal set; }

    /// <summary>
    /// 是否采用并行执行
    /// </summary>
    public object Concurrent { get; internal set; } = null;
}