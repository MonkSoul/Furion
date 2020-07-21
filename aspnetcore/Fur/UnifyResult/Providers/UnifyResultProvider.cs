using Fur.Mvc.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Profiling;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Fur.UnifyResult.Providers
{
    public class UnifyResultProvider : IUnifyResultProvider
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

        public async Task UnifyStatusCodeResult(HttpContext context, int statusCode)
        {
            if (statusCode == StatusCodes.Status401Unauthorized)
            {
                var errorMsg = "401 Unauthorized";
                MiniProfiler.Current.CustomTiming("authorize", errorMsg, "Unauthorized").Errored = true;

                await HandleInvaildStatusCode(context, statusCode, errorMsg);

            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                var errorMsg = "403 Forbidden";
                MiniProfiler.Current.CustomTiming("authorize", errorMsg, "Forbidden").Errored = true;

                await HandleInvaildStatusCode(context, statusCode, errorMsg);
            }
        }


        private Task HandleInvaildStatusCode(HttpContext context, int statusCode, string responseMessage)
        {
            responseMessage = JsonConvert.SerializeObject(new RESTfulResult()
            {
                StatusCode = statusCode,
                Results = null,
                Successed = false,
                Errors = responseMessage,
                UnAuthorizedRequest = false
            }, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            context.Response.ContentType = MediaTypeNames.Application.Json;

            return context.Response.WriteAsync(responseMessage);
        }
    }
}
