using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.MultipleTenants.Options;
using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseAccessor.Contexts.Options
{
    /// <summary>
    /// 数据库上下文配置选项
    /// </summary>
    [NonWrapper]
    public sealed class FurDbContextConfigureOptions
    {
        /// <summary>
        /// 多租户配置选项
        /// </summary>
        internal FurMultipleTenantConfigureOptions MultipleTenantConfigureOptions { get; private set; } = FurMultipleTenantConfigureOptions.None;

        /// <summary>
        /// 多租户提供器
        /// </summary>
        internal Type MultipleTenantProvider { get; private set; }

        /// <summary>
        /// 多租户数据库上下文
        /// </summary>
        internal Type MultipleTenantDbContext { get; private set; }

        /// <summary>
        /// 是否支持切面上下文
        /// <para>默认true：支持</para>
        /// </summary>
        public bool SupportTangent { get; set; } = true;

        #region 配置多租户 + public void AddMultipleTenantConfigure<TMultipleTenantDbContext, TMultipleTenantProvider>()
        /// <summary>
        /// 配置多租户
        /// </summary>
        /// <typeparam name="TMultipleTenantDbContext">多租户数据库上下文</typeparam>
        /// <typeparam name="TMultipleTenantProvider">多租户提供器</typeparam>
        public void AddMultipleTenantConfigure<TMultipleTenantDbContext, TMultipleTenantProvider>()
            where TMultipleTenantDbContext : DbContext
            where TMultipleTenantProvider : IMultipleTenantProvider
        {
            var multipleTenantProvider = typeof(TMultipleTenantProvider);

            // 不允许注册空多租户提供器
            if (multipleTenantProvider == typeof(IMultipleTenantProvider)) return;

            this.MultipleTenantProvider = multipleTenantProvider;
            this.MultipleTenantDbContext = typeof(TMultipleTenantDbContext);

            // 注册基于表的多租户实现提供器
            if (typeof(IMultipleTenantOnTableProvider).IsAssignableFrom(multipleTenantProvider))
            {
                this.MultipleTenantConfigureOptions = FurMultipleTenantConfigureOptions.OnTable;
            }
            // 注册基于架构的多租户实现提供器
            else if (typeof(IMultipleTenantOnSchemaProvider).IsAssignableFrom(multipleTenantProvider))
            {
                this.MultipleTenantConfigureOptions = FurMultipleTenantConfigureOptions.OnSchema;
            }
            // 注册基于数据库的多租户实现提供器
            else if (typeof(IMultipleTenantOnDatabaseProvider).IsAssignableFrom(multipleTenantProvider))
            {
                this.MultipleTenantConfigureOptions = FurMultipleTenantConfigureOptions.OnDatabase;
            }
            else throw new NotSupportedException($"{multipleTenantProvider}");
        }
        #endregion

        /// <summary>
        /// 支持多数据库上下文
        /// </summary>
        public bool SupportMultipleDbContext { get; set; } = true;

        /// <summary>
        /// 支持主从库数据库上下文
        /// </summary>
        public bool SupportMasterSlaveDbContext { get; set; } = true;
    }
}
