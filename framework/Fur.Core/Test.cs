using Fur.DatabaseAccessor;

namespace Fur.Core
{
    public class Test : EntityBase<int, DbContextLocator, FurDbContextLocator2>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}