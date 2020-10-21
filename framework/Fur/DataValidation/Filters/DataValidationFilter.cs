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
using Fur.UnifyResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Mime;

namespace Fur.DataValidation
{
    /// <summary>
    /// 数据验证控制器
    /// </summary>
    [SkipScan]
    public sealed class DataValidationFilter : IActionFilter, IOrderedFilter
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "validation";

        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public DataValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
        /// 是否时可重复使用的
        /// </summary>
        public static bool IsReusable => true;

        /// <summary>
        /// 动作方法执行之前操作
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 排除 Mvc 视图
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor.ControllerTypeInfo.BaseType == typeof(Controller)) return;

            var method = actionDescriptor.MethodInfo;
            var modelState = context.ModelState;

            // 跳过验证类型
            var nonValidationAttributeType = typeof(NonValidationAttribute);

            // 如果贴了 [NonValidation] 特性 或 所在类型贴了 [NonValidation] 特性，则跳过验证
            if (actionDescriptor.Parameters.Count == 0 ||
                method.IsDefined(nonValidationAttributeType, true) ||
                method.ReflectedType.IsDefined(nonValidationAttributeType, true))
            {
                // 打印验证跳过消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Skipped");
                return;
            }

            // 如果动作方法参数为 0 或 验证通过，则跳过，
            if (modelState.IsValid)
            {
                // 打印验证成功消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Successed");
                return;
            }

            // 返回验证失败结果
            if (context.Result == null && !modelState.IsValid)
            {
                // 设置验证失败结果
                SetValidateFailedResult(context, modelState);
            }
        }

        /// <summary>
        /// 设置验证失败结果
        /// </summary>
        /// <param name="context">动作方法执行上下文</param>
        /// <param name="modelState">模型验证状态</param>
        private void SetValidateFailedResult(ActionExecutingContext context, ModelStateDictionary modelState)
        {
            // 将验证错误信息转换成字典并序列化成 Json
            var validationResults = modelState.ToDictionary(u => u.Key, u => modelState[u.Key].Errors.Select(c => c.ErrorMessage));
            var validateFaildMessage = JsonConvert.SerializeObject(validationResults, Formatting.Indented);

            // 处理规范化结果
            var unifyResult = _serviceProvider.GetService<IUnifyResultProvider>();
            if (unifyResult != null)
            {
                context.Result = unifyResult.OnValidateFailed(context, modelState, validationResults, validateFaildMessage);
            }
            else
            {
                // 返回 400 错误
                var result = new BadRequestObjectResult(modelState);

                // 设置返回的响应类型
                result.ContentTypes.Add(MediaTypeNames.Application.Json);
                result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                context.Result = result;
            }

            // 打印验证失败信息
            App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", $"Validation Failed:\r\n{validateFaildMessage}", true);
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