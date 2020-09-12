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

using Fur.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    [NonBeScan]
    public abstract class EntityBase : EntityBase<int, DbContextLocator>
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1> : EntityBaseDependency<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2> : EntityBaseDependency<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : EntityBaseDependency<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : EntityBaseDependency<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : EntityBaseDependency<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
        where TDbContextLocator5 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : EntityBaseDependency<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
        where TDbContextLocator5 : class, IDbContextLocator, new()
        where TDbContextLocator6 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : EntityBaseDependency<TKey>
        where TKey : struct
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
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
    [NonBeScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : EntityBaseDependency<TKey>
        where TKey : struct
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
    /// 数据库实体依赖基类（禁止外部继承）
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    [NonBeScan]
    public abstract class EntityBaseDependency<TKey> : IEntity
        where TKey : struct
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; set; }
    }
}