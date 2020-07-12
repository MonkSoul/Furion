using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbExecuteNonQueryAttribute : TangentAdoNetAttribute
    {
        public DbExecuteNonQueryAttribute(string sql) : base(sql)
        {
        }
    }
}
