using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbTableFunctionAttribute : DbCanInvokeAttribute
    {
        public DbTableFunctionAttribute(string name) : base(name)
        {
        }
    }
}