using Autofac;
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
    public interface IDbQueryFilterOfT<TEntity>
        where TEntity : IDbEntity
    {
        #region 配置查询过滤器 + Dictionary<Expression<Func<TEntity, bool>>, IEnumerable<Type>> HasQueryFilter(DbContext dbContext, ILifetimeScope lifetimeScope)
        /// <summary>
        /// 配置查询过滤器
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="lifetimeScope">生命周期对象</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/></returns>
        Dictionary<Expression<Func<TEntity, bool>>, IEnumerable<Type>> HasQueryFilter(DbContext dbContext, ILifetimeScope lifetimeScope);
        #endregion
    }
}
