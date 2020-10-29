using Fur.DependencyInjection;
using System.Data;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 语句执行代理
    /// </summary>
    [SkipScan]
    public class SqlSentenceProxyAttribute : SqlProxyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql"></param>
        public SqlSentenceProxyAttribute(string sql)
        {
            Sql = sql;
            CommandType = CommandType.Text;
        }

        /// <summary>
        /// Sql 语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; }
    }
}