using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes.Basics
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TangentSqlAttribute : TangentAttribute
    {
        public TangentSqlAttribute(string sql) => Sql = sql;

        public string Sql { get; set; }
    }
}
