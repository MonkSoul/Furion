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

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="IActionResult" /> 内容转换器
/// </summary>
public class IActionResultContentConverter : IHttpContentConverter<IActionResult>
{
    /// <inheritdoc />
    public IActionResult? Read(HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken = default)
    {
        // 处理特定状态码结果
        if (TryGetStatusCodeResult(httpResponseMessage, out var statusCode, out var statusCodeResult))
        {
            return statusCodeResult;
        }

        // 获取响应内容标头
        var contentHeaders = httpResponseMessage.Content.Headers;

        // 获取内容类型
        var contentType = contentHeaders.ContentType?.MediaType;

        // 空检查
        ArgumentNullException.ThrowIfNull(contentType);

        switch (contentType)
        {
            case MediaTypeNames.Application.Json:
            case MediaTypeNames.Application.JsonPatch:
            case MediaTypeNames.Application.Xml:
            case MediaTypeNames.Application.XmlPatch:
            case MediaTypeNames.Text.Xml:
            case MediaTypeNames.Text.Html:
            case MediaTypeNames.Text.Plain:
                // 读取字符串内容
                var stringContent = httpResponseMessage.Content.ReadAsStringAsync(cancellationToken).GetAwaiter()
                    .GetResult();

                return new ContentResult
                {
                    Content = stringContent, StatusCode = (int)statusCode, ContentType = contentType
                };
            default:
                // 获取 ContentDisposition 实例
                var contentDisposition = contentHeaders.ContentDisposition;

                // 获取文件下载名
                var fileDownloadName = contentDisposition?.FileNameStar ?? contentDisposition?.FileName;

                // 读取流内容
                var streamContent = httpResponseMessage.Content.ReadAsStream(cancellationToken);

                return new FileStreamResult(streamContent, contentType)
                {
                    FileDownloadName =
                        string.IsNullOrWhiteSpace(fileDownloadName)
                            ? fileDownloadName
                            // 使用 UTF-8 解码文件名
                            : Uri.UnescapeDataString(fileDownloadName),
                    LastModified = contentHeaders.LastModified?.UtcDateTime
                };
        }
    }

    /// <inheritdoc />
    public async Task<IActionResult?> ReadAsync(HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default)
    {
        // 处理特定状态码结果
        if (TryGetStatusCodeResult(httpResponseMessage, out var statusCode, out var statusCodeResult))
        {
            return statusCodeResult;
        }

        // 获取响应内容标头
        var contentHeaders = httpResponseMessage.Content.Headers;

        // 获取内容类型
        var contentType = contentHeaders.ContentType?.MediaType;

        // 空检查
        ArgumentNullException.ThrowIfNull(contentType);

        switch (contentType)
        {
            case MediaTypeNames.Application.Json:
            case MediaTypeNames.Application.JsonPatch:
            case MediaTypeNames.Application.Xml:
            case MediaTypeNames.Application.XmlPatch:
            case MediaTypeNames.Text.Xml:
            case MediaTypeNames.Text.Html:
            case MediaTypeNames.Text.Plain:
                // 读取字符串内容
                var stringContent = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

                return new ContentResult
                {
                    Content = stringContent, StatusCode = (int)statusCode, ContentType = contentType
                };
            default:
                // 获取 ContentDisposition 实例
                var contentDisposition = contentHeaders.ContentDisposition;

                // 获取文件下载名
                var fileDownloadName = contentDisposition?.FileNameStar ?? contentDisposition?.FileName;

                // 读取流内容
                var streamContent = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);

                return new FileStreamResult(streamContent, contentType)
                {
                    FileDownloadName =
                        string.IsNullOrWhiteSpace(fileDownloadName)
                            ? fileDownloadName
                            // 使用 UTF-8 解码文件名
                            : Uri.UnescapeDataString(fileDownloadName),
                    LastModified = contentHeaders.LastModified?.UtcDateTime
                };
        }
    }

    /// <summary>
    ///     处理特定状态码结果
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <param name="statusCode">HTTP 状态码</param>
    /// <param name="statusCodeResult">
    ///     <see cref="IActionResult" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal static bool TryGetStatusCodeResult(HttpResponseMessage httpResponseMessage, out HttpStatusCode statusCode,
        out IActionResult? statusCodeResult)
    {
        // 获取状态码
        statusCode = httpResponseMessage.StatusCode;

        statusCodeResult = statusCode switch
        {
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.BadRequest => new BadRequestResult(),
            HttpStatusCode.Unauthorized => new UnauthorizedResult(),
            HttpStatusCode.NotFound => new NotFoundResult(),
            HttpStatusCode.Conflict => new ConflictResult(),
            HttpStatusCode.UnsupportedMediaType => new UnsupportedMediaTypeResult(),
            HttpStatusCode.UnprocessableEntity => new UnprocessableEntityResult(),
            _ => null
        };

        return statusCodeResult is not null;
    }
}