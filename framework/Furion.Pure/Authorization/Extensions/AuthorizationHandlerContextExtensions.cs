// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Authorization;

/// <summary>
/// 授权处理上下文拓展类
/// </summary>
[SuppressSniffer]
public static class AuthorizationHandlerContextExtensions
{
    /// <summary>
    /// 获取当前 HttpContext 上下文
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static DefaultHttpContext GetCurrentHttpContext(this AuthorizationHandlerContext context)
    {
        DefaultHttpContext httpContext;

        // 获取 httpContext 对象
        if (context.Resource is AuthorizationFilterContext filterContext) httpContext = (DefaultHttpContext)filterContext.HttpContext;
        else if (context.Resource is DefaultHttpContext defaultHttpContext) httpContext = defaultHttpContext;
        else httpContext = null;

        return httpContext;
    }
}