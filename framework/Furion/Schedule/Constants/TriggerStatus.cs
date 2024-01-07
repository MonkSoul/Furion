// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业触发器状态
/// </summary>
[SuppressSniffer]
public enum TriggerStatus : uint
{
    /// <summary>
    /// 积压
    /// </summary>
    /// <remarks>起始时间大于当前时间</remarks>
    Backlog = 0,

    /// <summary>
    /// 就绪
    /// </summary>
    Ready = 1,

    /// <summary>
    /// 正在运行
    /// </summary>
    Running = 2,

    /// <summary>
    /// 暂停
    /// </summary>
    Pause = 3,

    /// <summary>
    /// 阻塞
    /// </summary>
    /// <remarks>本该执行但是没有执行</remarks>
    Blocked = 4,

    /// <summary>
    /// 由失败进入就绪
    /// </summary>
    /// <remarks>运行错误当并未超出最大错误数，进入下一轮就绪</remarks>
    ErrorToReady = 5,

    /// <summary>
    /// 归档
    /// </summary>
    /// <remarks>结束时间小于当前时间</remarks>
    Archived = 6,

    /// <summary>
    /// 崩溃
    /// </summary>
    /// <remarks>错误次数超出了最大错误数</remarks>
    Panic = 7,

    /// <summary>
    /// 超限
    /// </summary>
    /// <remarks>运行次数超出了最大限制</remarks>
    Overrun = 8,

    /// <summary>
    /// 无触发时间
    /// </summary>
    /// <remarks>下一次执行时间为 null </remarks>
    Unoccupied = 9,

    /// <summary>
    /// 未启动
    /// </summary>
    NotStart = 10,

    /// <summary>
    /// 未知作业触发器
    /// </summary>
    /// <remarks>作业触发器运行时类型为 null</remarks>
    Unknown = 11,

    /// <summary>
    /// 未知作业处理程序
    /// </summary>
    /// <remarks>作业处理程序类型运行时类型为 null</remarks>
    Unhandled = 12
}