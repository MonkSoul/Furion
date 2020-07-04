using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TangentAttribute : Attribute
    {
        public Type SourceType { get; set; }
    }
}
