using Fur.DatabaseVisitor.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseVisitor.TenantSaaS
{
    public class FurTenantDbContext : FurDbContextOfT<FurTenantDbContext>
    {
        public FurTenantDbContext(DbContextOptions<FurTenantDbContext> options) : base(options)
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