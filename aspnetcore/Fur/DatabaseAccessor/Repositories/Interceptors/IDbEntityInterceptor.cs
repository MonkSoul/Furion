using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fur.DatabaseAccessor.Interceptors
{
    public interface IDbEntityInterceptor
    {
        void Inserting(EntityEntry entityEntry);

        void Inserted(EntityEntry entityEntry);

        void Updating(EntityEntry entityEntry);

        void Updated(EntityEntry entityEntry);
    }
}