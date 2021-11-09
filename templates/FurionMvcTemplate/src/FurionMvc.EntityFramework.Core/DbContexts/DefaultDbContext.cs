using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionMvc.EntityFramework.Core;

[AppDbContext("FurionMvc", DbProvider.Sqlite)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}
