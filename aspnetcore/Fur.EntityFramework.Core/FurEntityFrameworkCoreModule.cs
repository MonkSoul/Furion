using Autofac;
using Fur.EntityFramework.Core.DbContexts;
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
        }
    }
}
