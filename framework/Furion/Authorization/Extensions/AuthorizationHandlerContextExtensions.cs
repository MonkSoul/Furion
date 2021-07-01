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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Authorization
{
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
}