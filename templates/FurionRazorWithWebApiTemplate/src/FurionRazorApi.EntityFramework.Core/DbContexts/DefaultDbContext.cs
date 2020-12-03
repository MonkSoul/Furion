using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionRazorApi.EntityFramework.Core
{
    [AppDbContext("FurionRazorApi")]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}