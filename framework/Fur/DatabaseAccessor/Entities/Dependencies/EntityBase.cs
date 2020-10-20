// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
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
    [SkipScan]
    public abstract class EntityBase : EntityBase<int, MasterDbContextLocator>
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体依赖基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
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
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
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
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
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
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
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
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
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
    [SkipScan]
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : PrivateEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
        where TDbContextLocator8 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库实体依赖基类（禁止外部继承）
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    [SkipScan]
    public abstract class PrivateEntityBase<TKey> : IPrivateEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; set; }
    }
}