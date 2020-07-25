using Fur.ApplicationBase.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库实体依赖抽象类
    /// </summary>
    [NonWrapper]
    public abstract class DbEntityBase : IDbEntity
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
        /// </summary>
        public int TenantId { get; set; }
    }
}