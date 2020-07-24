using Fur.Core.DbContextIdentifiers;
using Fur.Core.DbEntities;
using Fur.DatabaseAccessor.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core.DbContexts
{
    public class FurMultipleSqlServerDbContext : FurDbContextOfT<FurMultipleSqlServerDbContext, FurMultipleDbContextIdentifier>
    {
        public virtual DbSet<Test> Tests { get; set; }

        public FurMultipleSqlServerDbContext(DbContextOptions<FurMultipleSqlServerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}