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

using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Furion.DataValidation;

/// <summary>
/// 数据验证拦截器（Razor Pages）
/// </summary>
[SuppressSniffer]
public sealed class DataValidationPageFilter : IAsyncPageFilter, IOrderedFilter
{
    /// <summary>
    /// Api 行为配置选项
    /// </summary>
    private readonly ApiBehaviorOptions _apiBehaviorOptions;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public DataValidationPageFilter(IOptions<ApiBehaviorOptions> options)
    {
        _apiBehaviorOptions = options.Value;
    }

    /// <summary>
    /// 过滤器排序
    /// </summary>
    private const int FilterOrder = -1000;

    /// <summary>
    /// 排序属性
    /// </summary>
    public int Order => FilterOrder;

    /// <summary>
    /// 是否是可重复使用的
    /// </summary>
    public static bool IsReusable => true;

    /// <summary>
    /// 模型绑定拦截
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 拦截请求
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        // 排除 WebSocket 请求处理
        if (context.HttpContext.IsWebSocketRequest())
        {
            await next.Invoke();
            return;
        }

        // 跳过验证类型
        var nonValidationAttributeType = typeof(NonValidationAttribute);
        var method = context.HandlerMethod?.MethodInfo;
        // 处理 Blazor Server
        if (method == null)
        {
            await CallUnHandleResult(context, next);
            return;
        }

        // 获取验证状态
        var modelState = context.ModelState;

        // 如果参数数量为 0 或贴了 [NonValidation] 特性 或所在类型贴了 [NonValidation] 特性或验证成功或已经设置了结果，则跳过验证
        if (context.HandlerArguments.Count == 0 ||
            method.IsDefined(nonValidationAttributeType, true) ||
            method.DeclaringType.IsDefined(nonValidationAttributeType, true) ||
            modelState.IsValid ||
            context.Result != null)
        {
            await CallUnHandleResult(context, next);
            return;
        }

        // 处理执行前验证信息
        var handledResult = HandleValidation(context, modelState);

        // 处理 Mvc 未处理结果情况
        if (!handledResult)
        {
            await CallUnHandleResult(context, next);
        }
    }

    /// <summary>
    /// 调用未处理的结果类型
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    private async Task CallUnHandleResult(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        // 处理执行后验证信息
        var resultContext = await next.Invoke();

        // 如果异常不为空且属于友好验证异常
        if (resultContext.Exception != null && resultContext.Exception is AppFriendlyException friendlyException && friendlyException.ValidationException)
        {
            // 存储验证执行结果
            context.HttpContext.Items[nameof(DataValidationFilter) + nameof(AppFriendlyException)] = resultContext;

            // 处理验证信息
            _ = HandleValidation(context, friendlyException.ErrorMessage, resultContext, friendlyException);
        }
    }

    /// <summary>
    /// 内部处理异常
    /// </summary>
    /// <param name="context"></param>
    /// <param name="errors"></param>
    /// <param name="resultContext"></param>
    /// <param name="friendlyException"></param>
    /// <returns>返回 false 表示结果没有处理</returns>
    private bool HandleValidation(PageHandlerExecutingContext context, object errors, PageHandlerExecutedContext resultContext = default, AppFriendlyException friendlyException = default)
    {
        dynamic finalContext = resultContext != null ? resultContext : context;

        // 解析验证消息
        var validationMetadata = ValidatorContext.GetValidationMetadata(errors);
        validationMetadata.ErrorCode = friendlyException?.ErrorCode;
        validationMetadata.OriginErrorCode = friendlyException?.OriginErrorCode;
        validationMetadata.StatusCode = friendlyException?.StatusCode;
        validationMetadata.Data = friendlyException?.Data;

        // 存储验证信息
        context.HttpContext.Items[nameof(DataValidationFilter) + nameof(ValidationMetadata)] = validationMetadata;

        // 返回自定义错误页面
        finalContext.Result = new BadPageResult(StatusCodes.Status400BadRequest)
        {
            Code = validationMetadata.Message
        };

        // 打印验证失败信息
        App.PrintToMiniProfiler("validation", "Failed", $"Validation Failed:\r\n\r\n{validationMetadata.Message}", true);

        return true;
    }
}