using Fur.ApplicationBase.Attributes;
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
}