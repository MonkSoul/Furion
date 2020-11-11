using Fur.DependencyInjection;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class SqlExecuteAttribute : SqlSentenceProxyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">sql 语句</param>
        public SqlExecuteAttribute(string sql) : base(sql)
        {
        }
    }
}