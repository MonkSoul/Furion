using Fur.DatabaseVisitor.Entities;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial interface ISqlRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
    }
}
