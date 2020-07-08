using System;

namespace Fur.DatabaseVisitor.Entities
{
    /// <summary>
    /// 数据库实体基础抽象类
    /// <para>包含常见的维护字段：
    /// <see cref="CreatedTime"/>
    /// <see cref="UpdatedTime"/>
    /// <see cref="IsDeleted"/>
    /// </para>
    /// </summary>
    public abstract class DbEntityBase : DbEntity
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
        /// 假删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}