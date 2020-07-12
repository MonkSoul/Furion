using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes.Basics
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TangentAttribute : Attribute
    {
        public Type DbContextIdentifier { get; set; }
        public Type SourceType { get; set; }
    }
}