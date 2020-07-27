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
using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            var isRegisterTenant = dbContext.GetService<ILifetimeScope>().IsRegistered<IMultipleTenantProvider>();

            // 如果没有任何数据库实体类型，无需配置
            if (_includeDbEntityTypes.Count > 0)
            {
                foreach (var dbEntityType in _dbEntityTypes)
                {
                    EntityTypeBuilder entityTypeBuilder = null;

                    // 配置全局查询筛选器
                    DbContextQueryFilterConfigure(dbContext, modelBuilder, dbEntityType, ref entityTypeBuilder);

                    if (_dbEntityTypesLoopIndex == _dbEntityTypes.Count)
                    {
                        var values = _dbContextIdentifierTypesOfDbEntityType.GetValueOrDefault(dbEntityType);
                        if (values != null && values.Count > 0 && !values.Contains(dbContextIdentifierType)) break;
                    }

                    if (!isRegisterTenant && dbEntityType == typeof(Tenant))
                    {
                        _dbEntityTypesLoopIndex++;
                        continue;
                    };

                    // 配置无键实体
                    DbNoKeyEntityConfigure(dbEntityType, modelBuilder, dbContextIdentifierType, ref entityTypeBuilder);

                    // 查找数据库实体类型下的所有配置类型
                    var dbEntityConfigureTypes = _includeDbEntityTypes
                        .Where(t => typeof(IDbEntityConfigure).IsAssignableFrom(t) && t.GetInterfaces().Any(u => u.IsGenericType && u.GetGenericArguments().First() == dbEntityType));

                    foreach (var dbEntityConfigureType in dbEntityConfigureTypes)
                    {
                        // 配置实体类型构建器信息
                        DbEntityBuilderConfigure(dbEntityType, modelBuilder, dbContextIdentifierType, dbEntityConfigureType, ref entityTypeBuilder);

                        // 配置种子数据
                        DbSeedDataConfigure(dbEntityType, modelBuilder, dbContext, dbContextIdentifierType, dbEntityConfigureType, ref entityTypeBuilder);

                        // 配置查询筛选器
                        DbQueryFilterConfigure(dbEntityType, modelBuilder, dbContext, dbContextIdentifierType, dbEntityConfigureType, ref entityTypeBuilder);
                    }

                    // 配置模型
                    DbModelEntityConfigure(modelBuilder, dbContextIdentifierType, dbEntityType, ref entityTypeBuilder);

                    _dbEntityTypesLoopIndex++;
                }
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
            if (entityTypeBuilder != null) return;

            if (dbEntityType.IsDefined(typeof(DbTableAttribute), false))
            {
                var dbTableAttribute = dbEntityType.GetCustomAttribute<DbTableAttribute>();
                if (IsAllowThisDbEntityToBuilder(dbTableAttribute.DbContextIdentifierTypes, dbContextIdentifierType))
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
            var dbEntityBaseType = dbEntityType.BaseType;
            if (dbEntityBaseType.IsGenericType && typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityBaseType.GetGenericTypeDefinition()))
            {
                var dbNoKeyEntityGenericArguments = dbEntityBaseType?.GetGenericArguments();
                if (dbNoKeyEntityGenericArguments != null)
                {
                    var dbContextIdentifierTypes = dbNoKeyEntityGenericArguments.Skip(1);

                    if (IsAllowThisDbEntityToBuilder(dbContextIdentifierTypes, dbContextIdentifierType))
                    {
                        BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);

                        entityTypeBuilder.HasNoKey();
                        entityTypeBuilder.ToView((Activator.CreateInstance(dbEntityType) as IDbNoKeyEntity).DB_DEFINED_NAME);
                    }

                    SaveDbContextIdentifiersOfDbEntityType(dbEntityType, dbContextIdentifierTypes);
                }
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
        private static void DbEntityBuilderConfigure(Type dbEntityType, ModelBuilder modelBuilder, Type dbContextIdentifierType, Type dbEntityConfigureType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbEntityBuilderGenericArguments = GetDbEntityConfigureTypeGenericArguments(dbEntityConfigureType, typeof(IDbEntityBuilder));
            if (dbEntityBuilderGenericArguments != null)
            {
                var dbContextIdentifierTypes = dbEntityBuilderGenericArguments.Skip(1);

                if (IsAllowThisDbEntityToBuilder(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);

                    entityTypeBuilder = dbEntityConfigureType
                        .GetMethod(nameof(IDbEntityBuilderOfT<IDbEntityBase>.HasEntityBuilder))
                        .Invoke(Activator.CreateInstance(dbEntityConfigureType), new object[] { entityTypeBuilder }) as EntityTypeBuilder;
                }

                SaveDbContextIdentifiersOfDbEntityType(dbEntityType, dbContextIdentifierTypes);
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
        private static void DbSeedDataConfigure(Type dbEntityType, ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType, Type dbEntityConfigureType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbSeedDataGenericArguments = GetDbEntityConfigureTypeGenericArguments(dbEntityConfigureType, typeof(IDbSeedData));

            if (dbSeedDataGenericArguments != null)
            {
                var dbContextIdentifierTypes = dbSeedDataGenericArguments.Skip(1);

                if (IsAllowThisDbEntityToBuilder(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);

                    var seedDatas = dbEntityConfigureType
                          .GetMethod(nameof(IDbSeedDataOfT<IDbEntityBase>.HasData))
                          .Invoke(Activator.CreateInstance(dbEntityConfigureType), new object[] { dbContext }).Adapt<IEnumerable<object>>();

                    if (seedDatas != null && seedDatas.Any()) entityTypeBuilder.HasData(seedDatas);
                }

                SaveDbContextIdentifiersOfDbEntityType(dbEntityType, dbContextIdentifierTypes);
            }
        }
        #endregion

        #region 配置查询筛选器 + private static void DbQueryFilterConfigure(Type dbEntityType, ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType, Type dbEntityConfigureType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置查询筛选器
        /// </summary>
        /// <param name="dbEntityType">实体类型</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="dbEntityConfigureType">数据库实体配置类型，也就是所有继承了可配置接口的类型</param>
        /// <param name="entityTypeBuilder">实体类型构建器</param>
        private static void DbQueryFilterConfigure(Type dbEntityType, ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType, Type dbEntityConfigureType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbQueryFilterGenericArguments = GetDbEntityConfigureTypeGenericArguments(dbEntityConfigureType, typeof(IDbQueryFilter));

            if (dbQueryFilterGenericArguments != null)
            {
                var dbContextIdentifierTypes = dbQueryFilterGenericArguments.Skip(1);
                if (IsAllowThisDbEntityToBuilder(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);

                    var queryFilters = dbEntityConfigureType
                          .GetMethod(nameof(IDbQueryFilterOfT<IDbEntityBase>.HasQueryFilter))
                          .Invoke(Activator.CreateInstance(dbEntityConfigureType), new object[] { dbContext }).Adapt<IEnumerable<LambdaExpression>>();

                    if (queryFilters != null && queryFilters.Any())
                    {
                        foreach (var queryFilter in queryFilters)
                        {
                            if (queryFilter != null) entityTypeBuilder.HasQueryFilter(queryFilter);
                        }
                    }
                }

                SaveDbContextIdentifiersOfDbEntityType(dbEntityType, dbContextIdentifierTypes);
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

                if (IsAllowThisDbEntityToBuilder(dbContextIdentifierTypes, dbContextIdentifierType))
                {
                    modelBuilder.HasDbFunction(dbFunction.Method);
                }

                SaveDbContextIdentifiersOfDbFunctions(dbFunction, dbContextIdentifierTypes);

                _dbFunctionsLoopIndex++;
            }
        }
        #endregion

        #region 配置全局查询筛选器 + private static void DbContextQueryFilterConfigure(DbContext dbContext, ModelBuilder modelBuilder, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        /// <summary>
        /// 配置全局查询筛选器
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="entityTypeBuilder">数据库实体类型构建器</param>
        private static void DbContextQueryFilterConfigure(DbContext dbContext, ModelBuilder modelBuilder, Type dbEntityType, ref EntityTypeBuilder entityTypeBuilder)
        {
            var dbContextType = dbContext.GetType();
            if (typeof(IDbContextQueryFilter).IsAssignableFrom(dbContextType))
            {
                BuilderEntityType(modelBuilder, dbEntityType, ref entityTypeBuilder);

                dbContextType
                    .GetMethod(nameof(IDbContextQueryFilter.HasQueryFilter))
                    .Invoke(dbContext, new object[] { dbContext, entityTypeBuilder });
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
        private static bool IsAllowThisDbEntityToBuilder(IEnumerable<Type> dbContextIdentifierTypes, Type dbContextIdentifierType)
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

        #region 获取数据库实体配置类型接口泛型参数 + private static Type[] GetDbEntityConfigureTypeGenericArguments(Type dbEntityConfigureType, Type interfaceType)
        /// <summary>
        /// 获取数据库实体配置类型接口泛型参数
        /// </summary>
        /// <param name="dbEntityConfigureType">数据库实体配置类型，也就是所有继承了可配置接口的类型</param>
        /// <param name="interfaceType">特定接口类型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        private static Type[] GetDbEntityConfigureTypeGenericArguments(Type dbEntityConfigureType, Type interfaceType)
        {
            return dbEntityConfigureType
                        .GetInterfaces().FirstOrDefault(c => c.IsGenericType && interfaceType.IsAssignableFrom(c.GetGenericTypeDefinition()))
                        ?.GetGenericArguments();
        }
        #endregion

        #region 保存数据库实体类型的数据库上下文标识器集合 + private static void SaveDbContextIdentifiersOfDbEntityType(Type dbEntityType, IEnumerable<Type> dbContextIdentifierTypes)
        /// <summary>
        /// 保存数据库实体类型的数据库上下文标识器集合
        /// </summary>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="dbContextIdentifierTypes">数据库上下文标识器类型集合</param>
        private static void SaveDbContextIdentifiersOfDbEntityType(Type dbEntityType, IEnumerable<Type> dbContextIdentifierTypes)
        {
            var values = _dbContextIdentifierTypesOfDbEntityType.GetValueOrDefault(dbEntityType) ?? new List<Type>();

            if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Any())
            {
                values.AddRange(dbContextIdentifierTypes);
            }

            _dbContextIdentifierTypesOfDbEntityType.AddOrUpdate(dbEntityType, values, (key, oldValues) => oldValues);
        }
        #endregion

        #region 保存数据库函数类型的数据库上下文标识器集合 + private static void SaveDbContextIdentifiersOfDbFunctions(MethodWrapper methodWrapper, IEnumerable<Type> dbContextIdentifierTypes)
        /// <summary>
        /// 保存数据库实体类型的数据库上下文标识器集合
        /// </summary>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="dbContextIdentifierTypes">数据库上下文标识器类型集合</param>
        private static void SaveDbContextIdentifiersOfDbFunctions(MethodWrapper methodWrapper, IEnumerable<Type> dbContextIdentifierTypes)
        {
            var values = _dbContextIdentifierTypesOfDbFunctions.GetValueOrDefault(methodWrapper) ?? new List<Type>();

            if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Any())
            {
                values.AddRange(dbContextIdentifierTypes);
            }

            _dbContextIdentifierTypesOfDbFunctions.AddOrUpdate(methodWrapper, values, (key, oldValues) => oldValues);
        }
        #endregion
    }
}
