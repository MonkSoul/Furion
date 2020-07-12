using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbExecuteReaderAttribute : TangentAdoNetAttribute
    {
        public DbExecuteReaderAttribute(string sql) : base(sql)
        {
        }
    }
}