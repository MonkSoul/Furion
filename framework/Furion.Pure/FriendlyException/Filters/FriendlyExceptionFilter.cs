// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion;
using Furion.DataValidation;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Logging;

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

        // 排除 WebSocket 请求处理
        if (context.HttpContext.IsWebSocketRequest()) return;

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
                if (!UnifyContext.CheckSupportMvcController(context.HttpContext, actionDescriptor, out _)
                    || UnifyContext.CheckHttpContextNonUnify(context.HttpContext)) return;

                // 执行规范化异常处理
                context.Result = unifyResult.OnException(context, exceptionMetadata);
            }
        }

        // 读取异常配置
        var friendlyExceptionSettings = context.HttpContext.RequestServices.GetRequiredService<IOptions<FriendlyExceptionSettingsOptions>>();

        // 判断是否启用异常日志输出
        if (friendlyExceptionSettings.Value.LogError == true)
        {
            // 创建日志记录器
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<FriendlyException>>();

            // 记录拦截日常
            logger.LogError(context.Exception, context.Exception.Message);
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