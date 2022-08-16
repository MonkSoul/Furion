// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion;
using Furion.DataValidation;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Microsoft.AspNetCore.Mvc.Filters;

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
        // 判断是否是验证异常
        var isValidationException = context.Exception is AppFriendlyException friendlyException && friendlyException.ValidationException;

        // 只有不是验证异常才处理
        if (!isValidationException)
        {
            // 解析异常处理服务，实现自定义异常额外操作，如记录日志等
            var globalExceptionHandler = context.HttpContext.RequestServices.GetService<IGlobalExceptionHandler>();
            if (globalExceptionHandler != null)
            {
                await globalExceptionHandler.OnExceptionAsync(context);
            }
        }

        // 如果异常在其他地方被标记了处理，那么这里不再处理
        if (context.ExceptionHandled) return;

        // 解析异常信息
        var exceptionMetadata = UnifyContext.GetExceptionMetadata(context);

        // 判断是否是 Razor Pages
        var isPageDescriptor = context.ActionDescriptor is CompiledPageActionDescriptor;

        // 判断是否是验证异常，如果是，则不处理
        if (isValidationException)
        {
            var resultHttpContext = context.HttpContext.Items[nameof(DataValidationFilter) + nameof(AppFriendlyException)];
            // 读取验证执行结果
            if (resultHttpContext != null)
            {
                var result = isPageDescriptor
                    ? (resultHttpContext as PageHandlerExecutedContext).Result
                    : (resultHttpContext as ActionExecutedContext).Result;

                // 直接将验证结果设置为异常结果
                context.Result = result ?? new BadPageResult(StatusCodes.Status400BadRequest)
                {
                    Code = ValidatorContext.GetValidationMetadata((context.Exception as AppFriendlyException).ErrorMessage).Message
                };

                // 标记验证异常已被处理
                context.ExceptionHandled = true;
                return;
            }
        }

        // 处理 Razor Pages
        if (isPageDescriptor)
        {
            // 返回自定义错误页面
            context.Result = new BadPageResult(isValidationException ? StatusCodes.Status400BadRequest : exceptionMetadata.StatusCode)
            {
                Title = isValidationException ? "ModelState Invalid" : ("Internal Server: " + exceptionMetadata.Errors.ToString()),
                Code = isValidationException
                    ? ValidatorContext.GetValidationMetadata((context.Exception as AppFriendlyException).ErrorMessage).Message
                    : context.Exception.ToString()
            };
        }
        // Mvc/WebApi
        else
        {
            // 获取控制器信息
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            // 判断是否跳过规范化结果，如果是，则只处理为友好异常消息
            if (UnifyContext.CheckFailedNonUnify(actionDescriptor.MethodInfo, out var unifyResult))
            {
                // WebAPI 情况
                if (Penetrates.IsApiController(actionDescriptor.MethodInfo.DeclaringType))
                {
                    // 返回 JsonResult
                    context.Result = new JsonResult(exceptionMetadata.Errors)
                    {
                        StatusCode = exceptionMetadata.StatusCode,
                    };
                }
                else
                {
                    // 返回自定义错误页面
                    context.Result = new BadPageResult(exceptionMetadata.StatusCode)
                    {
                        Title = "Internal Server: " + exceptionMetadata.Errors.ToString(),
                        Code = context.Exception.ToString()
                    };
                }
            }
            else
            {
                // 判断是否支持 MVC 规范化处理
                if (!UnifyContext.CheckSupportMvcController(context.HttpContext, actionDescriptor, out _)) return;

                // 执行规范化异常处理
                context.Result = unifyResult.OnException(context, exceptionMetadata);
            }
        }

        // 打印错误消息
        PrintToMiniProfiler(context.Exception);
    }

    /// <summary>
    /// 打印错误到 MiniProfiler 中
    /// </summary>
    /// <param name="exception"></param>
    internal static void PrintToMiniProfiler(Exception exception)
    {
        // 判断是否注入 MiniProfiler 组件
        if (App.Settings.InjectMiniProfiler != true || exception == null) return;

        // 获取异常堆栈
        var stackTrace = new StackTrace(exception, true);
        if (stackTrace.FrameCount == 0) return;
        var traceFrame = stackTrace.GetFrame(0);

        // 获取出错的文件名
        var exceptionFileName = traceFrame.GetFileName();

        // 获取出错的行号
        var exceptionFileLineNumber = traceFrame.GetFileLineNumber();

        // 打印错误文件名和行号
        if (!string.IsNullOrWhiteSpace(exceptionFileName) && exceptionFileLineNumber > 0)
        {
            App.PrintToMiniProfiler("errors", "Locator", $"{exceptionFileName}:line {exceptionFileLineNumber}", true);
        }

        // 打印完整的堆栈信息
        App.PrintToMiniProfiler("errors", "StackTrace", exception.ToString(), true);
    }
}