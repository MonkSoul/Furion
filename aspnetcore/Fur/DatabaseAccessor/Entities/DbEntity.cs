using Fur.AppCore.Attributes;
using Fur.DatabaseAccessor.Contexts.Locators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fur.DatabaseAccessor.Entities
{
    /// <summary>
    /// 数据库实体抽象类
    /// <para>包含创建时间、更新时间、删除标识</para>
    /// </summary>
    [NonWrapper]
    public abstract class DbEntity : DbEntityBase, IDbEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持指定主键类型</para>
    /// <para>包含创建时间、更新时间、删除标识</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey> : DbEntityBase<TKey>, IDbEntity
        where TKey : struct
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1> : DbEntity<TKey>
        where TKey : struct
        where TDbContextLocator1 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1, TDbContextLocator2> : DbEntity<TKey>
        where TKey : struct
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : DbEntity<TKey>
        where TKey : struct
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : DbEntity<TKey>
        where TKey : struct
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : DbEntity<TKey>
        where TKey : struct
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : DbEntity<TKey>
        where TKey : struct
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : DbEntity<TKey>
        where TKey : struct
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
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : DbEntity<TKey>
        where TKey : struct
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