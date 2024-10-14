// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="IHttpContentConverter{TResult}" /> 工厂
/// </summary>
public interface IHttpContentConverterFactory
{
    /// <summary>
    ///     获取 <see cref="IHttpContentConverter{TResult}" /> 实例
    /// </summary>
    /// <param name="converters"><see cref="IHttpContentConverter{TResult}" /> 数组</param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="IHttpContentConverter{TResult}" />
    /// </returns>
    IHttpContentConverter<TResult> GetConverter<TResult>(params IHttpContentConverter[]? converters);

    /// <summary>
    ///     将 <see cref="HttpResponseMessage" /> 转换为
    ///     <typeparamref name="TResult" />
    ///     实例
    /// </summary>
    /// <param name="httpResponseMessage"></param>
    /// <param name="converters"><see cref="IHttpContentConverter{TResult}" /> 数组</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    TResult? Read<TResult>(HttpResponseMessage httpResponseMessage,
        IHttpContentConverter[]? converters = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     将 <see cref="HttpResponseMessage" /> 转换为
    ///     <typeparamref name="TResult" />
    ///     实例
    /// </summary>
    /// <param name="httpResponseMessage"></param>
    /// <param name="converters"><see cref="IHttpContentConverter{TResult}" /> 数组</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    Task<TResult?> ReadAsync<TResult>(HttpResponseMessage httpResponseMessage,
        IHttpContentConverter[]? converters = null, CancellationToken cancellationToken = default);
}