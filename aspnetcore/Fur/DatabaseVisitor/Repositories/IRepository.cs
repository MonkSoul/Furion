using Fur.DatabaseVisitor.Entities;

namespace Fur.DatabaseVisitor.Repositories
{
    public interface IRepository
    {
        IRepositoryOfT<TEntity> GetRepository<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new();
    }
}