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

using System.Net.Http.Headers;
using System.Text;

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="HttpRequestMessage" /> 构建器
/// </summary>
public sealed partial class HttpRequestBuilder
{
    /// <summary>
    ///     请求地址
    /// </summary>
    public Uri? RequestUri { get; }

    /// <summary>
    ///     请求方式
    /// </summary>
    public HttpMethod? Method { get; }

    /// <summary>
    ///     跟踪标识
    /// </summary>
    /// <remarks>
    ///     <para>可为每个请求指定唯一标识符，用于请求的跟踪和调试。</para>
    ///     <para>唯一标识符将在 <see cref="HttpRequestMessage" /> 类型实例的 <c>Headers</c> 属性中通过 <c>X-Trace-ID</c> 作为键指定。</para>
    /// </remarks>
    public string? TraceIdentifier { get; private set; }

    /// <summary>
    ///     内容类型
    /// </summary>
    public string? ContentType { get; private set; }

    /// <summary>
    ///     内容编码
    /// </summary>
    /// <remarks>默认值为 <c>utf-8</c> 编码。</remarks>
    public Encoding? ContentEncoding { get; private set; } = Encoding.UTF8;

    /// <summary>
    ///     原始请求内容
    /// </summary>
    /// <remarks>此属性值最终将转换为 <see cref="HttpContent" /> 类型实例。</remarks>
    public object? RawContent { get; private set; }

    /// <summary>
    ///     请求标头集合
    /// </summary>
    public IDictionary<string, List<string?>>? Headers { get; private set; }

    /// <summary>
    ///     需要从请求中移除的标头集合
    /// </summary>
    public HashSet<string>? HeadersToRemove { get; private set; }

    /// <summary>
    ///     片段标识符
    /// </summary>
    /// <remarks>请求地址中的 <c>#</c> 符号后面的部分。</remarks>
    public string? Fragment { get; private set; }

    /// <summary>
    ///     超时时间
    /// </summary>
    /// <remarks>可为单次请求设置超时时间。</remarks>
    public TimeSpan? Timeout { get; private set; }

    /// <summary>
    ///     查询参数集合
    /// </summary>
    /// <remarks>请求地址中位于 <c>?</c> 符号之后且 <c>#</c> 符号之前的部分。</remarks>
    public IDictionary<string, List<string?>>? QueryParameters { get; private set; }

    /// <summary>
    ///     路径参数集合
    /// </summary>
    /// <remarks>用于替换请求地址中符合 <c>\{\s*(\w+\s*(\.\s*\w+\s*)*)\s*\}</c> 正则表达式匹配的数据。</remarks>
    public IDictionary<string, string?>? PathParameters { get; private set; }

    /// <summary>
    ///     路径参数集合
    /// </summary>
    /// <remarks>支持自定义类类型。用于替换请求地址中符合 <c>\{\s*(\w+\s*(\.\s*\w+\s*)*)\s*\}</c> 正则表达式匹配的数据。</remarks>
    public IDictionary<string, object>? ObjectPathParameters { get; private set; }

    /// <summary>
    ///     Cookies 集合
    /// </summary>
    /// <remarks>
    ///     <para>可为单次请求设置 Cookies。</para>
    ///     <para>Cookies 将在 <see cref="HttpRequestMessage" /> 类型实例的 <c>Headers</c> 属性中通过 <c>Cookie</c> 作为键指定。</para>
    ///     <para>使用该方式不会自动处理服务器返回的 <c>Set-Cookie</c> 头。</para>
    /// </remarks>
    public IDictionary<string, string?>? Cookies { get; private set; }

    /// <summary>
    ///     <see cref="HttpClient" /> 实例的配置名称。
    /// </summary>
    /// <remarks>
    ///     <para>此属性用于指定 <see cref="IHttpClientFactory" /> 创建 <see cref="HttpClient" /> 实例时传递的名称。</para>
    ///     <para>该名称用于标识在服务容器中与特定 <see cref="HttpClient" /> 实例相关的配置。</para>
    /// </remarks>
    public string? HttpClientFactoryName { get; private set; }

