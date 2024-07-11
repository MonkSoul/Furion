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

using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text;

namespace Microsoft.AspNetCore.Http;

/// <summary>
/// Http 拓展类
/// </summary>
[SuppressSniffer]
public static class HttpContextExtensions
{
    /// <summary>
    /// 获取 Action 特性
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static TAttribute GetMetadata<TAttribute>(this HttpContext httpContext)
        where TAttribute : class
    {
        return httpContext.GetEndpoint()?.Metadata?.GetMetadata<TAttribute>();
    }

    /// <summary>
    /// 获取 控制器/Action 描述器
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static ControllerActionDescriptor GetControllerActionDescriptor(this HttpContext httpContext)
    {
        return httpContext.GetEndpoint()?.Metadata?.FirstOrDefault(u => u is ControllerActionDescriptor) as ControllerActionDescriptor;
    }

    /// <summary>
    /// 设置规范化文档自动登录
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="accessToken"></param>
    public static void SigninToSwagger(this HttpContext httpContext, string accessToken)
    {
        // 设置 Swagger 刷新自动授权
        httpContext.Response.Headers["access-token"] = accessToken;
    }

    /// <summary>
    /// 设置规范化文档退出登录
    /// </summary>
    /// <param name="httpContext"></param>
    public static void SignoutToSwagger(this HttpContext httpContext)
    {
        httpContext.Response.Headers["access-token"] = "invalid_token";
    }

    /// <summary>
    /// 设置响应头 Tokens
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="accessToken"></param>
    /// <param name="refreshToken"></param>
    public static void SetTokensOfResponseHeaders(this HttpContext httpContext, string accessToken, string refreshToken = null)
    {
        httpContext.Response.Headers["access-token"] = accessToken;
        if (!string.IsNullOrWhiteSpace(refreshToken))
        {
            httpContext.Response.Headers["x-access-token"] = refreshToken;
        }
    }

    /// <summary>
    /// 获取本机 IPv4地址
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetLocalIpAddressToIPv4(this HttpContext context)
    {
        return context.Connection.LocalIpAddress?.MapToIPv4()?.ToString();
    }

    /// <summary>
    /// 获取本机 IPv6地址
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetLocalIpAddressToIPv6(this HttpContext context)
    {
        return context.Connection.LocalIpAddress?.MapToIPv6()?.ToString();
    }

    /// <summary>
    /// 获取远程 IPv4地址
    /// </summary>
    /// <param name="context"></param>
    /// <param name="xff">是否优先取 X-Forwarded-For</param>
    /// <returns></returns>
    public static string GetRemoteIpAddressToIPv4(this HttpContext context, bool xff = false)
    {
        var ipv4 = context.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();

        if (xff)
        {
            var xForwardedFor = context.Request.Headers["X-Forwarded-For"];
            return !string.IsNullOrWhiteSpace(xForwardedFor) ? xForwardedFor : ipv4;
        }

        return ipv4;
    }

    /// <summary>
    /// 获取远程 IPv6地址
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetRemoteIpAddressToIPv6(this HttpContext context)
    {
        return context.Connection.RemoteIpAddress?.MapToIPv6()?.ToString();
    }

    /// <summary>
    /// 获取完整请求地址
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string GetRequestUrlAddress(this HttpRequest request)
    {
        return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host.Value)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
    }

    /// <summary>
    /// 获取来源地址
    /// </summary>
    /// <param name="request"></param>
    /// <param name="refererHeaderKey"></param>
    /// <returns></returns>
    public static string GetRefererUrlAddress(this HttpRequest request, string refererHeaderKey = "Referer")
    {
        return request.Headers[refererHeaderKey].ToString();
    }

    /// <summary>
    /// 读取 Body 内容
    /// </summary>
    /// <param name="httpContext"></param>
    /// <remarks>需先在 Startup 的 Configure 中注册 app.EnableBuffering()</remarks>
    /// <returns></returns>
    public static async Task<string> ReadBodyContentAsync(this HttpContext httpContext)
    {
        if (httpContext == null) return default;
        return await httpContext.Request.ReadBodyContentAsync();
    }

    /// <summary>
    /// 读取 Body 内容
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>需先在 Startup 的 Configure 中注册 app.EnableBuffering()</remarks>
    /// <returns></returns>
    public static async Task<string> ReadBodyContentAsync(this HttpRequest request)
    {
        request.Body.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);
        var body = await reader.ReadToEndAsync();

        // 回到顶部，解决此类问题 https://gitee.com/dotnetchina/Furion/issues/I6NX9E
        request.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }

    /// <summary>
    /// 将 <see cref="BadPageResult"/> 写入响应流中
    /// </summary>
    /// <param name="httpResponse"><see cref="HttpResponse"/></param>
    /// <param name="badPageResult"><see cref="BadPageResult"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    public static async ValueTask WriteAsync(this HttpResponse httpResponse, BadPageResult badPageResult, CancellationToken cancellationToken = default)
    {
        await httpResponse.Body.WriteAsync(badPageResult.ToByteArray(), cancellationToken);
    }

    /// <summary>
    /// 判断是否是 WebSocket 请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool IsWebSocketRequest(this HttpContext context)
    {
        return context.WebSockets.IsWebSocketRequest || context.Request.Path == "/ws";
    }
}