// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
    /// 构造函数
    /// </summary>
    public JobCancellationToken()
    {
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
    /// <param name="jobId"></param>
    public void Cancel(string jobId)
    {
        var allJobKeys = _cancellationTokenSources.Keys
            .Where(u => u.StartsWith($"{jobId}__"));

        foreach (var key in allJobKeys)
        {
            try
            {
                if (_cancellationTokenSources.TryRemove(key, out var cancellationTokenSource))
                {
                    if (!cancellationTokenSource.IsCancellationRequested) cancellationTokenSource.Cancel();
                    cancellationTokenSource.Dispose();
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