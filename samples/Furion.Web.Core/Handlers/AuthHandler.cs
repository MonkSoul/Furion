using Furion.Authorization;
using Furion.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Furion.Web.Core;

/// <summary>
/// 授权自定义处理程序
/// </summary>
public class AuthHandler : AppAuthorizeHandler
{
    /// <summary>
    /// 验证管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 检查权限，如果方法时异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
        return Task.FromResult(CheckAuthorzie(httpContext));
    }

    /// <summary>
    /// 检查权限
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static bool CheckAuthorzie(DefaultHttpContext httpContext)
    {
        // 获取权限特性
        var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
        if (securityDefineAttribute == null) return true;

        // 解析服务
        var authorizationManager = httpContext.RequestServices.GetService<IAuthorizationManager>();

        // 检查授权
        return authorizationManager.CheckSecurity(securityDefineAttribute.ResourceId);
    }
}