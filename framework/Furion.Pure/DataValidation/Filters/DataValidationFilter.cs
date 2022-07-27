// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Furion.DataValidation;

/// <summary>
/// 数据验证控制器
/// </summary>
[SuppressSniffer]
public sealed class DataValidationFilter : IAsyncActionFilter, IOrderedFilter
{
    /// <summary>
    /// Api 行为配置选项
    /// </summary>
    private readonly ApiBehaviorOptions _apiBehaviorOptions;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public DataValidationFilter(IOptions<ApiBehaviorOptions> options)
    {
        _apiBehaviorOptions = options.Value;
    }

    /// <summary>
    /// 过滤器排序
    /// </summary>
    internal const int FilterOrder = -1000;

    /// <summary>
    /// 排序属性
    /// </summary>
    public int Order => FilterOrder;

    /// <summary>
    /// 是否是可重复使用的
    /// </summary>
    public static bool IsReusable => true;

    /// <summary>
    /// 拦截请求
    /// </summary>
    /// <param name="context">动作方法上下文</param>
    /// <param name="next">中间件委托</param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器/方法信息
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        // 跳过验证类型
        var nonValidationAttributeType = typeof(NonValidationAttribute);
        var method = actionDescriptor.MethodInfo;

        // 获取验证状态
        var modelState = context.ModelState;

        // 如果参数为 0或贴了 [NonValidation] 特性 或所在类型贴了 [NonValidation] 特性或验证成功或已经设置了结果，则跳过验证
        if (actionDescriptor.Parameters.Count == 0 ||
            method.IsDefined(nonValidationAttributeType, true) ||
            method.DeclaringType.IsDefined(nonValidationAttributeType, true) ||
            modelState.IsValid ||
            context.Result != null)
        {
            // 处理执行后验证信息
            var resultContext = await next();

            // 如果异常不为空且属于友好验证异常
            if (resultContext.Exception != null && resultContext.Exception is AppFriendlyException friendlyException && friendlyException.ValidationException)
            {
                // 存储执行结果
                context.HttpContext.Items[nameof(DataValidationFilter)] = resultContext;

                // 处理验证信息
                HandleValidation(context, method, actionDescriptor, friendlyException.ErrorMessage, resultContext, friendlyException);
            }
            return;
        }

        // 处理执行前验证信息
        HandleValidation(context, method, actionDescriptor, modelState);
    }

    /// <summary>
    /// 内部处理异常
    /// </summary>
    /// <param name="context"></param>
    /// <param name="method"></param>
    /// <param name="actionDescriptor"></param>
    /// <param name="errors"></param>
    /// <param name="resultContext"></param>
    /// <param name="friendlyException"></param>
    private void HandleValidation(ActionExecutingContext context, MethodInfo method, ControllerActionDescriptor actionDescriptor, object errors, ActionExecutedContext resultContext = default, AppFriendlyException friendlyException = default)
    {
        dynamic finalContext = resultContext != null ? resultContext : context;

        // 解析验证消息
        var validationMetadata = ValidatorContext.GetValidationMetadata(errors);
        validationMetadata.ErrorCode = friendlyException?.ErrorCode;
        validationMetadata.OriginErrorCode = friendlyException?.OriginErrorCode;
        validationMetadata.StatusCode = friendlyException?.StatusCode;

        // 判断是否跳过规范化结果，如果跳过，返回 400 BadRequestResult
        if (UnifyContext.CheckFailedNonUnify(actionDescriptor.MethodInfo, out var unifyResult))
        {
            // WebAPI 情况
            if (Penetrates.IsApiController(method.DeclaringType))
            {
                // 如果不启用 SuppressModelStateInvalidFilter，则跳过，理应手动验证
                if (!_apiBehaviorOptions.SuppressModelStateInvalidFilter)
                {
                    finalContext.Result = _apiBehaviorOptions.InvalidModelStateResponseFactory(context);
                }
                else
                {
                    // 返回 JsonResult
                    finalContext.Result = new JsonResult(validationMetadata.ValidationResult)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }
            else
            {
                // 返回自定义错误页面
                finalContext.Result = new BadPageResult(StatusCodes.Status400BadRequest)
                {
                    Code = validationMetadata.Message
                };
            }
        }
        else
        {
            // 判断是否支持 MVC 规范化处理，一旦启用，则自动调用规范化提供器进行操作
            if (!UnifyContext.CheckSupportMvcController(context.HttpContext, actionDescriptor, out _)) return;

            finalContext.Result = unifyResult.OnValidateFailed(context, validationMetadata);
        }

        // 打印验证失败信息
        App.PrintToMiniProfiler("validation", "Failed", $"Validation Failed:\r\n{validationMetadata.Message}", true);
    }
}