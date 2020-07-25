using Fur.ApplicationBase;
using Fur.ApplicationBase.Attributes;
using Fur.ApplicationBase.Wrappers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.Seed;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// 种子数据状态器
    /// <para>用来存储反射解析结果，避免重复解析</para>
    /// </summary>
    [NonWrapper]
    internal class DbSeedDataStater
    {
        /// <summary>
        /// 数据库种子数据状态器集合
        /// <para>避免重复解析状态器</para>
        /// </summary>
        private static readonly ConcurrentDictionary<Type, DbSeedDataStater> _dbSeedDataStateres;

        /// <summary>
        /// 应用所有标识为数据种子的类型
        /// </summary>
        private readonly static IEnumerable<TypeWrapper> _dataSeedTypes;

        #region 构造函数（静态） + static DbSeedDataStater()
        /// <summary>
        /// 构造函数（静态）
        /// </summary>
        static DbSeedDataStater()
        {
            _dbSeedDataStateres ??= new ConcurrentDictionary<Type, DbSeedDataStater>();
            _dataSeedTypes = ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers.Where(u => u.CanBeNew && u.Type.GetInterfaces().Any(c => c.IsGenericType && typeof(IDbDataSeed).IsAssignableFrom(c.GetGenericTypeDefinition())));
        }
        #endregion

        /// <summary>
        /// 实体模型类型
        /// </summary>
        internal Type DbEntityType { get; set; }

        /// <summary>
        /// 数据库上下文标识器
        /// </summary>
        internal IEnumerable<Type> DbContextIdentifierTypes { get; set; }

        /// <summary>
        /// 种子数据
        /// </summary>
        internal IEnumerable<object> SeedDatas { get; set; }

        #region 配置种子数据 + internal static void Configure(ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType)
        /// <summary>
        /// 配置种子数据
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        internal static void Configure(ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType)
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
    }
}
