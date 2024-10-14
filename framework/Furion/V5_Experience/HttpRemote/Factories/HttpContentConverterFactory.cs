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