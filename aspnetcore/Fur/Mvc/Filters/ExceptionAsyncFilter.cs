using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Profiling;
using System.Threading.Tasks;

namespace Fur.Mvc.Filters
{
    public class ExceptionAsyncFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            MiniProfiler.Current.CustomTiming("Errors", context.Exception.ToString());

            context.Result = new ContentResult() { Content = context.Exception.ToString(), StatusCode = StatusCodes.Status500InternalServerError };
            return Task.CompletedTask;
        }
    }
}
