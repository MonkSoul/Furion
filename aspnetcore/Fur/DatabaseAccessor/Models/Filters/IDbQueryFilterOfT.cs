using Fur.DatabaseAccessor.Providers;
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
    {
        #region 配置查询过滤器 + IEnumerable<Expression<Func<TEntity, bool>>> HasQueryFilter(ITenantProvider tenantProvider)
        /// <summary>
        /// 配置查询过滤器
        /// </summary>
        /// <param name="tenantProvider">租户提供器</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/></returns>
        Dictionary<Expression<Func<TEntity, bool>>, List<object>> HasQueryFilter(ITenantProvider tenantProvider);
        #endregion
    }
}
