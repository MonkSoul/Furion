using Furion.DataValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.UnifyResult
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
        IActionResult OnSucceeded(ActionExecutedContext context);

        /// <summary>
        /// 验证失败返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelStates"></param>
        /// <param name="validationResults"></param>
        /// <param name="validateFailedMessage"></param>
        /// <returns></returns>
        IActionResult OnValidateFailed(ActionExecutingContext context, ModelStateDictionary modelStates, IEnumerable<ValidateFailedModel> validationResults, string validateFailedMessage);

        /// <summary>
        /// 拦截返回状态码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        Task OnResponseStatusCodes(HttpContext context, int statusCode);
    }
}