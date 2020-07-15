using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Fur.Mvc.Filters
{
    public class UnifyResultAsyncActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}
