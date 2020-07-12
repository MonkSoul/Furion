using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbNonQueryAttribute : TangentSqlAttribute
    {
        public DbNonQueryAttribute(string sql) : base(sql)
        {
        }
    }
}
