using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.Identifiers;

namespace Fur.DatabaseVisitor.Repositories
{
    public interface IMultipleRepository
    {
        IMultipleRepositoryOfT<TEntity, TDbContextIdentifier> GetMultipleRepository<TEntity, TDbContextIdentifier>(bool newScope = false)
            where TEntity : class, IEntity, new()
            where TDbContextIdentifier : IDbContextIdentifier;
    }
}