using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionRazor.EntityFramework.Core;

[AppDbContext("FurionRazor", DbProvider.Sqlite)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}
