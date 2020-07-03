using Fur.ApplicationSystem;
using Fur.DatabaseVisitor.Dependencies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

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
            ResolveModelBuilderMethods(modelBuilder);
            foreach (var viewType in viewTypes)
            {
                modelBuilderEntityMethod = modelBuilderEntityMethod.MakeGenericMethod(new Type[] { viewType.Type });
                var entityTypeBuilder = modelBuilderEntityMethod.Invoke(modelBuilder, null);

                var entityTypeBuilderType = entityTypeBuilder.GetType();
                var hasNoKeyMethodInfo = entityTypeBuilderType.GetMethod("HasNoKey");
                hasNoKeyMethodInfo.Invoke(entityTypeBuilder, null);

                entityBuilderEntityToViewMethod = entityBuilderEntityToViewMethod.MakeGenericMethod(new Type[] { viewType.Type });
                var viewInstance = Activator.CreateInstance(viewType.Type) as View;
                entityBuilderEntityToViewMethod.Invoke(null, new object[] { entityTypeBuilder, viewInstance.ToViewName });
            }

            IsScanViewEntity = true;
        }

        private static MethodInfo modelBuilderEntityMethod = null;
        private static MethodInfo entityBuilderEntityToViewMethod = null;
        private static void ResolveModelBuilderMethods(ModelBuilder modelBuilder)
        {
            if (modelBuilderEntityMethod == null)
            {
                modelBuilderEntityMethod = modelBuilder.GetType().GetMethods()
                    .Where(u => u.Name == "Entity" && u.GetParameters().Length == 0)
                    .FirstOrDefault();
            }

            if (entityBuilderEntityToViewMethod == null)
            {
                var relationalEntityTypeBuilderExtensionsType = typeof(RelationalEntityTypeBuilderExtensions);
                entityBuilderEntityToViewMethod = relationalEntityTypeBuilderExtensionsType.GetMethods()
                    .Where(u => u.Name == "ToView" && u.GetParameters().Length == 2 && u.GetParameters().First().ParameterType.IsGenericType)
                    .FirstOrDefault();
            }
        }
    }
}
