using Fur.DatabaseAccessor.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fur.DatabaseAccessor.Models.Seed
{
    /// <summary>
    /// 数据库初始数据种子接口
    /// <para>使用：通过实体定义类继承该接口</para>
    /// <para>应用启动时将会扫描该接口的所有衍生类对象并调用其 <see cref="HasData"/> 方法，同时将该方法返回值添加到 <see cref="Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder.HasData(IEnumerable{object})"/> 中</para>
    /// </summary>
    /// <typeparam name="TEntity"><see cref="IDbEntity"/> 衍生类型</typeparam>
    public interface IDbDataSeedOfT<TEntity> : IDbDataSeed
        where TEntity : IDbEntity
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


    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1> : IDbDataSeedOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
    {
    }

    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2> : IDbDataSeedOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
    {
    }

    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3> : IDbDataSeedOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
    {
    }

    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4> : IDbDataSeedOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
    {
    }

    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5> : IDbDataSeedOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
    {
    }

    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6> : IDbDataSeedOfT<TEntity>
        where TEntity : IDbEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
    {
    }

    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7> : IDbDataSeedOfT<TEntity>
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

    public interface IDbDataSeedOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8> : IDbDataSeedOfT<TEntity>
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
