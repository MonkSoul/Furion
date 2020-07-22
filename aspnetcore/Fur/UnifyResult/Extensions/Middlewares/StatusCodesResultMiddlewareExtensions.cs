using Fur.UnifyResult.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Fur.UnifyResult.Extensions.Middlewares
{
    public static class StatusCodesResultMiddlewareExtensions
    {
        public static IApplicationBuilder UseFurUnifyStatusCodesResult(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<UnifyStatusCodesResultMiddleware>();
        }
    }
}
