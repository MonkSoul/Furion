using System;

namespace Fur.DatabaseVisitor.Tangent.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbCanInvokeAttribute : TangentAttribute
    {
        public DbCanInvokeAttribute(string name) => this.Name = name;
        public string Name { get; set; }
    }
}
