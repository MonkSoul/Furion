using Fur.DatabaseAccessor.Entities;

namespace Fur.Core.DbEntities
{
    public class Test : DbEntityBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}