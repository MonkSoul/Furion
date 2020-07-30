using System;

namespace Fur.DatabaseAccessor.Models.Entities
{
    public interface IDbEntity : IDbEntityBase
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}