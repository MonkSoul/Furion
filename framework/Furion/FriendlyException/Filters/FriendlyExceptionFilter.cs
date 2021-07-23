// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
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
        /// 异常拦截
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // 解析异常处理服务，实现自定义异常额外操作，如记录日志等
            var globalExceptionHandler = App.RootServices.GetService<IGlobalExceptionHandler>();
            if (globalExceptionHandler != null)
            {
                await globalExceptionHandler.OnExceptionAsync(context);
            }

            // 排除 Mvc 控制器处理
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (typeof(Controller).IsAssignableFrom(actionDescriptor.ControllerTypeInfo)) return;

            // 如果异常在其他地方被标记了处理，那么这里不再处理
            if (context.ExceptionHandled) return;

            // 设置异常结果
            var exception = context.Exception;

            // 解析验证异常
            var validationFlag = "[Validation]";
            var isValidationMessage = exception.Message.StartsWith(validationFlag);
            var errorMessage = isValidationMessage ? exception.Message[validationFlag.Length..] : exception.Message;

            // 判断是否跳过规范化结果
            if (UnifyContext.CheckFailed(actionDescriptor.MethodInfo, out var unifyResult))
            {
                // 解析异常信息
                var (statusCode, _, errors) = UnifyContext.GetExceptionMetadata(context);

                // 解析 JSON 序列化提供器
                var jsonSerializer = App.RootServices.GetService<IJsonSerializerProvider>();

                context.Result = new ContentResult
                {
                    Content = jsonSerializer.Serialize(errors),
                    StatusCode = statusCode
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