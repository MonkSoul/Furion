using Furion.DependencyInjection;
using Furion.UnifyResult;
using System;

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
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnifyResultStatusCodes(this IApplicationBuilder builder, Action<UnifyResultStatusCodesOptions> optionsBuilder = default)
        {
            // 提供配置
            var options = new UnifyResultStatusCodesOptions();
            optionsBuilder?.Invoke(options);

            builder.UseMiddleware<UnifyResultStatusCodesMiddleware>(options);

            return builder;
        }
    }
}