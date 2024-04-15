// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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