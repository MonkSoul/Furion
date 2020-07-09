using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbCompileTypeAttribute : TangentAttribute
    {
        public DbCompileTypeAttribute(string name) => this.Name = name;

        public string Name { get; set; }
    }
}