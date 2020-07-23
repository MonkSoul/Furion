using Fur.DatabaseAccessor.Models.Entities;
using System;

namespace Fur.DatabaseAccessor.Providers
{
    /// <summary>
    /// 维护字段提供器
    /// </summary>
    public interface IMaintenanceFieldsProvider
    {
        #region 获取创建时间字段信息 + (string propertyName, object value) GetCreatedTimeFieldInfo() => (nameof(DbEntityBase.CreatedTime), DateTime.Now)

        /// <summary>
        /// 获取创建时间字段信息
        /// </summary>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        (string propertyName, object value) GetCreatedTimeFieldInfo() => (nameof(DbEntityBase.CreatedTime), DateTime.Now);

        #endregion 获取创建时间字段信息 + (string propertyName, object value) GetCreatedTimeFieldInfo() => (nameof(DbEntityBase.CreatedTime), DateTime.Now)

        #region 获取更新时间字段信息 + (string propertyName, object value) GetUpdatedTimeFieldInfo() => (nameof(DbEntityBase.UpdatedTime), DateTime.Now)

        /// <summary>
        /// 获取更新时间字段信息
        /// </summary>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        (string propertyName, object value) GetUpdatedTimeFieldInfo() => (nameof(DbEntityBase.UpdatedTime), DateTime.Now);

        #endregion 获取更新时间字段信息 + (string propertyName, object value) GetUpdatedTimeFieldInfo() => (nameof(DbEntityBase.UpdatedTime), DateTime.Now)

        #region 获取软删除字段信息 + (string propertyName, object value) GetFakeDeleteFieldInfo() => (nameof(DbEntityBase.IsDeleted), 1)

        /// <summary>
        /// 获取软删除字段信息
        /// </summary>
        /// <returns></returns>
        (string propertyName, object value) GetFakeDeleteFieldInfo() => (nameof(DbEntityBase.IsDeleted), 1);

        #endregion 获取软删除字段信息 + (string propertyName, object value) GetFakeDeleteFieldInfo() => (nameof(DbEntityBase.IsDeleted), 1)
    }
}