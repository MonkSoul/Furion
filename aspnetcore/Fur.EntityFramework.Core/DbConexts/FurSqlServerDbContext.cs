using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Contexts.Locators;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core.DbContexts
{
    public class FurSqlServerDbContext : FurDbContext<FurSqlServerDbContext, FurDbContextLocator>
    {
        public FurSqlServerDbContext(DbContextOptions<FurSqlServerDbContext> options)
            : base(options)
        {
        }
    }
}