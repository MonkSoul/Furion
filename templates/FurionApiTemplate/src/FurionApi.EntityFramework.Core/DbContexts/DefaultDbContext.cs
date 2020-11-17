using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionApi.EntityFramework.Core
{
    [AppDbContext("FurionApi")]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}