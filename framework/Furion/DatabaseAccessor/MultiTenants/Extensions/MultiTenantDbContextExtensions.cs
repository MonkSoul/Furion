// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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