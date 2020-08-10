using Autofac;
using Fur.DatabaseAccessor.Entities;
using Fur.DatabaseAccessor.Options;
using Fur.DatabaseAccessor.Providers;
using Fur.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// Fur 数据库上下文构建器
    /// </summary>

    internal static class FurDbContextBuilder
    {
        /// <summary>
        /// 数据库关联实体类型集合
        /// </summary>
        private static readonly IEnumerable<Type> _dbRelevanceEntityTypes;

        /// <summary>
        /// 数据库关联函数集合
        /// </summary>
        private static readonly IEnumerable<MethodInfo> _dbRelevanceFunctions;

        /// <summary>
        /// 模型构建器
        /// </summary>
        private static ModelBuilder _modelBuilder;

        /// <summary>
        /// 当前数据库上下文定位器类型
        /// </summary>
        private static Type _dbContextLocatorType;

        /// <summary>
        /// 当前数据库上下文类型
        /// </summary>
        private static Type _dbContextType;

        /// <summary>
        /// 租户类型
        /// </summary>
        private static readonly Type _tenantType;

        /// <summary>
        /// 基于架构的 多租户提供器
        /// </summary>
        private static IMultipleTenantOnSchemaProvider _multipleTenantOnSchemaProvider;

        private static MethodInfo _entityDelegate;

        /// <summary>
        /// 构造函数
        /// </summary>
        static FurDbContextBuilder()
        {
            _dbRelevanceEntityTypes = App.Assemblies
                .SelectMany(a => a.GetTypes()
                    .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsInterface && (typeof(IDbEntityBase).IsAssignableFrom(t) || typeof(IDbEntityConfigure).IsAssignableFrom(t))));

            if (_dbRelevanceEntityTypes.Any())
            {
                _tenantType = typeof(Tenant);
                _dbRelevanceEntityCache = new ConcurrentDictionary<Type, DbRelevanceEntityBuilder>();
                _dbRelevanceEntityBasicGenericArgumentsCache = new ConcurrentDictionary<(Type, Type), Type[]>();
                _entityDelegate = typeof(ModelBuilder).GetMethods().FirstOrDefault(u => u.Name == nameof(ModelBuilder.Entity) && u.GetParameters().Length == 0);
            }

            _dbRelevanceFunctions = App.Assemblies
                .SelectMany(a => a.GetTypes()
                    .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .Where(m => t.IsAbstract && t.IsSealed && m.IsDefined(typeof(Attributes.DbFunctionAttribute), false))));
        }

        /// <summary>
        /// 扫描数据库对象类型加入模型构建器中
        /// </summary>
        /// <remarks>
        /// <para>包括视图、存储过程、函数（标量函数/表值函数）初始化、及种子数据、查询筛选器配置</para>
        /// </remarks>
        /// <param name="modelBuilder">模型上下文</param>
        /// <param name="dbContextLocatorType">数据库上下文定位器</param>
        /// <param name="dbContext">数据库上下文</param>
        internal static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, Type dbContextLocatorType, DbContext dbContext)
        {
            _modelBuilder = modelBuilder;
            _dbContextLocatorType = dbContextLocatorType;

            // 查找当前数据库上下文关联的实体类型
            var dbContextRelevanceEntityTypes = _dbRelevanceEntityTypes.Where(u => IsThisDbContextRelevanceEntityType(u));

            // 如果没找到，则跳过
            if (!dbContextRelevanceEntityTypes.Any())
                goto DbFunctionConfigureTag;

            // 注册基于Schema架构的多租户模式
            if (App.SupportedMultipleTenant && App.MultipleTenantOptions == FurMultipleTenantOptions.OnSchema && dbContextLocatorType != typeof(FurMultipleTenanDbContextLocator))
            {
                _multipleTenantOnSchemaProvider = dbContext.GetService<ILifetimeScope>()
                    .Resolve<IMultipleTenantOnSchemaProvider>();
            }

            // 判断当前数据库上下文是否有全局查询筛选器
            _dbContextType = dbContext.GetType();
            var hasDbContextQueryFilter = typeof(IDbContextQueryFilter).IsAssignableFrom(_dbContextType);

            foreach (var dbRelevanceEntityType in dbContextRelevanceEntityTypes)
            {
                // 获取实体状态器
                var dbRelevanceEntityBuilder = GetDbRelevanceEntityBuilder(dbRelevanceEntityType);

                EntityTypeBuilder entityTypeBuilder = default;

                // 配置数据库无键实体
                DbNoKeyEntityConfigure(dbRelevanceEntityBuilder, ref entityTypeBuilder);

                // 配置数据库实体类型构建器
                DbEntityTypeBuilderConfigure(dbRelevanceEntityBuilder, ref entityTypeBuilder);

                // 配置数据库种子数据
                DbSeedDataConfigure(dbContext, dbRelevanceEntityBuilder, ref entityTypeBuilder);

                // 配置数据库上下文查询筛选器
                DbContextQueryFilterConfigure(dbContext, dbRelevanceEntityBuilder, hasDbContextQueryFilter, ref entityTypeBuilder);

                // 配置数据库查询筛选器
                DbQueryFilterConfigure(dbContext, dbRelevanceEntityBuilder, ref entityTypeBuilder);

                // 配置模型类型构建器
                CreateDbEntityTypeBuilderIfNull(dbRelevanceEntityType, ref entityTypeBuilder);
            }

            // 配置数据库函数
            DbFunctionConfigureTag: DbFunctionConfigure();
        }

        /// <summary>
        /// 配置数据库无键实体
        /// </summary>
        /// <param name="dbRelevanceEntityBuilder">数据库关联实体构建器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbNoKeyEntityConfigure(DbRelevanceEntityBuilder dbRelevanceEntityBuilder, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (!dbRelevanceEntityBuilder.IsDbNoKeyEntityType) return;

            var dbRelevanceEntityType = dbRelevanceEntityBuilder.DbRelevanceEntityType;
            CreateDbEntityTypeBuilderIfNull(dbRelevanceEntityType, ref entityTypeBuilder);

            // 配置视图
            entityTypeBuilder.HasNoKey();
            entityTypeBuilder.ToView((Activator.CreateInstance(dbRelevanceEntityType) as IDbNoKeyEntity).DB_DEFINED_NAME);
        }

        /// <summary>
        /// 配置数据库实体类型构建器
        /// </summary>
        /// <param name="dbRelevanceEntityBuilder">数据库关联实体构建器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbEntityTypeBuilderConfigure(DbRelevanceEntityBuilder dbRelevanceEntityBuilder, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (dbRelevanceEntityBuilder.GenericArgumentTypesForInterfaces == null) return;
            var dbEntityGenericArguments = dbRelevanceEntityBuilder.GenericArgumentTypesForInterfaces.FirstOrDefault(u => typeof(IDbEntityBuilder).IsAssignableFrom(u.Key)).Value;
            if (dbEntityGenericArguments == null || !dbEntityGenericArguments.Any()) return;

            CreateDbEntityTypeBuilderIfNull(dbEntityGenericArguments.First(), ref entityTypeBuilder);

            var dbRelevanceEntityType = dbRelevanceEntityBuilder.DbRelevanceEntityType;
            // 配置实体构建器
            entityTypeBuilder = dbRelevanceEntityType.CallMethod(
                      nameof(IDbEntityBuilder<IDbEntityBase>.HasEntityBuilder),
                      Activator.CreateInstance(dbRelevanceEntityType),
                      entityTypeBuilder
                  ) as EntityTypeBuilder;
        }

        /// <summary>
        /// 配置数据库种子数据
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbRelevanceEntityBuilder">数据库关联实体构建器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbSeedDataConfigure(DbContext dbContext, DbRelevanceEntityBuilder dbRelevanceEntityBuilder, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbRelevanceEntityType = dbRelevanceEntityBuilder.DbRelevanceEntityType;
            if (typeof(IDbNoKeyEntity).IsAssignableFrom(dbRelevanceEntityType) || dbRelevanceEntityBuilder.GenericArgumentTypesForInterfaces == null) return;

            var dbEntityGenericArguments = dbRelevanceEntityBuilder.GenericArgumentTypesForInterfaces.FirstOrDefault(u => typeof(IDbSeedData).IsAssignableFrom(u.Key)).Value;
            if (dbEntityGenericArguments == null || !dbEntityGenericArguments.Any()) return;

            CreateDbEntityTypeBuilderIfNull(dbEntityGenericArguments.First(), ref entityTypeBuilder);

            // 配置种子数据
            var seedDatas = dbRelevanceEntityType.CallMethod(
                   nameof(IDbSeedData<IDbEntityBase>.HasData),
                   Activator.CreateInstance(dbRelevanceEntityType),
                   dbContext
               ).Adapt<IEnumerable<object>>();

            if (seedDatas == null && !seedDatas.Any()) return;

            entityTypeBuilder.HasData(seedDatas);
        }

        /// <summary>
        /// 配置数据库查询筛选器
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbRelevanceEntityBuilder">数据库关联实体构建器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbQueryFilterConfigure(DbContext dbContext, DbRelevanceEntityBuilder dbRelevanceEntityBuilder, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbRelevanceEntityType = dbRelevanceEntityBuilder.DbRelevanceEntityType;

            // 租户表无需参与过滤器
            if (dbRelevanceEntityType == _tenantType || dbRelevanceEntityBuilder.GenericArgumentTypesForInterfaces == null) return;

            var dbEntityGenericArguments = dbRelevanceEntityBuilder.GenericArgumentTypesForInterfaces.FirstOrDefault(u => typeof(IDbQueryFilter).IsAssignableFrom(u.Key)).Value;
            if (dbEntityGenericArguments == null || !dbEntityGenericArguments.Any()) return;

            CreateDbEntityTypeBuilderIfNull(dbEntityGenericArguments.First(), ref entityTypeBuilder);

            var queryFilters = dbRelevanceEntityType.CallMethod(
                       nameof(IDbQueryFilter<IDbEntityBase>.HasQueryFilter),
                       Activator.CreateInstance(dbRelevanceEntityType)
                       , dbContext
                    ).Adapt<IEnumerable<LambdaExpression>>();

            if (queryFilters == null && !queryFilters.Any()) return;

            foreach (var queryFilter in queryFilters)
            {
                if (queryFilter != null) entityTypeBuilder.HasQueryFilter(queryFilter);
            }
        }

        /// <summary>
        /// 配置数据库函数类型
        /// </summary>
        private static void DbFunctionConfigure()
        {
            var dbFunctionMethods = _dbRelevanceFunctions.Where(u => IsThisDbContextRelevanceFunction(u, _dbContextLocatorType));

            foreach (var dbFunction in dbFunctionMethods)
            {
                _modelBuilder.HasDbFunction(dbFunction);
            }
        }

        /// <summary>
        /// 配置数据库上下文查询筛选器
        /// </summary>
        /// <remarks>
        /// <para>一旦数据库上下文继承该接口，那么该数据库上下文所有的实体都将应用该查询筛选器</para>
        /// </remarks>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbRelevanceEntityBuilder">数据库关联实体构建器</param>
        /// <param name="hasDbContextQueryFilter">当前上下文是否有查询筛选器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbContextQueryFilterConfigure(DbContext dbContext, DbRelevanceEntityBuilder dbRelevanceEntityBuilder, bool hasDbContextQueryFilter, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbRelevanceEntityType = dbRelevanceEntityBuilder.DbRelevanceEntityType;

            // 租户表无需参与过滤器
            if (dbRelevanceEntityType == _tenantType) return;

            if (!hasDbContextQueryFilter || !dbRelevanceEntityBuilder.IsDbEntityType) return;

            CreateDbEntityTypeBuilderIfNull(dbRelevanceEntityType, ref entityTypeBuilder);
            _dbContextType.CallMethod(nameof(IDbContextQueryFilter.HasQueryFilter)
                , dbContext
                , dbRelevanceEntityType
                , entityTypeBuilder
             );
        }

        /// <summary>
        /// 创建数据库实体类型构建器
        /// </summary>
        /// <param name="dbRelevanceEntityType">数据库关联实体构建器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void CreateDbEntityTypeBuilderIfNull(Type dbRelevanceEntityType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var isNoSetEntityType = entityTypeBuilder == null;
            var modelBuilderEntity = (Func<EntityTypeBuilder>)Delegate.CreateDelegate(typeof(Func<EntityTypeBuilder>), _modelBuilder, _entityDelegate.MakeGenericMethod(dbRelevanceEntityType));
            entityTypeBuilder ??= modelBuilderEntity();

            // 忽略租户Id
            if (!isNoSetEntityType ||
                dbRelevanceEntityType == _tenantType ||
                (!App.SupportedMultipleTenant && typeof(IDbNoKeyEntity).IsAssignableFrom(dbRelevanceEntityType))) return;

            if (!App.SupportedMultipleTenant)
            {
                entityTypeBuilder.Ignore(nameof(DbEntityBase.TenantId));
                return;
            }

            if (_multipleTenantOnSchemaProvider != null)
            {
                var tableName = dbRelevanceEntityType.Name;
                var schema = _multipleTenantOnSchemaProvider.GetSchema();
                if (dbRelevanceEntityType.IsDefined(typeof(TableAttribute), false))
                {
                    var tableAttribute = dbRelevanceEntityType.GetCustomAttribute<TableAttribute>();
                    tableName = tableAttribute.Name;
                    if (tableAttribute.Schema.HasValue())
                    {
                        schema = tableAttribute.Schema;
                    }
                }
                entityTypeBuilder.ToTable(tableName, schema);
            }
        }

        /// <summary>
        /// 判断是否是当前数据库上下文实体关联类型
        /// </summary>
        /// <param name="dbRelevanceEntityType">数据库实体关联类型</param>
        /// <returns></returns>
        private static bool IsThisDbContextRelevanceEntityType(Type dbRelevanceEntityType)
        {
            // 判断是否启用多租户，如果不启用，则默认不解析 Tenant 类型，返回 false
            if (!App.SupportedMultipleTenant && dbRelevanceEntityType == _tenantType) return false;

            // 如果继承 IDbEntityConfigure，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
            if (typeof(IDbEntityConfigure).IsAssignableFrom(dbRelevanceEntityType))
            {
                var typeGenericArguments = GetGenericArgumentsForInterface(dbRelevanceEntityType, typeof(IDbEntityConfigure));

                if (!App.SupportedMultipleTenant && typeGenericArguments.First() == _tenantType) return false;
                if (CheckIsInDbContextLocators(typeGenericArguments.Skip(1), _dbContextLocatorType)) return true;
            }

            // 如果父类不是泛型类型，则返回 true
            if (dbRelevanceEntityType.BaseType == typeof(DbEntity) || dbRelevanceEntityType.BaseType == typeof(DbEntityBase) || dbRelevanceEntityType.BaseType == typeof(DbNoKeyEntity) || dbRelevanceEntityType.BaseType == typeof(object)) return true;
            // 如果是泛型类型，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
            else
            {
                var typeGenericArguments = GetGenericArgumentsForBaseType(dbRelevanceEntityType, typeof(IDbEntityBase));
                if (CheckIsInDbContextLocators(typeof(IDbNoKeyEntity).IsAssignableFrom(dbRelevanceEntityType) ? typeGenericArguments : typeGenericArguments.Skip(1), _dbContextLocatorType)) return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否是当前数据库上下文函数关联类型
        /// </summary>
        /// <param name="methodInflation">方法包装器</param>
        /// <param name="dbContextLocatorType">数据库上下文定位器</param>
        /// <returns></returns>
        private static bool IsThisDbContextRelevanceFunction(MethodInfo method, Type dbContextLocatorType)
        {
            var dbFunctionAttribute = method.GetCustomAttribute<Attributes.DbFunctionAttribute>();
            if (CheckIsInDbContextLocators(dbFunctionAttribute.DbContextLocators, dbContextLocatorType)) return true;

            return false;
        }

        /// <summary>
        /// 数据库关联实体类型缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<Type, DbRelevanceEntityBuilder> _dbRelevanceEntityCache;

        /// <summary>
        /// 获取数据库实体构建器
        /// </summary>
        /// <param name="dbRelevanceEntityType"></param>
        /// <returns></returns>
        private static DbRelevanceEntityBuilder GetDbRelevanceEntityBuilder(Type dbRelevanceEntityType)
        {
            var isCached = _dbRelevanceEntityCache.TryGetValue(dbRelevanceEntityType, out DbRelevanceEntityBuilder dbRelevanceEntityBuilder);
            if (isCached) return dbRelevanceEntityBuilder;

            var stater = new DbRelevanceEntityBuilder
            {
                DbRelevanceEntityType = dbRelevanceEntityType,
                IsDbEntityType = typeof(IDbEntityBase).IsAssignableFrom(dbRelevanceEntityType),
                IsDbNoKeyEntityType = typeof(IDbNoKeyEntity).IsAssignableFrom(dbRelevanceEntityType)
            };
            stater.GenericArgumentTypesForBaseType = GetGenericArgumentsForBaseType(dbRelevanceEntityType, typeof(IDbNoKeyEntity));

            var interfaces = dbRelevanceEntityType.GetInterfaces()
                .Where(u => u.IsGenericType && typeof(IDbEntityConfigure).IsAssignableFrom(u.GetGenericTypeDefinition()));

            if (interfaces.Any())
            {
                var interfaceGenericArgumentTypes = new Dictionary<Type, IEnumerable<Type>>();
                foreach (var inter in interfaces)
                {
                    var genericArguments = inter.GetGenericArguments();
                    interfaceGenericArgumentTypes.Add(inter, genericArguments);

                    // 缓存
                    _dbRelevanceEntityBasicGenericArgumentsCache.TryAdd((dbRelevanceEntityType, inter), genericArguments);
                }
                stater.GenericArgumentTypesForInterfaces = interfaceGenericArgumentTypes;
            }

            dbRelevanceEntityBuilder = stater;
            _dbRelevanceEntityCache.TryAdd(dbRelevanceEntityType, stater);

            return dbRelevanceEntityBuilder;
        }

        /// <summary>
        /// 检查当前数据库上下文定位器是否在指定的数据库上下文定位器集合中
        /// </summary>
        /// <param name="dbContextLocators">数据库上下文定位器集合</param>
        /// <param name="dbContextLocatorType">数据库上下文定位器</param>
        /// <returns>bool</returns>
        private static bool CheckIsInDbContextLocators(IEnumerable<Type> dbContextLocators, Type dbContextLocatorType)
        {
            return dbContextLocators == null || dbContextLocators.Count() == 0 || dbContextLocators.Contains(dbContextLocatorType);
        }

        /// <summary>
        /// 数据库关联实体类型基类型（含接口）泛型参数缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<(Type, Type), Type[]> _dbRelevanceEntityBasicGenericArgumentsCache;

        /// <summary>
        /// 获取接口泛型参数集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="filterType">过滤类型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        private static Type[] GetGenericArgumentsForInterface(Type type, Type filterType)
        {
            var isCached = _dbRelevanceEntityBasicGenericArgumentsCache.TryGetValue((type, filterType), out Type[] genericArguments);
            if (isCached) return genericArguments;

            genericArguments = type.GetInterfaces()
                    .FirstOrDefault(c => c.IsGenericType && filterType.IsAssignableFrom(c.GetGenericTypeDefinition()))
                    ?.GetGenericArguments();

            _dbRelevanceEntityBasicGenericArgumentsCache.TryAdd((type, filterType), genericArguments);
            return genericArguments;
        }

        /// <summary>
        /// 获取父类泛型参数集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="filterType">过滤类型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        private static Type[] GetGenericArgumentsForBaseType(Type type, Type filterType)
        {
            var isCached = _dbRelevanceEntityBasicGenericArgumentsCache.TryGetValue((type, filterType), out Type[] genericArguments);
            if (isCached) return genericArguments;

            var baseType = type.BaseType;
            if (baseType.IsGenericType && filterType.IsAssignableFrom(baseType.GetGenericTypeDefinition()))
            {
                genericArguments = baseType.GetGenericArguments();
            }
            _dbRelevanceEntityBasicGenericArgumentsCache.TryAdd((type, filterType), genericArguments);
            return genericArguments;
        }
    }
}