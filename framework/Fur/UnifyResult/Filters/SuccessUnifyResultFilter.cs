// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Fur.UnifyResult
{
    /// <summary>
    /// 规范化结构（请求成功）过滤器
    /// </summary>
    [SkipScan]
    public class SuccessUnifyResultFilter : IAsyncActionFilter, IOrderedFilter
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public SuccessUnifyResultFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 过滤器排序
        /// </summary>
        internal const int FilterOrder = 8888;

        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order => FilterOrder;

        /// <summary>
        /// 处理规范化结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 排除 Mvc 视图
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor.ControllerTypeInfo.BaseType == typeof(Controller))
            {
                await next();
                return;
            }

            var actionExecutedContext = await next();

            // 处理规范化结果
            var unifyResult = _serviceProvider.GetService<IUnifyResultProvider>();
            if (unifyResult != null && context.Result == null)
            {
                var result = unifyResult.OnSuccessed(actionExecutedContext);
                if (result != null) actionExecutedContext.Result = result;
            }
        }
    }
}