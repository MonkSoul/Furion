using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace FurApp.EntityFramework.Core
{
    [AppDbContext("FurApp")]
    public class FurAppDbContext : AppDbContext<FurAppDbContext>
    {
        public FurAppDbContext(DbContextOptions<FurAppDbContext> options) : base(options)
        {
        }
    }
}