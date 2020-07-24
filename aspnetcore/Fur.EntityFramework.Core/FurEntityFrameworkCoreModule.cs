using Autofac;
using Fur.Core.DbContextIdentifiers;
using Fur.DatabaseAccessor.Extensions.Injection;
using Fur.DatabaseAccessor.Providers;
using Fur.EntityFramework.Core.DbContexts;

namespace Fur.EntityFramework.Core
{
    public class FurEntityFrameworkCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterDefaultDbContext<FurSqlServerDbContext>()
                       .RegisterMultipleRepository<FurMultipleSqlServerDbContext, FurMultipleDbContextIdentifier>()
                       .RegisterMultiTenant<MultiTenantProvider>()
                       .RegisterRepositories()
                       .RegisterTangentDbContext();
        }
    }
}