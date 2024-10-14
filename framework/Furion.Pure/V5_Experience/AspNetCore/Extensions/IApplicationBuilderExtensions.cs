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