using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbSentenceAttribute : TangentAttribute
    {
        public DbSentenceAttribute(string sql) => Sql = sql;

        public string Sql { get; set; }
    }
}