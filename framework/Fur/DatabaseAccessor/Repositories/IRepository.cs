using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepository<TEntity> : IInsertableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// 实体集合
        /// </summary>
        DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 不跟踪的（脱轨）实体
        /// </summary>
        IQueryable<TEntity> DerailEntities { get; }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        DbConnection DbConnection { get; }

        /// <summary>
        /// 实体追综器
        /// </summary>
        ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// 租户Id
        /// </summary>
        Guid? TenantId { get; }

        /// <summary>
        /// 判断上下文是否更改
        /// </summary>
        /// <returns></returns>
        bool HasChanges();

        /// <summary>
        /// 接受所有更改
        /// </summary>
        void AcceptAllChanges();

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry Attach(object entity);

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Attach(TEntity entity);

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities"></param>
        void AttachRange(params object[] entities);

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities"></param>
        void AttachRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 获取所有数据库上下文
        /// </summary>
        /// <returns></returns>
        ConcurrentBag<DbContext> GetDbContexts();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        int SavePoolChanges();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SavePoolChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        Task<int> SavePoolChangesAsync();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 非泛型仓储
    /// </summary>
    public partial interface IRepository
    {
        /// <summary>
        /// 获取实体仓储
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> Get<TEntity>()
            where TEntity : class, IDbEntityBase, new();
    }
}