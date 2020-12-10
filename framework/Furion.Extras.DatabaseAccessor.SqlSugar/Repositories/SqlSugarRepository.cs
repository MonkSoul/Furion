namespace Furion.Extras.DatabaseAccessor.SqlSugar
{
    /// <summary>
    /// SqlSugar 仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SqlSugarRepository<TEntity> : ISqlSugarRepository<TEntity>
        where TEntity : class, new()
    {
    }
}
