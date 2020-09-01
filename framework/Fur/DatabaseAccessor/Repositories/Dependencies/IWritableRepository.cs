namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可写仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IWritableRepository<TEntity>
       where TEntity : class, IDbEntityBase, new()
    {
    }
}