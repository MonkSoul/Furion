using Fur.Core.DbEntities;
using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Extensions.ModelCreating;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core.DbContexts
{
    public class FurSqlServerDbContext : FurDbContextOfT<FurSqlServerDbContext>
    {
        public virtual DbSet<Test> Tests { get; set; }

        public FurSqlServerDbContext(DbContextOptions<FurSqlServerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>().HasTenantIdQueryFilter(TenantProvider);
        }
    }
}