namespace Fur.DatabaseAccessor.Models.Entities
{
    public abstract class DbNoKeyEntity : IDbNoKeyEntity, IDbEntity
    {
        public DbNoKeyEntity(string name, bool hasTenantIdFilter = false)
        {
            ObjectName = name;
            HasTenantIdFilter = hasTenantIdFilter;
        }

        public string ObjectName { get; set; }

        public bool HasTenantIdFilter { get; set; }
    }
}