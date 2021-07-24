// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Furion.DataValidation
{
    /// <summary>
    /// 数据验证控制器
    /// </summary>
    [SuppressSniffer]
    public sealed class DataValidationFilter : IActionFilter, IOrderedFilter
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "validation";

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
        /// 动作方法执行之前操作
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 获取控制器/方法信息
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var method = actionDescriptor.MethodInfo;

            // 跳过验证类型
            var nonValidationAttributeType = typeof(NonValidationAttribute);

            // 如果参数为 0或贴了 [NonValidation] 特性 或所在类型贴了 [NonValidation] 特性，则跳过验证
            if (actionDescriptor.Parameters.Count == 0 ||
                method.IsDefined(nonValidationAttributeType, true) ||
                method.DeclaringType.IsDefined(nonValidationAttributeType, true)) return;

            // 获取验证状态
            var modelState = context.ModelState;

            // 判断是否验证成功
            if (modelState.IsValid) return;

            // 返回验证失败结果
            if (context.Result == null && !modelState.IsValid)
            {
                // 设置验证失败结果
                SetValidateFailedResult(context, modelState, actionDescriptor);
            }
        }

        /// <summary>
        /// 设置验证失败结果
        /// </summary>
        /// <param name="context">动作方法执行上下文</param>
        /// <param name="modelState">模型验证状态</param>
        /// <param name="actionDescriptor"></param>
        private static void SetValidateFailedResult(ActionExecutingContext context, ModelStateDictionary modelState, ControllerActionDescriptor actionDescriptor)
        {
            // 解析验证消息
            var validationMetadata = ValidatorContext.GetValidationMetadata(modelState);

            // 判断是否跳过规范化结果
            if (UnifyContext.CheckFailedNonUnify(actionDescriptor.MethodInfo, out var unifyResult))
            {
                // 返回 400 状态码
                context.Result = new BadRequestObjectResult(modelState);
            }
            else context.Result = unifyResult.OnValidateFailed(context, validationMetadata);

            // 打印验证失败信息
            App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", $"Validation Failed:\r\n{validationMetadata.Message}", true);
        }

        /// <summary>
        /// 动作方法执行完成操作
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}