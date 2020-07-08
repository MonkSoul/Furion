using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;

namespace Fur.DatabaseVisitor.Repositories
{
    public interface IMultipleRepositoryOfT<TEntity, TDbContextIdentifier> : IRepositoryOfT<TEntity>
        where TEntity : class, IDbEntity, new()
        where TDbContextIdentifier : IDbContextIdentifier
    {
    }
}