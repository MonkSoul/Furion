// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 动态作业处理程序
/// </summary>
internal sealed class DynamicJob : IJob
{
    /// <summary>
    /// 具体处理逻辑
    /// </summary>
    /// <param name="context">作业执行前上下文</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        var dynamicExecuteAsync = context.JobDetail.DynamicExecuteAsync;
        if (dynamicExecuteAsync == null) return;

        await dynamicExecuteAsync(context, stoppingToken);
    }
}