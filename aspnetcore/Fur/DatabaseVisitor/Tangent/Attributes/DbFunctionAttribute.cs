using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbFunctionAttribute : TangentCompileTypeAttribute
    {
        public DbFunctionAttribute(string name) : base(name)
        {
        }
    }
}