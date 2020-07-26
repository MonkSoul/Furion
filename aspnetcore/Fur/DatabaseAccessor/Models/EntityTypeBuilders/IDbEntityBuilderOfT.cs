using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fur.DatabaseAccessor.Models.EntityTypeBuilders
{
    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IDbEntityBuilderOfT<TEntity> : IDbEntityBuilder
        where TEntity : class, IDbEntityBase
    {
        #region 配置实体信息 + EntityTypeBuilder HasEntityBuilder(EntityTypeBuilder entity);
        /// <summary>
        /// 配置实体信息
        /// </summary>
        /// <returns>实体类型构建器</returns>
        EntityTypeBuilder HasEntityBuilder(EntityTypeBuilder entity);
        #endregion
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库实体类型配置
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
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库实体类型配置
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
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
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
    /// 数据库实体类型配置
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
    public interface IDbEntityBuilderOfT<TEntity, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
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
