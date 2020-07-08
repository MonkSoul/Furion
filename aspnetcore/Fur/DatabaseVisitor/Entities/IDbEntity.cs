namespace Fur.DatabaseVisitor.Entities
{
    /// <summary>
    /// 数据库实体接口
    /// <para>所有的实体应继承，否则使用不了 <see cref="Fur.DatabaseVisitor.Repositories.IRepositoryOfT{TEntity}"/> 申明</para>
    /// </summary>
    public interface IDbEntity { }
}