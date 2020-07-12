using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TangentCompileTypeAttribute : TangentAttribute
    {
        public TangentCompileTypeAttribute(string name) => Name = name;

        public string Name { get; set; }
    }
}