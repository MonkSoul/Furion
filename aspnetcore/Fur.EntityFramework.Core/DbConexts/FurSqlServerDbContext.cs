using Fur.ApplicationBase;
using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.QueryFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Fur.EntityFramework.Core.DbContexts
{
    public class FurSqlServerDbContext : FurDbContextOfT<FurSqlServerDbContext, FurDbContextIdentifier>, IDbContextQueryFilter
    {
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
        }

        public void HasQueryFilter(Type dbEntityType, EntityTypeBuilder entityTypeBuilder)
        {
            if (AppGlobal.SupportedMultipleTenant)
            {
                var tenantIdQueryFilterExpression = dbEntityType.QueryFilterExpression<int>(nameof(DbEntity.TenantId), this.GetTenantId());
                entityTypeBuilder.HasQueryFilter(tenantIdQueryFilterExpression);
            }
        }
    }
}