using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core
{
    [AppDbContext("Sqlite3ConnectionString")]
    public class FurDbContext : AppDbContext<FurDbContext>
    {
        public FurDbContext(DbContextOptions<FurDbContext> options) : base(options)
        {
        }
    }
}