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
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Furion.HttpRemote;

/// <summary>
///     <inheritdoc cref="IHttpRemoteService" />
/// </summary>
internal sealed partial class HttpRemoteService : IHttpRemoteService
{
    /// <inheritdoc cref="IHttpClientFactory" />
    internal readonly IHttpClientFactory _httpClientFactory;

    /// <inheritdoc cref="IHttpContentConverterFactory" />
    internal readonly IHttpContentConverterFactory _httpContentConverterFactory;

    /// <inheritdoc cref="IHttpContentProcessorFactory" />
    internal readonly IHttpContentProcessorFactory _httpContentProcessorFactory;

    /// <inheritdoc cref="ILogger{T}" />
    internal readonly ILogger<Logging> _logger;

    /// <summary>
    ///     <inheritdoc cref="HttpRemoteService" />
    /// </summary>
    /// <param name="serviceProvider">
    ///     <see cref="IServiceProvider" />
    /// </param>
    /// <param name="logger">
    ///     <see cref="Logger{T}" />
    /// </param>
    /// <param name="httpClientFactory">
    ///     <see cref="IHttpClientFactory" />
    /// </param>
    /// <param name="httpContentProcessorFactory">
    ///     <see cref="IHttpContentProcessorFactory" />
    /// </param>
    /// <param name="httpContentConverterFactory">
    ///     <see cref="IHttpContentConverterFactory" />
    /// </param>
    /// <param name="httpRemoteOptions">
    ///     <see cref="HttpRemoteOptions" />
    /// </param>
    public HttpRemoteService(IServiceProvider serviceProvider, ILogger<Logging> logger,
        IHttpClientFactory httpClientFactory,
        IHttpContentProcessorFactory httpContentProcessorFactory,
        IHttpContentConverterFactory httpContentConverterFactory, HttpRemoteOptions httpRemoteOptions)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        ArgumentNullException.ThrowIfNull(httpContentProcessorFactory);
        ArgumentNullException.ThrowIfNull(httpContentConverterFactory);
        ArgumentNullException.ThrowIfNull(httpRemoteOptions);

