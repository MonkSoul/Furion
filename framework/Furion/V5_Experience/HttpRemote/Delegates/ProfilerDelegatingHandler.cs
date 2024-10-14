// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.HttpRemote.Extensions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 远程请求分析工具中间件
/// </summary>
/// <remarks>参考文献：https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0#outgoing-request-middleware</remarks>
/// <param name="logger">
///     <see cref="Logger{T}" />
/// </param>
public sealed class ProfilerDelegatingHandler(ILogger<Logging> logger) : DelegatingHandler
{
    /// <inheritdoc />
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // 记录请求标头
        LogRequestHeaders(request);

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        // 发送 HTTP 远程请求
        var httpResponseMessage = base.Send(request, cancellationToken);

        // 获取请求耗时
        var requestDuration = stopwatch.ElapsedMilliseconds;

        // 停止计时
        stopwatch.Stop();

        // 记录常规和响应标头
        LogResponseHeadersAndSummary(httpResponseMessage, requestDuration);

        return httpResponseMessage;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // 记录请求标头
        LogRequestHeaders(request);

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        // 发送 HTTP 远程请求
        var httpResponseMessage = await base.SendAsync(request, cancellationToken);

        // 获取请求耗时
        var requestDuration = stopwatch.ElapsedMilliseconds;

        // 停止计时
        stopwatch.Stop();

        // 记录常规和响应标头
        LogResponseHeadersAndSummary(httpResponseMessage, requestDuration);

        return httpResponseMessage;
    }

    /// <summary>
    ///     记录请求标头
    /// </summary>
    /// <param name="request">
    ///     <see cref="HttpRequestMessage" />
    /// </param>
    internal void LogRequestHeaders(HttpRequestMessage request) => Log(request.ProfilerHeaders());

    /// <summary>
    ///     记录常规和响应标头
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="requestDuration">请求耗时（毫秒）</param>
    internal void LogResponseHeadersAndSummary(HttpResponseMessage httpResponseMessage, long requestDuration) =>
        Log(httpResponseMessage.ProfilerGeneralAndHeaders(generalCustomKeyValues:
            [new KeyValuePair<string, IEnumerable<string>>("Request Duration (ms)", [$"{requestDuration:N2}"])]));

    /// <summary>
    ///     打印日志
    /// </summary>
    /// <param name="message">日志消息</param>
    internal void Log(string? message)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        // 打印日志
        logger.LogInformation("{message}", message);
    }
}