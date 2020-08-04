namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// 数据库上下文定位器
    /// </summary>
    /// <remarks>
    /// <para>所有的数据库上下文都必须有且一个的定位器，应用通过该定位器找到对应的数据库上下文并实例化</para>
    /// </remarks>
    public interface IDbContextLocator { }
}