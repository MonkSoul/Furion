using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionApp.EntityFramework.Core
{
    [AppDbContext("FurionApp")]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}