using Fur.Mvc.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Profiling;
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

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            var statusCode = context.Response.StatusCode;
            if (statusCode == StatusCodes.Status401Unauthorized)
            {
                var errorMsg = "401 Unauthorized";
                MiniProfiler.Current.CustomTiming("authorize", errorMsg, "Unauthorized").Errored = true;

                await HandleInvaildStatusCode(context, statusCode, errorMsg);

            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                var errorMsg = "403 Forbidden";
                MiniProfiler.Current.CustomTiming("authorize", errorMsg, "Forbidden").Errored = true;

                await HandleInvaildStatusCode(context, statusCode, errorMsg);
            }

        }

        private Task HandleInvaildStatusCode(HttpContext context, int statusCode, string responseMessage)
        {
            responseMessage = JsonConvert.SerializeObject(new UnifyResult()
            {
                StatusCode = statusCode,
                Results = null,
                Successed = false,
                Errors = responseMessage,
                UnAuthorizedRequest = false
            });

            context.Response.ContentType = "application/json;charset=utf-8";

            return context.Response.WriteAsync(responseMessage);
        }
    }
}
