// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.HttpRemote.Extensions;

/// <summary>
///     HTTP 远程服务拓展类
/// </summary>
public static class HttpRemoteExtensions
{
    /// <summary>
    ///     添加 HTTP 远程请求分析工具中间件
    /// </summary>
    /// <param name="builder">
    ///     <see cref="IHttpClientBuilder" />
    /// </param>
    /// <returns>
    ///     <see cref="IHttpClientBuilder" />
    /// </returns>
    public static IHttpClientBuilder AddProfilerDelegatingHandler(this IHttpClientBuilder builder) =>
        builder.AddHttpMessageHandler<ProfilerDelegatingHandler>();

    /// <summary>
    ///     分析 <see cref="HttpRequestMessage" /> 标头
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     <see cref="HttpRequestMessage" />
    /// </param>
    /// <param name="summary">摘要</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public static string? ProfilerHeaders(this HttpRequestMessage httpRequestMessage,
        string? summary = "Request Headers") =>
        StringUtility.FormatKeyValuesSummary(httpRequestMessage.Headers, summary);

    /// <summary>
    ///     分析 <see cref="HttpResponseMessage" /> 标头
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="summary">摘要</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public static string? ProfilerHeaders(this HttpResponseMessage httpResponseMessage,
        string? summary = "Response Headers") =>
        StringUtility.FormatKeyValuesSummary(httpResponseMessage.Headers.Concat(httpResponseMessage.Content.Headers),
            summary);

    /// <summary>
    ///     分析常规和 <see cref="HttpResponseMessage" /> 标头
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="responseSummary">响应标头摘要</param>
    /// <param name="generalSummary">常规摘要</param>
    /// <param name="generalCustomKeyValues">自定义常规摘要键值集合</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public static string ProfilerGeneralAndHeaders(this HttpResponseMessage httpResponseMessage,
        string? responseSummary = "Response Headers", string? generalSummary = "General",
        IEnumerable<KeyValuePair<string, IEnumerable<string>>>? generalCustomKeyValues = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpResponseMessage);

        // 获取 HttpRequestMessage 实例
        var httpRequestMessage = httpResponseMessage.RequestMessage;

        // 空检查
        ArgumentNullException.ThrowIfNull(httpRequestMessage);

        // 格式化常规条目
        var generalEntry = StringUtility.FormatKeyValuesSummary(new[]
        {
            new KeyValuePair<string, IEnumerable<string>>("Request URL",
                [httpRequestMessage.RequestUri?.OriginalString!]),
            new KeyValuePair<string, IEnumerable<string>>("HTTP Method", [httpRequestMessage.Method.ToString()]),
            new KeyValuePair<string, IEnumerable<string>>("Status Code",
                [$"{(int)httpResponseMessage.StatusCode} {httpResponseMessage.StatusCode}"])
        }.Concat(generalCustomKeyValues ?? []), generalSummary);

        // 格式化响应条目
        var responseEntry = httpResponseMessage.ProfilerHeaders(responseSummary);

        return $"{generalEntry}\r\n{responseEntry}";
    }
}