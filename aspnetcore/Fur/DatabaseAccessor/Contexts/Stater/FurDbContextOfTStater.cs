using Autofac;
using Fur.ApplicationBase;
using Fur.ApplicationBase.Wrappers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.Filters;
using Fur.DatabaseAccessor.Models.Seed;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseAccessor.Contexts.Stater
{
    /// <summary>
    /// Fur 数据库上下文 状态器
    /// <para>主要用于解决基类初始化方法重复调用问题</para>
    /// </summary>
    internal static class FurDbContextOfTStater
    {
        /// <summary>
        /// 检查 Fur 数据库上下文是否调用过 <see cref="FurDbContextOfT{TDbContext}.OnConfiguring(DbContextOptionsBuilder)"/>，避免重复初始化
        /// <para>默认 <c>false</c></para>
        /// </summary>
        private static bool onConfiguringStater = false;

        /// <summary>
        /// 检查 Fur 数据库上下文是否调用过 <see cref="FurDbContextOfT{TDbContext}.OnModelCreating(ModelBuilder)"/>，避免重复初始化
        /// </summary>
        private static bool onModelCreatingStater = false;

        #region 检查 Fur 数据库上下文是否调用过 OnConfiguring + internal static bool OnConfiguringStater()

        /// <summary>
        /// 检查 Fur 数据库上下文是否调用过 <see cref="FurDbContextOfT{TDbContext}.OnConfiguring(DbContextOptionsBuilder)"/>，避免重复初始化
        /// </summary>
        /// <returns></returns>
        internal static bool OnConfiguringStater()
        {
            if (!onConfiguringStater)
            {
                onConfiguringStater = true;
                return false;
            }
            return true;
        }

        #endregion

        #region 检查 Fur 数据库上下文是否调用过 OnModelCreating + internal static bool OnModelCreatingStater()
        /// <summary>
        /// 检查 Fur 数据库上下文是否调用过 <see cref="FurDbContextOfT{TDbContext}.OnModelCreating(ModelBuilder)"/>，避免重复初始化
        /// </summary>
        /// <returns>是或否</returns>
        internal static bool OnModelCreatingStater()
        {
            if (!onModelCreatingStater)
            {
                onModelCreatingStater = true;
                return false;
            }
            return true;
        }
        #endregion

        #region 扫描数据库对象类型加入模型构建器中 + internal static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, DbContext dbContext, ILifetimeScope lifetimeScope)
        /// <summary>
        /// 扫描数据库对象类型加入模型构建器中
        /// <para>包括视图、存储过程、函数（标量函数/表值函数）初始化、及种子数据、查询筛选器配置</para>
        /// </summary>
        /// <param name="modelBuilder">模型构建器，参见：<see cref="ModelBuilder"/></param>
        /// <param name="dbContext">数据库上下文，参见：<see cref="DbContext"/></param>
        internal static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, DbContext dbContext)
        {
            var publicClassTypes = ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers;
            var lifetimeScope = dbContext.GetService<ILifetimeScope>();

            // 配置无键实体
            ConfigureNoKeyEntity(publicClassTypes, modelBuilder);

            // 配置数据库函数
            ConfigureDbFunction(modelBuilder);

            // 配置种子数据
            ConfigureSeedData(publicClassTypes, modelBuilder, dbContext, lifetimeScope);

            //配置数据过滤器
            ConfigureQueryFilter(publicClassTypes, modelBuilder, dbContext, lifetimeScope);
        }
        #endregion

        #region 配置种子数据 + private static void ConfigureSeedData(IEnumerable<TypeWrapper> publicClassTypes, ModelBuilder modelBuilder, DbContext dbContext, ILifetimeScope lifetimeScope)
        /// <summary>
        /// 配置种子数据
        /// </summary>
        /// <param name="publicClassTypes">公开类型包装器</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="lifetimeScope">Autofac 生命周期对象</param>
        private static void ConfigureSeedData(IEnumerable<TypeWrapper> publicClassTypes, ModelBuilder modelBuilder, DbContext dbContext, ILifetimeScope lifetimeScope)
        {
            var dataSeedTypes = publicClassTypes.Where(u => u.CanBeNew &&
                                                                                                u.Type.GetInterfaces().Any(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IDbDataSeedOfT<>)));
            foreach (var seedType in dataSeedTypes)
            {
                var seedDataType = seedType.Type;

                var dbEntityType = seedDataType.GetInterfaces()
                    .FirstOrDefault(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IDbDataSeedOfT<>))
                    .GetGenericArguments().FirstOrDefault();

                var seedTypeInstance = Activator.CreateInstance(seedDataType);

                var hasDataMethod = seedDataType.GetMethod(nameof(IDbDataSeedOfT<IDbEntity>.HasData));

                if (!(hasDataMethod.Invoke(seedTypeInstance, new object[] { dbContext, lifetimeScope }) is IEnumerable<object> seedData) || seedData.Count() == 0) continue;

                var entityTypeBuilder = modelBuilder.Entity(dbEntityType);
                entityTypeBuilder.HasData(seedData);
            }
        }
        #endregion

        #region 配置无键实体 + private static void ConfigureNoKeyEntity(IEnumerable<TypeWrapper> publicClassTypes, ModelBuilder modelBuilder)
        /// <summary>
        /// 配置无键实体
        /// </summary>
        /// <param name="publicClassTypes">公开类型包装器</param>
        /// <param name="modelBuilder">模型构建器</param>
        private static void ConfigureNoKeyEntity(IEnumerable<TypeWrapper> publicClassTypes, ModelBuilder modelBuilder)
        {
            var noKeyEntityTypes = publicClassTypes.Where(u => u.CanBeNew && typeof(IDbNoKeyEntity).IsAssignableFrom(u.Type));
            foreach (var noKeyEntityType in noKeyEntityTypes)
            {
                var entityTypeBuilder = modelBuilder.Entity(noKeyEntityType.Type);
                entityTypeBuilder.HasNoKey();

                var noKeyEntityInstance = Activator.CreateInstance(noKeyEntityType.Type) as IDbNoKeyEntity;
                entityTypeBuilder.ToView(noKeyEntityInstance.__NAME__);
            }
        }
        #endregion

        #region 配置查询筛选器 + private static void ConfigureQueryFilter(IEnumerable<TypeWrapper> publicClassTypes, ModelBuilder modelBuilder, DbContext dbContext, ILifetimeScope lifetimeScope)
        /// <summary>
        /// 配置查询筛选器
        /// </summary>
        /// <param name="publicClassTypes">公开类型包装器</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="lifetimeScope">Autofac 生命周期对象</param>
        private static void ConfigureQueryFilter(IEnumerable<TypeWrapper> publicClassTypes, ModelBuilder modelBuilder, DbContext dbContext, ILifetimeScope lifetimeScope)
        {
            var queryFilterTypes = publicClassTypes.Where(u => u.CanBeNew &&
                                                                                            u.Type.GetInterfaces().Any(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IDbQueryFilterOfT<>)));

            foreach (var queryFilterType in queryFilterTypes)
            {
                var filterType = queryFilterType.Type;

                var dbEntityType = filterType.GetInterfaces()
                     .FirstOrDefault(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IDbQueryFilterOfT<>))
                     .GetGenericArguments().FirstOrDefault();

                var queryFilterTypeInstance = Activator.CreateInstance(filterType);

                var hasQueryFilterMethod = filterType.GetMethod(nameof(IDbQueryFilterOfT<IDbEntity>.HasQueryFilter));
                var queryFilters = hasQueryFilterMethod.Invoke(queryFilterTypeInstance, new object[] { dbContext, lifetimeScope }).Adapt<Dictionary<LambdaExpression, IEnumerable<Type>>>();

                if (queryFilters == null || queryFilters.Count == 0) continue;

                var entityTypeBuilder = modelBuilder.Entity(dbEntityType);
                foreach (var queryFilter in queryFilters.Keys)
                {
                    if (queryFilters[queryFilter].Any(u => lifetimeScope.ResolveNamed<DbContext>(u.Name).GetType().Name == dbContext.GetType().Name))
                    {
                        entityTypeBuilder.HasQueryFilter(queryFilter);
                    }
                }
            }
        }
        #endregion

        #region 配置数据库函数 + private static void ConfigureDbFunction(ModelBuilder modelBuilder)
        /// <summary>
        /// 配置数据库函数
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        private static void ConfigureDbFunction(ModelBuilder modelBuilder)
        {
            var dbFunctionMethods = ApplicationCore.ApplicationWrapper.PublicMethodWrappers
                .Where(u => u.IsStaticMethod && u.Method.IsDefined(typeof(DbFunctionAttribute)) && u.ThisDeclareType.IsAbstract && u.ThisDeclareType.IsSealed);

            foreach (var dbFunction in dbFunctionMethods)
            {
                modelBuilder.HasDbFunction(dbFunction.Method);
            }
        }
        #endregion
    }
}
