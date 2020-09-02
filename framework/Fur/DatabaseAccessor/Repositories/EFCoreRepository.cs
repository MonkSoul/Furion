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
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// EF Core仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity>
         where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextPool"></param>
        /// <param name="dbContext"></param>
        public EFCoreRepository(
            IDbContextPool dbContextPool
            , DbContext dbContext)
        {
            _dbContextPool = dbContextPool;
            // 保存当前数据库上下文到池中
            _dbContextPool.SaveDbContext(dbContext);

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
        /// <returns></returns>
        public virtual bool HasChanges()
        {
            return ChangeTracker.HasChanges();
        }

        /// <summary>
        /// 接受所有更改
        /// </summary>
        public virtual void AcceptAllChanges()
        {
            ChangeTracker.AcceptAllChanges();
        }

        /// <summary>
        /// 获取实体条目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry Entry(object entity)
            => DbContext.Entry(entity);

        /// <summary>
        /// 获取实体条目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Entry(TEntity entity)
            => DbContext.Entry(entity);

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityState EntityEntryState(object entity)
            => Entry(entity).State;

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityState EntityEntryState(TEntity entity)
            => Entry(entity).State;

        /// <summary>
        /// 实体属性条目
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual PropertyEntry EntityPropertyEntry(object entity, string propertyName)
            => Entry(entity).Property(propertyName);

        /// <summary>
        /// 实体属性条目
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual PropertyEntry EntityPropertyEntry(TEntity entity, string propertyName)
            => Entry(entity).Property(propertyName);

        /// <summary>
        /// 实体属性条目
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public virtual PropertyEntry<TEntity, TProperty> EntityPropertyEntry<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression)
            => Entry(entity).Property(propertyExpression);

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool IsAttach(object entity)
        {
            return EntityEntryState(entity) == EntityState.Detached;
        }

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool IsAttach(TEntity entity)
        {
            return EntityEntryState(entity) == EntityState.Detached;
        }

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry Attach(object entity)
        {
            return DbContext.Attach(entity);
        }

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Attach(TEntity entity)
        {
            return DbContext.Attach(entity);
        }

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AttachRange(params object[] entities)
        {
            DbContext.AttachRange(entities);
        }

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AttachRange(IEnumerable<TEntity> entities)
        {
            DbContext.AttachRange(entities);
        }

        /// <summary>
        /// 获取所有数据库上下文
        /// </summary>
        /// <returns></returns>
        public ConcurrentBag<DbContext> GetDbContexts()
            => _dbContextPool.GetDbContexts();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        public int SavePoolChanges()
            => _dbContextPool.SavePoolChanges();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public int SavePoolChanges(bool acceptAllChangesOnSuccess)
            => _dbContextPool.SavePoolChanges(acceptAllChangesOnSuccess);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        public Task<int> SavePoolChangesAsync()
            => _dbContextPool.SavePoolChangesAsync();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> SavePoolChangesAsync(CancellationToken cancellationToken = default)
            => _dbContextPool.SavePoolChangesAsync(cancellationToken);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> SavePoolChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            => _dbContextPool.SavePoolChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
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
        /// 获取实体仓储
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public virtual IRepository<TEntity> Get<TEntity>()
             where TEntity : class, IDbEntityBase, new()
        {
            return _serviceProvider.GetService<IRepository<TEntity>>();
        }
    }
}