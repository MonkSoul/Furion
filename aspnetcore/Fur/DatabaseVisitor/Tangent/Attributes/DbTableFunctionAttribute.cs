using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbTableFunctionAttribute : DbCompileTypeAttribute
    {
        public DbTableFunctionAttribute(string name) : base(name)
        {
        }
    }
}