using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbQueryAttribute : TangentAttribute
    {
        public DbQueryAttribute(string sql) => Sql = sql;

        public string Sql { get; set; }
    }
}