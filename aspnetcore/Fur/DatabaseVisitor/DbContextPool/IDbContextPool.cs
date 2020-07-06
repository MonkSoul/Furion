using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.DbContextPool
{
    public interface IDbContextPool
    {
        void SaveDbContext(DbContext dbContext);
        IEnumerable<DbContext> GetDbContexts();

        int SavePoolChanges();

        Task<int> SavePoolChangesAsync();
    }
}
