// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;

namespace Fur.Authorization
{
    /// <summary>
    /// 授权策略执行程序
    /// </summary>
    public abstract class AuthorizePolicyHandler : IAuthorizationHandler
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

        /// <summary>
        /// 验证管道
        /// </summary>
        /// <param name="context">授权上下文</param>
        /// <param name="actionDescriptor">动作方法描述器</param>
        /// <returns></returns>
        public virtual bool Pipeline(AuthorizationHandlerContext context, ControllerActionDescriptor actionDescriptor) => true;
    }
}