using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;

namespace Fur.DatabaseVisitor.Repositories.ReadAndWrite
{
    public interface IReadAndWriteRepositoryOfT<TEntity, TReadDbContextIdentifier, TWriteDbContextIdentifier>
        where TEntity : class, IDbEntity, new()
        where TReadDbContextIdentifier : IDbContextIdentifier
        where TWriteDbContextIdentifier : IDbContextIdentifier
    {
    }
}
