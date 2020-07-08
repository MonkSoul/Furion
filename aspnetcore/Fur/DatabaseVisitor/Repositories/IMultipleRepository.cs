using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;

namespace Fur.DatabaseVisitor.Repositories
{
    public interface IMultipleRepository
    {
        IMultipleRepositoryOfT<TEntity, TDbContextIdentifier> GetMultipleRepository<TEntity, TDbContextIdentifier>(bool newScope = false)
            where TEntity : class, IDbEntity, new()
            where TDbContextIdentifier : IDbContextIdentifier;
    }
}