using Autofac;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.Repositories;
using Fur.EntityFramework.Core.DbContexts;
using Fur.Record.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Fur.EntityFramework.Core
{
    public class FurEntityFrameworkCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FurSqlServerDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FurSqlServerDbContext>()
                .Named<DbContext>(nameof(DbContextIdentifier))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(MultipleEFCoreRepositoryOfT<,>))
                    .As(typeof(IMultipleRepositoryOfT<,>))
                    .InstancePerLifetimeScope();

            builder.RegisterType<FurMultipleSqlServerDbContext>()
                .Named<DbContext>(nameof(FurMultipleDbContextIdentifier))
                .InstancePerLifetimeScope();
        }
    }
}