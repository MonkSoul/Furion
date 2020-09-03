namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 查询仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISqlQueryableRepository<TEntity>
        where TEntity : class, IEntityBase, new()
    {
    }
}