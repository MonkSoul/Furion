// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业集群状态
/// </summary>
[SuppressSniffer]
public enum ClusterStatus : uint
{
    /// <summary>
    /// 宕机
    /// </summary>
    Crashed = 0,

    /// <summary>
    /// 工作中
    /// </summary>
    Working = 1,

    /// <summary>
    /// 等待被唤醒
    /// </summary>
    Waiting = 2
}