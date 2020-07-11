using Fur.DatabaseVisitor.Entities;

namespace Fur.DatabaseVisitor.Providers
{
    public interface IMaintenanceProvider
    {
        string GetCreatedTimePropertyName() => nameof(DbEntityBase.CreatedTime);

        string GetUpdatedTimePropertyName() => nameof(DbEntityBase.UpdatedTime);

        (string flagPropertyName, object flagValue) GetFakeDeletePropertyInfo() => (nameof(DbEntityBase.IsDeleted), 1);
    }
}