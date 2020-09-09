// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.FriendlyException;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// EF Core仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity> : EFCoreRepository<TEntity, DbContextLocator>, IRepository<TEntity>
        where TEntity : class, IEntityBase, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextResolve">数据库上下文解析器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        /// <param name="repository">非泛型仓储</param>
        /// <param name="serviceProvider">服务提供器</param>
        public EFCoreRepository(
            Func<Type, DbContext> dbContextResolve
            , IDbContextPool dbContextPool
            , IRepository repository
            , IServiceProvider serviceProvider) : base(dbContextResolve, dbContextPool, repository, serviceProvider)
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
        /// <param name="serviceProvider">服务提供器</param>
        public EFCoreRepository(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual IRepository<TEntity> Change<TEntity>()
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
        public virtual IRepository<TEntity, TDbContextLocator> Change<TEntity, TDbContextLocator>()
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
        /// <param name="dbContextResolve">数据库上下文解析器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        /// <param name="repository">非泛型仓储</param>
        /// <param name="serviceProvider">服务提供器</param>
        public EFCoreRepository(
            Func<Type, DbContext> dbContextResolve
            , IDbContextPool dbContextPool
            , IRepository repository
            , IServiceProvider serviceProvider)
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
            Model = dbContext.Model;
            ProviderName = Database.ProviderName;

            //初始化实体
            Entities = dbContext.Set<TEntity>();
            DetachedEntities = Entities.AsNoTracking();
            EntityType = Model.FindEntityType(typeof(TEntity));

            // 初始化服务提供器
            ServiceProvider = serviceProvider;
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
        public virtual IQueryable<TEntity> DetachedEntities { get; }

        /// <summary>
        /// 查看实体类型
        /// </summary>
        public virtual IEntityType EntityType { get; }

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
        /// 实体模型
        /// </summary>
        public virtual IModel Model { get; }

        /// <summary>
        /// 数据库提供器名
        /// </summary>
        public virtual string ProviderName { get; }

        /// <summary>
        /// 服务提供器
        /// </summary>
        public virtual IServiceProvider ServiceProvider { get; }

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
        /// 改变实体状态
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="entityState">实体状态</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry ChangeEntityState(object entity, EntityState entityState)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 改变实体状态
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="entityState">实体状态</param>
        /// <returns>EntityEntry<TEntity></returns>
        public virtual EntityEntry<TEntity> ChangeEntityState(TEntity entity, EntityState entityState)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 改变实体状态
        /// </summary>
        /// <param name="entityEntry">实体条目</param>
        /// <param name="entityState">实体状态</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry ChangeEntityState(EntityEntry entityEntry, EntityState entityState)
        {
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 改变实体状态
        /// </summary>
        /// <param name="entityEntry">实体条目</param>
        /// <param name="entityState">实体状态</param>
        /// <returns>EntityEntry<TEntity></returns>
        public virtual EntityEntry<TEntity> ChangeEntityState(EntityEntry<TEntity> entityEntry, EntityState entityState)
        {
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public virtual bool IsAttached(object entity)
        {
            return EntityEntryState(entity) == EntityState.Detached;
        }

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public virtual bool IsAttached(TEntity entity)
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
        /// 取消附加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Detach(object entity)
        {
            ChangeEntityState(entity, EntityState.Deleted);
        }

        /// <summary>
        /// 取消附加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Detach(TEntity entity)
        {
            ChangeEntityState(entity, EntityState.Deleted);
        }

        /// <summary>
        /// 取消附加实体
        /// </summary>
        /// <param name="entityEntry">实体条目</param>
        public virtual void Detach(EntityEntry entityEntry)
        {
            ChangeEntityState(entityEntry, EntityState.Deleted);
        }

        /// <summary>
        /// 取消附加实体
        /// </summary>
        /// <param name="entityEntry">实体条目</param>
        public virtual void Detach(EntityEntry<TEntity> entityEntry)
        {
            ChangeEntityState(entityEntry, EntityState.Deleted);
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
        /// 删除数据库
        /// </summary>
        public virtual void EnsureDeleted()
        {
            DbContext.Database.EnsureDeleted();
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        public virtual Task EnsureDeletedAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public virtual void EnsureCreated()
        {
            DbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public virtual Task EnsureCreatedAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.Database.EnsureCreatedAsync(cancellationToken);
        }

        /// <summary>
        /// 判断是否是 SqlServer 数据库
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsSqlServer()
        {
            return ProviderName.Equals(DatabaseProvider.SqlServer, StringComparison.Ordinal);
        }

        /// <summary>
        /// 判断是否是 Sqlite 数据库
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsSqlite()
        {
            return ProviderName.Equals(DatabaseProvider.Sqlite, StringComparison.Ordinal);
        }

        /// <summary>
        /// 判断是否是 Cosmos 数据库
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsCosmos()
        {
            return ProviderName.Equals(DatabaseProvider.Cosmos, StringComparison.Ordinal);
        }

        /// <summary>
        /// 判断是否是 内存中 数据库
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsInMemory()
        {
            return ProviderName.Equals(DatabaseProvider.InMemory, StringComparison.Ordinal);
        }

        /// <summary>
        /// 判断是否是 MySql 数据库
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsMySql()
        {
            return ProviderName.Equals(DatabaseProvider.MySql, StringComparison.Ordinal);
        }

        /// <summary>
        /// 判断是否是 PostgreSQL 数据库
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsNpgsql()
        {
            return ProviderName.Equals(DatabaseProvider.PostgreSQL, StringComparison.Ordinal);
        }

        /// <summary>
        /// 判断是否是关系型数据库
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsRelational()
        {
            return Database.IsRelational();
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual IRepository<TChangeEntity> Change<TChangeEntity>()
            where TChangeEntity : class, IEntityBase, new()
        {
            return _repository.Change<TChangeEntity>();
        }

        /// <summary>
        /// 切换多数据库上下文仓储
        /// </summary>
        /// <typeparam name="TChangeEntity">实体类型</typeparam>
        /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>仓储</returns>
        public virtual IRepository<TChangeEntity, TChangeDbContextLocator> Change<TChangeEntity, TChangeDbContextLocator>()
            where TChangeEntity : class, IEntityBase, new()
            where TChangeDbContextLocator : class, IDbContextLocator, new()
        {
            return _repository.Change<TChangeEntity, TChangeDbContextLocator>();
        }

        /// <summary>
        /// 将仓储约束为特定仓储
        /// </summary>
        /// <typeparam name="TRestrainRepository">特定仓储</typeparam>
        /// <returns>TRestrainRepository</returns>
        public virtual TRestrainRepository Constraint<TRestrainRepository>()
            where TRestrainRepository : class, IRepositoryDependency
        {
            var type = typeof(TRestrainRepository);
            if (!type.IsInterface || typeof(IRepositoryDependency) == type || type.Name.Equals(nameof(IRepository)) || (type.IsGenericType && type.GetGenericTypeDefinition().Name.Equals(nameof(IRepository))))
            {
                throw Oops.Oh("Invalid type conversion", typeof(InvalidCastException));
            }

            return this as TRestrainRepository;
        }
    }
}