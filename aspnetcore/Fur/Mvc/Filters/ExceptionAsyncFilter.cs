using Fur.FriendlyException.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Profiling;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.Mvc.Filters
{
    public class ExceptionAsyncFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            ConvertExceptionInfo(context, descriptor, out string exceptionMessage, out string exceptionErrorString);

            context.Result = new ContentResult() { Content = exceptionErrorString, StatusCode = StatusCodes.Status500InternalServerError };

            MiniProfiler.Current.CustomTiming("Errors !", exceptionErrorString);
            return Task.CompletedTask;
        }

        private static void ConvertExceptionInfo(ExceptionContext context, ControllerActionDescriptor descriptor, out string exceptionMessage, out string exceptionErrorString)
        {
            exceptionMessage = context.Exception.Message;
            exceptionErrorString = context.Exception.ToString();
            var ifExceptionAttributes = descriptor.MethodInfo.GetCustomAttributes<IfExceptionAttribute>(false);
            if (ifExceptionAttributes != null && ifExceptionAttributes.Any())
            {
                if (exceptionMessage.StartsWith("##") && exceptionMessage.EndsWith("##"))
                {
                    var code = exceptionMessage[2..^2];
                    var exceptionConvert = ifExceptionAttributes.FirstOrDefault(u => u.ExceptionCode.ToString() == code);
                    if (exceptionConvert != null)
                    {
                        exceptionMessage = exceptionMessage.Replace($"##{code}##", $"[{code}] {exceptionConvert.ExceptionMessage}");
                        exceptionErrorString = exceptionErrorString.Replace($"##{code}##", $"[{code}] {exceptionConvert.ExceptionMessage}");
                    }
                }
            }
        }
    }
}
