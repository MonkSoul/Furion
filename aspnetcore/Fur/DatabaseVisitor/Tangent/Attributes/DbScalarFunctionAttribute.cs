using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbScalarFunctionAttribute : DbCanInvokeAttribute
    {
        public DbScalarFunctionAttribute(string name) : base(name)
        {
        }
    }
}