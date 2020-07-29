using Autofac;
using Fur.DatabaseAccessor.Extensions;
using Fur.EntityFramework.Core.DbContexts;
using Fur.EntityFramework.Core.MultipleTenantProviders;

namespace Fur.EntityFramework.Core
{
    public class FurEntityFrameworkCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterDbContexts<FurSqlServerDbContext>(options =>
            {
                options.AddMultipleTenantConfigure<FurMultipleTenantDbContext, FurMultipleTenantOnSchemaProvider>();
            });
        }
    }
}