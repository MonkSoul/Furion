using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.Core.DbEntities
{
    [DbTable("Tests", typeof(FurDbContextIdentifier))]
    public class Test : DbEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}