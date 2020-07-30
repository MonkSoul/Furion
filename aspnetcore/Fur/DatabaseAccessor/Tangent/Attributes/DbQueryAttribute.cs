using Fur.AppBasic.Attributes;
using Fur.DatabaseAccessor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseAccessor.Tangent.Attributes
{
    /// <summary>
    /// 切面查询特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonWrapper]
    public class DbQueryAttribute : TangentSqlAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">sql 语句</param>
        public DbQueryAttribute(string sql) : base(sql)
        {
        }
    }
}