using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Identifiers;
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
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1> : DbEntityBaseOfT<TKey>
        where TKey : struct
        where TDbContextIdentifier1 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1, TDbContextIdentifier2> : DbEntityBaseOfT<TKey>
        where TKey : struct
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3> : DbEntityBaseOfT<TKey>
        where TKey : struct
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4> : DbEntityBaseOfT<TKey>
        where TKey : struct
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5> : DbEntityBaseOfT<TKey>
        where TKey : struct
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6> : DbEntityBaseOfT<TKey>
        where TKey : struct
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
    {
    }

    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier7">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7> : DbEntityBaseOfT<TKey>
        where TKey : struct
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
    /// 数据库泛型实体抽象类
    /// <para>支持多数据库上下文配置</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier7">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier8">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbEntityBaseOfT<TKey, TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8> : DbEntityBaseOfT<TKey>
        where TKey : struct
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