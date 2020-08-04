using Autofac;
using Fur.AppCore;
using Fur.Attributes;
using Fur.AppCore.Inflations;
using Fur.DatabaseAccessor.Contexts.Staters;
using Fur.DatabaseAccessor.Entities;
using Fur.DatabaseAccessor.Extensions;
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

namespace Fur.DatabaseAccessor.Contexts.Builders
{
    /// <summary>
    /// Fur 数据库上下文状态器
    /// </summary>
    /// <remarks>
    /// <para>解决 <see cref="FurDbContext{TDbContext, TDbContextLocator}"/> 重复初始化问题</para>
    /// </remarks>
    [NonInflated]
    internal static class FurDbContextBuilder
    {
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

            // 查找当前数据库上下文相关联的类型
            var dbContextRelevanceTypes = _dbEntityRelevanceTypes
                .Where(u => IsThisDbContextEntityType(u));

            // 如果没有找到则跳过
            if (!dbContextRelevanceTypes.Any())
                goto DbFunctionConfigureTag;

            var dbContextType = dbContext.GetType();
            var hasDbContextQueryFilter = typeof(IDbContextQueryFilter).IsAssignableFrom(dbContextType);

            // 注册基于Schema架构的多租户模式
            if (App.SupportedMultipleTenant && App.MultipleTenantOptions == FurMultipleTenantOptions.OnSchema && dbContextLocatorType != typeof(FurMultipleTenanDbContextLocator))
            {
                _multipleTenantOnSchemaProvider = dbContext
                    .GetService<ILifetimeScope>()
                    .Resolve<IMultipleTenantOnSchemaProvider>();
            }

            // 应该查找一次父接口父类就可以了
            foreach (var dbEntityRelevanceType in dbContextRelevanceTypes)
            {
                // 获取实体状态器
                var dbEntityStater = GetDbEntityStater(dbEntityRelevanceType);

                EntityTypeBuilder entityTypeBuilder = default;

                // 配置数据库无键实体
                DbNoKeyEntityConfigure(dbEntityStater, ref entityTypeBuilder);

                // 配置数据库实体类型构建器
                DbEntityTypeBuilderConfigure(dbEntityStater, ref entityTypeBuilder);

                // 配置数据库种子数据
                DbSeedDataConfigure(dbEntityStater, dbContext, ref entityTypeBuilder);

                // 配置数据库上下文查询筛选器
                DbContextQueryFilterConfigure(dbContext, dbContextType, dbEntityRelevanceType, hasDbContextQueryFilter, ref entityTypeBuilder);

                // 配置数据库查询筛选器
                DbQueryFilterConfigure(dbEntityStater, dbContext, ref entityTypeBuilder);

                // 配置模型类型构建器
                CreateDbEntityTypeBuilderIfNull(dbEntityRelevanceType, ref entityTypeBuilder);
            }

            // 配置数据库函数
            DbFunctionConfigureTag: DbFunctionConfigure();
        }

        /// <summary>
        /// 配置数据库无键实体
        /// </summary>
        /// <param name="dbEntityStater">数据库实体状态器</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbNoKeyEntityConfigure(DbEntityStater dbEntityStater, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (!dbEntityStater.IsDbNoKeyEntityType) return;

            var dbEntityRelevanceType = dbEntityStater.DbEntityRelevanceType;
            CreateDbEntityTypeBuilderIfNull(dbEntityRelevanceType, ref entityTypeBuilder);

