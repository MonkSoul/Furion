using Fur.Mvc.Attributes;
using Fur.Mvc.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var isAnonymouseRequest = descriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), false) || descriptor.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute), false);
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
                var errorInfo = context.ModelState.Keys.SelectMany(key => context.ModelState[key].Errors.Select(x => new { Field = key, x.ErrorMessage }));
                MiniProfiler.Current.CustomTiming("validation", "Validation Fail:\r\n" + JsonConvert.SerializeObject(errorInfo, Formatting.Indented), "Fail !").Errored = true;

                context.Result = new JsonResult(new UnifyResult()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Errors = new
                    {
                        Remark = "Validation Fail!",
                        Results = errorInfo
                    },
                    Successed = false,
                    Results = null,
                    UnAuthorizedRequest = isAnonymouseRequest || Convert.ToBoolean(context.HttpContext.Response.Headers["UnAuthorizedRequest"])
                });

                await Task.CompletedTask;
                return;
            }

            MiniProfiler.Current.CustomTiming("validation", "Validation Success", "Success");
            await next();
        }
    }
}
