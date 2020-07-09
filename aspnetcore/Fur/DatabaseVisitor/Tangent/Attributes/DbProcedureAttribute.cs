using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbProcedureAttribute : DbCompileTypeAttribute
    {
        public DbProcedureAttribute(string name) : base(name)
        {
        }
    }
}