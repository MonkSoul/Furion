using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.Repositories.Multiples;

namespace Fur.DatabaseVisitor.Repositories.ReadAndWrite
{
    public class ReadAndWriteEFCoreRepositoryOfT<TEntity, TReadDbContextIdentifier, TWriteDbContextIdentifier> : IReadAndWriteRepositoryOfT<TEntity, TReadDbContextIdentifier, TWriteDbContextIdentifier>
        where TEntity : class, IDbEntity, new()
        where TReadDbContextIdentifier : IDbContextIdentifier
        where TWriteDbContextIdentifier : IDbContextIdentifier
    {
        public ReadAndWriteEFCoreRepositoryOfT(
            IMultipleRepositoryOfT<TEntity, TReadDbContextIdentifier> readRepository
            , IMultipleRepositoryOfT<TEntity, TWriteDbContextIdentifier> writeRepository)
        {
            Read = readRepository;
            Write = writeRepository;
        }

        public IMultipleRepositoryOfT<TEntity, TReadDbContextIdentifier> Read { get; }

        public IMultipleRepositoryOfT<TEntity, TWriteDbContextIdentifier> Write { get; }
    }
}
