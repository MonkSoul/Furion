using Autofac;
using Fur.DatabaseAccessor.Models.Seed;
using Fur.DatabaseAccessor.Models.Tenants;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fur.Core.DbEntities
{
    public class TenantDataSeed : IDbDataSeedOfT<Tenant>
    {
        public IEnumerable<Tenant> HasData(DbContext dbContext, ILifetimeScope lifetimeScope)
        {
            if (!lifetimeScope.IsRegistered<IMultiTenantProvider>()) return default;

            return new List<Tenant>()
            {
                new Tenant() { Id = 1, Name = "默认租户", Host = "localhost:44307" },
                new Tenant() { Id = 2, Name = "默认租户", Host = "localhost:41529" },
                new Tenant() { Id = 3, Name = "默认租户", Host = "localhost:41530" }
            };
        }
    }
}
