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
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Authorization;

/// <summary>
/// 授权处理上下文拓展类
/// </summary>
[SuppressSniffer]
public static class AuthorizationHandlerContextExtensions
{
    /// <summary>
    /// 获取当前 HttpContext 上下文
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static DefaultHttpContext GetCurrentHttpContext(this AuthorizationHandlerContext context)
    {
        DefaultHttpContext httpContext;

        // 获取 httpContext 对象
        if (context.Resource is AuthorizationFilterContext filterContext) httpContext = (DefaultHttpContext)filterContext.HttpContext;
        else if (context.Resource is DefaultHttpContext defaultHttpContext) httpContext = defaultHttpContext;
        else httpContext = null;

        return httpContext;
    }
}