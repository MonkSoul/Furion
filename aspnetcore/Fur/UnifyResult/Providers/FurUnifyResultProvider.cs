using Fur.UnifyResult.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fur.UnifyResult.Providers
{
    public class FurUnifyResultProvider : IUnifyResultProvider
    {
        public IActionResult UnifyExceptionResult(ExceptionContext context, string exceptionMessage, string exceptionErrorString, bool unAuthorizedRequest, int statusCode)
        {
            return new JsonResult(new RESTfulResult()
            {
                StatusCode = statusCode,
                Errors = exceptionMessage,
                Successed = false,
                Results = null,
                UnAuthorizedRequest = unAuthorizedRequest
            });
        }

        public IActionResult UnifySuccessResult(ResultExecutingContext context, bool isReturnEmpty, bool unAuthorizedRequest, ObjectResult objectResult = null)
        {
            if (isReturnEmpty)
            {
                return new JsonResult(new RESTfulResult()
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    Errors = null,
                    Successed = true,
                    Results = null,
                    UnAuthorizedRequest = unAuthorizedRequest
                });
            }
            else
            {
                var statusCodes = objectResult.StatusCode ?? StatusCodes.Status200OK;

                return new JsonResult(new RESTfulResult()
                {
                    StatusCode = statusCodes,
                    Errors = null,
                    Successed = statusCodes > StatusCodes.Status200OK && statusCodes < StatusCodes.Status300MultipleChoices,
                    Results = objectResult.Value,
                    UnAuthorizedRequest = unAuthorizedRequest
                });
            }
        }

        public IActionResult UnifyValidateFailResult(ActionExecutingContext context, object errorInfo, bool unAuthorizedRequest)
        {
            return new JsonResult(new RESTfulResult()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = new
                {
                    Remark = "Validation Fail!",
                    Results = errorInfo
                },
                Successed = false,
                Results = null,
                UnAuthorizedRequest = unAuthorizedRequest
            });
        }
    }
}