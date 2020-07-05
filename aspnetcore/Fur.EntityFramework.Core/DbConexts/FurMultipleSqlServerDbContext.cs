using Fur.DatabaseVisitor.DbContexts;
using Fur.Record.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core.DbContexts
{
    public class FurMultipleSqlServerDbContext : FurDbContextOfT<FurMultipleSqlServerDbContext>
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
            modelBuilder.Entity<Test>();

            base.OnModelCreating(modelBuilder);
        }
    }
}