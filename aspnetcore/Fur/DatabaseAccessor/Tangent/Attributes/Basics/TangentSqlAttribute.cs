using Fur.AppBasic.Attributes;
using System;

namespace Fur.DatabaseAccessor.Tangent.Attributes.Basics
{
    /// <summary>
    /// 非编译类型特性，也就是select/delete/update/insert/drop/create/alter等
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonWrapper]
    public class TangentSqlAttribute : TangentAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">sql 语句</param>
        public TangentSqlAttribute(string sql) => Sql = sql;

        /// <summary>
        /// Sql 语句
        /// </summary>
        public string Sql { get; set; }
    }
}