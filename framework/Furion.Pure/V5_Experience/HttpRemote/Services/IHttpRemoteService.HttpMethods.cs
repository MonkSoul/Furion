// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 远程请求服务
/// </summary>
public partial interface IHttpRemoteService
{
    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Get(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Get(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> GetAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> GetAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? GetAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? GetAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> GetAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> GetAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Get<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Get<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> GetAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> GetAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? GetAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? GetAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? GetAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? GetAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? GetAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? GetAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> GetAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> GetAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> GetAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> GetAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> GetAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP GET 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> GetAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);


    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Put(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Put(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> PutAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> PutAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? PutAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? PutAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> PutAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> PutAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Put<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Put<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> PutAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> PutAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? PutAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? PutAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? PutAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? PutAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? PutAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? PutAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> PutAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> PutAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> PutAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> PutAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> PutAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PUT 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> PutAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Post(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Post(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> PostAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> PostAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? PostAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? PostAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> PostAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> PostAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Post<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Post<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> PostAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> PostAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? PostAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? PostAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? PostAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? PostAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? PostAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? PostAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> PostAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> PostAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> PostAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> PostAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> PostAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP POST 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> PostAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Delete(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Delete(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> DeleteAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> DeleteAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? DeleteAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? DeleteAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> DeleteAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> DeleteAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Delete<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Delete<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> DeleteAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> DeleteAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? DeleteAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => DeleteAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? DeleteAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => DeleteAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? DeleteAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => DeleteAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? DeleteAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? DeleteAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? DeleteAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> DeleteAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> DeleteAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> DeleteAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> DeleteAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> DeleteAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP DELETE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> DeleteAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Head(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Head(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> HeadAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> HeadAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? HeadAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? HeadAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> HeadAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> HeadAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Head<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Head<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> HeadAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> HeadAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? HeadAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? HeadAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? HeadAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? HeadAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? HeadAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? HeadAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> HeadAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> HeadAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> HeadAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> HeadAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> HeadAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP HEAD 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> HeadAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Options(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Options(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> OptionsAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> OptionsAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? OptionsAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? OptionsAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> OptionsAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> OptionsAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Options<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Options<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> OptionsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> OptionsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? OptionsAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => OptionsAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? OptionsAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => OptionsAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? OptionsAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => OptionsAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? OptionsAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? OptionsAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? OptionsAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> OptionsAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> OptionsAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> OptionsAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> OptionsAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> OptionsAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP OPTIONS 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> OptionsAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Trace(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Trace(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> TraceAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> TraceAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? TraceAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? TraceAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> TraceAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> TraceAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Trace<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Trace<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> TraceAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> TraceAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? TraceAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => TraceAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? TraceAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => TraceAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? TraceAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => TraceAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? TraceAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? TraceAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? TraceAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> TraceAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> TraceAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> TraceAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> TraceAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> TraceAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP TRACE 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> TraceAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Patch(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    HttpResponseMessage Patch(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> PatchAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    Task<HttpResponseMessage> PatchAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    TResult? PatchAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    TResult? PatchAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<TResult?> PatchAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<TResult?> PatchAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    HttpRemoteResult<TResult> Patch<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    HttpRemoteResult<TResult> Patch<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    Task<HttpRemoteResult<TResult>> PatchAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
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
    Task<HttpRemoteResult<TResult>> PatchAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? PatchAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PatchAs<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? PatchAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PatchAs<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? PatchAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PatchAs<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public string? PatchAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Stream? PatchAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public byte[]? PatchAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> PatchAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<string>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> PatchAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> PatchAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public Task<string?> PatchAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Stream" />
    /// </returns>
    public Task<Stream?> PatchAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <summary>
    ///     发送 HTTP PATCH 远程请求
    /// </summary>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <c>byte[]</c>
    /// </returns>
    public Task<byte[]?> PatchAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);
}