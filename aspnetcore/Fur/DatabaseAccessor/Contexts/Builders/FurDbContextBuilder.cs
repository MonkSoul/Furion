using Autofac;
using Fur.AppCore.Inflations;
using Fur.Attributes;
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
    [NonInflated]
    internal static class FurDbContextBuilder
    {
        /// <summary>
        /// 数据库实体关联的所有类型
        /// </summary>
        /// <remarks>
        /// <para>包含所有继承 <see cref="IDbEntityBase"/> 或 <see cref="IDbEntityConfigure"/> 的类型</para>
        /// </remarks>
        private static readonly IEnumerable<TypeInflation> _dbRelevanceEntityTypes;

        /// <summary>
        /// 数据库函数定义方法集合
        /// </summary>
        private static readonly IEnumerable<MethodInflation> _dbFunctionMethods;


        private static readonly ConcurrentDictionary<(Type, Type), Type[]> _basicGenericArgumentsCache;

        /// <summary>
        /// 模型构建器
        /// </summary>
        private static ModelBuilder _modelBuilder;

        /// <summary>
        /// 数据库上下文定位器类型
        /// </summary>
        private static Type _dbContextLocatorType;

        private static Type _dbContextType;

        /// <summary>
        ///基于架构的 多租户提供器
        /// </summary>
        private static IMultipleTenantOnSchemaProvider _multipleTenantOnSchemaProvider;

        /// <summary>
        /// 数据库实体状态器
        /// </summary>
        private static readonly ConcurrentDictionary<Type, DbRelevanceEntityBuilder> _dbEntityStaters;

        /// <summary>
        /// 租户类型
        /// </summary>
        private static readonly Type _tenantType = typeof(Tenant);

        /// <summary>
        /// 构造函数
        /// </summary>
        static FurDbContextBuilder()
        {
            var application = App.Inflations;

            _dbRelevanceEntityTypes ??= application.ClassTypes.Where(u => u.IsDbEntityRelevanceType);

            if (_dbRelevanceEntityTypes.Any())
            {
                _dbEntityStaters ??= new ConcurrentDictionary<Type, DbRelevanceEntityBuilder>();
                _basicGenericArgumentsCache = new ConcurrentDictionary<(Type, Type), Type[]>();
            }

            _dbFunctionMethods = application.Methods
                .Where(u => u.IsDbFunctionMethod);
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

            // 查找当前数据库上下文相关联的类型
            var dbContextRelevanceTypes = _dbRelevanceEntityTypes.Where(u => IsThisDbContextEntityType(u.ThisType));

            // 如果没有找到则跳过
            if (!dbContextRelevanceTypes.Any())
                goto DbFunctionConfigureTag;

            _dbContextLocatorType = dbContextLocatorType;
            _dbContextType = dbContext.GetType();
            var hasDbContextQueryFilter = typeof(IDbContextQueryFilter).IsAssignableFrom(_dbContextType);

            // 注册基于Schema架构的多租户模式
            if (App.SupportedMultipleTenant && App.MultipleTenantOptions == FurMultipleTenantOptions.OnSchema && dbContextLocatorType != typeof(FurMultipleTenanDbContextLocator))
            {
                _multipleTenantOnSchemaProvider = dbContext
                    .GetService<ILifetimeScope>()
                    .Resolve<IMultipleTenantOnSchemaProvider>();
            }

            // 应该查找一次父接口父类就可以了
            foreach (var typeInflation in dbContextRelevanceTypes)
            {
                var dbRelevanceEntityType = typeInflation.ThisType;
                // 获取实体状态器
                var dbEntityStater = GetDbEntityStater(dbRelevanceEntityType);

                EntityTypeBuilder entityTypeBuilder = default;

                // 配置数据库无键实体
                DbNoKeyEntityConfigure(dbEntityStater, ref entityTypeBuilder);

                // 配置数据库实体类型构建器
                DbEntityTypeBuilderConfigure(dbEntityStater, ref entityTypeBuilder);

                // 配置数据库种子数据
                DbSeedDataConfigure(dbEntityStater, dbContext, ref entityTypeBuilder);

                // 配置数据库上下文查询筛选器
                DbContextQueryFilterConfigure(dbContext, dbRelevanceEntityType, hasDbContextQueryFilter, ref entityTypeBuilder);

                // 配置数据库查询筛选器
                DbQueryFilterConfigure(dbEntityStater, dbContext, ref entityTypeBuilder);

                // 配置模型类型构建器
                CreateDbEntityTypeBuilderIfNull(dbRelevanceEntityType, ref entityTypeBuilder);
            }

            // 配置数据库函数
            DbFunctionConfigureTag: DbFunctionConfigure();
        }

        /// <summary>
        /// 配置数据库无键实体
        /// </summary>
        /// <param name="dbEntityStater">数据库实体状态器</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbNoKeyEntityConfigure(DbRelevanceEntityBuilder dbEntityStater, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (!dbEntityStater.IsDbNoKeyEntityType) return;

            var dbRelevanceEntityType = dbEntityStater.DbRelevanceEntityType;
            CreateDbEntityTypeBuilderIfNull(dbRelevanceEntityType, ref entityTypeBuilder);

            // 配置视图
            entityTypeBuilder.HasNoKey();
            entityTypeBuilder.ToView((Activator.CreateInstance(dbRelevanceEntityType) as IDbNoKeyEntity).DB_DEFINED_NAME);
        }

        /// <summary>
        /// 配置数据库实体类型构建器
        /// </summary>
        /// <param name="dbEntityStater">数据库实体状态器</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbEntityTypeBuilderConfigure(DbRelevanceEntityBuilder dbEntityStater, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (dbEntityStater.GenericArgumentTypesForInterfaces == null) return;

            var key = dbEntityStater.GenericArgumentTypesForInterfaces.Keys.FirstOrDefault(u => typeof(IDbEntityBuilder).IsAssignableFrom(u));
            if (key == null) return;

            var dbEntityGenericArguments = dbEntityStater.GenericArgumentTypesForInterfaces[key];
            var dbRelevanceEntityType = dbEntityStater.DbRelevanceEntityType;

            CreateDbEntityTypeBuilderIfNull(dbEntityGenericArguments.First(), ref entityTypeBuilder);

            // 配置实体构建器
            entityTypeBuilder = dbEntityStater.DbRelevanceEntityType.CallMethod(
                      nameof(IDbEntityBuilder<IDbEntityBase>.HasEntityBuilder),
                      Activator.CreateInstance(dbRelevanceEntityType),
                      entityTypeBuilder
                  ) as EntityTypeBuilder;
        }

        /// <summary>
        /// 配置数据库种子数据
        /// </summary>
        /// <param name="dbEntityStater">数据库实体状态器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbSeedDataConfigure(DbRelevanceEntityBuilder dbEntityStater, DbContext dbContext, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbRelevanceEntityType = dbEntityStater.DbRelevanceEntityType;
            if (typeof(IDbNoKeyEntity).IsAssignableFrom(dbRelevanceEntityType) || dbEntityStater.GenericArgumentTypesForInterfaces == null) return;

            var key = dbEntityStater.GenericArgumentTypesForInterfaces.Keys.FirstOrDefault(u => typeof(IDbSeedData).IsAssignableFrom(u));
            if (key == null) return;

            var dbEntityGenericArguments = dbEntityStater.GenericArgumentTypesForInterfaces[key];

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
        /// <param name="dbEntityStater">数据库实体状态器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbQueryFilterConfigure(DbRelevanceEntityBuilder dbEntityStater, DbContext dbContext, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbRelevanceEntityType = dbEntityStater.DbRelevanceEntityType;

            // 租户表无需参与过滤器
            if (dbRelevanceEntityType == _tenantType || dbEntityStater.GenericArgumentTypesForInterfaces == null) return;

            var key = dbEntityStater.GenericArgumentTypesForInterfaces.Keys.FirstOrDefault(u => typeof(IDbQueryFilter).IsAssignableFrom(u));
            if (key == null) return;
            var dbEntityGenericArguments = dbEntityStater.GenericArgumentTypesForInterfaces[key];

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
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextLocatorType">数据库上下文定位器</param>
        private static void DbFunctionConfigure()
        {
            var dbFunctionMethods = _dbFunctionMethods.Where(u => IsThisDbContextDbFunction(u, _dbContextLocatorType));

            foreach (var dbFunction in dbFunctionMethods)
            {
                _modelBuilder.HasDbFunction(dbFunction.ThisMethod);
            }
        }

        /// <summary>
        /// 配置数据库上下文查询筛选器
        /// </summary>
        /// <remarks>
        /// <para>一旦数据库上下文继承该接口，那么该数据库上下文所有的实体都将应用该查询筛选器</para>
        /// </remarks>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbRelevanceEntityType">数据库实体类型</param>
        /// <param name="hasDbContextQueryFilter">是否有数据库上下文查询筛选器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbContextQueryFilterConfigure(DbContext dbContext, Type dbRelevanceEntityType, bool hasDbContextQueryFilter, ref EntityTypeBuilder entityTypeBuilder)
        {
            // 租户表无需参与过滤器
            if (dbRelevanceEntityType == _tenantType) return;

            if (hasDbContextQueryFilter && typeof(IDbEntityBase).IsAssignableFrom(dbRelevanceEntityType))
            {
                CreateDbEntityTypeBuilderIfNull(dbRelevanceEntityType, ref entityTypeBuilder);

                _dbContextType.CallMethod(nameof(IDbContextQueryFilter.HasQueryFilter)
                    , dbContext
                    , dbRelevanceEntityType
                    , entityTypeBuilder
                 );
            }
        }

        /// <summary>
        /// 创建数据库实体类型构建器
        /// </summary>
        /// <param name="dbRelevanceEntityType"></param>
        /// <param name="entityTypeBuilder"></param>
        private static void CreateDbEntityTypeBuilderIfNull(Type dbRelevanceEntityType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var isSetEntityType = entityTypeBuilder == null;
            entityTypeBuilder ??= _modelBuilder.Entity(dbRelevanceEntityType);

            // 忽略租户Id
            if (isSetEntityType && dbRelevanceEntityType != _tenantType)
            {
                if (!App.SupportedMultipleTenant)
                {
                    if (typeof(IDbNoKeyEntity).IsAssignableFrom(dbRelevanceEntityType)) return;
                    entityTypeBuilder.Ignore(nameof(DbEntityBase.TenantId));
                }
                else
                {
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
            }
        }

        /// <summary>
        /// 判断该类型是否是当前数据库上下文的实体类型或包含实体的类型
        /// </summary>
        /// <param name="dbRelevanceEntityType">数据库实体类型或包含实体的类型</param>
        /// <param name="dbContextLocatorType">数据库上下文定位器</param>
        /// <returns>bool</returns>
        private static bool IsThisDbContextEntityType(Type dbRelevanceEntityType)
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
        /// 判断该方法是否是当前数据库上下文的函数类型
        /// </summary>
        /// <param name="methodWrapper"></param>
        /// <param name="dbContextLocatorType"></param>
        /// <returns>bool</returns>
        private static bool IsThisDbContextDbFunction(MethodInflation methodWrapper, Type dbContextLocatorType)
        {
            var dbFunctionAttribute = methodWrapper.CustomAttributes.FirstOrDefault(u => u.GetType() == typeof(Attributes.DbFunctionAttribute)) as Attributes.DbFunctionAttribute;
            if (CheckIsInDbContextLocators(dbFunctionAttribute.DbContextLocators, dbContextLocatorType)) return true;

            return false;
        }

        /// <summary>
        /// 获取数据库实体状态器
        /// </summary>
        /// <param name="dbRelevanceEntityType"></param>
        /// <returns></returns>
        private static DbRelevanceEntityBuilder GetDbEntityStater(Type dbRelevanceEntityType)
        {
            var isSet = _dbEntityStaters.TryGetValue(dbRelevanceEntityType, out DbRelevanceEntityBuilder dbEntityStater);
            if (!isSet)
            {
                var stater = new DbRelevanceEntityBuilder
                {
                    DbRelevanceEntityType = dbRelevanceEntityType,
                    IsDbEntityType = typeof(IDbEntityBase).IsAssignableFrom(dbRelevanceEntityType),
                    IsDbNoKeyEntityType = typeof(IDbNoKeyEntity).IsAssignableFrom(dbRelevanceEntityType)
                };
                stater.GenericArgumentTypesForBaseType = GetGenericArgumentsForBaseType(dbRelevanceEntityType, typeof(IDbNoKeyEntity));

                var interfaces = dbRelevanceEntityType.GetInterfaces().Where(u => u.IsGenericType && typeof(IDbEntityConfigure).IsAssignableFrom(u.GetGenericTypeDefinition()));
                if (interfaces.Any())
                {
                    var interfaceGenericArgumentTypes = new Dictionary<Type, IEnumerable<Type>>();
                    foreach (var inter in interfaces)
                    {
                        var genericArguments = inter.GetGenericArguments();
                        interfaceGenericArgumentTypes.Add(inter, genericArguments);

                        // 缓存
                        _basicGenericArgumentsCache.TryAdd((dbRelevanceEntityType, inter), genericArguments);
                    }
                    stater.GenericArgumentTypesForInterfaces = interfaceGenericArgumentTypes;
                }

                dbEntityStater = stater;
                _dbEntityStaters.TryAdd(dbRelevanceEntityType, stater);
            }

            return dbEntityStater;
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

        internal static Type[] GetGenericArgumentsForInterface(Type type, Type filterType)
        {
            var isCached = _basicGenericArgumentsCache.TryGetValue((type, filterType), out Type[] genericArguments);
            if (isCached) return genericArguments;

            genericArguments = type.GetInterfaces()
                    .FirstOrDefault(c => c.IsGenericType && filterType.IsAssignableFrom(c.GetGenericTypeDefinition()))
                    ?.GetGenericArguments();

            _basicGenericArgumentsCache.TryAdd((type, filterType), genericArguments);
            return genericArguments;
        }

        internal static Type[] GetGenericArgumentsForBaseType(Type type, Type filterType)
        {
            var isCached = _basicGenericArgumentsCache.TryGetValue((type, filterType), out Type[] genericArguments);
            if (isCached) return genericArguments;

            var baseType = type.BaseType;
            if (baseType.IsGenericType && filterType.IsAssignableFrom(baseType.GetGenericTypeDefinition()))
            {
                genericArguments = baseType.GetGenericArguments();
            }
            _basicGenericArgumentsCache.TryAdd((type, filterType), genericArguments);
            return genericArguments;
        }
    }
}