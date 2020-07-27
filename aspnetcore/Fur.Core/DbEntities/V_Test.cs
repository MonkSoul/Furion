using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.Core.DbEntities
{
    public class V_Test : DbNoKeyEntityOfT<FurDbContextIdentifier>
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