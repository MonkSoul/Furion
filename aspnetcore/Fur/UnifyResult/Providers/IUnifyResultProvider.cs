using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Fur.UnifyResult.Providers
{
    public interface IUnifyResultProvider
    {
        IActionResult UnifyExceptionResult(ExceptionContext context, string exceptionMessage, string exceptionErrorString, bool unAuthorizedRequest, int statusCode);

        IActionResult UnifyValidateFailResult(ActionExecutingContext context, object errorInfo, bool unAuthorizedRequest);

        IActionResult UnifySuccessResult(ResultExecutingContext context, bool isReturnEmpty, bool unAuthorizedRequest, ObjectResult objectResult = null);

        Task UnifyStatusCodeResult(HttpContext context, int statusCode);
    }
}
