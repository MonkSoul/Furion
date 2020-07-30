using Autofac;
using Fur.ApplicationBase;
using Fur.DatabaseAccessor.Contexts.Pools;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.MultipleTenants.Options;
using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Fur.DatabaseAccessor.Repositories.Interceptors;
using Fur.DatabaseAccessor.Repositories.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;

namespace Fur.DatabaseAccessor.Repositories
{
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 操作拦截器
        /// </summary>
        private readonly IDbEntityInterceptor _maintenanceInterceptor;

        /// <summary>
        /// 假删除提供器
        /// </summary>
        private readonly IFakeDeleteProvider _fakeDeleteProvider;

        /// <summary>
        /// 租户提供器
        /// </summary>
        private readonly IMultipleTenantOnTableProvider _multipleTenantOnTableProvider;

        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据库操作上下文</param>
        /// <param name="ILifetimeScope">Autofac生命周期对象</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public EFCoreRepository(DbContext dbContext
            , ILifetimeScope lifetimeScope
            , IDbContextPool dbContextPool)
        {
            _dbContextPool = dbContextPool;

            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
            _dbContextPool.SaveDbContext(DbContext);

            if (AppGlobal.SupportedMultipleTenant && AppGlobal.MultipleTenantConfigureOptions == FurMultipleTenantConfigureOptions.OnTable)
            {
                _multipleTenantOnTableProvider = lifetimeScope.Resolve<IMultipleTenantOnTableProvider>();
            }

            if (lifetimeScope.IsRegistered<IDbEntityInterceptor>())
            {
                _maintenanceInterceptor = lifetimeScope.Resolve<IDbEntityInterceptor>();
            }
            if (lifetimeScope.IsRegistered<IFakeDeleteProvider>())
            {
                _fakeDeleteProvider = lifetimeScope.Resolve<IFakeDeleteProvider>();
            }
        }

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
        public virtual Guid? TenantId => _multipleTenantOnTableProvider?.GetTenantId();
    }

    /// <summary>
    /// 非泛型仓储实现类
    /// </summary>
    public partial class EFCoreRepository : IRepository
    {
        /// <summary>
        /// Autofac生命周期对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ILifetimeScope">Autofac生命周期实例</param>
        public EFCoreRepository(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// 获取泛型仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepository{TEntity}"/></returns>
        public IRepository<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntityBase, new()
        {
            if (newScope)
            {
                return _lifetimeScope.BeginLifetimeScope().Resolve<IRepository<TEntity>>();
            }
            return _lifetimeScope.Resolve<IRepository<TEntity>>();
        }
    }
}