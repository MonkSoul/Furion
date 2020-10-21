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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Fur.UnifyResult
{
    /// <summary>
    /// RESTful 风格返回值
    /// </summary>
    public class RESTfulResultProvider : IUnifyResultProvider
    {
        /// <summary>
        /// 异常返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult OnException(ExceptionContext context)
        {
            return new JsonResult(new RESTfulResult
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Successed = false,
                Data = null,
                Errors = context.Exception.Message
            });
        }

        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult OnSuccessed(ActionExecutedContext context)
        {
            object data;
            // 处理内容结果
            if (context.Result is ContentResult contentResult) data = contentResult.Content;
            // 处理对象结果
            else if (context.Result is ObjectResult objectResult) data = objectResult.Value;
            else return null;

            return new JsonResult(new RESTfulResult
            {
                StatusCode = context.Result is EmptyResult ? StatusCodes.Status204NoContent : StatusCodes.Status200OK,
                Successed = true,
                Data = data,
                Errors = null
            });
        }

        /// <summary>
        /// 验证失败返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelStates"></param>
        /// <param name="validationResults"></param>
        /// <param name="validateFaildMessage"></param>
        /// <returns></returns>
        public IActionResult OnValidateFailed(ActionExecutingContext context, ModelStateDictionary modelStates, Dictionary<string, IEnumerable<string>> validationResults, string validateFaildMessage)
        {
            return new JsonResult(new RESTfulResult
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Successed = false,
                Data = null,
                Errors = validationResults
            });
        }
    }
}