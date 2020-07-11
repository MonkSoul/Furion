using Autofac;
using Fur.DatabaseVisitor.Extensions.Injection;
using Fur.EntityFramework.Core.DbContexts;
using Fur.Record.Identifiers;

namespace Fur.EntityFramework.Core
{
    public class FurEntityFrameworkCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterDefaultDbContext<FurSqlServerDbContext>()
                       .RegisterMultipleDbContext<FurMultipleSqlServerDbContext, FurMultipleDbContextIdentifier>()
                       .RegisterRepositories();
        }
    }
}