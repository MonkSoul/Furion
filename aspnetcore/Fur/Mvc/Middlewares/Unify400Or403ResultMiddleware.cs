using Fur.UnifyResult.Providers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fur.Mvc.Middlewares
{
    public class Unify400Or403ResultMiddleware
    {
        private readonly RequestDelegate _next;

        public Unify400Or403ResultMiddleware(RequestDelegate next)
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
