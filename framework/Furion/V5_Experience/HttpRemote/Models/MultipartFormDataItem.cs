// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text;

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="MultipartFormDataContent" /> 条目
/// </summary>
internal sealed class MultipartFormDataItem
{
    /// <summary>
    ///     <inheritdoc cref="MultipartFormDataItem" />
    /// </summary>
    /// <param name="name">表单名称</param>
    internal MultipartFormDataItem(string name)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }

    /// <summary>
    ///     表单名称
    /// </summary>
    internal string Name { get; }

    /// <summary>
    ///     内容类型
    /// </summary>
    internal string? ContentType { get; init; }

    /// <summary>
    ///     内容编码
    /// </summary>
    /// <remarks>默认值为 <c>utf-8</c> 编码。</remarks>
    internal Encoding? ContentEncoding { get; init; } = Encoding.UTF8;

    /// <summary>
    ///     原始请求内容
    /// </summary>
    /// <remarks>此属性值最终将转换为 <see cref="HttpContent" /> 类型实例。</remarks>
    internal object? RawContent { get; init; }

    /// <summary>
    ///     文件名
    /// </summary>
    /// <remarks>用于 <see cref="RawContent" /> 类型为 <see cref="Stream" /> 或 <c>byte[]</c> 时有效。</remarks>
    internal string? FileName { get; init; }

    /// <summary>
    ///     文件大小
    /// </summary>
    internal long? FileSize { get; init; }
}