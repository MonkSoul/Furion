namespace Fur.DatabaseAccessor.Models.Entities
{
    public abstract class DbNoKeyEntity : IDbNoKeyEntity, IDbEntity
    {
        public DbNoKeyEntity(string name) => __NAME__ = name;

        public string __NAME__ { get; set; }
    }
}