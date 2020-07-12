using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes.Basics
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TangentCompileTypeAttribute : TangentAttribute
    {
        public TangentCompileTypeAttribute(string name) => Name = name;

        public string Name { get; set; }
    }
}