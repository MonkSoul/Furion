using Fur.Mvc.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Fur.Mvc.Extensions.Middlewares
{
    public static class StatusCodesResultMiddlewareExtensions
    {
        public static IApplicationBuilder UseFurUnifyStatusCodesResult(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<UnifyStatusCodesResultMiddleware>();
        }
    }
}
