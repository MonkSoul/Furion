using Fur.DatabaseVisitor.Entities;

namespace Fur.DatabaseVisitor.Provider
{
    public interface IMaintenanceProvider
    {
        string GetCreatedTimeName() => nameof(DbEntityBaseOfT<int>.CreatedTime);

        string GetUpdatedTimeName() => nameof(DbEntityBaseOfT<int>.UpdatedTime);
    }
}