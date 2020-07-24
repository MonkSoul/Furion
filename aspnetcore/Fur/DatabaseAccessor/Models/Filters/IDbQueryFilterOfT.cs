using Fur.DatabaseAccessor.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor.Models.Filters
{
    /// <summary>
    /// 数据库查询过滤器
    /// <para>使用：通过实体定义类继承该接口</para>
    /// <para>执行查询操作时，会自动将过滤器添加到 <see cref="Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder.HasQueryFilter(LambdaExpression)"/> 中</para>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbQueryFilterOfT<TEntity> : IDbQueryFilter
        where TEntity : IDbEntity
    {
        #region 配置查询过滤器 + IEnumerable<Expression<Func<TEntity, bool>>> HasQueryFilter(DbContext dbContext)
        /// <summary>
        /// 配置查询过滤器
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<Expression<Func<TEntity, bool>>> HasQueryFilter(DbContext dbContext);
        #endregion
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
    {
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
    {
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
    {
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
    {
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
    {
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
    {
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
        where TDbContextIdentifier7 : IDbContextIdentifier
    {
    }

    public interface IDbQueryFilterOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8> : IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
        where TDbContextIdentifier7 : IDbContextIdentifier
        where TDbContextIdentifier8 : IDbContextIdentifier
    {
    }
}
