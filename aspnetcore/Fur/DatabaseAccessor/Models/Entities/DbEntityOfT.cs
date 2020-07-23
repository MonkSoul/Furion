using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库实体抽象类
    /// <para>简化 <see cref="IDbEntity"/> 手动实现</para>
    /// </summary>
    /// <typeparam name="TKey">
    /// 主键类型
    /// <code>struct</code>
    /// </typeparam>
    public abstract class DbEntityOfT<TKey> : IDbEntity where TKey : struct
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
        /// <para>参见：<see cref="Fur.DatabaseAccessor.TenantSaaS.Tenant"/></para>
        /// </summary>
        public int TenantId { get; set; }
    }
}