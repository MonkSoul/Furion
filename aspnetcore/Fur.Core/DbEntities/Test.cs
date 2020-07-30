using Fur.DatabaseAccessor.Entities;

namespace Fur.Core.DbEntities
{
    public class Test : DbEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}