using Fur.Core;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core
{
    public sealed class FurDbContext : AppDbContext<FurDbContext>
    {
        public FurDbContext(DbContextOptions<FurDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>();
        }
    }
}