namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISqlExecutableRepository<TEntity>
        where TEntity : class, IEntityBase, new()
    {
    }
}