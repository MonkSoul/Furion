using Fur.DatabaseAccessor.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor.MultipleTenants.Entities
{
    /// <summary>
    /// 租户实体
    /// </summary>
    [Table("Tenants")]
    public class Tenant : IDbEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 租户名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 主机地址
        /// </summary>
        [Required]
        public string Host { get; set; }
    }
}