using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.Mvc.Filters
{
    public class ValidateModelAsyncFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new
                {
                    Status = 400,
                    Error = context.ModelState.Keys.SelectMany(key => context.ModelState[key].Errors.Select(x => x))
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                await Task.CompletedTask;
                return;
            }

            await next();
        }
    }
}
