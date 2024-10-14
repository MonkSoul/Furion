// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.HttpRemote;

/// <inheritdoc cref="IHttpContentConverterFactory" />
internal sealed class HttpContentConverterFactory : IHttpContentConverterFactory
{
    /// <summary>
    ///     <see cref="IHttpContentConverter{TResult}" /> 字典集合
    /// </summary>
    internal readonly Dictionary<Type, IHttpContentConverter> _converters;

    /// <inheritdoc cref="IServiceProvider" />
    internal readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     <inheritdoc cref="HttpContentConverterFactory" />
    /// </summary>
    /// <param name="serviceProvider">
    ///     <see cref="IServiceProvider" />
    /// </param>
    /// <param name="converters"><see cref="IHttpContentConverter{TResult}" /> 数组</param>
    public HttpContentConverterFactory(IServiceProvider serviceProvider, IHttpContentConverter[]? converters)
    {
        _serviceProvider = serviceProvider;

        // 初始化响应内容转换器
        _converters = new Dictionary<Type, IHttpContentConverter>
        {
            [typeof(StringContentConverter)] = new StringContentConverter(),
            [typeof(ByteArrayContentConverter)] = new ByteArrayContentConverter(),
            [typeof(StreamContentConverter)] = new StreamContentConverter()
        };

        // 添加自定义 IHttpContentConverter 数组
        _converters.TryAdd(converters, value => value.GetType());
    }

    /// <inheritdoc />
    public IHttpContentConverter<TResult> GetConverter<TResult>(params IHttpContentConverter[]? converters)
    {
        // 检查类型是否是 HttpResponseMessage 类型
        if (typeof(TResult) == typeof(HttpResponseMessage))
        {
            throw new InvalidOperationException(
                $"`{nameof(HttpResponseMessage)}` type cannot be directly processed as `TResult`.");
        }

        // 初始化新的 IHttpContentConverter 字典集合
        var unionProcessors = new Dictionary<Type, IHttpContentConverter>(_converters);

        // 添加自定义 IHttpContentConverter 数组
        unionProcessors.TryAdd(converters, value => value.GetType());

        // 查找可以处理目标类型的响应内容转换器
        var typeConverter = unionProcessors.Values.OfType<IHttpContentConverter<TResult>>().LastOrDefault();

        // 空检查
        if (typeConverter is not null)
        {
            return typeConverter;
        }

        // 如果未找到，则统一使用 ObjectContentConverter 转换器进行处理
        return _serviceProvider.GetService<IObjectContentConverterFactory>()?.GetConverter<TResult>() ??
               new ObjectContentConverter<TResult>();
    }

    /// <inheritdoc />
    public TResult? Read<TResult>(HttpResponseMessage httpResponseMessage, IHttpContentConverter[]? converters = null,
        CancellationToken cancellationToken = default)
    {
        // 检查类型是否是 HttpResponseMessage 类型，如果是直接返回
        if (typeof(TResult) == typeof(HttpResponseMessage))
        {
            return (TResult)(object)httpResponseMessage;
        }

        return GetConverter<TResult>(converters).Read(httpResponseMessage, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TResult?> ReadAsync<TResult>(HttpResponseMessage httpResponseMessage,
        IHttpContentConverter[]? converters = null,
        CancellationToken cancellationToken = default)
    {
        // 检查类型是否是 HttpResponseMessage 类型，如果是直接返回
        if (typeof(TResult) == typeof(HttpResponseMessage))
        {
            return (TResult)(object)httpResponseMessage;
        }

        return await GetConverter<TResult>(converters).ReadAsync(httpResponseMessage, cancellationToken);
    }
}