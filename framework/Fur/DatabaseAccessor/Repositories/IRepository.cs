// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepository<TEntity> : IWritableRepository<TEntity>, IReadableRepository<TEntity>
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
        /// 获取实体条目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry Entry(object entity);

        /// <summary>
        /// 获取实体条目
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry(TEntity entity);

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityState EntityEntryState(object entity);

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityState EntityEntryState(TEntity entity);

        /// <summary>
        /// 实体属性条目
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        PropertyEntry EntityPropertyEntry(object entity, string propertyName);

        /// <summary>
        /// 实体属性条目
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        PropertyEntry EntityPropertyEntry(TEntity entity, string propertyName);

        /// <summary>
        /// 实体属性条目
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        PropertyEntry<TEntity, TProperty> EntityPropertyEntry<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression);

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsAttach(object entity);

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsAttach(TEntity entity);

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
        /// 判断实体是否设置了主键
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsKeySet(TEntity entity);
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