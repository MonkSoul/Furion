using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;

namespace Fur.PolicyAuthorization
{
    /// <summary>
    /// 授权策略执行程序
    /// </summary>
    public abstract class AuthorizePolicyHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            // 获取所有未成功验证的需求
            var pendingRequirements = context.PendingRequirements;

            // 获取动作方法描述器
            ControllerActionDescriptor actionDescriptor = null;
            if (context.Resource is Endpoint endpoint)
            {
                actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            }

            // 调用子类管道
            var pipeline = Pipeline(context, actionDescriptor);
            if (!pipeline) return Task.CompletedTask;

            // 通过授权验证
            foreach (var requirement in pendingRequirements)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        public virtual bool Pipeline(AuthorizationHandlerContext context, ControllerActionDescriptor actionDescriptor) => true;
    }
}