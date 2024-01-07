// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业处理程序执行器
/// </summary>
public interface IJobExecutor
{
    /// <summary>
    /// 执行作业处理程序
    /// </summary>
    /// <remarks>在这里可以实现超时控制，失败重试控制等等</remarks>
    /// <param name="context">作业处理程序执行前上下文</param>
    /// <param name="jobHandler">作业处理程序</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task ExecuteAsync(JobExecutingContext context, IJob jobHandler, CancellationToken stoppingToken);
}