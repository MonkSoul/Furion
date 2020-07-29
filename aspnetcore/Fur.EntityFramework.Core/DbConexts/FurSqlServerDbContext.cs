using Autofac;
using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core.DbContexts
{
    public class FurSqlServerDbContext : FurDbContextOfT<FurSqlServerDbContext, FurDbContextLocator>
    {
        private readonly ILifetimeScope _lifetimeScope;
        public FurSqlServerDbContext(DbContextOptions<FurSqlServerDbContext> options
            , ILifetimeScope lifetimeScope) : base(options)
        {
            _lifetimeScope = lifetimeScope;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServerWithMultipleTenantOnDatabase(_lifetimeScope);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}