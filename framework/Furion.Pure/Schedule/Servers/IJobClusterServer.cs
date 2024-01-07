// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业集群服务
/// </summary>
/// <remarks>集群并不能达到负载均衡的效果，而仅仅提供了故障转移的功能，当一个任务服务器宕机时，另一个任务服务器会启动</remarks>
public interface IJobClusterServer
{
    /// <summary>
    /// 当前作业调度器启动通知
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    void Start(JobClusterContext context);

    /// <summary>
    /// 等待被唤醒
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    /// <returns><see cref="Task"/></returns>
    Task WaitingForAsync(JobClusterContext context);

    /// <summary>
    /// 当前作业调度器停止通知
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    void Stop(JobClusterContext context);

    /// <summary>
    /// 当前作业调度器宕机
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    void Crash(JobClusterContext context);
}