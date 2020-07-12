using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TangentAdoNetAttribute : TangentAttribute
    {
        public TangentAdoNetAttribute(string sql) => Sql = sql;

        public string Sql { get; set; }
    }
}