        ServiceProvider = serviceProvider;
        RemoteOptions = httpRemoteOptions;

        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _httpContentProcessorFactory = httpContentProcessorFactory;
        _httpContentConverterFactory = httpContentConverterFactory;
    }

    /// <inheritdoc />
    public HttpRemoteOptions RemoteOptions { get; }

    /// <inheritdoc />
    public IServiceProvider ServiceProvider { get; }

    /// <inheritdoc />
    public HttpResponseMessage Send(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default) =>
        Send(httpRequestBuilder, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpResponseMessage Send(HttpRequestBuilder httpRequestBuilder, HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default)
    {
        // 发送 HTTP 远程请求
        var (httpResponseMessage, _) = SendCoreAsync(httpRequestBuilder, completionOption, default,
            (httpClient, httpRequestMessage, option, token) =>
                httpClient.Send(httpRequestMessage, option, token), cancellationToken).GetAwaiter().GetResult();

        return httpResponseMessage;
    }

    /// <inheritdoc />
    public Task<HttpResponseMessage> SendAsync(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default) =>
        SendAsync(httpRequestBuilder, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendAsync(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default)
    {
        // 发送 HTTP 远程请求
        var (httpResponseMessage, _) = await SendCoreAsync(httpRequestBuilder, completionOption,
            (httpClient, httpRequestMessage, option, token) =>
                httpClient.SendAsync(httpRequestMessage, option, token), default, cancellationToken);

        return httpResponseMessage;
    }

    /// <inheritdoc />
    public TResult? SendAs<TResult>(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default) => SendAs<TResult>(httpRequestBuilder,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public TResult? SendAs<TResult>(HttpRequestBuilder httpRequestBuilder, HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default)
    {
        // 发送 HTTP 远程请求
        var httpResponseMessage = Send(httpRequestBuilder, completionOption, cancellationToken);

        // 将 HttpResponseMessage 转换为 TResult 实例
        return _httpContentConverterFactory.Read<TResult>(httpResponseMessage,
            httpRequestBuilder.HttpContentConverterProviders?.SelectMany(u => u.Invoke()).ToArray(),
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<TResult?> SendAsAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default) => SendAsAsync<TResult>(httpRequestBuilder,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public async Task<TResult?> SendAsAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default)
    {
        // 发送 HTTP 远程请求
        var httpResponseMessage = await SendAsync(httpRequestBuilder, completionOption, cancellationToken);

        // 将 HttpResponseMessage 转换为 TResult 实例
        return await _httpContentConverterFactory.ReadAsync<TResult>(httpResponseMessage,
            httpRequestBuilder.HttpContentConverterProviders?.SelectMany(u => u.Invoke()).ToArray(),
            cancellationToken);
    }

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Send<TResult>(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default) =>
        Send<TResult>(httpRequestBuilder, HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public HttpRemoteResult<TResult> Send<TResult>(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption, CancellationToken cancellationToken = default)
    {
        // 发送 HTTP 远程请求
        var (httpResponseMessage, requestDuration) = SendCoreAsync(httpRequestBuilder, completionOption, default,
            (httpClient, httpRequestMessage, option, token) =>
                httpClient.Send(httpRequestMessage, option, token), cancellationToken).GetAwaiter().GetResult();

        // 将 HttpResponseMessage 转换为 TResult 实例
        var result = _httpContentConverterFactory.Read<TResult>(httpResponseMessage,
            httpRequestBuilder.HttpContentConverterProviders?.SelectMany(u => u.Invoke()).ToArray(), cancellationToken);

        // 初始化 HttpRemoteResult 实例
        var httpRemoteResult = new HttpRemoteResult<TResult>(httpResponseMessage)
        {
            Result = result,
            RequestDuration = requestDuration
        };

        return httpRemoteResult;
    }

    /// <inheritdoc />
    public Task<HttpRemoteResult<TResult>> SendAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        CancellationToken cancellationToken = default) => SendAsync<TResult>(httpRequestBuilder,
        HttpCompletionOption.ResponseContentRead, cancellationToken);

    /// <inheritdoc />
    public async Task<HttpRemoteResult<TResult>> SendAsync<TResult>(HttpRequestBuilder httpRequestBuilder,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default)
    {
        // 发送 HTTP 远程请求
        var (httpResponseMessage, requestDuration) = await SendCoreAsync(httpRequestBuilder, completionOption,
            (httpClient, httpRequestMessage, option, token) =>
                httpClient.SendAsync(httpRequestMessage, option, token), default, cancellationToken);

        // 将 HttpResponseMessage 转换为 TResult 实例
        var result = await _httpContentConverterFactory.ReadAsync<TResult>(httpResponseMessage,
            httpRequestBuilder.HttpContentConverterProviders?.SelectMany(u => u.Invoke()).ToArray(), cancellationToken);

        // 初始化 HttpRemoteResult 实例
        var httpRemoteResult = new HttpRemoteResult<TResult>(httpResponseMessage)
        {
            Result = result,
            RequestDuration = requestDuration
        };

        return httpRemoteResult;
    }

    /// <summary>
    ///     发送 HTTP 远程请求并处理 <see cref="HttpResponseMessage" /> 实例
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="sendAsyncMethod">异步发送请求的委托</param>
    /// <param name="sendMethod">同步发送请求的委托</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Tuple{T1, T2}" />
    /// </returns>
    internal async Task<(HttpResponseMessage ResponseMessage, long RequestDuration)> SendCoreAsync(
        HttpRequestBuilder httpRequestBuilder, HttpCompletionOption completionOption,
        Func<HttpClient, HttpRequestMessage, HttpCompletionOption, CancellationToken, Task<HttpResponseMessage>>?
            sendAsyncMethod,
        Func<HttpClient, HttpRequestMessage, HttpCompletionOption, CancellationToken, HttpResponseMessage>? sendMethod,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRequestBuilder);

        // 空检查
        if (sendAsyncMethod is null && sendMethod is null)
        {
            throw new InvalidOperationException("Both `sendAsyncMethod` and `sendMethod` cannot be null.");
        }

        // 解析 IHttpRequestEventHandler 事件处理程序
        var requestEventHandler =
            (httpRequestBuilder.RequestEventHandlerType is not null
                ? ServiceProvider.GetService(httpRequestBuilder.RequestEventHandlerType)
                : null) as IHttpRequestEventHandler;

        // 创建带有默认值的 HttpClient 实例
        var httpClientPooling = CreateHttpClientWithDefaults(httpRequestBuilder);
        var httpClient = httpClientPooling.Instance;

        // 构建 HttpRequestMessage 实例
        var httpRequestMessage =
            httpRequestBuilder.Build(RemoteOptions, _httpContentProcessorFactory, httpClient.BaseAddress);

        // 处理发送请求之前
        HandlePreSendRequest(httpRequestBuilder, requestEventHandler, httpRequestMessage);

        // 检查是否启用请求分析工具
        if (httpRequestBuilder.ProfilerEnabled)
        {
            ProfilerDelegatingHandler.LogRequestHeaders(_logger, httpRequestMessage);
        }

        // 创建关联的超时 Token 标识
        using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 设置单次请求超时时间控制
        if (httpRequestBuilder.Timeout is not null && httpRequestBuilder.Timeout.Value != TimeSpan.Zero)
        {
            cancellationTokenSource.CancelAfter(httpRequestBuilder.Timeout.Value);
        }

        HttpResponseMessage? httpResponseMessage = null;

        // 初始化 Stopwatch 实例并开启计时操作
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // 调用发送 HTTP 请求委托
            httpResponseMessage = sendAsyncMethod is not null
                ? await sendAsyncMethod(httpClient, httpRequestMessage, completionOption, cancellationTokenSource.Token)
                : sendMethod!(httpClient, httpRequestMessage, completionOption, cancellationTokenSource.Token);

            // 获取请求耗时
            var requestDuration = stopwatch.ElapsedMilliseconds;

            // 调用状态码处理程序
            if (sendAsyncMethod is not null)
            {
                await InvokeStatusCodeHandlersAsync(httpRequestBuilder, httpResponseMessage,
                    cancellationTokenSource.Token);
            }
            else
            {
                // ReSharper disable once MethodHasAsyncOverload
                InvokeStatusCodeHandlers(httpRequestBuilder, httpResponseMessage, cancellationTokenSource.Token);
            }

            // 检查是否启用请求分析工具
            if (httpRequestBuilder.ProfilerEnabled)
            {
                ProfilerDelegatingHandler.LogResponseHeadersAndSummary(_logger, httpResponseMessage, requestDuration);
            }

            // 如果 HTTP 响应的 IsSuccessStatusCode 属性是 false，则引发异常
            if (httpRequestBuilder.EnsureSuccessStatusCodeEnabled)
            {
                httpResponseMessage.EnsureSuccessStatusCode();
            }

            return (httpResponseMessage, requestDuration);
        }
        catch (Exception e)
        {
            // 处理发送请求发送异常
            HandleSendRequestFailed(httpRequestBuilder, requestEventHandler, e, httpResponseMessage);

            throw;
        }
        finally
        {
            // 停止计时
            stopwatch.Stop();

            // 处理发送请求之后
            HandlePostSendRequest(httpRequestBuilder, requestEventHandler, httpResponseMessage);

            // 释放资源集合
            if (!httpRequestBuilder.HttpClientPoolingEnabled)
            {
                httpRequestBuilder.ReleaseResources();
            }
        }
    }

    /// <summary>
    ///     处理发送请求之前
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="requestEventHandler">
    ///     <see cref="IHttpRequestEventHandler" />
    /// </param>
    /// <param name="httpRequestMessage">
    ///     <see cref="HttpRequestMessage" />
    /// </param>
    internal static void HandlePreSendRequest(HttpRequestBuilder httpRequestBuilder,
        IHttpRequestEventHandler? requestEventHandler, HttpRequestMessage httpRequestMessage)
    {
        // 空检查
        if (requestEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(requestEventHandler.OnPreSendRequest, httpRequestMessage);
        }

        httpRequestBuilder.OnPreSendRequest.TryInvoke(httpRequestMessage);
    }

    /// <summary>
    ///     处理发送请求之后
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="requestEventHandler">
    ///     <see cref="IHttpRequestEventHandler" />
    /// </param>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    internal static void HandlePostSendRequest(HttpRequestBuilder httpRequestBuilder,
        IHttpRequestEventHandler? requestEventHandler, HttpResponseMessage? httpResponseMessage)
    {
        // 空检查
        if (httpResponseMessage is null)
        {
            return;
        }

        // 空检查
        if (requestEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(requestEventHandler.OnPostSendRequest, httpResponseMessage);
        }

        httpRequestBuilder.OnPostSendRequest.TryInvoke(httpResponseMessage);
    }

    /// <summary>
    ///     处理发送请求发送异常
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="requestEventHandler">
    ///     <see cref="IHttpRequestEventHandler" />
    /// </param>
    /// <param name="e">
    ///     <see cref="Exception" />
    /// </param>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    internal static void HandleSendRequestFailed(HttpRequestBuilder httpRequestBuilder,
        IHttpRequestEventHandler? requestEventHandler, Exception e, HttpResponseMessage? httpResponseMessage)
    {
        // 空检查
        if (requestEventHandler is not null)
        {
            DelegateExtensions.TryInvoke(requestEventHandler.OnSendRequestFailed, e, httpResponseMessage);
        }

        httpRequestBuilder.OnSendRequestFailed.TryInvoke(e, httpResponseMessage);
    }

    /// <summary>
    ///     创建带有默认值的 <see cref="HttpClient" /> 实例
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpClientPooling" />
    /// </returns>
    internal HttpClientPooling CreateHttpClientWithDefaults(HttpRequestBuilder httpRequestBuilder)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRequestBuilder);

        // 检查是否已经存在 HttpClientPooling 实例
        if (httpRequestBuilder.HttpClientPooling is not null)
        {
            return httpRequestBuilder.HttpClientPooling;
        }

        // 使用锁确保线程安全
        lock (httpRequestBuilder)
        {
            return httpRequestBuilder.HttpClientPooling ?? CreateHttpClientPooling(httpRequestBuilder);
        }
    }

    /// <summary>
    ///     创建 <see cref="HttpClient" /> 实例管理器
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpClientPooling" />
    /// </returns>
    internal HttpClientPooling CreateHttpClientPooling(HttpRequestBuilder httpRequestBuilder)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRequestBuilder);

        Action<HttpClient>? release = null;
        HttpClient httpClient;

        // 检查是否设置了 HttpClient 实例提供器
        if (httpRequestBuilder.HttpClientProvider is null)
        {
            httpClient = string.IsNullOrWhiteSpace(httpRequestBuilder.HttpClientFactoryName)
                ? _httpClientFactory.CreateClient()
                : _httpClientFactory.CreateClient(httpRequestBuilder.HttpClientFactoryName);
        }
        else
        {
            // 调用 HttpClient 实例提供器
            var provider = httpRequestBuilder.HttpClientProvider();
            httpClient = provider.Instance;
            release = provider.Release;
        }

        // 空检查
        ArgumentNullException.ThrowIfNull(httpClient);

        // 添加默认的 User-Agent 标头
        AddDefaultUserAgentHeader(httpClient);

        // 存储 HttpClientPooling 实例并返回
        return httpRequestBuilder.HttpClientPooling = new HttpClientPooling(httpClient, release);
    }

    /// <summary>
    ///     向 <see cref="HttpClient" /> 添加默认的 <c>User-Agent</c> 标头
    /// </summary>
    /// <remarks>解决某些服务器可能需要这个头部信息才能正确响应请求。</remarks>
    /// <param name="httpClient">
    ///     <see cref="HttpClient" />
    /// </param>
    internal static void AddDefaultUserAgentHeader(HttpClient httpClient)
    {
        // 空检查
        if (httpClient.DefaultRequestHeaders.UserAgent.Count != 0)
        {
            return;
        }

        // User-Agent 默认格式为：程序集名称/程序集版本号
        httpClient.DefaultRequestHeaders.UserAgent.Add(typeof(HttpRemoteService).Assembly.ConvertTo(ass =>
            new ProductInfoHeaderValue(ass.GetName().Name!,
                ass.GetVersion()?.ToString() ?? Constants.UNKNOWN_USER_AGENT_VERSION)));
    }

    /// <summary>
    ///     调用状态码处理程序
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal static void InvokeStatusCodeHandlers(HttpRequestBuilder httpRequestBuilder,
        HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRequestBuilder);
        ArgumentNullException.ThrowIfNull(httpResponseMessage);

        // 后台线程中启动异步任务
        Task.Run(
            async () => await InvokeStatusCodeHandlersAsync(httpRequestBuilder, httpResponseMessage, cancellationToken),
            cancellationToken);
    }

    /// <summary>
    ///     调用状态码处理程序
    /// </summary>
    /// <param name="httpRequestBuilder">
    ///     <see cref="HttpRequestBuilder" />
    /// </param>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    internal static async Task InvokeStatusCodeHandlersAsync(HttpRequestBuilder httpRequestBuilder,
        HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRequestBuilder);
        ArgumentNullException.ThrowIfNull(httpResponseMessage);

        // 空检查
        if (httpRequestBuilder.StatusCodeHandlers.IsNullOrEmpty())
        {
            return;
        }

        // 获取响应状态码
        var statusCode = (int)httpResponseMessage.StatusCode;

        // 查找响应状态码所有处理程序
        var statusCodeHandlers = httpRequestBuilder.StatusCodeHandlers.Where(u => u.Key.Contains(statusCode))
            .Select(u => u.Value).ToList();

        // 空检查
        if (statusCodeHandlers.Count == 0)
        {
            return;
        }

        // 并行执行所有的处理程序，并等待所有任务完成
        await Task.WhenAll(statusCodeHandlers.Select(handler =>
            handler.TryInvokeAsync(httpResponseMessage, cancellationToken)));
    }
}