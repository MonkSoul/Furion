using Fur.Core;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core
{
    public sealed class FurDbContext2 : AppDbContext<FurDbContext2, FurDbContextLocator2>
    {
        public FurDbContext2(DbContextOptions<FurDbContext2> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>();
        }
    }
}