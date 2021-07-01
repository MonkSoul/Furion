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

using Furion;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 友好异常拦截器
    /// </summary>
    [SuppressSniffer]
    public sealed class FriendlyExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public FriendlyExceptionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 异常拦截
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // 解析异常处理服务，实现自定义异常额外操作，如记录日志等
            var globalExceptionHandler = _serviceProvider.GetService<IGlobalExceptionHandler>();
            if (globalExceptionHandler != null)
            {
                await globalExceptionHandler.OnExceptionAsync(context);
            }

            // 排除 Mvc 控制器处理
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (typeof(Controller).IsAssignableFrom(actionDescriptor.ControllerTypeInfo)) return;

            // 如果异常在其他地方被标记了处理，那么这里不再处理
            if (context.ExceptionHandled) return;

            // 标识异常已经被处理（该代码已取消，导致其他自定义异常过滤器无法拦截）
            // context.ExceptionHandled = true;

            // 设置异常结果
            var exception = context.Exception;

            // 解析验证异常
            var validationFlag = "[Validation]";
            var isValidationMessage = exception.Message.StartsWith(validationFlag);
            var errorMessage = isValidationMessage ? exception.Message[validationFlag.Length..] : exception.Message;

            // 判断是否跳过规范化结果
            if (UnifyContext.IsSkipUnifyHandler(actionDescriptor.MethodInfo, out var unifyResult))
            {
                // 解析异常信息
                var (StatusCode, _, Errors) = UnifyContext.GetExceptionMetadata(context);

                // 解析 JSON 序列化提供器
                var jsonSerializer = _serviceProvider.GetService<IJsonSerializerProvider>();

                context.Result = new ContentResult
                {
                    Content = jsonSerializer.Serialize(Errors),
                    StatusCode = StatusCode
                };
            }
            else context.Result = unifyResult.OnException(context);

            // 处理验证异常，打印验证失败信息
            if (isValidationMessage)
            {
                App.PrintToMiniProfiler("validation", "Failed", $"Exception Validation Failed:\r\n{errorMessage}", true);
            }
            // 打印错误到 MiniProfiler 中
            else Oops.PrintToMiniProfiler(context.Exception);
        }
    }
}