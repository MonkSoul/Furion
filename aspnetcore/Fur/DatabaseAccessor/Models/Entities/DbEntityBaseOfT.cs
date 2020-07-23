using System;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库实体基础抽象类
    /// <para>包含常见的维护字段：
    /// <see cref="CreatedTime"/>
    /// <see cref="UpdatedTime"/>
    /// <see cref="IsDeleted"/>
    /// </para>
    /// </summary>
    /// <typeparam name="TKey">
    /// 主键类型
    /// <code>struct</code></typeparam>
    public abstract class DbEntityBaseOfT<TKey> : DbEntityOfT<TKey> where TKey : struct
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