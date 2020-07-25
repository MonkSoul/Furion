using Fur.ApplicationBase.Attributes;
using System;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库泛型实体抽象类
    /// <para>支持指定主键类型</para>
    /// <para>包含创建时间、更新时间、删除标识</para>
    /// </summary>
    /// <typeparam name="TKey">支持指定主键类型</typeparam>
    [NonWrapper]
    public abstract class DbEntityOfT<TKey> : DbEntityBaseOfT<TKey>
        where TKey : struct
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