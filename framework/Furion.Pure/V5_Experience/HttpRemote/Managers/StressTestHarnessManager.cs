// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Diagnostics;

namespace Furion.HttpRemote;

/// <summary>
///     压力测试管理器
/// </summary>
internal sealed class StressTestHarnessManager
{
    /// <inheritdoc cref="IHttpRemoteService" />
    internal readonly IHttpRemoteService _httpRemoteService;

    /// <inheritdoc cref="HttpStressTestHarnessBuilder" />
    internal readonly HttpStressTestHarnessBuilder _httpStressTestHarnessBuilder;

    /// <summary>
    ///     <inheritdoc cref="StressTestHarnessManager" />
    /// </summary>
    /// <param name="httpRemoteService">
    ///     <see cref="IHttpRemoteService" />
    /// </param>
    /// <param name="httpStressTestHarnessBuilder">
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    internal StressTestHarnessManager(IHttpRemoteService httpRemoteService,
        HttpStressTestHarnessBuilder httpStressTestHarnessBuilder,
        Action<HttpRequestBuilder>? configure = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteService);
        ArgumentNullException.ThrowIfNull(httpStressTestHarnessBuilder);

        _httpRemoteService = httpRemoteService;
        _httpStressTestHarnessBuilder = httpStressTestHarnessBuilder;

        // 构建 HttpRequestBuilder 实例
        RequestBuilder = httpStressTestHarnessBuilder.Build(_httpRemoteService.RemoteOptions, configure);
    }

    /// <summary>
    ///     <inheritdoc cref="HttpRequestBuilder" />
    /// </summary>
    internal HttpRequestBuilder RequestBuilder { get; }

    /// <summary>
    ///     开始测试
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="StressTestHarnessResult" />
    /// </returns>
    internal StressTestHarnessResult Start(CancellationToken cancellationToken = default) =>
        StartAsync(cancellationToken).GetAwaiter().GetResult();

    /// <summary>
    ///     开始测试
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" />
    /// </returns>
    internal async Task<StressTestHarnessResult> StartAsync(CancellationToken cancellationToken = default)
    {
        // 初始化压力测试次数和轮次
        var numberOfRequests = _httpStressTestHarnessBuilder.NumberOfRequests;
        var numberOfRounds = _httpStressTestHarnessBuilder.NumberOfRounds;

        // 初始化总的成功/失败的请求数量
        var totalSuccessfulRequests = 0L;
        var totalFailedRequests = 0L;

        // 用于记录每个请求的响应时间
        var allResponseTimes = new List<long>();

        // 初始化总的测试时间
        var totalTime = TimeSpan.Zero;

        // 初始化信号量来控制并发度
        var semaphoreSlim = new SemaphoreSlim(_httpStressTestHarnessBuilder.MaxDegreeOfParallelism);

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        // 循环执行指定轮次
        for (var round = 0; round < numberOfRounds && !cancellationToken.IsCancellationRequested; round++)
        {
            // 初始化 Task 数组来存储所有并发任务
            var tasks = new Task[numberOfRequests];

            // 重置响应时间数组
            var responseTimes = new long[numberOfRequests];

            // 重新开始计时
            stopwatch.Restart();

            // 循环创建指定并发请求数量的任务
            for (var i = 0; i < numberOfRequests && !cancellationToken.IsCancellationRequested; i++)
            {
                var index = i;

                // 创建新的异步任务
                tasks[i] = Task.Run(async () =>
                {
                    // 等待信号量
                    await semaphoreSlim.WaitAsync(cancellationToken);

                    // 请求开始时间
                    var requestStart = Stopwatch.GetTimestamp();

                    try
                    {
                        // 发送 HTTP 远程请求
                        var httpResponseMessage =
                            await _httpRemoteService.SendAsync(RequestBuilder, cancellationToken);

                        // 检查响应状态码是否是成功状态
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            // 原子递增成功请求计数
                            Interlocked.Increment(ref totalSuccessfulRequests);
                        }
                        else
                        {
                            // 原子递增失败请求计数
                            Interlocked.Increment(ref totalFailedRequests);
                        }
                    }
                    catch (Exception e) when (cancellationToken.IsCancellationRequested ||
                                              e is OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        // 原子递增失败请求计数
                        Interlocked.Increment(ref totalFailedRequests);
                    }
                    finally
                    {
                        // 计算并存储请求的响应时间
                        var requestEnd = Stopwatch.GetTimestamp();
                        responseTimes[index] = requestEnd - requestStart;

                        // 释放信号量
                        semaphoreSlim.Release();
                    }
                }, cancellationToken);
            }

            // 等待所有任务完成
            await Task.WhenAll(tasks).ConfigureAwait(false);

            // 记录本轮测试结束时间，并累加总的测试时间
            totalTime += stopwatch.Elapsed;

            // 将本轮的响应时间追加到总的响应时间数组中
            allResponseTimes.AddRange(responseTimes);
        }

        // 停止计时
        stopwatch.Stop();

        // 获取请求总用时（秒）
        var totalTimeInSeconds = totalTime.TotalSeconds;

        // 释放 HttpClient 实例管理器
        RequestBuilder.ReleaseHttpClientPooling();

        return new StressTestHarnessResult(
            numberOfRequests * numberOfRounds,
            totalTimeInSeconds,
            totalSuccessfulRequests,
            totalFailedRequests,
            allResponseTimes.ToArray());
    }
}