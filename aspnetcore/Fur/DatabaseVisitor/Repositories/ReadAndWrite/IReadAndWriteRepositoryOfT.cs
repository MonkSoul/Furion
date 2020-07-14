using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.Repositories.Multiples;

namespace Fur.DatabaseVisitor.Repositories.ReadAndWrite
{
    public interface IReadAndWriteRepositoryOfT<TEntity, TReadDbContextIdentifier, TWriteDbContextIdentifier>
        where TEntity : class, IDbEntity, new()
        where TReadDbContextIdentifier : IDbContextIdentifier
        where TWriteDbContextIdentifier : IDbContextIdentifier
    {
        IMultipleRepositoryOfT<TEntity, TReadDbContextIdentifier> DbRead { get; }
        IMultipleRepositoryOfT<TEntity, TWriteDbContextIdentifier> DbWrite { get; }
    }
}
