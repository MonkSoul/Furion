using Fur.ApplicationBase.Attributes;
using System;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库实体抽象类
    /// <para>包含创建时间、更新时间、删除标识</para>
    /// </summary>
    [NonWrapper]
    public abstract class DbEntity : DbEntityBase
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}