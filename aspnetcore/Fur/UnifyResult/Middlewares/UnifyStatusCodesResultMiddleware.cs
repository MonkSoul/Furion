using Fur.AppBasic.Attributes;
using Fur.UnifyResult.Providers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fur.UnifyResult.Middlewares
{
    [NonWrapper]
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