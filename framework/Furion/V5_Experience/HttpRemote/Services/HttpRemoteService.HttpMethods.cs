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
///     <inheritdoc cref="IHttpRemoteService" />
/// </summary>
internal sealed partial class HttpRemoteService
{
    /// <inheritdoc />
    public HttpResponseMessage Get(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Get(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Get(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Get, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> GetAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> GetAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Get, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? GetAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        GetAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? GetAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Get, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> GetAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        GetAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> GetAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Get, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Get<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Get<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Get<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Get, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> GetAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        GetAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> GetAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Get, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? GetAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? GetAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? GetAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? GetAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? GetAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? GetAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> GetAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> GetAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> GetAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => GetAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> GetAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> GetAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> GetAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        GetAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Put(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Put(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Put(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Put, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> PutAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> PutAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Put, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? PutAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PutAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? PutAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Put, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> PutAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        PutAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> PutAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Put, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Put<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Put<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Put<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Put, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> PutAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        PutAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> PutAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Put, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? PutAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? PutAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? PutAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? PutAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? PutAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? PutAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> PutAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> PutAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> PutAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PutAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> PutAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> PutAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> PutAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PutAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Post(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Post(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Post(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Post, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> PostAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> PostAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Post, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? PostAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PostAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? PostAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Post, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> PostAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        PostAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> PostAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Post, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Post<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Post<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Post<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Post, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> PostAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        PostAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> PostAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Post, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? PostAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? PostAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? PostAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? PostAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? PostAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? PostAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> PostAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> PostAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> PostAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PostAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> PostAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> PostAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> PostAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PostAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Delete(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Delete(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Delete(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Delete, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> DeleteAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => DeleteAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> DeleteAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Delete, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? DeleteAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        DeleteAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? DeleteAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Delete, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> DeleteAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        DeleteAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> DeleteAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Delete, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Delete<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Delete<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Delete<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Delete, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> DeleteAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        DeleteAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> DeleteAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Delete, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? DeleteAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => DeleteAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? DeleteAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => DeleteAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? DeleteAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => DeleteAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? DeleteAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? DeleteAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? DeleteAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> DeleteAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> DeleteAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> DeleteAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> DeleteAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> DeleteAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> DeleteAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        DeleteAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Head(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Head(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Head(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Head, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> HeadAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> HeadAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Head, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? HeadAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        HeadAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? HeadAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Head, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> HeadAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        HeadAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> HeadAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Head, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Head<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Head<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Head<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Head, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> HeadAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        HeadAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> HeadAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Head, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? HeadAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? HeadAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? HeadAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? HeadAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? HeadAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? HeadAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> HeadAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> HeadAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> HeadAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => HeadAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> HeadAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> HeadAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> HeadAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        HeadAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Options(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Options(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Options(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Options, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> OptionsAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => OptionsAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> OptionsAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Options, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? OptionsAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        OptionsAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? OptionsAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Options, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> OptionsAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        OptionsAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> OptionsAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Options, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Options<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Options<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Options<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Options, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> OptionsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        OptionsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> OptionsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Options, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? OptionsAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => OptionsAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? OptionsAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => OptionsAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? OptionsAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => OptionsAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? OptionsAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? OptionsAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? OptionsAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> OptionsAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> OptionsAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> OptionsAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> OptionsAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> OptionsAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> OptionsAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        OptionsAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Trace(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Trace(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Trace(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Trace, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> TraceAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => TraceAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> TraceAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Trace, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? TraceAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        TraceAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? TraceAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Trace, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> TraceAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        TraceAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> TraceAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Trace, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Trace<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Trace<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Trace<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Trace, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> TraceAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        TraceAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> TraceAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Trace, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? TraceAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => TraceAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? TraceAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => TraceAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? TraceAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => TraceAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? TraceAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? TraceAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? TraceAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> TraceAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> TraceAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> TraceAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> TraceAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> TraceAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> TraceAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        TraceAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Patch(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => Patch(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Patch(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send(HttpRequestBuilder.Create(HttpMethod.Patch, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> PatchAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PatchAsync(requestUri, configure,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpResponseMessage> PatchAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync(HttpRequestBuilder.Create(HttpMethod.Patch, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public TResult? PatchAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PatchAs<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? PatchAs<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAs<TResult>(HttpRequestBuilder.Create(HttpMethod.Patch, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> PatchAsAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        PatchAsAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<TResult?> PatchAsAsync<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Patch, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Patch<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        Patch<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Patch<TResult>(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(HttpRequestBuilder.Create(HttpMethod.Patch, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> PatchAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure = null, CancellationToken cancellationToken = default) =>
        PatchAsync<TResult>(requestUri, configure, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> PatchAsync<TResult>(string? requestUri,
        Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        SendAsync<TResult>(HttpRequestBuilder.Create(HttpMethod.Patch, requestUri, configure), completionOption,
            cancellationToken);

    /// <inheritdoc />
    public string? PatchAsString(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PatchAs<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Stream? PatchAsStream(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PatchAs<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public byte[]? PatchAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) => PatchAs<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public string? PatchAsString(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAs<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Stream? PatchAsStream(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAs<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public byte[]? PatchAsByteArray(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAs<byte[]>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<string?> PatchAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<string>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> PatchAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<Stream>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> PatchAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure = null,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<byte[]>(requestUri, configure, cancellationToken);

    /// <inheritdoc />
    public Task<string?> PatchAsStringAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<string>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<Stream?> PatchAsStreamAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<Stream>(requestUri, configure, completionOption, cancellationToken);

    /// <inheritdoc />
    public Task<byte[]?> PatchAsByteArrayAsync(string? requestUri, Action<HttpRequestBuilder>? configure,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default) =>
        PatchAsAsync<byte[]>(requestUri, configure, completionOption, cancellationToken);
}