            // 配置视图
            entityTypeBuilder.HasNoKey();
            entityTypeBuilder.ToView((Activator.CreateInstance(dbEntityRelevanceType) as IDbNoKeyEntity).DB_DEFINED_NAME);
        }

        /// <summary>
        /// 配置数据库实体类型构建器
        /// </summary>
        /// <param name="dbEntityStater">数据库实体状态器</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbEntityTypeBuilderConfigure(DbEntityStater dbEntityStater, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (dbEntityStater.DbEntityConfigureGenericArgumentTypes == null) return;

            var key = dbEntityStater.DbEntityConfigureGenericArgumentTypes.Keys.FirstOrDefault(u => typeof(IDbEntityBuilder).IsAssignableFrom(u));
            if (key == null) return;

            var dbEntityGenericArguments = dbEntityStater.DbEntityConfigureGenericArgumentTypes[key];
            var dbEntityRelevanceType = dbEntityStater.DbEntityRelevanceType;

            CreateDbEntityTypeBuilderIfNull(dbEntityGenericArguments.First(), ref entityTypeBuilder);

            // 配置实体构建器
            entityTypeBuilder = dbEntityStater.DbEntityRelevanceType.CallMethod(
                      nameof(IDbEntityBuilder<IDbEntityBase>.HasEntityBuilder),
                      Activator.CreateInstance(dbEntityRelevanceType),
                      entityTypeBuilder
                  ) as EntityTypeBuilder;
        }

        /// <summary>
        /// 配置数据库种子数据
        /// </summary>
        /// <param name="dbEntityStater">数据库实体状态器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbSeedDataConfigure(DbEntityStater dbEntityStater, DbContext dbContext, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbEntityRelevanceType = dbEntityStater.DbEntityRelevanceType;
            if (typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityRelevanceType) || dbEntityStater.DbEntityConfigureGenericArgumentTypes == null) return;

            var key = dbEntityStater.DbEntityConfigureGenericArgumentTypes.Keys.FirstOrDefault(u => typeof(IDbSeedData).IsAssignableFrom(u));
            if (key == null) return;

            var dbEntityGenericArguments = dbEntityStater.DbEntityConfigureGenericArgumentTypes[key];

            CreateDbEntityTypeBuilderIfNull(dbEntityGenericArguments.First(), ref entityTypeBuilder);

            // 配置种子数据
            var seedDatas = dbEntityRelevanceType.CallMethod(
                   nameof(IDbSeedData<IDbEntityBase>.HasData),
                   Activator.CreateInstance(dbEntityRelevanceType),
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
        private static void DbQueryFilterConfigure(DbEntityStater dbEntityStater, DbContext dbContext, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbEntityRelevanceType = dbEntityStater.DbEntityRelevanceType;

            // 租户表无需参与过滤器
            if (dbEntityRelevanceType == _tenantType || dbEntityStater.DbEntityConfigureGenericArgumentTypes == null) return;

            var key = dbEntityStater.DbEntityConfigureGenericArgumentTypes.Keys.FirstOrDefault(u => typeof(IDbQueryFilter).IsAssignableFrom(u));
            if (key == null) return;
            var dbEntityGenericArguments = dbEntityStater.DbEntityConfigureGenericArgumentTypes[key];

            CreateDbEntityTypeBuilderIfNull(dbEntityGenericArguments.First(), ref entityTypeBuilder);

            var queryFilters = dbEntityRelevanceType.CallMethod(
                       nameof(IDbQueryFilter<IDbEntityBase>.HasQueryFilter),
                       Activator.CreateInstance(dbEntityRelevanceType)
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
        /// <param name="dbContextType">数据库上下文类型</param>
        /// <param name="dbEntityRelevanceType">数据库实体类型</param>
        /// <param name="hasDbContextQueryFilter">是否有数据库上下文查询筛选器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbContextQueryFilterConfigure(DbContext dbContext, Type dbContextType, Type dbEntityRelevanceType, bool hasDbContextQueryFilter, ref EntityTypeBuilder entityTypeBuilder)
        {
            // 租户表无需参与过滤器
            if (dbEntityRelevanceType == _tenantType) return;

            if (hasDbContextQueryFilter && typeof(IDbEntityBase).IsAssignableFrom(dbEntityRelevanceType))
            {
                CreateDbEntityTypeBuilderIfNull(dbEntityRelevanceType, ref entityTypeBuilder);

                dbContextType.CallMethod(nameof(IDbContextQueryFilter.HasQueryFilter)
                    , dbContext
                    , dbEntityRelevanceType
                    , entityTypeBuilder
                 );
            }
        }

        /// <summary>
        /// 创建数据库实体类型构建器
        /// </summary>
        /// <param name="dbEntityRelevanceType"></param>
        /// <param name="entityTypeBuilder"></param>
        private static void CreateDbEntityTypeBuilderIfNull(Type dbEntityRelevanceType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var isSetEntityType = entityTypeBuilder == null;
            entityTypeBuilder ??= _modelBuilder.Entity(dbEntityRelevanceType);

            // 忽略租户Id
            if (isSetEntityType && dbEntityRelevanceType != _tenantType)
            {
                if (!App.SupportedMultipleTenant)
                {
                    if (typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityRelevanceType)) return;
                    entityTypeBuilder.Ignore(nameof(DbEntityBase.TenantId));
                }
                else
                {
                    if (_multipleTenantOnSchemaProvider != null)
                    {
                        var tableName = dbEntityRelevanceType.Name;
                        var schema = _multipleTenantOnSchemaProvider.GetSchema();
                        if (dbEntityRelevanceType.IsDefined(typeof(TableAttribute), false))
                        {
                            var tableAttribute = dbEntityRelevanceType.GetCustomAttribute<TableAttribute>();
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
        /// <param name="dbEntityRelevanceType">数据库实体类型或包含实体的类型</param>
        /// <param name="dbContextLocatorType">数据库上下文定位器</param>
        /// <returns>bool</returns>
        private static bool IsThisDbContextEntityType(Type dbEntityRelevanceType)
        {
            // 判断是否启用多租户，如果不启用，则默认不解析 Tenant 类型，返回 false
            if (!App.SupportedMultipleTenant)
            {
                if (dbEntityRelevanceType == _tenantType) return false;
                var typeGenericArguments = dbEntityRelevanceType.GetTypeGenericArguments(typeof(IDbEntityConfigure), GenericArgumentSourceOptions.Interface);
                if (typeGenericArguments != null && typeGenericArguments.First() == _tenantType) return false;
            }

            // 如果是实体类型
            if (typeof(IDbEntityBase).IsAssignableFrom(dbEntityRelevanceType))
            {
                // 有主键实体
                if (!typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityRelevanceType))
                {
                    // 如果父类不是泛型类型，则返回 true
                    if (dbEntityRelevanceType.BaseType == typeof(DbEntity) || dbEntityRelevanceType.BaseType == typeof(DbEntityBase) || dbEntityRelevanceType.BaseType == typeof(Object)) return true;
                    // 如果是泛型类型，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
                    else
                    {
                        var typeGenericArguments = dbEntityRelevanceType.GetTypeGenericArguments(typeof(IDbEntityBase), GenericArgumentSourceOptions.BaseType);
                        if (CheckIsInDbContextLocators(typeGenericArguments.Skip(1), _dbContextLocatorType)) return true;
                    }
                }
                // 无键实体
                else
                {
                    // 如果父类不是泛型类型，则返回 true
                    if (dbEntityRelevanceType.BaseType == typeof(DbNoKeyEntity)) return true;
                    // 如果是泛型类型，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
                    else
                    {
                        var typeGenericArguments = dbEntityRelevanceType.GetTypeGenericArguments(typeof(IDbNoKeyEntity), GenericArgumentSourceOptions.BaseType);
                        if (CheckIsInDbContextLocators(typeGenericArguments, _dbContextLocatorType)) return true;
                    }
                }
            }

            // 如果继承 IDbEntityConfigure，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
            if (typeof(IDbEntityConfigure).IsAssignableFrom(dbEntityRelevanceType))
            {
                var typeGenericArguments = dbEntityRelevanceType.GetTypeGenericArguments(typeof(IDbEntityConfigure), GenericArgumentSourceOptions.Interface);
                if (CheckIsInDbContextLocators(typeGenericArguments.Skip(1), _dbContextLocatorType)) return true;
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
        /// <param name="dbEntityRelevanceType"></param>
        /// <returns></returns>
        private static DbEntityStater GetDbEntityStater(Type dbEntityRelevanceType)
        {
            var isSet = _dbEntityStaters.TryGetValue(dbEntityRelevanceType, out DbEntityStater dbEntityStater);
            if (!isSet)
            {
                var stater = new DbEntityStater
                {
                    DbEntityRelevanceType = dbEntityRelevanceType,
                    IsDbEntityType = typeof(IDbEntityBase).IsAssignableFrom(dbEntityRelevanceType),
                    IsDbNoKeyEntityType = typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityRelevanceType)
                };
                var baseType = dbEntityRelevanceType.BaseType;
                // 目前只有无键实体基类是类类型
                if (baseType.IsGenericType && typeof(IDbNoKeyEntity).IsAssignableFrom(baseType.GetGenericTypeDefinition()))
                {
                    stater.BaseTypeGenericArgumentTypes = baseType.GetGenericArguments();
                }
                var interfaces = dbEntityRelevanceType.GetInterfaces().Where(u => u.IsGenericType && typeof(IDbEntityConfigure).IsAssignableFrom(u.GetGenericTypeDefinition()));
                if (interfaces.Any())
                {
                    var interfaceGenericArgumentTypes = new Dictionary<Type, IEnumerable<Type>>();
                    foreach (var inter in interfaces)
                    {
                        interfaceGenericArgumentTypes.Add(inter, inter.GetGenericArguments());
                    }
                    stater.DbEntityConfigureGenericArgumentTypes = interfaceGenericArgumentTypes;
                }

                dbEntityStater = stater;
                _dbEntityStaters.TryAdd(dbEntityRelevanceType, stater);
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

        /// <summary>
        /// 数据库实体关联的所有类型
        /// </summary>
        /// <remarks>
        /// <para>所有继承 <see cref="IDbEntityBase"/> 或 <see cref="IDbEntityConfigure"/> 的类型</para>
        /// </remarks>
        private static readonly IEnumerable<Type> _dbEntityRelevanceTypes;

        /// <summary>
        /// 数据库函数定义方法集合
        /// </summary>
        private static readonly IEnumerable<MethodInflation> _dbFunctionMethods;

        /// <summary>
        /// 模型构建器
        /// </summary>
        private static ModelBuilder _modelBuilder;

        /// <summary>
        /// 数据库上下文定位器类型
        /// </summary>
        private static Type _dbContextLocatorType;

        /// <summary>
        ///基于架构的 多租户提供器
        /// </summary>
        private static IMultipleTenantOnSchemaProvider _multipleTenantOnSchemaProvider;

        /// <summary>
        /// 数据库实体状态器
        /// </summary>
        private static readonly ConcurrentDictionary<Type, DbEntityStater> _dbEntityStaters;

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

            _dbEntityRelevanceTypes ??= application.ClassTypes
                .Where(u => u.IsDbEntityRelevanceType)
                .Distinct()
                .Select(u => u.ThisType);

            if (_dbEntityRelevanceTypes.Any())
            {
                _dbEntityStaters ??= new ConcurrentDictionary<Type, DbEntityStater>();
            }

            _dbFunctionMethods = application.Methods
                .Where(u => u.IsDbFunctionMethod);
        }
    }
}