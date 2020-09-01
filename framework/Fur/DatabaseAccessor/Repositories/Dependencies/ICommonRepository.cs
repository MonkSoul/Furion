namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 公共仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface ICommonRepository<TEntity>
       where TEntity : class, IDbEntityBase, new()
    {
    }
}
