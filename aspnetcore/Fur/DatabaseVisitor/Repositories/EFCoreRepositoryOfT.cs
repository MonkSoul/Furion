using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Contexts;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        /// <summary>
        /// 维护字段提供器
        /// </summary>
        private readonly IMaintenanceFieldsProvider _maintenanceProvider;

        /// <summary>
        /// 租户提供器
        /// </summary>
        private readonly ITenantProvider _tenantProvider;

        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        #region 构造函数 + public EFCoreRepositoryOfT(DbContext dbContext , IServiceProvider serviceProvider, ITenantProvider tenantProvider, IDbContextPool dbContextPool)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据库操作上下文</param>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="tenantProvider">租户提供器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public EFCoreRepositoryOfT(DbContext dbContext
            , IServiceProvider serviceProvider
            , ITenantProvider tenantProvider
            , IDbContextPool dbContextPool)
        {
            _tenantProvider = tenantProvider;
            _dbContextPool = dbContextPool;

            DbContext = dbContext;
            Entity = DbContext.Set<TEntity>();
            _dbContextPool.SaveDbContext(DbContext);

            var autofacContainer = serviceProvider.GetAutofacRoot();
            if (autofacContainer.IsRegistered<IMaintenanceFieldsProvider>())
            {
                _maintenanceProvider = autofacContainer.Resolve<IMaintenanceFieldsProvider>();
            }
        }
        #endregion

        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        public virtual DbContext DbContext { get; }

        /// <summary>
        /// 实体对象
        /// </summary>
        public virtual DbSet<TEntity> Entity { get; }

        /// <summary>
        /// 不跟踪的（脱轨）实体
        /// </summary>
        public virtual IQueryable<TEntity> DerailEntity => Entity.AsNoTracking();

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public virtual DatabaseFacade Database => DbContext.Database;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public virtual DbConnection DbConnection => DbContext.Database.GetDbConnection();

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual int TenantId => _tenantProvider.GetTenantId();
    }
}