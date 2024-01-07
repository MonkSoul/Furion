// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业集群服务上下文
/// </summary>
[SuppressSniffer]
public sealed class JobClusterContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clusterId">作业集群 Id</param>
    internal JobClusterContext(string clusterId)
    {
        ClusterId = clusterId;
    }

    /// <summary>
    /// 作业集群 Id
    /// </summary>
    public string ClusterId { get; }
}