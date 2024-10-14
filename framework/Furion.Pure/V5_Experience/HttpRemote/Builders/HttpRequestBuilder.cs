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

using Furion.Extensions;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using CacheControlHeaderValue = System.Net.Http.Headers.CacheControlHeaderValue;

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="HttpRequestMessage" /> 构建器
/// </summary>
public sealed partial class HttpRequestBuilder
{
    /// <summary>
    ///     <inheritdoc cref="HttpRequestBuilder" />
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    internal HttpRequestBuilder(HttpMethod httpMethod, Uri? requestUri)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpMethod);

        Method = httpMethod;
        RequestUri = requestUri;
    }

    /// <summary>
    ///     构建 <see cref="HttpRequestMessage" /> 实例
    /// </summary>
    /// <param name="httpRemoteOptions">
    ///     <see cref="HttpRemoteOptions" />
    /// </param>
    /// <param name="httpContentProcessorFactory">
    ///     <see cref="IHttpContentProcessorFactory" />
    /// </param>
    /// <param name="baseUri">基地址</param>
    /// <returns>
    ///     <see cref="HttpRequestMessage" />
    /// </returns>
    internal HttpRequestMessage Build(HttpRemoteOptions httpRemoteOptions,
        IHttpContentProcessorFactory httpContentProcessorFactory, Uri? baseUri)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteOptions);
        ArgumentNullException.ThrowIfNull(httpContentProcessorFactory);
        ArgumentNullException.ThrowIfNull(Method);

        // 构建最终的请求地址
        var finalRequestUri = BuildFinalRequestUri(baseUri);

        // 初始化 HttpRequestMessage 实例
        var httpRequestMessage = new HttpRequestMessage(Method, finalRequestUri);

        // 追加请求标头
        AppendHeaders(httpRequestMessage);

        // 追加 Cookies
        AppendCookies(httpRequestMessage);

        // 构建并设置指定的 HttpRequestMessage 请求消息的内容
        BuildAndSetContent(httpRequestMessage, httpContentProcessorFactory, httpRemoteOptions);

        return httpRequestMessage;
    }

    /// <summary>
    ///     构建最终的请求地址
    /// </summary>
    /// <param name="baseUri">基地址</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal string BuildFinalRequestUri(Uri? baseUri)
    {
        // 初始化 UriBuilder 实例
        var uriBuilder = new UriBuilder(baseUri is null ? RequestUri! : new Uri(baseUri, RequestUri!));

        // 追加片段标识符
        AppendFragment(uriBuilder);

        // 追加查询参数
        AppendQueryParameters(uriBuilder);

        // 替换路径参数
        var finalRequestUri = ReplacePathPlaceholders(uriBuilder);

        return finalRequestUri;
    }

    /// <summary>
    ///     追加片段标识符
    /// </summary>
    /// <param name="uriBuilder">
    ///     <see cref="UriBuilder" />
    /// </param>
    internal void AppendFragment(UriBuilder uriBuilder)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(Fragment))
        {
            return;
        }

        uriBuilder.Fragment = Fragment;
    }

    /// <summary>
    ///     追加查询参数
    /// </summary>
    /// <param name="uriBuilder">
    ///     <see cref="UriBuilder" />
    /// </param>
    internal void AppendQueryParameters(UriBuilder uriBuilder)
    {
        // 空检查
        if (QueryParameters.IsNullOrEmpty())
        {
            return;
        }

        // 解析 URL 中的查询字符串为键值对列表
        var queryParameters = uriBuilder.Query.ParseUrlQueryParameters();

        // 追加查询参数
        foreach (var (key, values) in QueryParameters)
        {
            queryParameters.AddRange(values.Select(value =>
                new KeyValuePair<string, string?>(key, value)));
        }

        // 构建查询字符串赋值给 UriBuilder 的 Query 属性
        uriBuilder.Query =
            "?" + string.Join('&', queryParameters.Select(u => $"{u.Key}={u.Value}"));
    }

    /// <summary>
    ///     替换路径参数
    /// </summary>
    /// <param name="uriBuilder">
    ///     <see cref="UriBuilder" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal string ReplacePathPlaceholders(UriBuilder uriBuilder)
    {
        var newUri = uriBuilder.Uri.ToString();

        return PathParameters.IsNullOrEmpty() ? newUri : newUri.ReplacePlaceholders(PathParameters)!;
    }

    /// <summary>
    ///     追加请求标头
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     <see cref="HttpRequestMessage" />
    /// </param>
    internal void AppendHeaders(HttpRequestMessage httpRequestMessage)
    {
        // 添加跟踪标识
        if (!string.IsNullOrWhiteSpace(TraceIdentifier))
        {
            httpRequestMessage.Headers.TryAddWithoutValidation(Constants.X_TRACE_ID_HEADER, TraceIdentifier);
        }

        // 设置身份验证凭据请求授权标头
        if (AuthenticationHeader is not null)
        {
            httpRequestMessage.Headers.Authorization = AuthenticationHeader;
        }

        // 设置禁用 HTTP 缓存
        if (DisableCacheEnabled)
        {
            httpRequestMessage.Headers.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true, NoStore = true, MustRevalidate = true
            };
        }

        // 空检查
        if (Headers.IsNullOrEmpty())
        {
            return;
        }

        // 遍历请求标头集合并追加到 HttpRequestMessage.Headers 中
        foreach (var (key, values) in Headers)
        {
            httpRequestMessage.Headers.TryAddWithoutValidation(key, values);
        }
    }

    /// <summary>
    ///     追加 Cookies
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     <see cref="HttpRequestMessage" />
    /// </param>
    internal void AppendCookies(HttpRequestMessage httpRequestMessage)
    {
        // 空检查
        if (Cookies.IsNullOrEmpty())
        {
            return;
        }

        httpRequestMessage.Headers.TryAddWithoutValidation(HeaderNames.Cookie,
            string.Join("; ", Cookies.Select(u => $"{u.Key}={u.Value}")));
    }

    /// <summary>
    ///     构建并设置指定的 <see cref="HttpRequestMessage" /> 请求消息的内容
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     <see cref="HttpRequestMessage" />
    /// </param>
    /// <param name="httpContentProcessorFactory">
    ///     <see cref="IHttpContentProcessorFactory" />
    /// </param>
    /// <param name="httpRemoteOptions">
    ///     <see cref="HttpRemoteOptions" />
    /// </param>
    internal void BuildAndSetContent(HttpRequestMessage httpRequestMessage,
        IHttpContentProcessorFactory httpContentProcessorFactory, HttpRemoteOptions httpRemoteOptions)
    {
        // 获取自定义的 IHttpContentProcessor 集合
        var processors = HttpContentProcessorProviders?.SelectMany(u => u.Invoke()).ToArray();

        // 构建 MultipartFormDataContent 请求内容
        if (MultipartFormDataBuilder is not null)
        {
            ContentType = MediaTypeNames.Multipart.FormData;
            RawContent = MultipartFormDataBuilder.Build(httpRemoteOptions, httpContentProcessorFactory, processors);
        }

        // 检查是否设置了内容
        if (RawContent is null)
        {
            return;
        }

        // 设置默认的内容类型
        SetDefaultContentType(httpRemoteOptions.DefaultContentType);

        // 构建 HttpContent 实例
        var httpContent =
            httpContentProcessorFactory.BuildHttpContent(RawContent, ContentType!, ContentEncoding, processors);

        // 调用用于处理在设置请求消息的内容时的操作
        OnPreSetContent?.Invoke(httpContent);

        // 设置 HttpRequestMessage 请求消息的内容
        httpRequestMessage.Content = httpContent;
    }

    /// <summary>
    ///     设置默认的内容类型
    /// </summary>
    /// <param name="defaultContentType">默认请求内容类型</param>
    internal void SetDefaultContentType(string? defaultContentType)
    {
        // 空检查
        if (!string.IsNullOrWhiteSpace(ContentType))
        {
            return;
        }

        ContentType = RawContent switch
        {
            FormUrlEncodedContent => MediaTypeNames.Application.FormUrlEncoded,
            (byte[] or Stream or ByteArrayContent or StreamContent) and not StringContent => MediaTypeNames.Application
                .Octet,
            MultipartContent => MediaTypeNames.Multipart.FormData,
            _ => defaultContentType ?? Constants.DEFAULT_CONTENT_TYPE
        };
    }
}