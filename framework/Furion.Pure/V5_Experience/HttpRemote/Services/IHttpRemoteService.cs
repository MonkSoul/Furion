// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 远程请求服务
/// </summary>
public partial interface IHttpRemoteService
{
    /// <summary>
    ///     <inheritdoc cref="HttpRemoteOptions" />
    /// </summary>
    HttpRemoteOptions RemoteOptions { get; }

    /// <summary>
    ///     <inheritdoc cref="IServiceProvider" />
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Send(HttpRequestBuilder httpRequestBuilder, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Send(HttpRequestBuilder httpRequestBuilder, HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> SendAsync(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> SendAsync(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    TResult? SendAs<TResult>(HttpRequestBuilder httpRequestBuilder, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    TResult? SendAs<TResult>(HttpRequestBuilder httpRequestBuilder, HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    Task<TResult?> SendAsAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    Task<TResult?> SendAsAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Send<TResult>(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Send<TResult>(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> SendAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP 远程请求
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> SendAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);
}