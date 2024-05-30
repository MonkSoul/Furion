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
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Authorization;

/// <summary>
/// 授权处理上下文拓展类
/// </summary>
[SuppressSniffer]
public static class AuthorizationHandlerContextExtensions
{
    internal const string FAIL_STATUSCODE_KEY = $"{nameof(AuthorizationHandlerContext)}_FAIL_STATUSCODE";

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

    /// <summary>
    /// 设置授权状态码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    public static void StatusCode(this AuthorizationHandlerContext context, int statusCode)
    {
        var httpContext = context.GetCurrentHttpContext();
        if (httpContext != null)
        {
            httpContext.Items[FAIL_STATUSCODE_KEY] = statusCode;
        }
    }

    /// <summary>
    /// 标记授权失败并设置状态码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    public static void Fail(this AuthorizationHandlerContext context, int statusCode)
    {
        context.Fail();
        context.StatusCode(statusCode);
    }
}