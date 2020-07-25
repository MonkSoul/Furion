namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库实体依赖接口
    /// <para>所有的数据库实体必须直接或间接继承 <see cref="IDbEntity"/>，否则数据库操作功能将受限</para>
    /// </summary>
    public interface IDbEntity { }
}