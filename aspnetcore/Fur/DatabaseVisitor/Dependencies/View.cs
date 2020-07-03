namespace Fur.DatabaseVisitor.Dependencies
{
    /// <summary>
    /// 所有的视图需要集成该抽象类
    /// </summary>
    public abstract class View : IView, IEntity
    {
        public string ToViewName { get; set; }
        public View(string toViewName) => ToViewName = toViewName;
    }
}
