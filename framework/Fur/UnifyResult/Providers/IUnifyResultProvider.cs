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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Fur.UnifyResult
{
    /// <summary>
    /// 规范化结果提供器
    /// </summary>
    public interface IUnifyResultProvider
    {
        /// <summary>
        /// 异常返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IActionResult OnException(ExceptionContext context);

        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IActionResult OnSuccessed(ActionExecutedContext context);

        /// <summary>
        /// 验证失败返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelStates"></param>
        /// <param name="validationResults"></param>
        /// <param name="validateFaildMessage"></param>
        /// <returns></returns>
        IActionResult OnValidateFailed(ActionExecutingContext context, ModelStateDictionary modelStates, Dictionary<string, IEnumerable<string>> validationResults, string validateFaildMessage);
    }
}