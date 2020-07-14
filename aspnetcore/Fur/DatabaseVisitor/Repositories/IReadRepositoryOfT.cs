using Fur.DatabaseVisitor.Entities;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial interface IReadRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
    }
}
