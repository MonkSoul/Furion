using Fur.DatabaseVisitor.Dependencies;

namespace Fur.DatabaseVisitor.Repositories
{
    public interface IRepository
    {
        IRepositoryOfT<TEntity> GetRepository<TEntity>(bool newScope = false) where TEntity : class, IEntity, new();
    }
}