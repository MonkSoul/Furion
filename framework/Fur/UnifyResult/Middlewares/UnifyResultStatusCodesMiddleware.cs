using Fur.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="serviceProvider"></param>
        public UnifyResultStatusCodesMiddleware(RequestDelegate next
            , IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
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
            var unifyResult = _serviceProvider.GetService<IUnifyResultProvider>();
            if (unifyResult != null)
            {
                await unifyResult.OnResponseStatusCodes(context, context.Response.StatusCode);
            }
        }
    }
}