﻿// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Utilities;
using System.Diagnostics;

namespace Furion.HttpRemote;

/// <summary>
///     压力测试结果
/// </summary>
public sealed class StressTestHarnessResult
{
    /// <summary>
    ///     用于将 <see cref="Stopwatch" /> 的 <c>ticks</c> 转换为毫秒
    /// </summary>
    internal static readonly double _ticksPerMillisecond = Stopwatch.Frequency / 1000.0;

    /// <summary>
    ///     <inheritdoc cref="StressTestHarnessResult" />
    /// </summary>
    /// <param name="totalRequests">总请求次数</param>
    /// <param name="totalTimeInSeconds">总用时（秒）</param>
    /// <param name="successfulRequests">成功请求次数</param>
    /// <param name="failedRequests">失败请求次数</param>
    /// <param name="responseTimes">请求的响应时间数组</param>
    /// <exception cref="ArgumentException"></exception>
    public StressTestHarnessResult(long totalRequests, double totalTimeInSeconds, long successfulRequests,
        long failedRequests, long[] responseTimes)
    {
        // 检查请求的响应时间数组长度是否等于总请求次数
        if (responseTimes.Length != totalRequests)
        {
            throw new ArgumentException(
                $"The number of response times ({responseTimes.Length}) does not match the total number of requests ({totalRequests}).",
                nameof(responseTimes)
            );
        }

        TotalRequests = totalRequests;
        TotalTimeInSeconds = totalTimeInSeconds;
        SuccessfulRequests = successfulRequests;
        FailedRequests = failedRequests;

        // 计算每秒查询率 (QPS)
        CalculateQueriesPerSecond(totalRequests, totalTimeInSeconds);

        // 计算最小、最大和平均响应时间（毫秒）
        CalculateMinMaxAvgResponseTime(responseTimes, totalRequests);

        // 计算各个百分位的响应时间（毫秒）
        CalculatePercentiles(responseTimes);
    }

    /// <summary>
    ///     总请求次数
    /// </summary>
    public long TotalRequests { get; }

    /// <summary>
    ///     总用时（秒）
    /// </summary>
    public double TotalTimeInSeconds { get; }

    /// <summary>
    ///     成功请求次数
    /// </summary>
    public long SuccessfulRequests { get; }

    /// <summary>
    ///     失败请求次数
    /// </summary>
    public long FailedRequests { get; }

    /// <summary>
    ///     每秒查询率 (QPS)
    /// </summary>
    public double QueriesPerSecond { get; private set; }

    /// <summary>
    ///     最小响应时间（毫秒）
    /// </summary>
    public double MinResponseTime { get; private set; }

    /// <summary>
    ///     最大响应时间（毫秒）
    /// </summary>
    public double MaxResponseTime { get; private set; }

    /// <summary>
    ///     平均响应时间（毫秒）
    /// </summary>
    public double AverageResponseTime { get; private set; }

    /// <summary>
    ///     P10 响应时间（毫秒）
    /// </summary>
    public double Percentile10ResponseTime { get; private set; }

    /// <summary>
    ///     P25 响应时间（毫秒）
    /// </summary>
    public double Percentile25ResponseTime { get; private set; }

    /// <summary>
    ///     P50 响应时间（毫秒）
    /// </summary>
    public double Percentile50ResponseTime { get; private set; }

    /// <summary>
    ///     P75 响应时间（毫秒）
    /// </summary>
    public double Percentile75ResponseTime { get; private set; }

    /// <summary>
    ///     P90 响应时间（毫秒）
    /// </summary>
    public double Percentile90ResponseTime { get; private set; }

    /// <summary>
    ///     P99 响应时间（毫秒）
    /// </summary>
    public double Percentile99ResponseTime { get; private set; }

    /// <summary>
    ///     P99.99 响应时间（毫秒）
    /// </summary>
    public double Percentile9999ResponseTime { get; private set; }

