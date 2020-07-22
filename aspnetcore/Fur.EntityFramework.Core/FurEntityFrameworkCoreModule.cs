using Autofac;
using Fur.Core.DbContextIdentifiers;
using Fur.DatabaseVisitor.Extensions.Injection;
using Fur.DatabaseVisitor.Providers;
using Fur.EntityFramework.Core.DbContexts;

namespace Fur.EntityFramework.Core
{
    public class FurEntityFrameworkCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterDefaultDbContext<FurSqlServerDbContext>()
                       .RegisterMultipleRepository<FurMultipleSqlServerDbContext, FurMultipleDbContextIdentifier>()
                       .RegisterTenant<TenantProvider>()
                       .RegisterRepositories()
                       .RegisterTangentDbContext();
        }
    }
}