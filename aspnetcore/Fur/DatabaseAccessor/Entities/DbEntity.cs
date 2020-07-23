using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor.Entities
{
    /// <summary>
    /// 数据库实体抽象类
    /// <para>简化 <see cref="IDbEntity"/> 手动实现</para>
    /// </summary>
    public abstract class DbEntity : IDbEntity
    {
        /// <summary>
        /// 主键Id
        /// <para>默认自增</para>
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 租户Id
        /// <para>参见：<see cref="Fur.DatabaseAccessor.TenantSaaS.Tenant"/></para>
        /// </summary>
        public int TenantId { get; set; }
    }
}