using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionApi.EntityFramework.Core;

[AppDbContext("Default", DbProvider.Sqlite)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}