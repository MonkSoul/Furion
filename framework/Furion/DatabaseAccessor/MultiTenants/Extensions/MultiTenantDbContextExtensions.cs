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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.DatabaseAccessor.Extensions
{
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
}