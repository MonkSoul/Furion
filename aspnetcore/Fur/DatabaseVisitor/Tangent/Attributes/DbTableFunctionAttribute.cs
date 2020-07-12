using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbTableFunctionAttribute : TangentCompileTypeAttribute
    {
        public DbTableFunctionAttribute(string name) : base(name)
        {
        }
    }
}