using Fur.DatabaseVisitor.Entities;

namespace Fur.DatabaseVisitor.Provider
{
    public interface IMaintenanceProvider
    {
        string GetInsertedTimeName() => nameof(DbEntityBaseOfT<int>.CreatedTime);

        string GetUpdatedTimeName() => nameof(DbEntityBaseOfT<int>.UpdatedTime);
    }
}