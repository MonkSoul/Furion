using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 租户实体
    /// </summary>
    public class Tenant : IEntity<MultiTenantDbContextLocator>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Tenant()
        {
            CreatedTime = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名
        /// </summary>
        [Required, MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 来源主机地址
        /// </summary>
        [MaxLength(256)]
        public string Host { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [EmailAddress, MaxLength(256)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone, MaxLength(32)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 架构名
        /// </summary>
        [Phone, MaxLength(32)]
        public string Schema { get; set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        [MaxLength(256)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
    }
}