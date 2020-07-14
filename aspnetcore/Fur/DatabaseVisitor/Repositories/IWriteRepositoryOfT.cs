using Fur.DatabaseVisitor.Entities;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial interface IWriteRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
    }
}
