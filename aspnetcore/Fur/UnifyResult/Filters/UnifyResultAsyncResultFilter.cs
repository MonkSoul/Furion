using Fur.UnifyResult.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Fur.UnifyResult.Filters
{
    public sealed class UnifyResultAsyncResultFilter : IAsyncResultFilter
    {
        private readonly IUnifyResultProvider _unifyResultProvider;

        public UnifyResultAsyncResultFilter(IUnifyResultProvider unifyResultProvider)
        {
            _unifyResultProvider = unifyResultProvider;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var isAnonymouseRequest = descriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), false) || descriptor.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute), false);
            var unAuthorizedRequest = isAnonymouseRequest || Convert.ToBoolean(context.HttpContext.Response.Headers["UnAuthorizedRequest"]);

            if (context.Result is EmptyResult)
            {
                context.Result = _unifyResultProvider.UnifySuccessResult(context, true, unAuthorizedRequest);
            }
            else
            {
                if (context.Result is ObjectResult objectResult)
                {
                    context.Result = _unifyResultProvider.UnifySuccessResult(context, objectResult == null, unAuthorizedRequest, objectResult);
                }
            }

            await next();
        }
    }
}