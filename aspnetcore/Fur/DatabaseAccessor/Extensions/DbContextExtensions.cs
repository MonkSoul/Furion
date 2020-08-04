using Autofac;
using Fur.AppCore;
using Fur.Attributes;
using Fur.DatabaseAccessor.Options;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 数据库上下文拓展类
    /// </summary>
    [NonInflated]
    public static class DbContextExtensions
    {
        /// <summary>
        /// 获取租户Id
        /// <para>主要用于查询筛选器</para>
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns></returns>
        public static Guid? GetTenantId(this DbContext dbContext)
        {
            if (!App.SupportedMultipleTenant || App.MultipleTenantOptions != FurMultipleTenantOptions.OnTable) return default;

            var lifetimeScope = dbContext.GetService<ILifetimeScope>();
            var tenantProvider = lifetimeScope.Resolve<IMultipleTenantOnTableProvider>();

            return tenantProvider.GetTenantId();
        }

        /// <summary>
        /// 基于数据库的多租户配置
        /// </summary>
        /// <param name="optionsBuilder">数据库上下文选项配置构建器，参见：<see cref="DbContextOptionsBuilder"/></param>
        /// <param name="lifetimeScope">autofac 生命周期对象</param>
        public static void UseSqlServerWithMultipleTenantOnDatabase(this DbContextOptionsBuilder optionsBuilder, ILifetimeScope lifetimeScope)
        {
            if (App.SupportedMultipleTenant && App.MultipleTenantOptions == FurMultipleTenantOptions.OnDatabase)
            {
                var multipleTenantOnDatabaseProvider = lifetimeScope.Resolve<IMultipleTenantOnDatabaseProvider>();
                optionsBuilder.UseSqlServer(multipleTenantOnDatabaseProvider.GetConnectionString(), options =>
                {
                    options.MigrationsAssembly("Fur.Database.Migrations");
                });
            }
        }
    }
}