using Autofac;
using Fur.ApplicationBase;
using Fur.ApplicationBase.Attributes;
using Fur.ApplicationBase.Wrappers;
using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.Filters;
using Fur.DatabaseAccessor.Models.Seed;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// Fur 数据库上下文 状态器
    /// <para>主要用于解决基类初始化方法重复调用问题</para>
    /// </summary>
    [NonWrapper]
    internal static class FurDbContextOfTStater
    {
        #region 扫描数据库对象类型加入模型构建器中 +internal static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, Type dbContextIdentifierType, DbContext dbContext)
        /// <summary>
        /// 扫描数据库对象类型加入模型构建器中
        /// <para>包括视图、存储过程、函数（标量函数/表值函数）初始化、及种子数据、查询筛选器配置</para>
        /// </summary>
        /// <param name="modelBuilder">模型上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        /// <param name="dbContext">数据库上下文</param>
        internal static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, Type dbContextIdentifierType, DbContext dbContext)
        {
            // 配置无键实体
            ConfigureNoKeyEntity(modelBuilder, dbContextIdentifierType);

            // 配置数据库函数
            ConfigureDbFunction(modelBuilder, dbContextIdentifierType);

            // 配置种子数据
            ConfigureSeedData(modelBuilder, dbContext, dbContextIdentifierType);

            //配置数据过滤器
            ConfigureQueryFilter(modelBuilder, dbContext, dbContextIdentifierType);
        }
        #endregion

        #region 配置无键实体 + private static void ConfigureNoKeyEntity(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        /// <summary>
        /// 配置无键实体
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        private static void ConfigureNoKeyEntity(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        {
            foreach (var noKeyEntityType in _noKeyEntityTypes)
            {
                var dbEntityType = noKeyEntityType.Type;

                // 缓存解析结果，避免重复解析
                if (!_dbNoKeyEntityStateres.TryGetValue(dbEntityType, out DbNoKeyEntityStater dbNoKeyEntityStater))
                {
                    var _dbNoKeyEntityStater = new DbNoKeyEntityStater
                    {
                        EntityName = (Activator.CreateInstance(dbEntityType) as IDbNoKeyEntity).ENTITY_NAME
                    };
                    var dbEntityBaseType = dbEntityType.BaseType;

                    if (dbEntityBaseType.IsGenericType && typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityBaseType.GetGenericTypeDefinition()))
                    {
                        _dbNoKeyEntityStater.DbContextIdentifierTypes = dbEntityBaseType.GetGenericArguments();
                    }

                    _dbNoKeyEntityStateres.TryAdd(dbEntityType, _dbNoKeyEntityStater);
                    dbNoKeyEntityStater = _dbNoKeyEntityStater;
                }

                // 只有等于数据库上下文标识器才会被初始化
                var dbContextIdentifierTypes = dbNoKeyEntityStater.DbContextIdentifierTypes;
                if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Count() > 0 && !dbContextIdentifierTypes.Any(u => u == dbContextIdentifierType)) continue;

                var entityTypeBuilder = modelBuilder.Entity(dbEntityType);
                entityTypeBuilder.HasNoKey();
                entityTypeBuilder.ToView(dbNoKeyEntityStater.EntityName);
            }
        }
        #endregion

        #region 配置数据库函数 + private static void ConfigureDbFunction(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        /// <summary>
        /// 配置数据库函数
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        private static void ConfigureDbFunction(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        {
            foreach (var dbFunction in _dbFunctionMethods)
            {
                // 缓存解析结果，避免重复解析
                if (!_dbFunctionStateres.TryGetValue(dbFunction, out DbFunctionStater dbFunctionStater))
                {
                    var _dbFunctionStater = new DbFunctionStater
                    {
                        DbEFFunctionAttribute = dbFunction.CustomAttributes.FirstOrDefault(u => u is DbEFFunctionAttribute) as DbEFFunctionAttribute
                    };

                    _dbFunctionStateres.TryAdd(dbFunction, _dbFunctionStater);
                    dbFunctionStater = _dbFunctionStater;
                }

                // 只有等于数据库上下文标识器才会被初始化
                var dbContextIdentifierTypes = dbFunctionStater.DbEFFunctionAttribute.DbContextIdentifierTypes;
                if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Length > 0 && !dbContextIdentifierTypes.Contains(dbContextIdentifierType)) continue;

                modelBuilder.HasDbFunction(dbFunction.Method);
            }
        }
        #endregion

        #region 配置种子数据 + private static void ConfigureSeedData(ModelBuilder modelBuilder, ILifetimeScope lifetimeScope, Type dbContextIdentifierType)
        /// <summary>
        /// 配置种子数据
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        private static void ConfigureSeedData(ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType)
        {
            foreach (var seedType in _dataSeedTypes)
            {
                var seedDataType = seedType.Type;

                // 缓存解析结果，避免重复解析
                if (!_dbSeedDataStateres.TryGetValue(seedDataType, out DbSeedDataStater dbSeedDataStater))
                {
                    var _dbSeedDataStater = new DbSeedDataStater();
                    var seedDataGenericArguments = seedDataType.GetInterfaces().FirstOrDefault(c => c.IsGenericType && typeof(IDbDataSeed).IsAssignableFrom(c.GetGenericTypeDefinition())).GetGenericArguments();

                    _dbSeedDataStater.DbContextIdentifierTypes = seedDataGenericArguments.Skip(1);
                    _dbSeedDataStater.DbEntityType = seedDataGenericArguments.First();

                    var _dbContextIdentifierTypes = _dbSeedDataStater.DbContextIdentifierTypes;
                    if (!(_dbContextIdentifierTypes != null && _dbContextIdentifierTypes.Count() > 0 && !_dbContextIdentifierTypes.Any(u => u == dbContextIdentifierType)))
                    {
                        var seedDataTypeInstance = Activator.CreateInstance(seedDataType);
                        var hasDataMethod = seedDataType.GetMethod(nameof(IDbDataSeedOfT<IDbEntity>.HasData));

                        _dbSeedDataStater.SeedDatas = hasDataMethod.Invoke(seedDataTypeInstance, new object[] { dbContext }).Adapt<IEnumerable<object>>();
                    }

                    _dbSeedDataStateres.TryAdd(seedDataType, _dbSeedDataStater);
                    dbSeedDataStater = _dbSeedDataStater;
                }

                // 只有等于数据库上下文标识器才会被初始化
                var dbContextIdentifierTypes = dbSeedDataStater.DbContextIdentifierTypes;
                if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Count() > 0 && !dbContextIdentifierTypes.Any(u => u == dbContextIdentifierType)) continue;

                var seedDatas = dbSeedDataStater.SeedDatas;
                if (seedDatas == null || seedDatas.Count() == 0) continue;

                var entityTypeBuilder = modelBuilder.Entity(dbSeedDataStater.DbEntityType);
                entityTypeBuilder.HasData(seedDatas);
            }
        }
        #endregion

        #region 配置查询筛选器 + private static void ConfigureQueryFilter(ModelBuilder modelBuilder, ILifetimeScope lifetimeScope, Type dbContextIdentifierType)
        /// <summary>
        /// 配置查询筛选器
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        private static void ConfigureQueryFilter(ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType)
        {
            foreach (var queryFilterType in _queryFilterTypes)
            {
                var filterType = queryFilterType.Type;

                // 缓存解析结果，避免重复解析
                if (!_dbQueryFilterStateres.TryGetValue(filterType, out DbQueryFilterStater dbQueryFilterStater))
                {
                    var _dbQueryFilterStater = new DbQueryFilterStater();
                    var queryFilterGenericArguments = filterType.GetInterfaces().FirstOrDefault(c => c.IsGenericType && typeof(IDbQueryFilter).IsAssignableFrom(c.GetGenericTypeDefinition())).GetGenericArguments();

                    _dbQueryFilterStater.DbContextIdentifierTypes = queryFilterGenericArguments.Skip(1);
                    _dbQueryFilterStater.DbEntityType = queryFilterGenericArguments.First();

                    var _dbContextIdentifierTypes = _dbQueryFilterStater.DbContextIdentifierTypes;
                    if (!(_dbContextIdentifierTypes != null && _dbContextIdentifierTypes.Count() > 0 && !_dbContextIdentifierTypes.Any(u => u == dbContextIdentifierType)))
                    {
                        var queryFilterTypeInstance = Activator.CreateInstance(filterType);
                        var hasQueryFilterMethod = filterType.GetMethod(nameof(IDbQueryFilterOfT<IDbEntity>.HasQueryFilter));

                        _dbQueryFilterStater.QueryFilters = hasQueryFilterMethod.Invoke(queryFilterTypeInstance, new object[] { dbContext }).Adapt<IEnumerable<LambdaExpression>>();
                    }

                    _dbQueryFilterStateres.TryAdd(filterType, _dbQueryFilterStater);
                    dbQueryFilterStater = _dbQueryFilterStater;
                }

                // 只有等于数据库上下文标识器才会被初始化
                var dbContextIdentifierTypes = dbQueryFilterStater.DbContextIdentifierTypes;
                if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Count() > 0 && !dbContextIdentifierTypes.Any(u => u == dbContextIdentifierType)) continue;

                var queryFilters = dbQueryFilterStater.QueryFilters;
                if (queryFilters == null || queryFilters.Count() == 0) continue;

                var entityTypeBuilder = modelBuilder.Entity(dbQueryFilterStater.DbEntityType);
                foreach (var queryFilter in queryFilters)
                {
                    entityTypeBuilder.HasQueryFilter(queryFilter);
                }
            }
        }
        #endregion

        #region 初始化 Fur 数据库上下文状态器 + static FurDbContextOfTStater()

        private readonly static IEnumerable<TypeWrapper> _publicClassTypes;
        private readonly static IEnumerable<TypeWrapper> _noKeyEntityTypes;
        private readonly static IEnumerable<TypeWrapper> _dataSeedTypes;
        private readonly static IEnumerable<TypeWrapper> _queryFilterTypes;
        private readonly static IEnumerable<MethodWrapper> _methodWrappers;
        private readonly static IEnumerable<MethodWrapper> _dbFunctionMethods;

        private static readonly ConcurrentDictionary<Type, DbNoKeyEntityStater> _dbNoKeyEntityStateres;
        private static readonly ConcurrentDictionary<MethodWrapper, DbFunctionStater> _dbFunctionStateres;
        private static readonly ConcurrentDictionary<Type, DbSeedDataStater> _dbSeedDataStateres;
        private static readonly ConcurrentDictionary<Type, DbQueryFilterStater> _dbQueryFilterStateres;

        static FurDbContextOfTStater()
        {
            _dbNoKeyEntityStateres ??= new ConcurrentDictionary<Type, DbNoKeyEntityStater>();
            _dbFunctionStateres ??= new ConcurrentDictionary<MethodWrapper, DbFunctionStater>();
            _dbSeedDataStateres ??= new ConcurrentDictionary<Type, DbSeedDataStater>();
            _dbQueryFilterStateres ??= new ConcurrentDictionary<Type, DbQueryFilterStater>();

            _publicClassTypes ??= ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers;
            _methodWrappers ??= ApplicationCore.ApplicationWrapper.PublicMethodWrappers;

            _noKeyEntityTypes ??= _publicClassTypes.Where(u => u.CanBeNew && typeof(IDbNoKeyEntity).IsAssignableFrom(u.Type));
            _dataSeedTypes = _publicClassTypes.Where(u => u.CanBeNew && u.Type.GetInterfaces().Any(c => c.IsGenericType && typeof(IDbDataSeed).IsAssignableFrom(c.GetGenericTypeDefinition())));
            _queryFilterTypes = _publicClassTypes.Where(u => u.CanBeNew && u.Type.GetInterfaces().Any(c => c.IsGenericType && typeof(IDbQueryFilter).IsAssignableFrom(c.GetGenericTypeDefinition())));
            _dbFunctionMethods = _methodWrappers.Where(u => u.IsStaticMethod && u.Method.IsDefined(typeof(DbEFFunctionAttribute)) && u.ThisDeclareType.IsAbstract && u.ThisDeclareType.IsSealed);
        }
        #endregion
    }
}
