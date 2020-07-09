using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbExecuteAttribute : TangentAttribute
    {
        public DbExecuteAttribute(string sql) => Sql = sql;

        public string Sql { get; set; }
    }
}
