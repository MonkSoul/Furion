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
        // 跳过验证类型
        var nonValidationAttributeType = typeof(NonValidationAttribute);
        var method = context.HandlerMethod.MethodInfo;

        // 获取验证状态
        var modelState = context.ModelState;

        // 如果参数为 0或贴了 [NonValidation] 特性 或所在类型贴了 [NonValidation] 特性或验证成功或已经设置了结果，则跳过验证
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
        var method = context.HandlerMethod.MethodInfo;

        dynamic finalContext = resultContext != null ? resultContext : context;

        // 解析验证消息
        var validationMetadata = ValidatorContext.GetValidationMetadata(errors);
        validationMetadata.ErrorCode = friendlyException?.ErrorCode;
        validationMetadata.OriginErrorCode = friendlyException?.OriginErrorCode;
        validationMetadata.StatusCode = friendlyException?.StatusCode;

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