using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbExecuteScalarAttribute : TangentAdoNetAttribute
    {
        public DbExecuteScalarAttribute(string sql) : base(sql)
        {
        }
    }
}