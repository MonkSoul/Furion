using Fur.DatabaseAccessor.Contexts.Locators;
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1, TDbContextLocator2> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体类型配置
    /// <para>通常在 <see cref="TEntity"/> 中继承使用</para>
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体类型配置
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
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体类型配置
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
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
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
    /// 数据库实体类型配置
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
    public interface IDbEntityBuilderOfT<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : IDbEntityBuilderOfT<TEntity>
        where TEntity : class, IDbEntityBase
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
