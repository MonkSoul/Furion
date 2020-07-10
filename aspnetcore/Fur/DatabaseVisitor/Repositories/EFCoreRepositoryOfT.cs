using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Contexts;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Provider;
using Fur.DatabaseVisitor.TenantSaaS;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        private readonly IMaintenanceProvider _maintenanceProvider;
        private readonly ITenantProvider _tenantProvider;
        private readonly IDbContextPool _dbContextPool;

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
            if (autofacContainer.IsRegistered<IMaintenanceProvider>())
            {
                _maintenanceProvider = autofacContainer.Resolve<IMaintenanceProvider>();
            }
        }

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
        public virtual IQueryable<TEntity> DerailEntity => DerailEntity;

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