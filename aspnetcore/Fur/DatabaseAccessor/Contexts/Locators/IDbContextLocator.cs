namespace Fur.DatabaseAccessor.Contexts.Locators
{
    /// <summary>
    /// 数据库上下文定位器
    /// <para>所有的数据库上下文都必须有唯一的定位器，依赖注入容器通过该定位器找到对应的数据库上下文</para>
    /// </summary>
    public interface IDbContextLocator { }
}