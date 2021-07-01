// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Furion.Authorization
{
    /// <summary>
    /// 授权策略执行程序
    /// </summary>
    [SuppressSniffer]
    public abstract class AppAuthorizeHandler : IAuthorizationHandler
    {
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
                }
            }
            else context.Fail();
        }
    }
}