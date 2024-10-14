// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     字符串内容转换器
/// </summary>
public class StringContentConverter : IHttpContentConverter<string>
{
    /// <inheritdoc />
    public virtual string? Read(HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default) =>
        httpResponseMessage.Content.ReadAsStringAsync(cancellationToken).GetAwaiter().GetResult();

    /// <inheritdoc />
    public virtual async Task<string?> ReadAsync(HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default) =>
        await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
}