using Fur.DatabaseVisitor.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseVisitor.TenantSaaS
{
    public class Tenant : IDbEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Host { get; set; }
    }
}