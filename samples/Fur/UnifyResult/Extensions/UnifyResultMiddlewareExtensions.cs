using Fur.DependencyInjection;
using Fur.UnifyResult;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 状态码中间件拓展
    /// </summary>
    [SkipScan]
    public static class UnifyResultMiddlewareExtensions
    {
        /// <summary>
        /// 添加状态码拦截中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnifyResultStatusCodes(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<UnifyResultStatusCodesMiddleware>();

            return builder;
        }
    }
}