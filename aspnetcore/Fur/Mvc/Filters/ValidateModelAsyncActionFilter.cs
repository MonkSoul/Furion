using Fur.Mvc.Attributes;
using Fur.UnifyResult.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StackExchange.Profiling;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.Mvc.Filters
{
    public class ValidateModelAsyncActionFilter : IAsyncActionFilter
    {
        private readonly IUnifyResultProvider _unifyResultProvider;
        public ValidateModelAsyncActionFilter(IUnifyResultProvider unifyResultProvider)
        {
            _unifyResultProvider = unifyResultProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var methodInfo = descriptor.MethodInfo;

            if (methodInfo.GetParameters().Length == 0 || methodInfo.IsDefined(typeof(NotVaildateAttribute)) || methodInfo.DeclaringType.IsDefined(typeof(NotVaildateAttribute)))
            {
                MiniProfiler.Current.CustomTiming("validation", "Validation Disable", "Disable !");
                await next();
                return;
            }

            MiniProfiler.Current.CustomTiming("validation", "Validation Enable", "Enable");
            if (!context.ModelState.IsValid)
            {
                var isAnonymouseRequest = descriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), false) || descriptor.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute), false);
                var unAuthorizedRequest = isAnonymouseRequest || Convert.ToBoolean(context.HttpContext.Response.Headers["UnAuthorizedRequest"]);
                var errorInfo = context.ModelState.Keys.SelectMany(key => context.ModelState[key].Errors.Select(x => new { Field = key, x.ErrorMessage }));

                MiniProfiler.Current.CustomTiming("validation", "Validation Fail:\r\n" + JsonConvert.SerializeObject(errorInfo, Formatting.Indented), "Fail !").Errored = true;

                context.Result = _unifyResultProvider.UnifyValidateFailResult(context, errorInfo, unAuthorizedRequest);

                await Task.CompletedTask;
                return;
            }

            MiniProfiler.Current.CustomTiming("validation", "Validation Success", "Success");
            await next();
        }
    }
}
