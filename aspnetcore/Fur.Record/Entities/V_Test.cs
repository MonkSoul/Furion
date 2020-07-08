using Fur.DatabaseVisitor.Entities;

namespace Fur.Record.Entities
{
    public class V_Test : DbView
    {
        public V_Test() : base("V_Test")
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int TenantId { get; set; }
    }
}