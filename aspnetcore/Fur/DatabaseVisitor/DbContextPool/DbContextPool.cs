using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.DbContextPool
{
    public class DbContextPool : IDbContextPool, IScopedLifetime
    {
        private readonly ConcurrentBag<DbContext> dbContexts;
        public DbContextPool()
        {
            if (dbContexts == null) dbContexts = new ConcurrentBag<DbContext>();
        }

        public void SaveDbContext(DbContext dbContext)
        {
            if (!dbContexts.Contains(dbContext))
            {
                dbContexts.Add(dbContext);
            }
        }
        public IEnumerable<DbContext> GetDbContexts()
        {
            return dbContexts.ToList();
        }

        public void SavePoolChanges()
        {
            foreach (var dbContext in dbContexts)
            {
                if (dbContext.ChangeTracker.HasChanges())
                {
                    dbContext.SaveChanges();
                }
            }
        }

        public async Task SavePoolChangesAsync()
        {
            foreach (var dbContext in dbContexts)
            {
                if (dbContext.ChangeTracker.HasChanges())
                {
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
