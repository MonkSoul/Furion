using Fur.Mvc.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Fur.Mvc.Filters
{
    public class UnifyResultAsyncResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var isAnonymouseRequest = descriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), false) || descriptor.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute), false);

            if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new UnifyResult()
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    Errors = null,
                    Successed = true,
                    Results = null,
                    UnAuthorizedRequest = isAnonymouseRequest || Convert.ToBoolean(context.HttpContext.Response.Headers["UnAuthorizedRequest"])
                });
            }
            else
            {
                if (context.Result is ObjectResult objectResult)
                {
                    var statusCodes = objectResult.StatusCode ?? StatusCodes.Status200OK;

                    context.Result = new JsonResult(new UnifyResult()
                    {
                        StatusCode = statusCodes,
                        Errors = null,
                        Successed = statusCodes > StatusCodes.Status200OK && statusCodes < StatusCodes.Status300MultipleChoices,
                        Results = objectResult.Value,
                        UnAuthorizedRequest = isAnonymouseRequest || Convert.ToBoolean(context.HttpContext.Response.Headers["UnAuthorizedRequest"])
                    });
                }
            }

            await next();
        }
    }
}
