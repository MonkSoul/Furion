using Fur.Mvc.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Fur.Mvc.Extensions.Middlewares
{
    public static class UnifyResultMiddlewareExtensions
    {
        public static IApplicationBuilder UseFurUnifyResult(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<Unify400Or403ResultMiddleware>();
        }
    }
}
