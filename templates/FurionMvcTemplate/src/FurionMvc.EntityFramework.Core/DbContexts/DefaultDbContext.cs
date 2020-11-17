using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionMvc.EntityFramework.Core
{
    [AppDbContext("FurionMvc")]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}