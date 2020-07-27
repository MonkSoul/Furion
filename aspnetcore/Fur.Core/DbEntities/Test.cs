using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.Core.DbEntities
{
    [Table("Tests")]
    public class Test : DbEntityOfT<int, FurDbContextIdentifier>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}