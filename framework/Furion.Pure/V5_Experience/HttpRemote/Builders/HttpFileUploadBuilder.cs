// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Channels;

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 文件上传构建器
/// </summary>
/// <remarks>使用 <c>HttpRequestBuilder.Upload(requestUri, fileFullName, name)</c> 静态方法创建。</remarks>
public sealed class HttpFileUploadBuilder
{
    /// <summary>
    ///     <inheritdoc cref="HttpFileUploadBuilder" />
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="name">表单名称</param>
    internal HttpFileUploadBuilder(HttpMethod httpMethod, Uri? requestUri, string fileFullName, string name)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpMethod);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileFullName);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Method = httpMethod;
        RequestUri = requestUri;

        FileFullName = fileFullName;
        Name = name;
    }

    /// <summary>
    ///     请求地址
    /// </summary>
    public Uri? RequestUri { get; }

    /// <summary>
    ///     请求方式
    /// </summary>
    public HttpMethod Method { get; }

    /// <summary>
    ///     文件完整路径
    /// </summary>
    public string FileFullName { get; }

    /// <summary>
    ///     表单名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     内容类型
    /// </summary>
    public string? ContentType { get; private set; }

    /// <summary>
    ///     进度更新（通知）的间隔时间
    /// </summary>
    /// <remarks>默认值为 1 秒。</remarks>
    public TimeSpan ProgressInterval { get; private set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    ///     用于处理在文件开始传输时的操作
    /// </summary>
    public Action? OnTransferStarted { get; private set; }

    /// <summary>
    ///     用于处理在文件传输完成时的操作
    /// </summary>
    public Action<long>? OnTransferCompleted { get; private set; }

    /// <summary>
    ///     用于处理在文件传输发生异常时的操作
    /// </summary>
    public Action<Exception>? OnTransferFailed { get; private set; }

    /// <summary>
    ///     用于传输进度发生变化时的操作
    /// </summary>
    public Func<FileTransferProgress, Task>? OnProgressChanged { get; private set; }

    /// <summary>
    ///     实现 <see cref="IHttpFileTransferEventHandler" /> 的类型
    /// </summary>
    internal Type? FileTransferEventHandlerType { get; private set; }

    /// <summary>
    ///     设置内容类型
    /// </summary>
    /// <param name="contentType">内容类型</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public HttpFileUploadBuilder SetContentType(string contentType)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        // 解析内容类型字符串
        var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(contentType);

        ContentType = mediaTypeHeaderValue.MediaType;

        return this;
    }

    /// <summary>
    ///     设置用于上传进度发生变化时执行的委托
    /// </summary>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public HttpFileUploadBuilder SetOnProgressChanged(Func<FileTransferProgress, Task> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        OnProgressChanged = configure;

        return this;
    }

    /// <summary>
    ///     设置进度更新（通知）的间隔时间
    /// </summary>
    /// <param name="progressInterval">进度更新（通知）的间隔时间</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public HttpFileUploadBuilder SetProgressInterval(TimeSpan progressInterval)
    {
        // 小于或等于 0 检查
        if (progressInterval <= TimeSpan.Zero)
        {
            throw new ArgumentException("Progress interval must be greater than 0.", nameof(progressInterval));
        }

        ProgressInterval = progressInterval;

        return this;
    }

    /// <summary>
    ///     设置在文件开始传输时的操作
    /// </summary>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public HttpFileUploadBuilder SetOnTransferStarted(Action configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        OnTransferStarted = configure;

        return this;
    }

    /// <summary>
    ///     设置在文件传输完成时的操作
    /// </summary>
    /// <param name="configure">自定义配置委托；委托参数为文件传输总花费时间（毫秒）</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public HttpFileUploadBuilder SetOnTransferCompleted(Action<long> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        OnTransferCompleted = configure;

        return this;
    }

    /// <summary>
    ///     设置在文件传输发生异常时的操作
    /// </summary>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public HttpFileUploadBuilder SetOnTransferFailed(Action<Exception> configure)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(configure);

        OnTransferFailed = configure;

        return this;
    }

    /// <summary>
    ///     设置 HTTP 文件传输事件处理程序
    /// </summary>
    /// <param name="fileTransferEventHandlerType">实现 <see cref="IHttpFileTransferEventHandler" /> 接口的类型</param>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public HttpFileUploadBuilder SetEventHandler(Type fileTransferEventHandlerType)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(fileTransferEventHandlerType);

        // 检查类型是否实现了 IHttpFileTransferEventHandler 接口
        if (!typeof(IHttpFileTransferEventHandler).IsAssignableFrom(fileTransferEventHandlerType))
        {
            throw new InvalidOperationException(
                $"`{fileTransferEventHandlerType}` type is not assignable from `{typeof(IHttpFileTransferEventHandler)}`.");
        }

        FileTransferEventHandlerType = fileTransferEventHandlerType;

        return this;
    }

    /// <summary>
    ///     设置 HTTP 文件传输事件处理程序
    /// </summary>
    /// <typeparam name="TFileTransferEventHandler">
    ///     <see cref="IHttpFileTransferEventHandler" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="HttpFileUploadBuilder" />
    /// </returns>
    public HttpFileUploadBuilder SetEventHandler<TFileTransferEventHandler>()
        where TFileTransferEventHandler : IHttpFileTransferEventHandler =>
        SetEventHandler(typeof(TFileTransferEventHandler));

    /// <summary>
    ///     构建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpRemoteOptions">
    ///     <see cref="HttpRemoteOptions" />
    /// </param>
    /// <param name="progressChannel">文件传输进度信息的通道</param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    internal HttpRequestBuilder Build(HttpRemoteOptions httpRemoteOptions,
        Channel<FileTransferProgress> progressChannel,
        Action<HttpRequestBuilder>? configure = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteOptions);
        ArgumentNullException.ThrowIfNull(progressChannel);

        // 初始化 HttpRequestBuilder 实例
        var httpRequestBuilder = HttpRequestBuilder.Create(Method, RequestUri, configure).SetMultipartContent(builder =>
            builder.AddProgressFileStream(FileFullName, Name, progressChannel,
                ContentType ?? MediaTypeNames.Application.Octet));

        // 检查是否设置了事件处理程序且该处理程序实现了 IHttpRequestEventHandler 接口，如果有则设置给 httpRequestBuilder
        if (FileTransferEventHandlerType is not null &&
            typeof(IHttpRequestEventHandler).IsAssignableFrom(FileTransferEventHandlerType))
        {
            httpRequestBuilder.SetEventHandler(FileTransferEventHandlerType);
        }

        return httpRequestBuilder;
    }
}