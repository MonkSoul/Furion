using Fur.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Fur.Authorization
{
    /// <summary>
    /// 授权策略执行程序
    /// </summary>
    [SkipScan]
    public abstract class AppAuthorizeHandler : IAuthorizationHandler
    {
        /// <summary>
        /// 授权验证核心方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            // 获取所有未成功验证的需求
            var pendingRequirements = context.PendingRequirements;

            DefaultHttpContext httpContext;

            // 修复全局权限过滤器bug
            if (context.Resource is AuthorizationFilterContext filterContext) httpContext = (DefaultHttpContext)filterContext.HttpContext;
            else if (context.Resource is DefaultHttpContext defaultHttpContext) httpContext = defaultHttpContext;
            else httpContext = null;

            // 调用子类管道
            var pipeline = Pipeline(context, httpContext);
            if (!pipeline) return Task.CompletedTask;

            // 通过授权验证
            foreach (var requirement in pendingRequirements)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 验证管道
        /// </summary>
        /// <param name="context">授权上下文</param>
        /// <param name="endpointMetadata">终点路由元数据</param>
        /// <returns></returns>
        public virtual bool Pipeline(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            return true;
        }
    }
}