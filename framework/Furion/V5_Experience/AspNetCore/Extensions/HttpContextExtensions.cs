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