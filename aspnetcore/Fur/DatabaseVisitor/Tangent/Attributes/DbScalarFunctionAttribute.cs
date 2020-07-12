using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbScalarFunctionAttribute : TangentCompileTypeAttribute
    {
        public DbScalarFunctionAttribute(string name) : base(name)
        {
        }
    }
}