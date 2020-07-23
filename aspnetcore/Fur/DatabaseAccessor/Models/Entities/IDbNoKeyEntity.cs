namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库视图接口
    /// <para>需要动态配置 <see cref="Fur.DatabaseAccessor.DbContexts.FurDbContextOfT{TDbContext}.ScanDbCompileEntityToCreateModelEntity(Microsoft.EntityFrameworkCore.ModelBuilder)"/> 需继承它</para>
    /// </summary>
    public interface IDbNoKeyEntity
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        string __NAME__ { get; set; }
    }
}