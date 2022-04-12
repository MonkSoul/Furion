using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Furion.EntityFramework.Core;

[AppDbContext("Sqlite3ConnectionString", DbProvider.Sqlite)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}
