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

using Furion.DataValidation;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Furion.UnifyResult
{
    /// <summary>
    /// 规范化结构（请求成功）过滤器
    /// </summary>
    [SuppressSniffer]
    public class SucceededUnifyResultFilter : IAsyncActionFilter, IOrderedFilter
    {
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
            if (typeof(Controller).IsAssignableFrom(actionDescriptor.ControllerTypeInfo))
            {
                await next();
                return;
            }

            var actionExecutedContext = await next();

            // 如果没有异常再执行
            if (actionExecutedContext.Exception == null && !UnifyContext.IsSkipUnifyHandlerOnSucceedReturn(actionDescriptor.MethodInfo, out var unifyResult))
            {
                // 处理规范化结果
                if (unifyResult != null)
                {
                    // 处理 BadRequestObjectResult 验证结果
                    if (actionExecutedContext.Result is BadRequestObjectResult badRequestObjectResult)
                    {
                        // 解析验证消息
                        var (validationResults, validateFaildMessage, modelState) = ValidatorContext.OutputValidationInfo(badRequestObjectResult.Value);

                        var result = unifyResult.OnValidateFailed(context, modelState, validationResults, validateFaildMessage);
                        if (result != null) actionExecutedContext.Result = result;

                        // 打印验证失败信息
                        App.PrintToMiniProfiler("validation", "Failed", $"Validation Failed:\r\n{validateFaildMessage}", true);
                    }
                    else
                    {
                        var result = unifyResult.OnSucceeded(actionExecutedContext);
                        if (result != null) actionExecutedContext.Result = result;
                    }
                }
            }
        }
    }
}