// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
                if (!UnifyContext.CheckSupportMvcController(context.HttpContext, actionDescriptor, out _)) return;

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