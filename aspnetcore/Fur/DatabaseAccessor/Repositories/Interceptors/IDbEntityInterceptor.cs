using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fur.DatabaseAccessor.Repositories.Interceptors
{
    public interface IDbEntityInterceptor
    {
        void Inserting(EntityEntry entityEntry);
        void Inserted(EntityEntry entityEntry);

        void Updating(EntityEntry entityEntry);
        void Updated(EntityEntry entityEntry);
    }
}
