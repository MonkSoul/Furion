using Fur.AppCore.Attributes;
using Fur.UnifyResult.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Fur.UnifyResult.Extensions
{
    [NonWrapper]
    public static class StatusCodesResultMiddlewareExtensions
    {
        public static IApplicationBuilder UseFurUnifyStatusCodesResult(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<UnifyStatusCodesResultMiddleware>();
        }
    }
}