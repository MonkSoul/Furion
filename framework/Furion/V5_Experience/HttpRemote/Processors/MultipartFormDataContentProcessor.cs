// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Net.Mime;
using System.Text;

namespace Furion.HttpRemote;

/// <summary>
///     多部分表单数据内容处理器
/// </summary>
public class MultipartFormDataContentProcessor : IHttpContentProcessor
{
    /// <inheritdoc />
    public virtual bool CanProcess(object? rawContent, string contentType) =>
        rawContent is MultipartFormDataContent ||
        contentType.IsIn([MediaTypeNames.Multipart.FormData], StringComparer.OrdinalIgnoreCase);

    /// <inheritdoc />
    public virtual HttpContent? Process(object? rawContent, string contentType, Encoding? encoding) =>
        rawContent switch
        {
            null => null,
            HttpContent httpContent => httpContent,
            _ => throw new NotImplementedException()
        };
}