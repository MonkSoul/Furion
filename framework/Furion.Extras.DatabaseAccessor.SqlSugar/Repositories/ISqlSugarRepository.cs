namespace Furion.Extras.DatabaseAccessor.SqlSugar
{
    /// <summary>
    /// SqlSugar 仓储接口定义
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISqlSugarRepository<TEntity>
        where TEntity : class, new()
    {
    }
}
