using Fur.DependencyInjection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 对象类型执行代理
    /// </summary>
    [SkipScan]
    public class SqlObjectProxyAttribute : SqlProxyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">对象名</param>
        public SqlObjectProxyAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 对象名
        /// </summary>
        public string Name { get; set; }
    }
}