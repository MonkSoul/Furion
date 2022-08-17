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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.DatabaseAccessor.Extensions;

/// <summary>
/// 多租户数据库上下文拓展
/// </summary>
[SuppressSniffer]
public static class MultiTenantDbContextExtensions
{
    /// <summary>
    /// 刷新多租户缓存
    /// </summary>
    /// <param name="dbContext"></param>
    public static void RefreshTenantCache(this DbContext dbContext)
    {
        _ = dbContext;

        // 判断 HttpContext 是否存在
        var httpContext = App.HttpContext;
        if (httpContext == null) return;

        // 获取主机地址
        var host = httpContext.Request.Host.Value;

        // 获取服务提供器
        var serviceProvider = httpContext.RequestServices;

        // 缓存的 Key
        var tenantCachedKey = $"MULTI_TENANT:{host}";

        // 从内存缓存中移除多租户信息
        var distributedCache = serviceProvider.GetService<IDistributedCache>();
        distributedCache.Remove(tenantCachedKey);
    }
}