// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Text;

namespace Furion.HttpRemote;

/// <inheritdoc cref="IHttpContentProcessorFactory" />
internal sealed class HttpContentProcessorFactory : IHttpContentProcessorFactory
{
    /// <summary>
    ///     <see cref="IHttpContentProcessor" /> 字典集合
    /// </summary>
    internal readonly Dictionary<Type, IHttpContentProcessor> _processors;

    /// <summary>
    ///     <inheritdoc cref="HttpContentProcessorFactory" />
    /// </summary>
    /// <param name="processors"><see cref="IHttpContentProcessor" /> 数组</param>
    public HttpContentProcessorFactory(IHttpContentProcessor[]? processors)
    {
        // 初始化请求内容处理器
        _processors = new Dictionary<Type, IHttpContentProcessor>
        {
            [typeof(StringContentProcessor)] = new StringContentProcessor(),
            [typeof(FormUrlEncodedContentProcessor)] = new FormUrlEncodedContentProcessor(),
            [typeof(ByteArrayContentProcessor)] = new ByteArrayContentProcessor(),
            [typeof(StreamContentProcessor)] = new StreamContentProcessor(),
            [typeof(MultipartFormDataContentProcessor)] = new MultipartFormDataContentProcessor()
        };

        // 添加自定义 IHttpContentProcessor 数组
        _processors.TryAdd(processors, value => value.GetType());
    }

    /// <inheritdoc />
    public IHttpContentProcessor GetProcessor(object? rawContent, string contentType,
        params IHttpContentProcessor[]? processors)
    {
        // 初始化新的 IHttpContentProcessor 字典集合
        var unionProcessors = new Dictionary<Type, IHttpContentProcessor>(_processors);

        // 添加自定义 IHttpContentProcessor 数组
        unionProcessors.TryAdd(processors, value => value.GetType());

        // 查找可以处理指定内容类型或数据类型的 IHttpContentProcessor 实例
        return unionProcessors.Values.LastOrDefault(u => u.CanProcess(rawContent, contentType)) ??
               throw new InvalidOperationException(
                   $"No processor found that can handle the content type `{contentType}` and the provided raw content of type `{rawContent?.GetType()}`. " +
                   "Please ensure that the correct content type is specified and that a suitable processor is registered.");
    }

    /// <inheritdoc />
    public HttpContent? BuildHttpContent(object? rawContent, string contentType, Encoding? encoding = null,
        params IHttpContentProcessor[]? processors)
    {
        // 查找可以处理指定内容类型或数据类型的 IHttpContentProcessor 实例
        var httpContentProcessor = GetProcessor(rawContent, contentType, processors);

        // 将原始内容转换为 HttpContent 实例
        return httpContentProcessor.Process(rawContent, contentType, encoding ?? Encoding.UTF8);
    }
}