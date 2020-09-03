// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// EF Core仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity> : EFCoreRepository<TEntity, DbContextLocator>, IRepository<TEntity>
        where TEntity : class, IEntityBase, new()
    {
        public EFCoreRepository(IDbContextPool dbContextPool, IRepository repository, Func<Type, DbContext> dbContextResolve)
            : base(dbContextPool, repository, dbContextResolve)
        {
        }
    }

    /// <summary>
    /// 非泛型EF Core仓储实现
    /// </summary>
    public partial class EFCoreRepository : IRepository
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public EFCoreRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual IRepository<TEntity> Use<TEntity>()
            where TEntity : class, IEntityBase, new()
        {
            return _serviceProvider.GetService<IRepository<TEntity>>();
        }

        /// <summary>
        /// 切换多数据库上下文仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>仓储</returns>
        public virtual IRepository<TEntity, TDbContextLocator> Use<TEntity, TDbContextLocator>()
            where TEntity : class, IEntityBase, new()
            where TDbContextLocator : class, IDbContextLocator, new()
        {
            return _serviceProvider.GetService<IRepository<TEntity, TDbContextLocator>>();
        }
    }

    /// <summary>
    /// 多数据库上下文仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator> : IRepository<TEntity, TDbContextLocator>
        where TEntity : class, IEntityBase, new()
        where TDbContextLocator : class, IDbContextLocator, new()
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextPool">数据库上下文池</param>
        /// <param name="repository">非泛型仓储</param>
        /// <param name="dbContextResolve">数据库上下文解析器</param>
        public EFCoreRepository(IDbContextPool dbContextPool, IRepository repository, Func<Type, DbContext> dbContextResolve)
        {
            // 解析数据库上下文
            var dbContext = dbContextResolve(typeof(TDbContextLocator));

            _dbContextPool = dbContextPool;
            // 保存当前数据库上下文到池中
            _dbContextPool.AddToPool(dbContext);

            // 非泛型仓储
            _repository = repository;

            // 初始化数据库相关数据
            DbContext = dbContext;
            Database = dbContext.Database;
            DbConnection = Database.GetDbConnection();
            ChangeTracker = dbContext.ChangeTracker;

            //初始化实体
            Entities = dbContext.Set<TEntity>();
            DerailEntities = Entities.AsNoTracking();
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual DbContext DbContext { get; }

        /// <summary>
        /// 实体集合
        /// </summary>
        public virtual DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 不跟踪的（脱轨）实体
        /// </summary>
        public virtual IQueryable<TEntity> DerailEntities { get; }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public virtual DatabaseFacade Database { get; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public virtual DbConnection DbConnection { get; }

        /// <summary>
        /// 实体追综器
        /// </summary>
        public virtual ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; }

        /// <summary>
        /// 判断上下文是否更改
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool HasChanges()
        {
            return ChangeTracker.HasChanges();
        }

        /// <summary>
        /// 将实体加入数据上下文托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry Entry(object entity)
        {
            return DbContext.Entry(entity);
        }

        /// <summary>
        /// 将实体加入数据上下文托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Entry(TEntity entity)
        {
            return DbContext.Entry(entity);
        }

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual EntityState EntityEntryState(object entity)
        {
            return Entry(entity).State;
        }

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityState</returns>
        public virtual EntityState EntityEntryState(TEntity entity)
        {
            return Entry(entity).State;
        }

        /// <summary>
        /// 将实体属性加入托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>PropertyEntry</returns>
        public virtual PropertyEntry EntityPropertyEntry(object entity, string propertyName)
        {
            return Entry(entity).Property(propertyName);
        }

        /// <summary>
        /// 将实体属性加入托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>PropertyEntry</returns>
        public virtual PropertyEntry EntityPropertyEntry(TEntity entity, string propertyName)
        {
            return Entry(entity).Property(propertyName);
        }

        /// <summary>
        /// 将实体属性加入托管
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicate">属性表达式</param>
        /// <returns>PropertyEntry</returns>
        public virtual PropertyEntry<TEntity, TProperty> EntityPropertyEntry<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyPredicate)
        {
            return Entry(entity).Property(propertyPredicate);
        }

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public virtual bool IsAttach(object entity)
        {
            return EntityEntryState(entity) == EntityState.Detached;
        }

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public virtual bool IsAttach(TEntity entity)
        {
            return EntityEntryState(entity) == EntityState.Detached;
        }

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry Attach(object entity)
        {
            return DbContext.Attach(entity);
        }

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry<TEntity> Attach(TEntity entity)
        {
            return DbContext.Attach(entity);
        }

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void AttachRange(params object[] entities)
        {
            DbContext.AttachRange(entities);
        }

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void AttachRange(IEnumerable<TEntity> entities)
        {
            DbContext.AttachRange(entities);
        }

        /// <summary>
        /// 获取所有数据库上下文
        /// </summary>
        /// <returns>ConcurrentBag<DbContext></returns>
        public ConcurrentBag<DbContext> GetDbContexts()
        {
            return _dbContextPool.GetDbContexts();
        }

        /// <summary>
        /// 判断实体是否设置了主键
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public virtual bool IsKeySet(TEntity entity)
        {
            return Entry(entity).IsKeySet;
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>IQueryable<TEntity></returns>
        public virtual IQueryable<TEntity> AsQueryable(bool noTracking = false)
        {
            return !noTracking ? Entities : DerailEntities;
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>IQueryable<TEntity></returns>
        public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            var entities = AsQueryable(noTracking);
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (asSplitQuery) entities = entities.AsSplitQuery();
            if (predicate != null) entities = entities.Where(predicate);

            return entities;
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TUseEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual IRepository<TUseEntity> Use<TUseEntity>()
            where TUseEntity : class, IEntityBase, new()
        {
            return _repository.Use<TUseEntity>();
        }

        /// <summary>
        /// 切换多数据库上下文仓储
        /// </summary>
        /// <typeparam name="TUseEntity">实体类型</typeparam>
        /// <typeparam name="TUseDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>仓储</returns>
        public virtual IRepository<TUseEntity, TUseDbContextLocator> Use<TUseEntity, TUseDbContextLocator>()
            where TUseEntity : class, IEntityBase, new()
            where TUseDbContextLocator : class, IDbContextLocator, new()
        {
            return _repository.Use<TUseEntity, TUseDbContextLocator>();
        }
    }
}