// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Collections.Concurrent;

namespace Furion.Schedule;

/// <summary>
/// 取消作业执行 Token 器
/// </summary>
internal sealed class JobCancellationToken : IJobCancellationToken
{
    /// <summary>
    /// 取消作业执行 Token 集合
    /// </summary>
    private readonly ConcurrentDictionary<string, CancellationTokenSource> _cancellationTokenSources;

    /// <summary>
    /// 作业调度器日志服务
    /// </summary>
    private readonly IScheduleLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">作业调度器日志服务</param>
    public JobCancellationToken(IScheduleLogger logger)
    {
        _logger = logger;
        _cancellationTokenSources = new();
    }

    /// <summary>
    /// 获取或创建取消作业执行 Token
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="runId">作业运行唯一标识</param>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="CancellationToken"/></returns>
    public CancellationTokenSource GetOrCreate(string jobId, Guid runId, CancellationToken stoppingToken)
    {
        return _cancellationTokenSources.GetOrAdd(GetTokenKey(jobId, runId)
            , _ => CancellationTokenSource.CreateLinkedTokenSource(stoppingToken));
    }

    /// <summary>
    /// 取消（完成）本次作业执行
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="runId">作业运行唯一标识</param>
    public void Cancel(string jobId, Guid runId)
    {
        try
        {
            if (_cancellationTokenSources.TryRemove(GetTokenKey(jobId, runId), out var cancellationTokenSource))
            {
                if (!cancellationTokenSource.IsCancellationRequested) cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }
        catch (TaskCanceledException) { }
        catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is TaskCanceledException) { }
        catch { }
    }

    /// <summary>
    /// 取消（完成）所有作业执行
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    public void Cancel(string jobId)
    {
        // 获取所有以作业 Id 开头的作业 TokenKey
        var allJobKeys = _cancellationTokenSources.Keys
            .Where(u => u.StartsWith($"{jobId}__"));

        foreach (var tokenKey in allJobKeys)
        {
            try
            {
                if (_cancellationTokenSources.TryRemove(tokenKey, out var cancellationTokenSource))
                {
                    if (!cancellationTokenSource.IsCancellationRequested) cancellationTokenSource.Cancel();
                    cancellationTokenSource.Dispose();

                    // 输出日志
                    _logger.LogWarning("The scheduler of <{JobId}> cancellation request has been sent to stop its execution.", jobId);
                }
                else
                {
                    // 输出日志
                    _logger.LogWarning(message: "The scheduler of <{jobId}> is not found.", jobId);
                }
            }
            catch (TaskCanceledException) { }
            catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is TaskCanceledException) { }
            catch { }
        }
    }

    /// <summary>
    /// 获取取消作业执行 Token 键
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="runId">作业运行唯一标识</param>
    /// <returns><see cref="string"/></returns>
    private static string GetTokenKey(string jobId, Guid runId)
    {
        return $"{jobId}__{runId}";
    }
}