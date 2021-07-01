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

using Furion.DataValidation;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.UnifyResult
{
    /// <summary>
    /// RESTful 风格返回值
    /// </summary>
    [SuppressSniffer, UnifyModel(typeof(RESTfulResult<>))]
    public class RESTfulResultProvider : IUnifyResultProvider
    {
        /// <summary>
        /// 异常返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult OnException(ExceptionContext context)
        {
            // 解析异常信息
            var (StatusCode, _, Errors) = UnifyContext.GetExceptionMetadata(context);

            return new JsonResult(new RESTfulResult<object>
            {
                StatusCode = StatusCode,
                Succeeded = false,
                Data = null,
                Errors = Errors,
                Extras = UnifyContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }

        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult OnSucceeded(ActionExecutedContext context)
        {
            object data;
            // 处理内容结果
            if (context.Result is ContentResult contentResult) data = contentResult.Content;
            // 处理对象结果
            else if (context.Result is ObjectResult objectResult) data = objectResult.Value;
            else if (context.Result is EmptyResult) data = null;
            else return null;

            return new JsonResult(new RESTfulResult<object>
            {
                StatusCode = context.Result is EmptyResult ? StatusCodes.Status204NoContent : StatusCodes.Status200OK,  // 处理没有返回值情况 204
                Succeeded = true,
                Data = data,
                Errors = null,
                Extras = UnifyContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }

        /// <summary>
        /// 验证失败返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelStates"></param>
        /// <param name="validationResults"></param>
        /// <param name="validateFailedMessage"></param>
        /// <returns></returns>
        public IActionResult OnValidateFailed(ActionExecutingContext context, ModelStateDictionary modelStates, IEnumerable<ValidateFailedModel> validationResults, string validateFailedMessage)
        {
            return new JsonResult(new RESTfulResult<object>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Succeeded = false,
                Data = null,
                Errors = validationResults,
                Extras = UnifyContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }

        /// <summary>
        /// 处理输出状态码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultStatusCodesOptions options)
        {
            // 设置响应状态码
            UnifyContext.SetResponseStatusCodes(context, statusCode, options);

            switch (statusCode)
            {
                // 处理 401 状态码
                case StatusCodes.Status401Unauthorized:
                    await context.Response.WriteAsJsonAsync(new RESTfulResult<object>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Succeeded = false,
                        Data = null,
                        Errors = "401 Unauthorized",
                        Extras = UnifyContext.Take(),
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    }, App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;
                // 处理 403 状态码
                case StatusCodes.Status403Forbidden:
                    await context.Response.WriteAsJsonAsync(new RESTfulResult<object>
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Succeeded = false,
                        Data = null,
                        Errors = "403 Forbidden",
                        Extras = UnifyContext.Take(),
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    }, App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;

                default:
                    break;
            }
        }
    }
}