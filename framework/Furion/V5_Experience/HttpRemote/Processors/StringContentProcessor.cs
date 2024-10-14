// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Furion.HttpRemote;

/// <summary>
///     字符串内容处理器
/// </summary>
public class StringContentProcessor : IHttpContentProcessor
{
    /// <inheritdoc />
    public virtual bool CanProcess(object? rawContent, string contentType) =>
        rawContent is StringContent ||
        contentType.IsIn([
            MediaTypeNames.Application.Json,
            MediaTypeNames.Application.JsonPatch,
            MediaTypeNames.Application.Xml,
            MediaTypeNames.Application.XmlPatch,
            MediaTypeNames.Text.Xml,
            MediaTypeNames.Text.Html,
            MediaTypeNames.Text.Plain
        ], StringComparer.OrdinalIgnoreCase);

    /// <inheritdoc />
    public virtual HttpContent? Process(object? rawContent, string contentType, Encoding? encoding)
    {
        // 跳过空值和 HttpContent 类型
        switch (rawContent)
        {
            case null:
                return null;
            case HttpContent httpContent:
                // 设置 Content-Type
                httpContent.Headers.ContentType ??=
                    new MediaTypeHeaderValue(contentType) { CharSet = encoding?.BodyName ?? Constants.UTF8_ENCODING };

                return httpContent;
        }

        // 将原始请求内容转换为字符串
        var content = rawContent.GetType().IsBasicType() || rawContent is JsonElement
            ? rawContent.ToCultureString(CultureInfo.InvariantCulture)
            : JsonSerializer.Serialize(rawContent);

        // 初始化 StringContent 实例
        var stringContent = new StringContent(content!, encoding,
            new MediaTypeHeaderValue(contentType) { CharSet = encoding?.BodyName ?? Constants.UTF8_ENCODING });

        return stringContent;
    }
}