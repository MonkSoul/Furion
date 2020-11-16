using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurApi.EntityFramework.Core
{
    [AppDbContext("FurApi")]
    public class FurApiDbContext : AppDbContext<FurApiDbContext>
    {
        public FurApiDbContext(DbContextOptions<FurApiDbContext> options) : base(options)
        {
        }
    }
}