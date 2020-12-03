using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurionBlazorApi.EntityFramework.Core
{
    [AppDbContext("FurionBlazorApi")]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}