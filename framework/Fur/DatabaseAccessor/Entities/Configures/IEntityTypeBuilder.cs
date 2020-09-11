// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IEntityTypeBuilder<TEntity> : IEntityTypeBuilder<TEntity, DbContextLocator>
        where TEntity : class, IEntity, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
        where TDbContextLocator5 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
        where TDbContextLocator5 : class, IDbContextLocator, new()
        where TDbContextLocator6 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
        where TDbContextLocator5 : class, IDbContextLocator, new()
        where TDbContextLocator6 : class, IDbContextLocator, new()
        where TDbContextLocator7 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : IEntityTypeBuilderDependency<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
        where TDbContextLocator5 : class, IDbContextLocator, new()
        where TDbContextLocator6 : class, IDbContextLocator, new()
        where TDbContextLocator7 : class, IDbContextLocator, new()
        where TDbContextLocator8 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体类型配置依赖接口（禁止外部继承）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityTypeBuilderDependency<TEntity> : IModelBuilder
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// 实体类型配置
        /// </summary>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        void Configure(EntityTypeBuilder<TEntity> entityBuilder, DbContext dbContext, Type dbContextLocator);
    }
}