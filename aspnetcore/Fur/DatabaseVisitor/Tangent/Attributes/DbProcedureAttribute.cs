using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbProcedureAttribute : TangentCompileTypeAttribute
    {
        public DbProcedureAttribute(string name) : base(name)
        {
        }

        public bool WithOutputOrReturn { get; set; } = false;
    }
}