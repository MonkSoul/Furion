namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可读仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
    }
}