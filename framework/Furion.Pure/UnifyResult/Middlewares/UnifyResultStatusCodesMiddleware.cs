// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
        /// 是否拦截 404 状态码
        /// </summary>
        private readonly bool _intercept404StatusCodes;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <param name="intercept404StatusCodes"></param>
        public UnifyResultStatusCodesMiddleware(RequestDelegate next, UnifyResultStatusCodesOptions options, bool intercept404StatusCodes)
        {
            _next = next;
            _options = options;
            _intercept404StatusCodes = intercept404StatusCodes;
        }

        /// <summary>
        /// 中间件执行方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            // 只有请求错误（短路状态码）才支持规范化处理
            if (context.Response.StatusCode < 400) return;

            // 处理规范化结果
            if (!UnifyContext.CheckStatusCode(context, _intercept404StatusCodes, out var unifyResult))
            {
                await unifyResult.OnResponseStatusCodes(context, context.Response.StatusCode, _options);
            }
        }
    }
}