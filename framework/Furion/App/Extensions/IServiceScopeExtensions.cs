// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="IServiceScope"/> 拓展类
/// </summary>
public static class IServiceScopeExtensions
{
    /// <summary>
    /// 在当前服务作用域下创建 <see cref="DefaultHttpContext"/> 实例
    /// </summary>
    /// <remarks>解决多线程中获取 <see cref="HttpContext"/> 空问题</remarks>
    /// <param name="serviceScope"><see cref="IServiceScope"/></param>
    /// <param name="feature"><see cref="IFeatureCollection"/>，可通过 HttpContext.Features 获取</param>
    /// <param name="claims"><see cref="ClaimsPrincipal"/>，可通过 HttpContext.User 获取</param>
    public static void CreateDefaultHttpContext(this IServiceScope serviceScope, IFeatureCollection feature, ClaimsPrincipal claims)
    {
        var httpContextAccessor = serviceScope.ServiceProvider.GetService<IHttpContextAccessor>();
        httpContextAccessor.HttpContext = new DefaultHttpContext(feature)
        {
            RequestServices = serviceScope.ServiceProvider,
            User = claims
        };
    }
}