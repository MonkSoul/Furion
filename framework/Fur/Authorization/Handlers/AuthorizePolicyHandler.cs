// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				   Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;

namespace Fur.Authorization
{
    /// <summary>
    /// 授权策略执行程序
    /// </summary>
    [NonBeScan]
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
        public virtual bool Pipeline(AuthorizationHandlerContext context, ControllerActionDescriptor actionDescriptor)
        {
            return true;
        }
    }
}