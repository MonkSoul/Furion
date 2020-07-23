using Fur.DatabaseAccessor.Models.Entities;
using System.Collections.Generic;

namespace Fur.DatabaseAccessor.Models.Seed
{
    /// <summary>
    /// 数据库初始数据种子接口
    /// <para>使用：通过实体定义类继承该接口</para>
    /// <para>应用启动时将会扫描该接口的所有衍生类对象并调用其 <see cref="HasData"/> 方法，同时将该方法返回值添加到 <see cref="Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder.HasData(IEnumerable{object})"/> 中</para>
    /// </summary>
    /// <typeparam name="TEntity"><see cref="IDbEntity"/> 衍生类型</typeparam>
    public interface IDbDataSeedOfT<TEntity>
        where TEntity : IDbEntity
    {
        #region 配置初始化数据 + IEnumerable<TEntity> HasData()
        /// <summary>
        /// 配置初始化数据
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TEntity> HasData();
        #endregion
    }
}