    /// <summary>
    ///     <see cref="HttpClient" /> 实例提供器
    /// </summary>
    /// <value>
    ///     <para>返回一个包含 <see cref="HttpClient" /> 实例及其释放方法的委托。</para>
    ///     <para>释放方法的委托用于在不再需要 <see cref="HttpClient" /> 实例时释放资源。</para>
    /// </value>
    public Func<(HttpClient Instance, Action<HttpClient>? Release)>? HttpClientProvider { get; private set; }

    /// <summary>
    ///     <see cref="IHttpContentProcessor" /> 集合提供器
    /// </summary>
    /// <value>返回多个包含实现 <see cref="IHttpContentProcessor" /> 集合的集合。</value>
    public IList<Func<IEnumerable<IHttpContentProcessor>>>? HttpContentProcessorProviders { get; private set; }

    /// <summary>
    ///     <see cref="IHttpContentConverter" /> 集合提供器
    /// </summary>
    /// <value>返回多个包含实现 <see cref="IHttpContentConverter" /> 集合的集合。</value>
    public IList<Func<IEnumerable<IHttpContentConverter>>>? HttpContentConverterProviders { get; private set; }

    /// <summary>
    ///     用于处理在设置 <see cref="HttpRequestMessage" /> 的请求消息的内容时的操作
    /// </summary>
    public Action<HttpContent?>? OnPreSetContent { get; private set; }

    /// <summary>
    ///     用于处理在发送 HTTP 请求之前的操作
    /// </summary>
    public Action<HttpRequestMessage>? OnPreSendRequest { get; private set; }

    /// <summary>
    ///     用于处理在发送 HTTP 请求之后的操作
    /// </summary>
    public Action<HttpResponseMessage>? OnPostSendRequest { get; private set; }

    /// <summary>
    ///     用于处理在发送 HTTP 请求发生异常时的操作
    /// </summary>
    public Action<Exception, HttpResponseMessage?>? OnSendRequestFailed { get; private set; }

    /// <summary>
    ///     身份验证凭据请求授权标头
    /// </summary>
    /// <remarks>可为单次请求设置身份验证凭据请求授权标头。</remarks>
    public AuthenticationHeaderValue? AuthenticationHeader { get; private set; }

    /// <summary>
    ///     <inheritdoc cref="HttpMultipartFormDataBuilder" />
    /// </summary>
    internal HttpMultipartFormDataBuilder? MultipartFormDataBuilder { get; private set; }

    /// <summary>
    ///     如果 HTTP 响应的 <c>IsSuccessStatusCode</c> 属性是 <c>false</c>，则引发异常。
    /// </summary>
    /// <remarks>默认值为 <c>false</c>。</remarks>
    internal bool EnsureSuccessStatusCodeEnabled { get; private set; }

    /// <summary>
    ///     是否禁用 HTTP 缓存
    /// </summary>
    /// <remarks>可为单次请求设置禁用 HTTP 缓存。</remarks>
    internal bool DisableCacheEnabled { get; private set; }

    /// <summary>
    ///     实现 <see cref="IHttpRequestEventHandler" /> 的类型
    /// </summary>
    internal Type? RequestEventHandlerType { get; private set; }

    /// <summary>
    ///     用于请求结束时需要释放的对象集合
    /// </summary>
    internal List<IDisposable>? Disposables { get; private set; }

    /// <summary>
    ///     <see cref="HttpClient" /> 实例管理器
    /// </summary>
    internal HttpClientPooling? HttpClientPooling { get; set; }

    /// <summary>
    ///     是否启用 <see cref="HttpClient" /> 的池化管理
    /// </summary>
    internal bool HttpClientPoolingEnabled { get; private set; }

    /// <summary>
    ///     是否启用请求分析工具
    /// </summary>
    internal bool ProfilerEnabled { get; private set; }

    /// <summary>
    ///     状态码处理程序
    /// </summary>
    internal IDictionary<IEnumerable<int>, Func<HttpResponseMessage, CancellationToken, Task>>? StatusCodeHandlers
    {
        get;
        private set;
    }
}