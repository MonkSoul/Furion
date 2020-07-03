using Fur.ApplicationSystem;
using Fur.ApplicationSystem.Models;
using Fur.DatabaseVisitor.Dependencies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Fur.DatabaseVisitor.DbContexts
{
    /// <summary>
    /// DbContext 抽象父类
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class FurDbContextOfT<TDbContext> : DbContext where TDbContext : DbContext
    {
        private static bool IsScanViewEntity = false;

        public FurDbContextOfT(DbContextOptions<TDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AutoConfigureDbViewEntity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void AutoConfigureDbViewEntity(ModelBuilder modelBuilder)
        {
            if (IsScanViewEntity) return;

            var viewTypes = ApplicationGlobal.ApplicationInfo.PublicClassTypes.Where(u => typeof(View).IsAssignableFrom(u.Type));
            foreach (var viewType in viewTypes)
            {
                var entityMethodInfo = modelBuilder.GetType().GetMethods()
                    .Where(u => u.Name == "Entity" && u.GetParameters().Length == 0)
                    .FirstOrDefault();
                entityMethodInfo = entityMethodInfo.MakeGenericMethod(new Type[] { viewType.Type });
                var entityTypeBuilder = entityMethodInfo.Invoke(modelBuilder, null);

                var entityTypeBuilderType = entityTypeBuilder.GetType();
                var hasNoKeyMethodInfo = entityTypeBuilderType.GetMethod("HasNoKey");
                hasNoKeyMethodInfo.Invoke(entityTypeBuilder, null);

                var relationalEntityTypeBuilderExtensionsType = typeof(RelationalEntityTypeBuilderExtensions);
                var toViewMethod = relationalEntityTypeBuilderExtensionsType.GetMethods()
                    .Where(u => u.Name == "ToView" && u.GetParameters().Length == 2 && u.GetParameters().First().ParameterType.IsGenericType)
                    .FirstOrDefault();

                toViewMethod = toViewMethod.MakeGenericMethod(new Type[] { viewType.Type });
                var viewInstance = Activator.CreateInstance(viewType.Type) as View;
                toViewMethod.Invoke(null, new object[] { entityTypeBuilder, viewInstance.ToViewName });
            }

            IsScanViewEntity = true;
        }
    }
}
