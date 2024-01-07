// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业处理程序
/// </summary>
public interface IJob
{
    /// <summary>
    /// 具体处理逻辑
    /// </summary>
    /// <param name="context">作业执行前上下文</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken);

    /// <summary>
    /// 作业处理程序执行异常回退逻辑
    /// </summary>
    /// <param name="context">作业执行前上下文</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    Task FallbackAsync(JobExecutedContext context, CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}