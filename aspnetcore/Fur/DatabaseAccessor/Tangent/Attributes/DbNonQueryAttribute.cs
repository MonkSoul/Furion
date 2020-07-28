using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseAccessor.Tangent.Attributes
{
    /// <summary>
    /// 切面增删改特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonWrapper]
    public class DbNonQueryAttribute : TangentSqlAttribute
    {
        #region 构造函数 + public DbNonQueryAttribute(string sql) : base(sql)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">sql语句</param>
        public DbNonQueryAttribute(string sql) : base(sql)
        {
        }

        #endregion
    }
}