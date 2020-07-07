using System;
using System.Data;

namespace Fur.DatabaseVisitor.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute
    {
        public ParameterAttribute(string name) => Name = name;

        public string Name { get; set; }

        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;
    }
}