// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Furion.AspNetCore.Extensions;

/// <summary>
///     <see cref="IApplicationBuilder" /> 拓展类
/// </summary>
public static class IApplicationBuilderExtensions
{
    /// <summary>
    ///     启用请求正文缓存
    /// </summary>
    /// <remarks>
    ///     <para>支持 <c>HttpRequest.Body</c> 重复读取。</para>
    ///     <para>https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/use-http-context?view=aspnetcore-8.0#enable-request-body-buffering</para>
    /// </remarks>
    /// <param name="app">
    ///     <see cref="IApplicationBuilder" />
    /// </param>
    /// <returns>
    ///     <see cref="IApplicationBuilder" />
    /// </returns>
    public static IApplicationBuilder UseEnableBuffering(this IApplicationBuilder app) =>
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            context.Request.Body.Position = 0;

            await next.Invoke();
        });
}