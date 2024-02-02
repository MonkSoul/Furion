using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionBlazorApp.EntityFramework.Core;

[AppDbContext("FurionBlazorApp", DbProvider.Sqlite)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}
