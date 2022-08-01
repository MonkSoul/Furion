// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Furion.UnifyResult;

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

        // 只有请求错误（短路状态码）才支持规范化处理
        if (context.Response.StatusCode < 400 || context.Response.StatusCode == 404) return;

        // 处理规范化结果
        if (!UnifyContext.CheckStatusCodeNonUnify(context, out var unifyResult))
        {
            await unifyResult.OnResponseStatusCodes(context, context.Response.StatusCode, context.RequestServices.GetService<IOptions<UnifyResultSettingsOptions>>()?.Value);
        }
    }
}