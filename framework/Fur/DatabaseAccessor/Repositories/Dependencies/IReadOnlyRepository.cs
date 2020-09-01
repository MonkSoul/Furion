namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 只读仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadOnlyRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
    }
}