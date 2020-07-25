namespace Fur.DatabaseAccessor.Contexts.Identifiers
{
    /// <summary>
    /// 数据库上下文标识器
    /// <para>所有的数据库上下文都必须有唯一的标识器，依赖注入容器通过该标识器找到对应的数据库上下文</para>
    /// </summary>
    public interface IDbContextIdentifier { }
}