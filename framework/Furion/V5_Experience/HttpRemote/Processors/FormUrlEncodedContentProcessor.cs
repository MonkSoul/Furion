// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Furion.HttpRemote;

/// <summary>
///     URL 编码的表单内容处理器
/// </summary>
public class FormUrlEncodedContentProcessor : IHttpContentProcessor
{
    /// <inheritdoc />
    public virtual bool CanProcess(object? rawContent, string contentType) =>
        rawContent is FormUrlEncodedContent || contentType.IsIn([MediaTypeNames.Application.FormUrlEncoded],
            StringComparer.OrdinalIgnoreCase);

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

        // 将原始请求类型转换为字符串字典类型
        var nameValueCollection = rawContent.ObjectToDictionary()!
            .ToDictionary(u => u.Key.ToCultureString(CultureInfo.InvariantCulture)!,
                u => u.Value?.ToCultureString(CultureInfo.InvariantCulture)
            );

        // 初始化 FormUrlEncodedContent 实例
        var formUrlEncodedContent = new FormUrlEncodedContent(nameValueCollection);
        formUrlEncodedContent.Headers.ContentType =
            new MediaTypeHeaderValue(contentType) { CharSet = encoding?.BodyName ?? Constants.UTF8_ENCODING };

        return formUrlEncodedContent;
    }
}