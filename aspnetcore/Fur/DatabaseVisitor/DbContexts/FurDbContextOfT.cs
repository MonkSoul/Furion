using Fur.ApplicationSystem;
using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.TenantSaaS;
using Fur.EmitExpression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseVisitor.DbContexts
{
    /// <summary>
    /// DbContext 抽象父类
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class FurDbContextOfT<TDbContext> : DbContext where TDbContext : DbContext
    {
        private ITenantProvider TenantProvider;

        protected int TenantId
        {
            get
            {
                TenantProvider ??= this.GetService<ITenantProvider>();
                return TenantProvider.GetTenantId();
            }
        }

        public virtual DbSet<Tenant> Tenants { get; set; }

        public FurDbContextOfT(DbContextOptions<TDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (FurDbContextOfTStatus.CheckOnConfiguringInit()) return;
            base.OnConfiguring(optionsBuilder);
        }

        public int GetTenantId(string host)
        {
            var tenant = Tenants.FirstOrDefault(t => t.Host == host);
            return tenant?.Id ?? 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (FurDbContextOfTStatus.CheckOnModelCreatingInit()) return;

            TenantProvider ??= this.GetService<ITenantProvider>();

            modelBuilder.Entity<Tenant>().HasData(
                new Tenant()
                {
                    Id = 1,
                    Name = "默认租户",
                    Host = "localhost:44307"
                },
                new Tenant()
                {
                    Id = 2,
                    Name = "默认租户",
                    Host = "localhost:41529"
                });

            AutoConfigureDbViewEntity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void AutoConfigureDbViewEntity(ModelBuilder modelBuilder)
        {
            CreateModelBuilderMethodDelegate();

            var viewTypes = ApplicationGlobal.ApplicationInfo.PublicClassTypes
                .Where(u => typeof(View).IsAssignableFrom(u.Type) && u.CanNewType);

            foreach (var viewType in viewTypes)
            {
                var entityTypeBuilder = ModelBuilderMethod_Entity(modelBuilder, viewType.Type);
                entityTypeBuilder = EntityTypeBuilderMethod_HasNoKey(entityTypeBuilder);

                var viewInstance = ExpressionCreateObject.CreateInstance<View>(viewType.Type);
                entityTypeBuilder = EntityTypeBuilderMethod_ToView(entityTypeBuilder, viewInstance.ToViewName);

                var lambdaExpression = CreateDbViewHasQueryFilterLambdaExpression(viewType.Type, TenantId);
                entityTypeBuilder = EntityTypeBuilderMethod_HasQueryFilter(entityTypeBuilder, lambdaExpression);
            }

            var dbFunctionMethods = ApplicationGlobal.ApplicationInfo.PublicInstanceMethods
                .Where(u => u.IsStaticType && u.Method.IsDefined(typeof(DbFunctionAttribute)) && u.DeclareType.IsAbstract && u.DeclareType.IsSealed);

            foreach (var dbFunction in dbFunctionMethods)
            {
                ModelBuilderMethod_HasDbFunction(modelBuilder, dbFunction.Method);
            }
        }

        private static Func<ModelBuilder, Type, EntityTypeBuilder> ModelBuilderMethod_Entity = null;
        private static Func<EntityTypeBuilder, EntityTypeBuilder> EntityTypeBuilderMethod_HasNoKey = null;
        private static Func<EntityTypeBuilder, string, EntityTypeBuilder> EntityTypeBuilderMethod_ToView = null;
        private static Func<EntityTypeBuilder, LambdaExpression, EntityTypeBuilder> EntityTypeBuilderMethod_HasQueryFilter = null;
        private static Func<ModelBuilder, MethodInfo, DbFunctionBuilder> ModelBuilderMethod_HasDbFunction = null;

        private static void CreateModelBuilderMethodDelegate()
        {
            if (ModelBuilderMethod_Entity == null)
            {
                var entityMethod = typeof(ModelBuilder).GetMethods()
                    .Where(u => u.Name == "Entity" && u.GetParameters().Length == 1 && u.GetParameters().First().ParameterType == typeof(Type))
                    .FirstOrDefault();

                ModelBuilderMethod_Entity = (Func<ModelBuilder, Type, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<ModelBuilder, Type, EntityTypeBuilder>), entityMethod);
            }

            if (EntityTypeBuilderMethod_HasNoKey == null)
            {
                EntityTypeBuilderMethod_HasNoKey = (Func<EntityTypeBuilder, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<EntityTypeBuilder, EntityTypeBuilder>), typeof(EntityTypeBuilder).GetMethod("HasNoKey"));
            }

            if (EntityTypeBuilderMethod_ToView == null)
            {
                var relationalEntityTypeBuilderExtensionsType = typeof(RelationalEntityTypeBuilderExtensions);
                var toViewMethod = relationalEntityTypeBuilderExtensionsType.GetMethods()
                    .Where(u => u.Name == "ToView" && u.GetParameters().Length == 2 && u.GetParameters().Last().ParameterType == typeof(string))
                    .FirstOrDefault();

                EntityTypeBuilderMethod_ToView = (Func<EntityTypeBuilder, string, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<EntityTypeBuilder, string, EntityTypeBuilder>), toViewMethod);
            }

            if (EntityTypeBuilderMethod_HasQueryFilter == null)
            {
                EntityTypeBuilderMethod_HasQueryFilter = (Func<EntityTypeBuilder, LambdaExpression, EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<EntityTypeBuilder, LambdaExpression, EntityTypeBuilder>), typeof(EntityTypeBuilder).GetMethod("HasQueryFilter"));
            }

            if (ModelBuilderMethod_HasDbFunction == null)
            {
                var relationalModelBuilderExtensionsType = typeof(RelationalModelBuilderExtensions);
                var hasDbFunctionMethod = relationalModelBuilderExtensionsType.GetMethods()
                         .Where(u => u.Name == "HasDbFunction" && u.GetParameters().Length == 2 && u.GetParameters().Last().ParameterType == typeof(MethodInfo))
                         .FirstOrDefault();

                ModelBuilderMethod_HasDbFunction = (Func<ModelBuilder, MethodInfo, DbFunctionBuilder>)Delegate.CreateDelegate(typeof(Func<ModelBuilder, MethodInfo, DbFunctionBuilder>), hasDbFunctionMethod);
            }
        }

        private static LambdaExpression CreateDbViewHasQueryFilterLambdaExpression(Type type, int TenantId)
        {
            // b => EF.Property<int>(b, nameof(Entity<int>.TenantId)) == tenantProvider.GetTenantId()
            ParameterExpression parameter = Expression.Parameter(type, "b");

            var efPropertyMethod = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(int));
            ConstantExpression constantKey = Expression.Constant("TenantId");
            ConstantExpression constantValue = Expression.Constant(TenantId);

            var expression = Expression.Equal(Expression.Call(efPropertyMethod, parameter, constantKey), constantValue);

            return Expression.Lambda(expression, parameter);
        }
    }

    internal static class FurDbContextOfTStatus
    {
        private static bool OnConfiguringInit = false;
        private static bool OnModelCreatingInit = false;
        internal static bool CheckOnConfiguringInit()
        {
            if (!OnConfiguringInit)
            {
                OnConfiguringInit = true;
                return false;
            }
            return true;
        }

        internal static bool CheckOnModelCreatingInit()
        {
            if (!OnModelCreatingInit)
            {
                OnModelCreatingInit = true;
                return false;
            }
            return true;
        }
    }
}