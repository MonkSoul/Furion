using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fur.DatabaseAccessor.Models.QueryFilters
{
    /// <summary>
    /// 数据库上下文查询筛选器
    /// <para>可应用与当前上下文中全局配置</para>
    /// </summary>
    public interface IDbContextQueryFilter
    {
        void HasQueryFilter(EntityTypeBuilder entityTypeBuilder);
    }
}
