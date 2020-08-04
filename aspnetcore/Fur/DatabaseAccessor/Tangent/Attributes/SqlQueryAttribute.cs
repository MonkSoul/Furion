using Fur.Attributes;
using System;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 切面查询特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonInflated]
    public class SqlQueryAttribute : TangentSqlAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">sql 语句</param>
        public SqlQueryAttribute(string sql) : base(sql)
        {
        }
    }
}