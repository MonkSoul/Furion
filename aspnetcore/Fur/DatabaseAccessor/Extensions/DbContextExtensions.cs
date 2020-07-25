using Autofac;
using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Fur.DatabaseAccessor.Extensions
{
    [NonWrapper]
    public static class DbContextExtensions
    {
        public static int GetTenantId(this DbContext dbContext)
        {
            var lifetimeScope = dbContext.GetService<ILifetimeScope>();
            if (!lifetimeScope.IsRegistered<IMultipleTenantProvider>()) return default;

            var tenantProvider = lifetimeScope.Resolve<IMultipleTenantProvider>();

            return tenantProvider.GetTenantId();
        }
    }
}
