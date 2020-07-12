using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbQueryAttribute : TangentSqlAttribute
    {
        public DbQueryAttribute(string sql) : base(sql)
        {
        }
    }
}