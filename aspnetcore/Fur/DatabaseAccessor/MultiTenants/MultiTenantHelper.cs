using Autofac;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor.MultiTenants
{
    public static class MultiTenantHelper
    {
        public static Expression<Func<TEntity, bool>> MultiTenantQueryFilter<TEntity>(DbContext dbContext) where TEntity : IDbEntity
        {
            var lifetimeScope = dbContext.GetService<ILifetimeScope>();
            if (!lifetimeScope.IsRegistered<IMultiTenantProvider>()) return default;

            var tenantProvider = lifetimeScope.Resolve<IMultiTenantProvider>();

            return b => EF.Property<int>(b, nameof(DbEntityBase.TenantId)) == tenantProvider.GetTenantId();
        }
    }
}
