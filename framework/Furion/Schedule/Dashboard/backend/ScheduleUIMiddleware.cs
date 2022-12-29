// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

namespace Furion.Schedule;

/// <summary>
/// Schedule 模块 UI 中间件
/// </summary>
[SuppressSniffer]
public sealed class ScheduleUIMiddleware
{
    /// <summary>
    /// 请求委托
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="rootPath">API 请求路径</param>
    public ScheduleUIMiddleware(RequestDelegate next
        , string rootPath)
    {
        _next = next;
        ApiRequestPath = rootPath;
    }

    /// <summary>
    /// API 请求路径
    /// </summary>
    private string ApiRequestPath { get; set; }

    /// <summary>
    /// 中间件执行方法
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <returns><see cref="Task"/></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 如果不是以 ApiRequestPath 开头，则跳过
        if (!context.Request.Path.StartsWithSegments(ApiRequestPath))
        {
            await _next(context);
            return;
        }

        // 获取匹配的路由标识
        var action = context.Request.Path.Value?[ApiRequestPath.Length..]?.ToLower();

        // 路由匹配
        switch (action)
        {
            case "/get-jobs":
                await context.Response.WriteAsync("get-jobs");
                break;
        }
    }
}