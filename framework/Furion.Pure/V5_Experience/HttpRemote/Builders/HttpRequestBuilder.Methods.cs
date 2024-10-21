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
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="HttpRequestMessage" /> 构建器
/// </summary>
public sealed partial class HttpRequestBuilder
{
    /// <summary>
    ///     设置跟踪标识
    /// </summary>
    /// <param name="traceIdentifier">设置跟踪标识</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetTraceIdentifier(string traceIdentifier, bool escape = false)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(traceIdentifier);

        TraceIdentifier = traceIdentifier.EscapeDataString(escape);

        return this;
    }

    /// <summary>
    ///     设置内容类型
    /// </summary>
    /// <param name="contentType">内容类型</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetContentType(string contentType)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        // 解析内容类型字符串
        var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(contentType);

        ContentType = mediaTypeHeaderValue.MediaType;

        // 检查是否包含 charset 设置
        if (!string.IsNullOrWhiteSpace(mediaTypeHeaderValue.CharSet))
        {
            SetContentEncoding(mediaTypeHeaderValue.CharSet);
        }

        return this;
    }

    /// <summary>
    ///     设置内容类型
    /// </summary>
    /// <param name="encoding">内容编码</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetContentEncoding(Encoding encoding)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(encoding);

        ContentEncoding = encoding;

        return this;
    }

    /// <summary>
    ///     设置内容类型
    /// </summary>
    /// <param name="encodingName">内容编码名</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetContentEncoding(string encodingName)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(encodingName);

        SetContentEncoding(Encoding.GetEncoding(encodingName));

        return this;
    }

    /// <summary>
    ///     设置 JSON 内容
    /// </summary>
    /// <param name="rawJson">JSON 字符串/原始对象</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetJsonContent(object? rawJson, Encoding? contentEncoding = null) =>
        SetRawContent(rawJson, MediaTypeNames.Application.Json, contentEncoding);

    /// <summary>
    ///     设置 HTML 内容
    /// </summary>
    /// <param name="htmlString">HTML 字符串</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetHtmlContent(string? htmlString, Encoding? contentEncoding = null) =>
        SetRawContent(htmlString, MediaTypeNames.Text.Html, contentEncoding);

    /// <summary>
    ///     设置 XML 内容
    /// </summary>
    /// <param name="xmlString">XML 字符串</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetXmlContent(string? xmlString, Encoding? contentEncoding = null) =>
        SetRawContent(xmlString, MediaTypeNames.Application.Xml, contentEncoding);

    /// <summary>
    ///     设置文本内容
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetTextContent(string? text, Encoding? contentEncoding = null) =>
        SetRawContent(text, MediaTypeNames.Text.Plain, contentEncoding);

    /// <summary>
    ///     设置 URL 表单内容
    /// </summary>
    /// <param name="rawObject">原始对象</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <param name="useStringContent">
    ///     是否使用 <see cref="StringContent" /> 构建
    ///     <see cref="FormUrlEncodedContent" />。默认 <c>false</c>。
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetFormUrlEncodedContent(object? rawObject, Encoding? contentEncoding = null,
        bool useStringContent = false)
    {
        SetRawContent(rawObject, MediaTypeNames.Application.FormUrlEncoded, contentEncoding);

        // 检查是否启用 StringContent 方式构建 application/x-www-form-urlencoded 请求内容
        if (useStringContent)
        {
            AddHttpContentProcessors(() => [new StringContentForFormUrlEncodedContentProcessor()]);
        }

        return this;
    }

    /// <summary>
    ///     设置原始请求内容
    /// </summary>
    /// <param name="rawContent">原始请求内容</param>
    /// <param name="contentType">内容类型</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetRawContent(object? rawContent, string? contentType = null,
        Encoding? contentEncoding = null)
    {
        RawContent = rawContent;

        // 空检查
        if (!string.IsNullOrWhiteSpace(contentType))
        {
            SetContentType(contentType);
        }

        // 空检查
        if (contentEncoding is not null)
        {
            SetContentEncoding(contentEncoding);
        }

        return this;
    }

    /// <summary>
    ///     设置请求内容，请求类型为 <c>multipart/form-data</c>
    /// </summary>
    /// <remarks>
    ///     该操作将强制覆盖 <see cref="SetRawContent" />、<see cref="SetContentEncoding(System.Text.Encoding)" /> 和
    ///     <see cref="SetContentType" /> 设置的内容。
    /// </remarks>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetMultipartContent(Action<HttpMultipartFormDataBuilder> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        // 初始化 HttpMultipartFormDataBuilder 实例
        var httpMultipartFormDataBuilder = new HttpMultipartFormDataBuilder(this);

        // 调用自定义配置委托
        configure.Invoke(httpMultipartFormDataBuilder);

        MultipartFormDataBuilder = httpMultipartFormDataBuilder;

        return this;
    }

    /// <summary>
    ///     设置请求内容，请求类型为 <c>multipart/form-data</c>
    /// </summary>
    /// <remarks>
    ///     该操作将强制覆盖 <see cref="SetRawContent" />、<see cref="SetContentEncoding(System.Text.Encoding)" /> 和
    ///     <see cref="SetContentType" /> 设置的内容。
    /// </remarks>
    /// <param name="httpMultipartFormDataBuilder">
    ///     <see cref="HttpMultipartFormDataBuilder" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    internal HttpRequestBuilder SetMultipartContent(HttpMultipartFormDataBuilder httpMultipartFormDataBuilder)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpMultipartFormDataBuilder);

        MultipartFormDataBuilder = httpMultipartFormDataBuilder;

        return this;
    }

    /// <summary>
    ///     设置请求标头
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="headers">请求标头集合</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithHeaders(IDictionary<string, object?> headers,
        bool escape = false,
        CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(headers);

        Headers ??= new Dictionary<string, List<string?>>(comparer);

        // 存在则合并否则添加
        Headers.AddOrUpdate(headers.ToDictionary(u => u.Key,
            u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
            comparer), false);

        return this;
    }

    /// <summary>
    ///     设置请求标头
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="headerSource">请求头源对象</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithHeaders(object headerSource, bool escape = false, CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(headerSource);

        Headers ??= new Dictionary<string, List<string?>>(comparer);

        // 存在则合并否则添加
        Headers.AddOrUpdate(headerSource.ObjectToDictionary()!
            .ToDictionary(u => u.Key.ToCultureString(culture ?? CultureInfo.InvariantCulture)!,
                u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
                comparer), false);

        return this;
    }

    /// <summary>
    ///     设置片段标识符
    /// </summary>
    /// <param name="fragment">片段标识符</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetFragment(string fragment, bool escape = false)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(fragment);

        Fragment = fragment.EscapeDataString(escape);

        return this;
    }

    /// <summary>
    ///     设置超时时间
    /// </summary>
    /// <param name="timeout">超时时间</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetTimeout(TimeSpan timeout)
    {
        Timeout = timeout;

        return this;
    }

    /// <summary>
    ///     设置超时时间
    /// </summary>
    /// <param name="timeoutMilliseconds">超时时间（毫秒）</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetTimeout(double timeoutMilliseconds)
    {
        // 检查参数是否小于 0
        if (timeoutMilliseconds < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(timeoutMilliseconds), "Timeout value must be non-negative.");
        }

        Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

        return this;
    }

    /// <summary>
    ///     设置查询参数集合
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="parameters">查询参数集合</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithQueryParameters(IDictionary<string, object?> parameters,
        bool escape = false,
        CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(parameters);

        QueryParameters ??= new Dictionary<string, List<string?>>(comparer);

        // 存在则合并否则添加
        QueryParameters.AddOrUpdate(parameters.ToDictionary(u => u.Key,
            u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
            comparer));

        return this;
    }

    /// <summary>
    ///     设置查询参数集合
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="parameterSource">查询参数集合</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithQueryParameters(object parameterSource, bool escape = false,
        CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(parameterSource);

        QueryParameters ??= new Dictionary<string, List<string?>>(comparer);

        // 存在则合并否则添加
        QueryParameters.AddOrUpdate(parameterSource.ObjectToDictionary()!
            .ToDictionary(u => u.Key.ToCultureString(culture ?? CultureInfo.InvariantCulture)!,
                u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
                comparer));

        return this;
    }

    /// <summary>
    ///     设置路径参数集合
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="parameters">路径参数集合</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithPathParameters(IDictionary<string, object?> parameters,
        bool escape = false,
        CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(parameters);

        PathParameters ??= new Dictionary<string, string?>(comparer);

        // 存在则更新否则添加
        PathParameters.AddOrUpdate(parameters.ToDictionary(u => u.Key,
            u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
            comparer));

        return this;
    }

    /// <summary>
    ///     设置路径参数集合
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="parameterSource">路径参数源对象</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithPathParameters(object parameterSource, bool escape = false,
        CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(parameterSource);

        PathParameters ??= new Dictionary<string, string?>(comparer);

        // 存在则更新否则添加
        PathParameters.AddOrUpdate(parameterSource.ObjectToDictionary()!
            .ToDictionary(u => u.Key.ToCultureString(culture ?? CultureInfo.InvariantCulture)!,
                u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
                comparer));

        return this;
    }

    /// <summary>
    ///     设置 Cookies 集合
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="cookies">Cookies 集合</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithCookies(IDictionary<string, object?> cookies,
        bool escape = false,
        CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(cookies);

        Cookies ??= new Dictionary<string, string?>(comparer);

        // 存在则更新否则添加
        Cookies.AddOrUpdate(cookies.ToDictionary(u => u.Key,
            u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
            comparer));

        return this;
    }

    /// <summary>
    ///     设置 Cookies 集合
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="cookieSource">Cookie 参数源对象</param>
    /// <param name="escape">是否转义字符串，默认 <c>false</c></param>
    /// <param name="culture">
    ///     <see cref="CultureInfo" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer{T}" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithCookies(object cookieSource, bool escape = false,
        CultureInfo? culture = null,
        IEqualityComparer<string>? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(cookieSource);

        Cookies ??= new Dictionary<string, string?>(comparer);

        // 存在则更新否则添加
        Cookies.AddOrUpdate(cookieSource.ObjectToDictionary()!
            .ToDictionary(u => u.Key.ToCultureString(culture ?? CultureInfo.InvariantCulture)!,
                u => u.Value?.ToCultureString(culture ?? CultureInfo.InvariantCulture)?.EscapeDataString(escape),
                comparer));

        return this;
    }

    /// <summary>
    ///     设置 <see cref="HttpClient" /> 实例的配置名称
    /// </summary>
    /// <param name="httpClientFactoryName"><see cref="HttpClient" /> 实例的配置名称</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetHttpClientFactoryName(string httpClientFactoryName)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpClientFactoryName);

        HttpClientFactoryName = httpClientFactoryName;

        return this;
    }

    /// <summary>
    ///     设置 <see cref="HttpClient" /> 实例提供器
    /// </summary>
    /// <param name="configure"><inheritdoc cref="HttpClient" /> 实例提供器</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetHttpClientProvider(Func<(HttpClient Instance, Action<HttpClient>? Release)> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        HttpClientProvider = configure;

        return this;
    }

    /// <summary>
    ///     添加 <see cref="IHttpContentProcessor" /> 集合提供器
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="configure"><see cref="IHttpContentProcessor" /> 实例提供器</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder AddHttpContentProcessors(Func<IEnumerable<IHttpContentProcessor>> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        HttpContentProcessorProviders ??= new List<Func<IEnumerable<IHttpContentProcessor>>>();

        HttpContentProcessorProviders.Add(configure);

        return this;
    }

    /// <summary>
    ///     添加 <see cref="IHttpContentProcessor" /> 集合提供器
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="configure"><see cref="IHttpContentProcessor" /> 实例提供器</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder AddHttpContentConverters(Func<IEnumerable<IHttpContentConverter>> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        HttpContentConverterProviders ??= new List<Func<IEnumerable<IHttpContentConverter>>>();

        HttpContentConverterProviders.Add(configure);

        return this;
    }

    /// <summary>
    ///     设置用于处理在设置 <see cref="HttpRequestMessage" /> 的 <c>Content</c> 时的操作
    /// </summary>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetOnPreSetContent(Action<HttpContent?> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        // 如果 OnPreSetContent 未设置则直接赋值
        if (OnPreSetContent is null)
        {
            OnPreSetContent = configure;
        }
        // 否则创建级联调用委托
        else
        {
            // 复制一个新的委托避免死循环
            var originalOnPreSetContent = OnPreSetContent;

            OnPreSetContent = content =>
            {
                originalOnPreSetContent.Invoke(content);
                configure.Invoke(content);
            };
        }

        return this;
    }

    /// <summary>
    ///     设置在发送 HTTP 请求之前执行的操作
    /// </summary>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetOnPreSendRequest(Action<HttpRequestMessage> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        OnPreSendRequest = configure;

        return this;
    }

    /// <summary>
    ///     设置在发送 HTTP 请求之后执行的操作
    /// </summary>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetOnPostSendRequest(Action<HttpResponseMessage> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        OnPostSendRequest = configure;

        return this;
    }

    /// <summary>
    ///     设置在发送 HTTP 请求发生异常时的操作
    /// </summary>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetOnSendRequestFailed(Action<Exception, HttpResponseMessage?> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        OnSendRequestFailed = configure;

        return this;
    }

    /// <summary>
    ///     如果 HTTP 响应的 IsSuccessStatusCode 属性是 <c>false</c>，则引发异常。
    /// </summary>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder EnsureSuccessStatusCode()
    {
        EnsureSuccessStatusCodeEnabled = true;

        return this;
    }

    /// <summary>
    ///     设置是否如果 HTTP 响应的 IsSuccessStatusCode 属性是 <c>false</c>，则引发异常。
    /// </summary>
    /// <param name="enabled">布尔值</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder EnsureSuccessStatusCode(bool enabled)
    {
        EnsureSuccessStatusCodeEnabled = enabled;

        return this;
    }

    /// <summary>
    ///     设置 Basic 身份验证凭据请求授权标头
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder AddBasicAuthentication(string username, string password)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        // 将用户名和密码转换为 Base64 字符串
        var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));

        AddAuthentication(new AuthenticationHeaderValue(Constants.BASIC_AUTHENTICATION_SCHEME, base64Credentials));

        return this;
    }

    /// <summary>
    ///     设置 JWT (JSON Web Token) 身份验证凭据请求授权标头
    /// </summary>
    /// <param name="jwtToken">JWT 字符串</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder AddJwtBearerAuthentication(string jwtToken)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(jwtToken);

        AddAuthentication(new AuthenticationHeaderValue(Constants.JWT_BEARER_AUTHENTICATION_SCHEME, jwtToken));

        return this;
    }

    /// <summary>
    ///     设置身份验证凭据请求授权标头
    /// </summary>
    /// <param name="authenticationHeader">
    ///     <see cref="AuthenticationHeaderValue" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder AddAuthentication(AuthenticationHeaderValue authenticationHeader)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(authenticationHeader);

        AuthenticationHeader = authenticationHeader;

        return this;
    }

    /// <summary>
    ///     设置禁用 HTTP 缓存
    /// </summary>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder DisableCache()
    {
        DisableCacheEnabled = true;

        return this;
    }

    /// <summary>
    ///     设置禁用 HTTP 缓存
    /// </summary>
    /// <param name="disabled">布尔值</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder DisableCache(bool disabled)
    {
        DisableCacheEnabled = disabled;

        return this;
    }

    /// <summary>
    ///     设置 HTTP 远程请求事件处理程序
    /// </summary>
    /// <param name="requestEventHandlerType">实现 <see cref="IHttpRequestEventHandler" /> 接口的类型</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public HttpRequestBuilder SetEventHandler(Type requestEventHandlerType)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(requestEventHandlerType);

        // 检查类型是否实现了 IHttpRequestEventHandler 接口
        if (!typeof(IHttpRequestEventHandler).IsAssignableFrom(requestEventHandlerType))
        {
            throw new InvalidOperationException(
                $"`{requestEventHandlerType}` type is not assignable from `{typeof(IHttpRequestEventHandler)}`.");
        }

        RequestEventHandlerType = requestEventHandlerType;

        return this;
    }

    /// <summary>
    ///     设置 HTTP 远程请求事件处理程序
    /// </summary>
    /// <typeparam name="TRequestEventHandler">
    ///     <see cref="IHttpRequestEventHandler" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SetEventHandler<TRequestEventHandler>()
        where TRequestEventHandler : IHttpRequestEventHandler =>
        SetEventHandler(typeof(TRequestEventHandler));

    /// <summary>
    ///     设置是否启用 <see cref="HttpClient" /> 的池化管理
    /// </summary>
    /// <remarks>用于在并发请求中复用同一个 <see cref="HttpClient" /> 实例。</remarks>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder UseHttpClientPool()
    {
        HttpClientPoolingEnabled = true;

        return this;
    }

    /// <summary>
    ///     模拟浏览器环境
    /// </summary>
    /// <remarks>设置此配置后，将在单次请求标头中添加主流浏览器的 <c>User-Agent</c> 值。</remarks>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder SimulateBrowser() =>
        WithHeaders(new Dictionary<string, object?> { { HeaderNames.UserAgent, Constants.USER_AGENT_OF_BROWSER } });

    /// <summary>
    ///     释放资源集合
    /// </summary>
    /// <remarks>包含自定义 <see cref="HttpClient" /> 实例和其他可释放对象集合。</remarks>
    public void ReleaseResources()
    {
        // 空检查
        if (HttpClientPooling is not null)
        {
            HttpClientPooling.Release?.Invoke(HttpClientPooling.Instance);
            HttpClientPooling = null;
        }

        // 释放可释放的对象集合
        ReleaseDisposables();
    }

    /// <summary>
    ///     添加状态码处理程序
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="statusCodes">HTTP 状态码集合</param>
    /// <param name="handler">自定义处理程序</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithStatusCodeHandler(IEnumerable<int> statusCodes,
        Func<HttpResponseMessage, CancellationToken, Task> handler)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(statusCodes);

        // 检查数量是否为空
        if (statusCodes.TryGetCount(out var count) && count == 0)
        {
            throw new ArgumentException(
                "The status codes array cannot be empty. At least one status code must be provided.",
                nameof(statusCodes));
        }

        // 空检查
        ArgumentNullException.ThrowIfNull(handler);

        StatusCodeHandlers ??= new Dictionary<IEnumerable<int>, Func<HttpResponseMessage, CancellationToken, Task>>();

        StatusCodeHandlers[statusCodes] = handler;

        return this;
    }

    /// <summary>
    ///     添加状态码处理程序
    /// </summary>
    /// <remarks>支持多次调用。</remarks>
    /// <param name="statusCode">HTTP 状态码</param>
    /// <param name="handler">自定义处理程序</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder WithStatusCodeHandler(int statusCode,
        Func<HttpResponseMessage, CancellationToken, Task> handler) =>
        WithStatusCodeHandler([statusCode], handler);

    /// <summary>
    ///     设置是否启用请求分析工具
    /// </summary>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public HttpRequestBuilder Profiler()
    {
        ProfilerEnabled = true;

        return this;
    }

    /// <summary>
    ///     添加请求结束时需要释放的对象
    /// </summary>
    /// <param name="disposable">
    ///     <see cref="IDisposable" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    internal HttpRequestBuilder AddDisposable(IDisposable disposable)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(disposable);

        Disposables ??= [];
        Disposables.Add(disposable);

        return this;
    }

    /// <summary>
    ///     释放可释放的对象集合
    /// </summary>
    internal void ReleaseDisposables()
    {
        // 空检查
        if (Disposables.IsNullOrEmpty())
        {
            return;
        }

        // 逐条遍历进行释放
        foreach (var disposable in Disposables)
        {
            disposable.Dispose();
        }

        // 清空集合
        Disposables.Clear();
    }
}