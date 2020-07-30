using Fur.DatabaseAccessor.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor.MultipleTenants.Entities
{
    /// <summary>
    /// 租户实体
    /// </summary>
    public class Tenant : IDbEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 来源主机地址
        /// </summary>
        [Required]
        public string Host { get; set; }

        /// <summary>
        /// 架构名
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

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
}