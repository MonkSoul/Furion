using Fur.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fur.UnifyResult
{
    /// <summary>
    /// 状态码中间件
    /// </summary>
    [SkipScan]
    public class UnifyResultStatusCodesMiddleware
    {
        /// <summary>
        /// 请求委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public UnifyResultStatusCodesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 中间件执行方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            // 处理规范化结果
            if (!UnifyResultContext.IsSkipUnifyHandler(context, out var unifyResult))
            {
                await unifyResult.OnResponseStatusCodes(context, context.Response.StatusCode);
            }
        }
    }
}