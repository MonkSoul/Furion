// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Furion.UnifyResult
{
    /// <summary>
    /// 状态码中间件
    /// </summary>
    [SuppressSniffer]
    public class UnifyResultStatusCodesMiddleware
    {
        /// <summary>
        /// 请求委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 配置选项
        /// </summary>
        private readonly UnifyResultStatusCodesOptions _options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public UnifyResultStatusCodesMiddleware(RequestDelegate next, UnifyResultStatusCodesOptions options)
        {
            _next = next;
            _options = options;
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
            if (!UnifyContext.IsSkipUnifyHandlerOnSpecifiedStatusCode(context, out var unifyResult))
            {
                await unifyResult.OnResponseStatusCodes(context, context.Response.StatusCode, _options);
            }
        }
    }
}