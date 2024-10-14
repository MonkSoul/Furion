// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.HttpRemote.Extensions;

/// <summary>
///     <see cref="HttpContext" /> 拓展类
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    ///     转发时徐忽略的响应标头数组
    /// </summary>
    /// <remarks>解决响应标头包含 <c>Transfer-Encoding: chunked</c> 导致进入响应一直处理等待中问题。</remarks>
    internal static string[] _ignoreResponseHeaders = ["Transfer-Encoding"];

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static HttpResponseMessage Forward(this HttpContext? httpContext, string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        Forward(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static HttpResponseMessage Forward(this HttpContext? httpContext, HttpMethod httpMethod,
        string? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        Forward(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static HttpResponseMessage Forward(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        Forward(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri,
            configure, completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static HttpResponseMessage Forward(this HttpContext? httpContext, HttpMethod httpMethod,
        Uri? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(httpMethod);

        // 创建 HttpRequestBuilder 实例
        var httpRequestBuilder = CreateRequestBuilder(httpContext, httpMethod, requestUri, configure);

        // 获取 IHttpRemoteService 实例
        var httpRemoteService = httpContext.RequestServices.GetRequiredService<IHttpRemoteService>();

        // 发送 HTTP 远程请求
        var httpResponseMessage =
            httpRemoteService.Send(httpRequestBuilder, completionOption, httpContext.RequestAborted);

        // 根据配置选项将 HttpResponseMessage 信息转发到 HttpContext 中
        ForwardResponseMessageToContext(httpContext, httpResponseMessage, forwardOptions);

        return httpResponseMessage;
    }

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static Task<HttpResponseMessage> ForwardAsync(this HttpContext? httpContext,
        string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsync(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static Task<HttpResponseMessage> ForwardAsync(this HttpContext? httpContext,
        HttpMethod httpMethod,
        string? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsync(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static Task<HttpResponseMessage> ForwardAsync(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsync(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri,
            configure, completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <returns>
    ///     <see cref="HttpResponseMessage" />
    /// </returns>
    public static async Task<HttpResponseMessage> ForwardAsync(this HttpContext? httpContext,
        HttpMethod httpMethod,
        Uri? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(httpMethod);

        // 创建 HttpRequestBuilder 实例
        var httpRequestBuilder = await CreateRequestBuilderAsync(httpContext, httpMethod, requestUri, configure);

        // 获取 IHttpRemoteService 实例
        var httpRemoteService = httpContext.RequestServices.GetRequiredService<IHttpRemoteService>();

        // 发送 HTTP 远程请求
        var httpResponseMessage =
            await httpRemoteService.SendAsync(httpRequestBuilder, completionOption,
                httpContext.RequestAborted);

        // 根据配置选项将 HttpResponseMessage 信息转发到 HttpContext 中
        ForwardResponseMessageToContext(httpContext, httpResponseMessage, forwardOptions);

        return httpResponseMessage;
    }

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static TResult? ForwardAs<TResult>(this HttpContext? httpContext, string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAs<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static TResult? ForwardAs<TResult>(this HttpContext? httpContext, HttpMethod httpMethod,
        string? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAs<TResult>(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static TResult? ForwardAs<TResult>(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAs<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri,
            configure, completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static TResult? ForwardAs<TResult>(this HttpContext? httpContext, HttpMethod httpMethod,
        Uri? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(httpMethod);

        // 创建 HttpRequestBuilder 实例
        var httpRequestBuilder = CreateRequestBuilder(httpContext, httpMethod, requestUri, configure);

        // 获取 IHttpRemoteService 实例
        var httpRemoteService = httpContext.RequestServices.GetRequiredService<IHttpRemoteService>();

        // 获取 IHttpContentConverterFactory 实例
        var httpContentConverterFactory =
            httpContext.RequestServices.GetRequiredService<IHttpContentConverterFactory>();

        // 发送 HTTP 远程请求
        var httpResponseMessage =
            httpRemoteService.Send(httpRequestBuilder, completionOption, httpContext.RequestAborted);

        // 根据配置选项将 HttpResponseMessage 信息转发到 HttpContext 中
        ForwardResponseMessageToContext(httpContext, httpResponseMessage, forwardOptions);

        // 将 HttpResponseMessage 转换为 TResult 实例
        return httpContentConverterFactory.Read<TResult>(httpResponseMessage,
            httpRequestBuilder.HttpContentConverterProviders?.SelectMany(u => u.Invoke()).ToArray(),
            httpContext.RequestAborted);
    }

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static Task<TResult?> ForwardAsAsync<TResult>(this HttpContext? httpContext, string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsAsync<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static Task<TResult?> ForwardAsAsync<TResult>(this HttpContext? httpContext, HttpMethod httpMethod,
        string? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsAsync<TResult>(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static Task<TResult?> ForwardAsAsync<TResult>(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsAsync<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri,
            configure, completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    public static async Task<TResult?> ForwardAsAsync<TResult>(this HttpContext? httpContext, HttpMethod httpMethod,
        Uri? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(httpMethod);

        // 创建 HttpRequestBuilder 实例
        var httpRequestBuilder = await CreateRequestBuilderAsync(httpContext, httpMethod, requestUri, configure);

        // 获取 IHttpRemoteService 实例
        var httpRemoteService = httpContext.RequestServices.GetRequiredService<IHttpRemoteService>();

        // 获取 IHttpContentConverterFactory 实例
        var httpContentConverterFactory =
            httpContext.RequestServices.GetRequiredService<IHttpContentConverterFactory>();

        // 发送 HTTP 远程请求
        var httpResponseMessage =
            await httpRemoteService.SendAsync(httpRequestBuilder, completionOption, httpContext.RequestAborted);

        // 根据配置选项将 HttpResponseMessage 信息转发到 HttpContext 中
        ForwardResponseMessageToContext(httpContext, httpResponseMessage, forwardOptions);

        // 将 HttpResponseMessage 转换为 TResult 实例
        return await httpContentConverterFactory.ReadAsync<TResult>(httpResponseMessage,
            httpRequestBuilder.HttpContentConverterProviders?.SelectMany(u => u.Invoke()).ToArray(),
            httpContext.RequestAborted);
    }

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static HttpRemoteResult<TResult> Forward<TResult>(this HttpContext? httpContext, string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        Forward<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static HttpRemoteResult<TResult> Forward<TResult>(this HttpContext? httpContext, HttpMethod httpMethod,
        string? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        Forward<TResult>(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static HttpRemoteResult<TResult> Forward<TResult>(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        Forward<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri,
            configure, completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static HttpRemoteResult<TResult> Forward<TResult>(this HttpContext? httpContext, HttpMethod httpMethod,
        Uri? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(httpMethod);

        // 创建 HttpRequestBuilder 实例
        var httpRequestBuilder = CreateRequestBuilder(httpContext, httpMethod, requestUri, configure);

        // 获取 IHttpRemoteService 实例
        var httpRemoteService = httpContext.RequestServices.GetRequiredService<IHttpRemoteService>();

        // 发送 HTTP 远程请求
        var result = httpRemoteService.Send<TResult>(httpRequestBuilder, completionOption, httpContext.RequestAborted);

        // 根据配置选项将 HttpResponseMessage 信息转发到 HttpContext 中
        ForwardResponseMessageToContext(httpContext, result.ResponseMessage, forwardOptions);

        return result;
    }

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static Task<HttpRemoteResult<TResult>> ForwardAsync<TResult>(this HttpContext? httpContext,
        string? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsync<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static Task<HttpRemoteResult<TResult>> ForwardAsync<TResult>(this HttpContext? httpContext,
        HttpMethod httpMethod,
        string? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsync<TResult>(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure,
            completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static Task<HttpRemoteResult<TResult>> ForwardAsync<TResult>(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null) =>
        ForwardAsync<TResult>(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri,
            configure, completionOption, forwardOptions);

    /// <summary>
    ///     转发 <see cref="HttpContext" /> 到新的 HTTP 远程地址
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <param name="completionOption">
    ///     <see cref="HttpCompletionOption" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="HttpRemoteResult{TResult}" />
    /// </returns>
    public static async Task<HttpRemoteResult<TResult>> ForwardAsync<TResult>(this HttpContext? httpContext,
        HttpMethod httpMethod,
        Uri? requestUri, Action<HttpRequestBuilder>? configure = null,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        HttpContextForwardOptions? forwardOptions = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(httpMethod);

        // 创建 HttpRequestBuilder 实例
        var httpRequestBuilder = await CreateRequestBuilderAsync(httpContext, httpMethod, requestUri, configure);

        // 获取 IHttpRemoteService 实例
        var httpRemoteService = httpContext.RequestServices.GetRequiredService<IHttpRemoteService>();

        // 发送 HTTP 远程请求
        var result =
            await httpRemoteService.SendAsync<TResult>(httpRequestBuilder, completionOption,
                httpContext.RequestAborted);

        // 根据配置选项将 HttpResponseMessage 信息转发到 HttpContext 中
        ForwardResponseMessageToContext(httpContext, result.ResponseMessage, forwardOptions);

        return result;
    }

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder CreateRequestBuilder(this HttpContext? httpContext, string? requestUri,
        Action<HttpRequestBuilder>? configure = null) =>
        CreateRequestBuilder(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute),
            configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder CreateRequestBuilder(this HttpContext? httpContext,
        HttpMethod httpMethod, string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        CreateRequestBuilder(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder CreateRequestBuilder(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null) =>
        CreateRequestBuilder(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri, configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static HttpRequestBuilder CreateRequestBuilder(this HttpContext? httpContext,
        HttpMethod httpMethod, Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        new HttpContextForwardBuilder(httpMethod, requestUri, httpContext).Build(configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static Task<HttpRequestBuilder> CreateRequestBuilderAsync(this HttpContext? httpContext, string? requestUri,
        Action<HttpRequestBuilder>? configure = null) =>
        CreateRequestBuilderAsync(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method),
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute),
            configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static Task<HttpRequestBuilder> CreateRequestBuilderAsync(this HttpContext? httpContext,
        HttpMethod httpMethod, string? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        CreateRequestBuilderAsync(httpContext, httpMethod,
            string.IsNullOrWhiteSpace(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute), configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static Task<HttpRequestBuilder> CreateRequestBuilderAsync(this HttpContext? httpContext, Uri? requestUri,
        Action<HttpRequestBuilder>? configure = null) =>
        CreateRequestBuilderAsync(httpContext, Helpers.ParseHttpMethod(httpContext?.Request.Method), requestUri,
            configure);

    /// <summary>
    ///     创建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    public static Task<HttpRequestBuilder> CreateRequestBuilderAsync(this HttpContext? httpContext,
        HttpMethod httpMethod, Uri? requestUri, Action<HttpRequestBuilder>? configure = null) =>
        new HttpContextForwardBuilder(httpMethod, requestUri, httpContext).BuildAsync(configure);

    /// <summary>
    ///     根据配置选项将 <see cref="HttpResponseMessage" /> 信息转发到 <see cref="HttpContext" /> 中
    /// </summary>
    /// <param name="httpContext">
    ///     <see cref="HttpContext" />
    /// </param>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="forwardOptions">
    ///     <see cref="HttpContextForwardOptions" />
    /// </param>
    internal static void ForwardResponseMessageToContext(HttpContext httpContext,
        HttpResponseMessage httpResponseMessage,
        HttpContextForwardOptions? forwardOptions)
    {
        // 获取 HttpResponse 实例
        var httpResponse = httpContext.Response;

        // 检查是否配置了响应状态码转发
        if (forwardOptions?.ForwardStatusCode ?? true)
        {
            httpResponse.StatusCode = (int)httpResponseMessage.StatusCode;
        }

        // 检查是否配置了响应标头转发
        if (forwardOptions?.ForwardResponseHeaders ?? true)
        {
            // 逐条更新响应标头
            foreach (var (key, values) in httpResponseMessage.Headers)
            {
                // 忽略特定响应标头
                if (key.IsIn(_ignoreResponseHeaders, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }

                httpResponse.Headers[key] = values.ToArray();
            }
        }

        // 检查是否配置了响应内容 Content-Disposition 标头转发
        if (forwardOptions?.ForwardContentDispositionHeader ?? true)
        {
            // 获取 ContentDisposition 实例
            var contentDisposition = httpResponseMessage.Content.Headers.ContentDisposition;
            httpResponse.Headers.ContentDisposition = contentDisposition?.ToString();
        }

        // 调用用于在转发响应之前执行自定义操作
        forwardOptions?.OnForwarding?.Invoke(httpContext, httpResponseMessage);
    }
}