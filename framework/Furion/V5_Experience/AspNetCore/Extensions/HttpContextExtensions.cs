// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text;

namespace Furion.AspNetCore.Extensions;

/// <summary>
///     <see cref="HttpContext" /> 拓展类
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    ///     获取完整的请求 URL 地址
    /// </summary>
    /// <param name="httpRequest">
    ///     <see cref="HttpRequest" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public static string GetFullRequestUrl(this HttpRequest httpRequest) =>
        new StringBuilder()
            .Append(httpRequest.Scheme)
            .Append("://")
            .Append(httpRequest.Host.Value)
            .Append(httpRequest.PathBase)
            .Append(httpRequest.Path)
            .Append(httpRequest.QueryString)
            .ToString();

    /// <summary>
    ///     获取响应状态文本
    /// </summary>
    /// <param name="httpResponse">
    ///     <see cref="HttpResponse" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public static string? GetStatusText(this HttpResponse httpResponse)
    {
        // 获取响应状态码
        var statusCode = httpResponse.StatusCode;

        // 检查响应状态码是否是预设的 HttpStatusCode 值
        return !Enum.IsDefined(typeof(HttpStatusCode), statusCode) ? null : ((HttpStatusCode)statusCode).ToString();
    }

    /// <summary>
    ///     配置允许跨域响应头
    /// </summary>
    /// <param name="httpResponse">
    ///     <see cref="HttpResponse" />
    /// </param>
    public static void AllowCors(this HttpResponse httpResponse)
    {
        httpResponse.Headers.AccessControlAllowOrigin = "*";
        httpResponse.Headers.AccessControlAllowHeaders = "*";
    }

    /// <summary>
    ///     添加响应头导出
    /// </summary>
    /// <param name="headers">
    ///     <see cref="IHeaderDictionary" />
    /// </param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public static void AppendExpose(this IHeaderDictionary headers, string key, StringValues value)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        headers.AccessControlExposeHeaders = key;
        headers.Append(key, value);
    }
}