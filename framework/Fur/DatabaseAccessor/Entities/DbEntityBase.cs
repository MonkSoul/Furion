// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    public abstract class DbEntityBase : DbEntityBase<int>
    {
    }

    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class DbEntityBase<TKey> : IDbEntityBase
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

    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    public abstract class DbEntityBase<TKey, TDbContextLocator1> : DbEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    public abstract class DbEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> : DbEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    public abstract class DbEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : DbEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    public abstract class DbEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : DbEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    public abstract class DbEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : DbEntityBase<TKey>
        where TKey : struct
        where TDbContextLocator1 : class, IDbContextLocator, new()
        where TDbContextLocator2 : class, IDbContextLocator, new()
        where TDbContextLocator3 : class, IDbContextLocator, new()
        where TDbContextLocator4 : class, IDbContextLocator, new()
        where TDbContextLocator5 : class, IDbContextLocator, new()
    {
    }

    /// <summary>
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    public abstract class DbEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : DbEntityBase<TKey>
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
    /// 数据库实体依赖基接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    public abstract class DbEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : DbEntityBase<TKey>
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
    /// 数据库实体依赖基接口
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
    public abstract class DbEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : DbEntityBase<TKey>
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
}