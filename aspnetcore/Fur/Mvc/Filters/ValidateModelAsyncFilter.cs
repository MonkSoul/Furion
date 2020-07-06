using Fur.Mvc.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StackExchange.Profiling;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.Mvc.Filters
{
    public class ValidateModelAsyncFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var methodInfo = controllerActionDescriptor.MethodInfo;

            if (methodInfo.IsDefined(typeof(NotVaildateAttribute)) || methodInfo.DeclaringType.IsDefined(typeof(NotVaildateAttribute)))
            {
                MiniProfiler.Current.CustomTiming("validation", "Validation Disable", "Disable !");
                await next();
                return;
            }

            MiniProfiler.Current.CustomTiming("validation", "Validation Enable", "Enable");
            if (!context.ModelState.IsValid)
            {
                var errorInfo = context.ModelState.Keys.SelectMany(key => context.ModelState[key].Errors.Select(x => x));
                MiniProfiler.Current.CustomTiming("validation", "Validation Fail:\r\n" + JsonConvert.SerializeObject(errorInfo, Formatting.Indented), "Fail !");
                context.Result = new JsonResult(new
                {
                    Status = 400,
                    Error = errorInfo
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                await Task.CompletedTask;
                return;
            }

            MiniProfiler.Current.CustomTiming("validation", "Validation Success", "Success");
            await next();
        }
    }
}
