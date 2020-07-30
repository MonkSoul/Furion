using Autofac;
using Fur.AppBasic;
using Fur.AppBasic.Attributes;
using Fur.AppBasic.Wrappers;
using Fur.DatabaseAccessor.Entities;
using Fur.DatabaseAccessor.Entities.Configurations;
using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.MultipleTenants;
using Fur.DatabaseAccessor.MultipleTenants.Entities;
using Fur.DatabaseAccessor.MultipleTenants.Options;
using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Fur.DatabaseAccessor.Options;
using Fur.TypeExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static Fur.TypeExtensions.TypeExtensions;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// Fur 数据库上下文状态器
    /// </summary>
    /// <remarks>
    /// <para>解决 <see cref="FurDbContext{TDbContext, TDbContextLocator}"/> 重复初始化问题</para>
    /// </remarks>
    [NonWrapper]
    internal static class FurDbContextStater
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
            var dbContextEntityTypes = _dbEntityRelevanceTypes
                .Where(u => IsThisDbContextEntityType(u));

            // 如果没有找到则跳过
            if (!dbContextEntityTypes.Any())
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

            foreach (var dbEntityType in dbContextEntityTypes)
            {
                EntityTypeBuilder entityTypeBuilder = default;

                // 配置数据库无键实体
                DbNoKeyEntityConfigure(dbEntityType, entityTypeBuilder);

                // 配置数据库实体类型构建器
                DbEntityTypeBuilderConfigure(dbEntityType, entityTypeBuilder);

                // 配置数据库种子数据
                DbSeedDataConfigure(dbContext, dbEntityType, entityTypeBuilder);

                // 配置数据库上下文查询筛选器
                DbContextQueryFilterConfigure(dbContext, dbContextType, dbEntityType, hasDbContextQueryFilter, entityTypeBuilder);

                // 配置数据库查询筛选器
                DbQueryFilterConfigure(dbContext, dbEntityType, entityTypeBuilder);

                // 配置模型类型构建器
                CreateDbEntityTypeBuilderIfNull(dbEntityType, entityTypeBuilder);
            }

            // 配置数据库函数
            DbFunctionConfigureTag: DbFunctionConfigure();
        }

        /// <summary>
        /// 配置数据库无键实体
        /// </summary>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbNoKeyEntityConfigure(Type dbEntityType, EntityTypeBuilder entityTypeBuilder)
        {
            if (!typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityType)) return;
            if (!CheckInDbContextAndHandle(dbEntityType, typeof(IDbNoKeyEntity), entityTypeBuilder, GenericArgumentSourceOptions.BaseType)) return;

            entityTypeBuilder.HasNoKey();
            entityTypeBuilder.ToView((Activator.CreateInstance(dbEntityType) as IDbNoKeyEntity).DB_DEFINED_NAME);
        }

        /// <summary>
        /// 配置数据库实体类型构建器
        /// </summary>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbEntityTypeBuilderConfigure(Type dbEntityType, EntityTypeBuilder entityTypeBuilder)
        {
            if (!CheckInDbContextAndHandle(dbEntityType, typeof(IDbEntityBuilder), entityTypeBuilder)) return;

            entityTypeBuilder = dbEntityType.CallMethod(
                      nameof(IDbEntityBuilder<IDbEntityBase>.HasEntityBuilder),
                      Activator.CreateInstance(dbEntityType),
                      entityTypeBuilder
                  ) as EntityTypeBuilder;
        }

        /// <summary>
        /// 配置数据库种子数据
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbSeedDataConfigure(DbContext dbContext, Type dbEntityType, EntityTypeBuilder entityTypeBuilder)
        {
            if (typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityType)) return;
            if (!CheckInDbContextAndHandle(dbEntityType, typeof(IDbSeedData), entityTypeBuilder)) return;

            var seedDatas = dbEntityType.CallMethod(
                   nameof(IDbSeedData<IDbEntityBase>.HasData),
                   Activator.CreateInstance(dbEntityType),
                   dbContext
               ).Adapt<IEnumerable<object>>();

            if (seedDatas == null && !seedDatas.Any()) return;

            entityTypeBuilder.HasData(seedDatas);
        }

        /// <summary>
        /// 配置数据库查询筛选器
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbQueryFilterConfigure(DbContext dbContext, Type dbEntityType, EntityTypeBuilder entityTypeBuilder)
        {
            // 租户表无需参与过滤器
            if (dbEntityType == typeof(Tenant)) return;
            if (!CheckInDbContextAndHandle(dbEntityType, typeof(IDbQueryFilter), entityTypeBuilder)) return;

            var queryFilters = dbEntityType.CallMethod(
                       nameof(IDbQueryFilter<IDbEntityBase>.HasQueryFilter),
                       Activator.CreateInstance(dbEntityType)
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
                _modelBuilder.HasDbFunction(dbFunction.Method);
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
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="hasDbContextQueryFilter">是否有数据库上下文查询筛选器</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbContextQueryFilterConfigure(DbContext dbContext, Type dbContextType, Type dbEntityType, bool hasDbContextQueryFilter, EntityTypeBuilder entityTypeBuilder)
        {
            // 租户表无需参与过滤器
            if (dbEntityType == typeof(Tenant)) return;

            if (typeof(IDbEntityBase).IsAssignableFrom(dbEntityType) && hasDbContextQueryFilter)
            {
                CreateDbEntityTypeBuilderIfNull(dbEntityType, entityTypeBuilder);

                dbContextType.CallMethod(nameof(IDbContextQueryFilter.HasQueryFilter)
                    , dbContext
                    , dbEntityType
                    , entityTypeBuilder
                 );
            }
        }

        /// <summary>
        /// 创建数据库实体类型构建器
        /// </summary>
        /// <param name="dbEntityType"></param>
        /// <param name="entityTypeBuilder"></param>
        private static void CreateDbEntityTypeBuilderIfNull(Type dbEntityType, EntityTypeBuilder entityTypeBuilder)
        {
            var isSetEntityType = entityTypeBuilder == null;
            entityTypeBuilder ??= _modelBuilderEntityMethod.MakeGenericMethod(dbEntityType).Invoke(_modelBuilder, null) as EntityTypeBuilder;

            // 忽略租户Id
            if (isSetEntityType)
            {
                if (!App.SupportedMultipleTenant)
                {
                    entityTypeBuilder.Ignore(nameof(DbEntityBase.TenantId));
                }
                else
                {
                    if (_multipleTenantOnSchemaProvider != null && dbEntityType != typeof(Tenant))
                    {
                        var tableName = dbEntityType.Name;
                        var schema = _multipleTenantOnSchemaProvider.GetSchema();
                        if (dbEntityType.IsDefined(typeof(TableAttribute), false))
                        {
                            var tableAttribute = dbEntityType.GetCustomAttribute<TableAttribute>();
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
        /// <param name="dbEntityType">数据库实体类型或包含实体的类型</param>
        /// <param name="dbContextLocatorType">数据库上下文定位器</param>
        /// <returns>bool</returns>
        private static bool IsThisDbContextEntityType(Type dbEntityType)
        {
            // 判断是否启用多租户，如果不启用，则默认不解析 Tenant 类型，返回 false
            if (!App.SupportedMultipleTenant)
            {
                if (dbEntityType == typeof(Tenant)) return false;
                var typeGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbEntityConfigure), GenericArgumentSourceOptions.Interface);
                if (typeGenericArguments != null && typeGenericArguments.First() == typeof(Tenant)) return false;
            }

            // 如果是实体类型
            if (typeof(IDbEntityBase).IsAssignableFrom(dbEntityType))
            {
                // 有主键实体
                if (!typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityType))
                {
                    // 如果父类不是泛型类型，则返回 true
                    if (dbEntityType.BaseType == typeof(DbEntity) || dbEntityType.BaseType == typeof(DbEntityBase) || dbEntityType.BaseType == typeof(Object)) return true;
                    // 如果是泛型类型，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
                    else
                    {
                        var typeGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbEntityBase), GenericArgumentSourceOptions.BaseType);
                        if (CheckIsInDbContextLocators(typeGenericArguments.Skip(1), _dbContextLocatorType)) return true;
                    }
                }
                // 无键实体
                else
                {
                    // 如果父类不是泛型类型，则返回 true
                    if (dbEntityType.BaseType == typeof(DbNoKeyEntity)) return true;
                    // 如果是泛型类型，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
                    else
                    {
                        var typeGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbNoKeyEntity), GenericArgumentSourceOptions.BaseType);
                        if (CheckIsInDbContextLocators(typeGenericArguments, _dbContextLocatorType)) return true;
                    }
                }
            }

            // 如果继承 IDbEntityConfigure，但数据库上下文定位器泛型参数未空或包含 dbContextLocatorType，返回 true
            if (typeof(IDbEntityConfigure).IsAssignableFrom(dbEntityType))
            {
                var typeGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbEntityConfigure), GenericArgumentSourceOptions.Interface);
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
        private static bool IsThisDbContextDbFunction(MethodWrapper methodWrapper, Type dbContextLocatorType)
        {
            var dbFunctionAttribute = methodWrapper.CustomAttributes.FirstOrDefault(u => u.GetType() == typeof(Attributes.DbFunctionAttribute)) as Attributes.DbFunctionAttribute;
            if (CheckIsInDbContextLocators(dbFunctionAttribute.DbContextLocators, dbContextLocatorType)) return true;

            return false;
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
        /// 处理当前上下文
        /// </summary>
        /// <param name="dbEntityType">实体类型</param>
        /// <param name="DependencyType">依赖类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        /// <param name="sourceOptions">泛型参数来源</param>
        private static bool CheckInDbContextAndHandle(Type dbEntityType,
            Type DependencyType,
           EntityTypeBuilder entityTypeBuilder,
            GenericArgumentSourceOptions sourceOptions = GenericArgumentSourceOptions.Interface)
        {
            var dbEntityGenericArguments = dbEntityType.GetTypeGenericArguments(DependencyType, sourceOptions);
            if (dbEntityGenericArguments != null)
            {
                var dbContextLocators = dbEntityGenericArguments.Skip(1);
                if (CheckIsInDbContextLocators(dbContextLocators, _dbContextLocatorType))
                {
                    CreateDbEntityTypeBuilderIfNull(sourceOptions == GenericArgumentSourceOptions.BaseType ? dbEntityType : dbEntityGenericArguments.First(), entityTypeBuilder);
                    return true;
                }
            }

            return false;
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
        private static readonly IEnumerable<MethodWrapper> _dbFunctionMethods;

        /// <summary>
        /// 模型构建器 <see cref="ModelBuilder.Entity{TEntity}"/> 泛型方法
        /// </summary>
        private static readonly MethodInfo _modelBuilderEntityMethod;

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
        /// 构造函数
        /// </summary>
        static FurDbContextStater()
        {
            var application = App.Application;

            _dbEntityRelevanceTypes ??= application.PublicClassTypeWrappers
                .Where(u => u.CanBeNew && (typeof(IDbEntityBase).IsAssignableFrom(u.Type) || typeof(IDbEntityConfigure).IsAssignableFrom(u.Type)))
                .Distinct()
                .Select(u => u.Type);

            if (_dbEntityRelevanceTypes.Any())
            {
                _modelBuilderEntityMethod ??= typeof(ModelBuilder)
                    .GetMethods()
                    .FirstOrDefault(u => u.Name == nameof(ModelBuilder.Entity) && u.IsGenericMethod && u.GetParameters().Length == 0);
            }

            _dbFunctionMethods = application.PublicMethodWrappers
                .Where(u => u.IsStaticMethod && u.ThisDeclareType.IsAbstract && u.ThisDeclareType.IsSealed && u.Method.IsDefined(typeof(Attributes.DbFunctionAttribute), false));
        }
    }
}