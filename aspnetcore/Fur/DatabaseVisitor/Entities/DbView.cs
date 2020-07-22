namespace Fur.DatabaseVisitor.Entities
{
    /// <summary>
    /// 数据库视图抽线类
    /// <para>简化 <see cref="IDbView"/> 手动实现</para>
    /// </summary>
    public abstract class DbView : IDbView, IDbEntity
    {
        /// <summary>
        /// 视图名称
        /// </summary>
        public string ViewName { get; set; }

        #region 默认构造函数 + public View(string viewName) => ViewName = viewName;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="viewName">视图名称</param>
        public DbView(string viewName) => ViewName = viewName;

        #endregion 默认构造函数 + public View(string viewName) => ViewName = viewName;
    }
}