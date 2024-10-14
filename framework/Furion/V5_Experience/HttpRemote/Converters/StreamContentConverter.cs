// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     流内容转换器
/// </summary>
public class StreamContentConverter : IHttpContentConverter<Stream>
{
    /// <inheritdoc />
    public virtual Stream? Read(HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default) =>
        httpResponseMessage.Content.ReadAsStream(cancellationToken);

    /// <inheritdoc />
    public virtual async Task<Stream?> ReadAsync(HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default) =>
        await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
}