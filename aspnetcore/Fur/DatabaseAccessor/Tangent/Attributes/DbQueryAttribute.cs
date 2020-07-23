using Fur.DatabaseAccessor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseAccessor.Tangent.Attributes
{
    /// <summary>
    /// 切面查询特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DbQueryAttribute : TangentSqlAttribute
    {
        #region 构造函数 + public DbQueryAttribute(string sql) : base(sql)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">sql 语句</param>
        public DbQueryAttribute(string sql) : base(sql)
        {
        }

        #endregion 构造函数 + public DbQueryAttribute(string sql) : base(sql)
    }
}