    /// <inheritdoc />
    public override string ToString() =>
        StringUtility.FormatKeyValuesSummary([
            new KeyValuePair<string, IEnumerable<string>>("Total Requests", [$"{TotalRequests}"]),
            new KeyValuePair<string, IEnumerable<string>>("Total Time (s)", [$"{TotalTimeInSeconds:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("Successful Requests", [$"{SuccessfulRequests}"]),
            new KeyValuePair<string, IEnumerable<string>>("Failed Requests", [$"{FailedRequests}"]),
            new KeyValuePair<string, IEnumerable<string>>("QPS", [$"{QueriesPerSecond:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("Min RT (ms)", [$"{MinResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("Max RT (ms)", [$"{MaxResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("Avg RT (ms)", [$"{AverageResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("P10 RT (ms)", [$"{Percentile10ResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("P25 RT (ms)", [$"{Percentile25ResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("P50 RT (ms)", [$"{Percentile50ResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("P75 RT (ms)", [$"{Percentile75ResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("P90 RT (ms)", [$"{Percentile90ResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("P99 RT (ms)", [$"{Percentile99ResponseTime:N2}"]),
            new KeyValuePair<string, IEnumerable<string>>("99.99 RT (ms)", [$"{Percentile9999ResponseTime:N2}"])
        ], "Stress Test Harness Result")!;

    /// <summary>
    ///     计算每秒查询率 (QPS)
    /// </summary>
    /// <param name="totalRequests">总请求次数</param>
    /// <param name="totalTimeInSeconds">总用时（秒）</param>
    internal void CalculateQueriesPerSecond(long totalRequests, double totalTimeInSeconds) =>
        QueriesPerSecond = totalTimeInSeconds > 0 ? totalRequests / totalTimeInSeconds : 0;

    /// <summary>
    ///     计算最小、最大和平均响应时间（毫秒）
    /// </summary>
    /// <param name="responseTimes">每个请求的响应时间数组</param>
    /// <param name="totalRequests">总请求次数</param>
    internal void CalculateMinMaxAvgResponseTime(long[] responseTimes, long totalRequests)
    {
        // 计算最小响应时间和最大响应时间并转换为毫秒
        MinResponseTime = responseTimes.Min() / _ticksPerMillisecond;
        MaxResponseTime = responseTimes.Max() / _ticksPerMillisecond;

        // 计算总响应时间
        var totalResponseTime = responseTimes.Sum();

        // 计算平均响应时间
        var averageResponseTime = totalResponseTime > 0
            ? totalResponseTime / totalRequests
            : 0L;

        // 将平均响应时间转换为毫秒
        AverageResponseTime = averageResponseTime / _ticksPerMillisecond;
    }

    /// <summary>
    ///     计算各个百分位的响应时间（毫秒）
    /// </summary>
    /// <param name="responseTimes">请求的响应时间数组</param>
    internal void CalculatePercentiles(long[] responseTimes)
    {
        // 对请求响应时间数组进行排序
        var sortedResponseTimes = responseTimes.OrderBy(t => t).ToArray();

        // 计算百分位数的响应时间并转换为毫秒
        Percentile10ResponseTime = CalculatePercentile(sortedResponseTimes, 0.1);
        Percentile25ResponseTime = CalculatePercentile(sortedResponseTimes, 0.25);
        Percentile50ResponseTime = CalculatePercentile(sortedResponseTimes, 0.5);
        Percentile75ResponseTime = CalculatePercentile(sortedResponseTimes, 0.75);
        Percentile90ResponseTime = CalculatePercentile(sortedResponseTimes, 0.9);
        Percentile99ResponseTime = CalculatePercentile(sortedResponseTimes, 0.99);
        Percentile9999ResponseTime = CalculatePercentile(sortedResponseTimes, 0.9999);
    }

    /// <summary>
    ///     计算百分位数并转换为毫秒
    /// </summary>
    /// <param name="sortedResponseTimes">排序后的请求的响应时间数组</param>
    /// <param name="percentile">百分位数</param>
    /// <returns>
    ///     <see cref="double" />
    /// </returns>
    internal static double CalculatePercentile(long[] sortedResponseTimes, double percentile)
    {
        var index = (int)Math.Ceiling(percentile * sortedResponseTimes.Length) - 1;
        if (index >= sortedResponseTimes.Length)
        {
            index = sortedResponseTimes.Length - 1;
        }

        return sortedResponseTimes[index] / _ticksPerMillisecond;
    }
}