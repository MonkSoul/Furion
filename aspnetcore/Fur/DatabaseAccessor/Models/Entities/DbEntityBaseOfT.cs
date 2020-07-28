using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Locators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持指定主键类型</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey> : IDbEntityBase
        where TKey : struct
    {
        /// <summary>
        /// 主键Id
        /// <para>默认自增</para>
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public int TenantId { get; set; }
    }


    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1> : DbEntityBaseOfT<TKey>
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
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1, TDbContextLocator2> : DbEntityBaseOfT<TKey>
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
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : DbEntityBaseOfT<TKey>
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
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : DbEntityBaseOfT<TKey>
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
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : DbEntityBaseOfT<TKey>
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
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : DbEntityBaseOfT<TKey>
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
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : DbEntityBaseOfT<TKey>
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
    public abstract class DbEntityBaseOfT<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : DbEntityBaseOfT<TKey>
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