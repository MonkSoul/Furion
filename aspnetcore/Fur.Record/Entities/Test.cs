using Fur.DatabaseVisitor.Dependencies;

namespace Fur.Record.Entities
{
    public class Test : EntityBase<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}