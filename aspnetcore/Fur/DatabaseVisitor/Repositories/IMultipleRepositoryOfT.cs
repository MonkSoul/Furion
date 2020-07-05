using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.Identifiers;

namespace Fur.DatabaseVisitor.Repositories
{
    public interface IMultipleRepositoryOfT<TEntity, TDbContextIdentifier> : IRepositoryOfT<TEntity>
        where TEntity : class, IEntity, new()
        where TDbContextIdentifier : IDbContextIdentifier
    {
    }
}