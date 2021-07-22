// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
                    await WriteAsJsonAsync(context, statusCode, "401 Unauthorized");
                    break;
                // 处理 403 状态码
                case StatusCodes.Status403Forbidden:
                    await WriteAsJsonAsync(context, statusCode, "403 Forbidden");
                    break;

                default: break;
            }
        }

        /// <summary>
        /// 写入响应 Json
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static async Task WriteAsJsonAsync(HttpContext context, int statusCode, string message)
        {
            await context.Response.WriteAsJsonAsync(new RESTfulResult<object>
            {
                StatusCode = statusCode,
                Succeeded = false,
                Data = null,
                Errors = message,
                Extras = UnifyContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            }, App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
        }
    }
}