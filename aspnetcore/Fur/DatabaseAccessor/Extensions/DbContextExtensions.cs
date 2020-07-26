using Autofac;
using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 数据库上下文拓展类
    /// </summary>
    [NonWrapper]
    public static class DbContextExtensions
    {
        #region 获取租户Id + public static int GetTenantId(this DbContext dbContext)
        /// <summary>
        /// 获取租户Id
        /// <para>主要用于查询筛选器</para>
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns></returns>
        public static int GetTenantId(this DbContext dbContext)
        {
            var lifetimeScope = dbContext.GetService<ILifetimeScope>();
            if (!lifetimeScope.IsRegistered<IMultipleTenantProvider>()) return default;

            var tenantProvider = lifetimeScope.Resolve<IMultipleTenantProvider>();

            return tenantProvider.GetTenantId();
        }
        #endregion
    }
}
