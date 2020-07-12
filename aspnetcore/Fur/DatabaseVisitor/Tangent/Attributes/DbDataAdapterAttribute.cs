using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbDataAdapterAttribute : TangentAdoNetAttribute
    {
        public DbDataAdapterAttribute(string sql) : base(sql) { }
    }
}
