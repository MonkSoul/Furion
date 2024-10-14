// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     管理 <see cref="HttpClient" /> 实例及及其释放操作
/// </summary>
internal sealed class HttpClientPooling
{
    /// <summary>
    ///     <inheritdoc cref="HttpClientPooling" />
    /// </summary>
    /// <param name="httpClient">
    ///     <see cref="HttpClient" />
    /// </param>
    /// <param name="release">用于释放 <see cref="HttpClient" /> 实例的方法委托</param>
    internal HttpClientPooling(HttpClient httpClient, Action<HttpClient>? release)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpClient);

        Instance = httpClient;
        Release = release;
    }

    /// <summary>
    ///     <see cref="HttpClient" />
    /// </summary>
    internal HttpClient Instance { get; }

    /// <summary>
    ///     用于释放 <see cref="HttpClient" /> 实例的方法委托
    /// </summary>
    internal Action<HttpClient>? Release { get; }
}