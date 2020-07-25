using Autofac;
using Fur.DatabaseAccessor.Contexts.Pools;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;
using System.Linq;

namespace Fur.DatabaseAccessor.Repositories
{
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>
        where TEntity : class, IDbEntity, new()
    {
        /// <summary>
        /// 维护字段提供器
        /// </summary>
        private readonly IMaintenanceFieldsProvider _maintenanceProvider;

        /// <summary>
        /// 租户提供器
        /// </summary>
        private readonly IMultipleTenantProvider _tenantProvider;

        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        #region 构造函数 + public EFCoreRepositoryOfT(DbContext dbContext , ILifetimeScope lifetimeScope, IDbContextPool dbContextPool)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据库操作上下文</param>
        /// <param name="ILifetimeScope">Autofac生命周期对象</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public EFCoreRepositoryOfT(DbContext dbContext
            , ILifetimeScope lifetimeScope
            , IDbContextPool dbContextPool)
        {
            _dbContextPool = dbContextPool;

            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
            _dbContextPool.SaveDbContext(DbContext);

            if (lifetimeScope.IsRegistered<IMaintenanceFieldsProvider>())
            {
                _maintenanceProvider = lifetimeScope.Resolve<IMaintenanceFieldsProvider>();
            }
            if (lifetimeScope.IsRegistered<IMultipleTenantProvider>())
            {
                _tenantProvider = lifetimeScope.Resolve<IMultipleTenantProvider>();
            }
        }

        #endregion 构造函数 + public EFCoreRepositoryOfT(DbContext dbContext , ILifetimeScope lifetimeScope, IDbContextPool dbContextPool)

        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        public virtual DbContext DbContext { get; }

        /// <summary>
        /// 实体对象
        /// </summary>
        public virtual DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 不跟踪的（脱轨）实体
        /// </summary>
        public virtual IQueryable<TEntity> DerailEntities => Entities.AsNoTracking();

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
        public virtual int? TenantId => _tenantProvider?.GetTenantId();
    }
}