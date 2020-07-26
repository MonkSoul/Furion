using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fur.DatabaseAccessor.Models.SeedDatas
{
    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IDbSeedDataOfT<TEntity> : IDbSeedData
        where TEntity : IDbEntityBase
    {
        #region 配置初始化数据 + IEnumerable<TEntity> HasData(DbContext dbContext)
        /// <summary>
        /// 配置初始化数据
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TEntity> HasData(DbContext dbContext);
        #endregion
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier7">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
        where TDbContextIdentifier7 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库种子数据
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier7">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier8">数据库上下文标识器</typeparam>
    public interface IDbSeedDataOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8> : IDbSeedDataOfT<TEntity>
        where TEntity : IDbEntityBase
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
