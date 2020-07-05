using Fur.DatabaseVisitor.DbContexts;
using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.TenantSaaS;
using Fur.Record.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

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
            var tenantProvider = this.GetService<ITenantProvider>();

            modelBuilder.Entity<Test>().HasQueryFilter(b => EF.Property<int>(b, nameof(Entity<int>.TenantId)) == tenantProvider.GetTenantId());

            base.OnModelCreating(modelBuilder);
        }
    }
}