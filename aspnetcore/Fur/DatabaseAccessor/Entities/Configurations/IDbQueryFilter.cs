using Fur.DatabaseAccessor.Contexts.Locators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor.Entities.Configurations
{
    /// <summary>
    /// 数据库查询筛选器依赖接口
    /// <para>主要用来反射查找，无实际作用</para>
    /// </summary>
    public interface IDbQueryFilter : IDbEntityConfigure { }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IDbQueryFilter<TEntity> : IDbQueryFilter
        where TEntity : IDbEntityBase
    {
        /// <summary>
        /// 配置查询过滤器
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<Expression<Func<TEntity, bool>>> HasQueryFilter(DbContext dbContext);
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1, TDbContextLocator2> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
        where TDbContextLocator7 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库查询筛选器
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
    public interface IDbQueryFilter<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : IDbQueryFilter<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
        where TDbContextLocator7 : IDbContextLocator
        where TDbContextLocator8 : IDbContextLocator
    {
    }
}