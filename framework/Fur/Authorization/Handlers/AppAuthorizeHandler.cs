// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

            // 调用子类管道
            var pipeline = Pipeline(context, context.Resource as DefaultHttpContext);
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