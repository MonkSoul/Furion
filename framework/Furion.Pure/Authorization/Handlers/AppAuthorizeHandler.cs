// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Furion.Authorization;

/// <summary>
/// 授权策略执行程序
/// </summary>
[SuppressSniffer]
public abstract class AppAuthorizeHandler : IAuthorizationHandler
{
    /// <summary>
    /// 刷新 Token 身份标识
    /// </summary>
    private readonly string[] _refreshTokenClaims = new[] { "f", "e", "s", "l", "k" };

    /// <summary>
    /// 授权验证核心方法
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual async Task HandleAsync(AuthorizationHandlerContext context)
    {
        // 判断是否授权
        var isAuthenticated = context.User.Identity.IsAuthenticated;
        if (isAuthenticated)
        {
            // 禁止使用刷新 Token 进行单独校验
            if (_refreshTokenClaims.All(k => context.User.Claims.Any(c => c.Type == k)))
            {
                context.Fail();
                return;
            }

            await AuthorizeHandleAsync(context);
        }
        else context.GetCurrentHttpContext()?.SignoutToSwagger();    // 退出Swagger登录
    }

    /// <summary>
    /// 验证管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public virtual Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        return Task.FromResult(true);
    }

    /// <summary>
    /// 策略验证管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <param name="requirement"></param>
    /// <returns></returns>
    public virtual Task<bool> PolicyPipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext, IAuthorizationRequirement requirement)
    {
        return Task.FromResult(true);
    }

    /// <summary>
    /// 授权处理
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    protected async Task AuthorizeHandleAsync(AuthorizationHandlerContext context)
    {
        // 获取所有未成功验证的需求
        var pendingRequirements = context.PendingRequirements;

        // 获取 HttpContext 上下文
        var httpContext = context.GetCurrentHttpContext();

        // 调用子类管道
        var pipeline = await PipelineAsync(context, httpContext);
        if (pipeline)
        {
            // 通过授权验证
            foreach (var requirement in pendingRequirements)
            {
                // 验证策略管道
                var policyPipeline = await PolicyPipelineAsync(context, httpContext, requirement);
                if (policyPipeline) context.Succeed(requirement);
                else context.Fail();
            }
        }
        else context.Fail();
    }
}