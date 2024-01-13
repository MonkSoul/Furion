// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.TaskQueue;

/// <summary>
/// 任务队列配置选项构建器
/// </summary>
[SuppressSniffer]
public sealed class TaskQueueOptionsBuilder
{
    /// <summary>
    /// 默认内置任务队列内存通道容量
    /// </summary>
    /// <remarks>超过 n 条待处理消息，第 n+1 条将进入等待，默认为 3000</remarks>
    public int ChannelCapacity { get; set; } = 3000;

    /// <summary>
    /// 未察觉任务异常事件处理程序
    /// </summary>
    public EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskExceptionHandler { get; set; }

    /// <summary>
    /// 是否采用并行执行
    /// </summary>
    public bool Concurrent { get; set; } = true;

    /// <summary>
    /// 构建任务配置选项
    /// </summary>
    internal void Build()
    {
    }
}