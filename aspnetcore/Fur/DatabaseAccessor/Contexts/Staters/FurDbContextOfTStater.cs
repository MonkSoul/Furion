using Autofac;
using Fur.ApplicationBase;
using Fur.ApplicationBase.Attributes;
using Fur.ApplicationBase.Wrappers;
using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.EntityTypeBuilders;
using Fur.DatabaseAccessor.Models.QueryFilters;
using Fur.DatabaseAccessor.Models.SeedDatas;
using Fur.DatabaseAccessor.MultipleTenants.Entities;
using Fur.TypeExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static Fur.TypeExtensions.TypeExtensions;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// Fur 数据库上下文状态器
    /// <para>解决 <see cref="FurDbContextOfT{TDbContext, TDbContextIdentifier}"/> 重复初始化问题</para>
    /// </summary>
    [NonWrapper]
    internal static class FurDbContextOfTStater
    {
        /// <summary>
        /// 包含数据库实体的所有类型
        /// <para>包括本身继承 <see cref="IDbEntityBase"/> 或 继承的泛型接口中含 <see cref="IDbEntityBase"/> 类型</para>
        /// </summary>
        private static ConcurrentBag<Type> _includeDbEntityTypes;

        /// <summary>
        /// 所有数据库实体类型
        /// </summary>
        private static ConcurrentBag<Type> _dbEntityTypes;

        /// <summary>
        /// 应用所有标识为数据库函数的方法
        /// </summary>
        private readonly static ConcurrentBag<MethodWrapper> _dbFunctionMethods;

        /// <summary>
        /// 模型构建器泛型 <see cref="ModelBuilder.Entity{TEntity}"/> 方法
        /// </summary>
        private static readonly MethodInfo _modelBuilderEntityMethod;

        /// <summary>
        /// 数据库实体类型所拥有的数据库上下文标识器集合
        /// <para>避免子数据库上下文重复遍历</para>
        /// </summary>
        private static readonly ConcurrentDictionary<Type, List<Type>> _dbContextIdentifierTypesOfDbEntityType;

        /// <summary>
        /// 数据库实体集合遍历索引
        /// <para>避免重复解析数据库实体</para>
        /// </summary>
        private static int _dbEntityTypesLoopIndex = 0;

        /// <summary>
        /// 数据库函数类型所拥有的数据库上下文标识器集合
        /// <para>避免子数据库上下文重复遍历</para>
        /// </summary>
        private static readonly ConcurrentDictionary<MethodWrapper, List<Type>> _dbContextIdentifierTypesOfDbFunctions;

        /// <summary>
        /// 数据库函数集合遍历索引
        /// <para>避免重复解析数据库函数</para>
        /// </summary>
        private static int _dbFunctionsLoopIndex = 0;

        #region 构造函数 + static FurDbContextOfTStater()
        /// <summary>
        /// 构造函数
        /// </summary>
        static FurDbContextOfTStater()
        {
            _includeDbEntityTypes ??= new ConcurrentBag<Type>(
                AppGlobal.Application.PublicClassTypeWrappers
                .Where(u => u.CanBeNew && (typeof(IDbEntityBase).IsAssignableFrom(u.Type) || typeof(IDbEntityConfigure).IsAssignableFrom(u.Type)))
                .Distinct()
                .Select(u => u.Type));

            if (_includeDbEntityTypes.Count > 0)
            {
                _dbEntityTypes ??= new ConcurrentBag<Type>(_includeDbEntityTypes.Where(t => typeof(IDbEntityBase).IsAssignableFrom(t)));

                _modelBuilderEntityMethod ??= typeof(ModelBuilder)
                    .GetMethods()
                    .FirstOrDefault(u => u.Name == nameof(ModelBuilder.Entity) && u.IsGenericMethod && u.GetParameters().Length == 0);

                _dbContextIdentifierTypesOfDbEntityType ??= new ConcurrentDictionary<Type, List<Type>>();
            }

            _dbFunctionMethods = new ConcurrentBag<MethodWrapper>(
                AppGlobal.Application.PublicMethodWrappers
                .Where(u => u.IsStaticMethod && u.Method.IsDefined(typeof(DbEFFunctionAttribute)) && u.ThisDeclareType.IsAbstract && u.ThisDeclareType.IsSealed));

            _dbContextIdentifierTypesOfDbFunctions ??= new ConcurrentDictionary<MethodWrapper, List<Type>>();
        }
        #endregion

        private static bool IsDbContextType(Type type, Type dbContextIdentifierType)
        {
            // 1. 如果是实体类型，且不是无键实体
            if (typeof(IDbEntityBase).IsAssignableFrom(type))
            {
                // 有键实体
                if (!typeof(IDbNoKeyEntity).IsAssignableFrom(type))
                {
                    // 如果没有定义 [DbTable] 特性
                    if (!type.IsDefined(typeof(DbTableAttribute), false)) return true;

                    // 如果指定了，但是 DbContextIdentifierTypes为空或不为空且包含 dbContextIdentifierType
                    var dbContextIdentifierTypes = type.GetCustomAttribute<DbTableAttribute>(false).DbContextIdentifierTypes;
                    if (CheckIsInDbContextIdentifierTypes(dbContextIdentifierTypes, dbContextIdentifierType)) return true;
                }
                // 无键实体
                else
                {
                    var baseType = type.BaseType;
                    // 如果父类不是泛型
                    if (baseType == typeof(DbNoKeyEntity)) return true;
                    else
                    {
                        var typeGenericArguments = type.GetTypeGenericArguments(typeof(IDbNoKeyEntity), FromTypeOptions.BaseType);
                        if (CheckIsInDbContextIdentifierTypes(typeGenericArguments, dbContextIdentifierType)) return true;
                    }
                }
            }

            if (typeof(IDbEntityConfigure).IsAssignableFrom(type))
            {
                var typeGenericArguments = type.GetTypeGenericArguments(typeof(IDbEntityConfigure), FromTypeOptions.Interface);
                if (CheckIsInDbContextIdentifierTypes(typeGenericArguments.Skip(1), dbContextIdentifierType)) return true;
            }

            return false;
        }

        #region 扫描数据库对象类型加入模型构建器中 +private static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, Type dbContextIdentifierType, DbContext dbContext)
        /// <summary>
        /// 扫描数据库对象类型加入模型构建器中
        /// <para>包括视图、存储过程、函数（标量函数/表值函数）初始化、及种子数据、查询筛选器配置</para>
        /// </summary>
        /// <param name="modelBuilder">模型上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="dbContext">数据库上下文</param>
        internal static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, Type dbContextIdentifierType, DbContext dbContext)
        {
            var dbContextEntityTypes = _includeDbEntityTypes.Where(u => IsDbContextType(u, dbContextIdentifierType));

            if (!dbContextEntityTypes.Any()) return;

            foreach (var dbEntityType in dbContextEntityTypes)
            {
                // 如果未启用多租户支持，则跳过解析
                if (!AppGlobal.IsSupportTenant && dbEntityType == typeof(Tenant)) continue;

                EntityTypeBuilder entityTypeBuilder = default;

                // 配置数据库上下文查询筛选器
                DbContextQueryFilterConfigure(dbContext, modelBuilder, dbEntityType, ref entityTypeBuilder);

                // 配置无键实体
                DbNoKeyEntityConfigure(dbEntityType, modelBuilder, dbContextIdentifierType, ref entityTypeBuilder);

                // 配置实体类型构建器信息
                DbEntityBuilderConfigure(dbEntityType, modelBuilder, dbContextIdentifierType, ref entityTypeBuilder);

                // 配置种子数据
                DbSeedDataConfigure(dbEntityType, modelBuilder, dbContext, dbContextIdentifierType, ref entityTypeBuilder);

                // 配置查询筛选器
                DbQueryFilterConfigure(dbEntityType, modelBuilder, dbContext, dbContextIdentifierType, ref entityTypeBuilder);

                // 配置模型
                DbModelEntityConfigure(modelBuilder, dbContextIdentifierType, dbEntityType, ref entityTypeBuilder);
            }

            // 配置数据库函数
            DbFunctionConfigure(modelBuilder, dbContextIdentifierType);
        }
        #endregion


        #region 配置数据库模型实体 + private static void DbModelEntityConfigure(ModelBuilder modelBuilder, Type dbContextIdentifierType, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置数据库模型实体
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbModelEntityConfigure(ModelBuilder modelBuilder, Type dbContextIdentifierType, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (entityTypeBuilder != null || !typeof(IDbEntityBase).IsAssignableFrom(dbEntityType)) return;

            if (dbEntityType.IsDefined(typeof(DbTableAttribute), false))
            {
                var dbTableAttribute = dbEntityType.GetCustomAttribute<DbTableAttribute>();
                if (CheckIsInDbContextIdentifierTypes(dbTableAttribute.DbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);
                }
            }
            else
            {
                BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);
            }
        }
        #endregion

        #region 配置无键实体 + private static void DbNoKeyEntityConfigure(Type dbEntityType, ModelBuilder modelBuilder, Type dbContextIdentifierType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置无键实体
        /// </summary>
        /// <param name="dbEntityType">实体类型</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbNoKeyEntityConfigure(Type dbEntityType, ModelBuilder modelBuilder, Type dbContextIdentifierType, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (!typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityType)) return;

            var dbNoKeyEntityGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbNoKeyEntity), FromTypeOptions.BaseType);
            if (dbNoKeyEntityGenericArguments != null)
            {
                var dbContextIdentifierTypes = dbNoKeyEntityGenericArguments.Skip(1);

                if (CheckIsInDbContextIdentifierTypes(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);

                    entityTypeBuilder.HasNoKey();
                    entityTypeBuilder.ToView((Activator.CreateInstance(dbEntityType) as IDbNoKeyEntity).DB_DEFINED_NAME);
                }

                _dbContextIdentifierTypesOfDbEntityType.AddOrUpdate(dbEntityType, dbContextIdentifierTypes);
            }
        }
        #endregion

        #region 配置实体构建器信息 + private static void DbEntityBuilderConfigure(Type dbEntityType, ModelBuilder modelBuilder, Type dbContextIdentifierType, Type dbEntityConfigureType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置实体构建器信息
        /// </summary>
        /// <param name="dbEntityType">实体类型</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="dbEntityConfigureType">数据库实体配置类型，也就是所有继承了可配置接口的类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbEntityBuilderConfigure(Type dbEntityType, ModelBuilder modelBuilder, Type dbContextIdentifierType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbEntityBuilderGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbEntityBuilder), FromTypeOptions.Interface);

            if (dbEntityBuilderGenericArguments != null)
            {
                var dbContextIdentifierTypes = dbEntityBuilderGenericArguments.Skip(1);

                if (CheckIsInDbContextIdentifierTypes(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbEntityBuilderGenericArguments.First(), ref entityTypeBuilder);

                    entityTypeBuilder = dbEntityType.CallMethod(nameof(IDbEntityBuilderOfT<IDbEntityBase>.HasEntityBuilder), Activator.CreateInstance(dbEntityType), entityTypeBuilder) as EntityTypeBuilder;
                }

                _dbContextIdentifierTypesOfDbEntityType.AddOrUpdate(dbEntityType, dbContextIdentifierTypes);
            }
        }
        #endregion

        #region 配置种子数据 + private static void DbSeedDataConfigure(Type dbEntityType, ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType, Type dbEntityConfigureType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置种子数据
        /// </summary>
        /// <param name="dbEntityType">实体类型</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="dbEntityConfigureType">数据库实体配置类型，也就是所有继承了可配置接口的类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbSeedDataConfigure(Type dbEntityType, ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType, ref EntityTypeBuilder entityTypeBuilder)
        {
            if (typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityType)) return;

            var dbSeedDataGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbSeedData), FromTypeOptions.Interface);

            if (dbSeedDataGenericArguments != null)
            {
                var dbContextIdentifierTypes = dbSeedDataGenericArguments.Skip(1);

                if (CheckIsInDbContextIdentifierTypes(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbSeedDataGenericArguments.First(), ref entityTypeBuilder);

                    var seedDatas = dbEntityType.CallMethod(nameof(IDbSeedDataOfT<IDbEntityBase>.HasData), Activator.CreateInstance(dbEntityType), dbContext).Adapt<IEnumerable<object>>();
                    if (seedDatas != null && seedDatas.Any()) entityTypeBuilder.HasData(seedDatas);
                }

                _dbContextIdentifierTypesOfDbEntityType.AddOrUpdate(dbEntityType, dbContextIdentifierTypes);
            }
        }
        #endregion

        #region 配置查询筛选器 + private static void DbQueryFilterConfigure(Type dbEntityType, ModelBuilder modelBuilder, DbContext dbContext,  Type dbEntityConfigureType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置查询筛选器
        /// </summary>
        /// <param name="dbEntityType">实体类型</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="dbEntityConfigureType">数据库实体配置类型，也就是所有继承了可配置接口的类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbQueryFilterConfigure(Type dbEntityType, ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbQueryFilterGenericArguments = dbEntityType.GetTypeGenericArguments(typeof(IDbQueryFilter), FromTypeOptions.Interface);
            if (dbQueryFilterGenericArguments != null)
            {
                var dbContextIdentifierTypes = dbQueryFilterGenericArguments.Skip(1);
                if (CheckIsInDbContextIdentifierTypes(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbQueryFilterGenericArguments.First(), ref entityTypeBuilder);

                    var queryFilters = dbEntityType.CallMethod(nameof(IDbQueryFilterOfT<IDbEntityBase>.HasQueryFilter), Activator.CreateInstance(dbEntityType), dbContext).Adapt<IEnumerable<LambdaExpression>>();

                    if (queryFilters != null && queryFilters.Any())
                    {
                        foreach (var queryFilter in queryFilters)
                        {
                            if (queryFilter != null) entityTypeBuilder.HasQueryFilter(queryFilter);
                        }
                    }
                }

                _dbContextIdentifierTypesOfDbEntityType.AddOrUpdate(dbEntityType, dbContextIdentifierTypes);
            }
        }
        #endregion

        #region 配置数据库函数 + private static void DbFunctionConfigure(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        /// <summary>
        /// 配置数据库函数
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        private static void DbFunctionConfigure(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        {
            if (_dbFunctionMethods.Count == 0) return;

            foreach (var dbFunction in _dbFunctionMethods)
            {
                if (_dbFunctionsLoopIndex == _dbFunctionMethods.Count)
                {
                    var values = _dbContextIdentifierTypesOfDbFunctions.GetValueOrDefault(dbFunction);
                    if (values != null && values.Count > 0 && !values.Contains(dbContextIdentifierType)) break;
                }

                var dbEFFunctionAttribute = dbFunction.CustomAttributes.FirstOrDefault(u => u is DbEFFunctionAttribute) as DbEFFunctionAttribute;

                var dbContextIdentifierTypes = dbEFFunctionAttribute.DbContextIdentifierTypes;

                if (CheckIsInDbContextIdentifierTypes(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    modelBuilder.HasDbFunction(dbFunction.Method);
                }

                _dbContextIdentifierTypesOfDbFunctions.AddOrUpdate(dbFunction, dbContextIdentifierTypes);

                _dbFunctionsLoopIndex++;
            }
        }
        #endregion



        #region 配置数据库上下文查询筛选器 + private static void DbContextQueryFilterConfigure(DbContext dbContext, ModelBuilder modelBuilder, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置数据库上下文查询筛选器
        /// <para>一旦数据库上下文继承该接口，那么该数据库上下文所有的实体都将应用该查询筛选器</para>
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbContextQueryFilterConfigure(DbContext dbContext, ModelBuilder modelBuilder, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbContextType = dbContext.GetType();
            if (typeof(IDbContextQueryFilter).IsAssignableFrom(dbContextType) && typeof(IDbEntityBase).IsAssignableFrom(dbEntityType))
            {
                BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);

                dbContextType.CallMethod(nameof(IDbContextQueryFilter.HasQueryFilter), dbContext, entityTypeBuilder);
            }
        }
        #endregion












        #region 是否允许当前数据库上下文标识器构建实体 + private static bool IsAllowThisDbEntityToBuilder(IEnumerable<Type> dbContextIdentifierTypes, Type dbContextIdentifierType)
        /// <summary>
        /// 是否允许当前数据库上下文标识器构建实体
        /// </summary>
        /// <param name="dbContextIdentifierTypes">当前类型数据库上下文标识器集合</param>
        /// <param name="dbContextIdentifierType">当前数据库上下文标识器</param>
        /// <returns></returns>
        private static bool CheckIsInDbContextIdentifierTypes(IEnumerable<Type> dbContextIdentifierTypes, Type dbContextIdentifierType)
        {
            return dbContextIdentifierTypes == null || dbContextIdentifierTypes.Count() == 0 || dbContextIdentifierTypes.Contains(dbContextIdentifierType);
        }
        #endregion

        #region 配置实体模型构建器 + private static void BuilderEntityType(ModelBuilder modelBuilder, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置实体模型构建器
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbEntityType">实体类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void BuilderEntityType(ModelBuilder modelBuilder, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        {
            entityTypeBuilder ??= _modelBuilderEntityMethod.MakeGenericMethod(dbEntityType).Invoke(modelBuilder, null) as EntityTypeBuilder;
        }
        #endregion
    }
}
