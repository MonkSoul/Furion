using Fur.DatabaseVisitor.Dependencies;

namespace Fur.DatabaseVisitor.Provider
{
    public interface IMaintenanceProvider
    {
        string GetInsertedTimeName() => nameof(EntityBase<int>.CreatedTime);

        string GetUpdatedTimeName() => nameof(EntityBase<int>.UpdatedTime);
    }
}