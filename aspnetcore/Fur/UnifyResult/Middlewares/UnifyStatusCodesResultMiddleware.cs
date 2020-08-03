using Fur.AppCore.Attributes;
using Fur.UnifyResult.Providers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fur.UnifyResult.Middlewares
{
    [NonInflated]
    public class UnifyStatusCodesResultMiddleware
    {
        private readonly RequestDelegate _next;

        public UnifyStatusCodesResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnifyResultProvider unifyResultProvider)
        {
            await _next(context);
            var statusCode = context.Response.StatusCode;
            await unifyResultProvider.UnifyStatusCodeResult(context, statusCode);
        }
    }
}