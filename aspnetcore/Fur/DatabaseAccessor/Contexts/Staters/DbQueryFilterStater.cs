using Fur.ApplicationBase;
using Fur.ApplicationBase.Attributes;
using Fur.ApplicationBase.Wrappers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.QueryFilters;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// 查询筛选器状态器
    /// <para>用来存储反射解析结果，避免重复解析</para>
    /// </summary>
    [NonWrapper]
    internal class DbQueryFilterStater
    {
        /// <summary>
        /// 数据库查询筛选器状态器集合
        /// <para>避免重复解析状态器</para>
        /// </summary>
        private static readonly ConcurrentDictionary<Type, DbQueryFilterStater> _dbQueryFilterStaters;

        /// <summary>
        /// 应用所有标识为查询筛选器的类型
        /// </summary>
        private readonly static IEnumerable<TypeWrapper> _queryFilterTypes;

        #region 构造函数（静态） + static DbQueryFilterStater()
        /// <summary>
        /// 构造函数（静态）
        /// </summary>
        static DbQueryFilterStater()
        {
            _dbQueryFilterStaters ??= new ConcurrentDictionary<Type, DbQueryFilterStater>();
            _queryFilterTypes = ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers.Where(u => u.CanBeNew && u.Type.GetInterfaces().Any(c => c.IsGenericType && typeof(IDbQueryFilter).IsAssignableFrom(c.GetGenericTypeDefinition())));
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
        /// 查询筛选器
        /// </summary>
        internal IEnumerable<LambdaExpression> QueryFilters { get; set; }

        #region 配置查询筛选器 + internal static void Configure(ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType)
        /// <summary>
        /// 配置查询筛选器
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        internal static void Configure(ModelBuilder modelBuilder, DbContext dbContext, Type dbContextIdentifierType)
        {
            foreach (var queryFilterType in _queryFilterTypes)
            {
                var filterType = queryFilterType.Type;

                // 缓存解析结果，避免重复解析
                if (!_dbQueryFilterStaters.TryGetValue(filterType, out DbQueryFilterStater dbQueryFilterStater))
                {
                    var _dbQueryFilterStater = new DbQueryFilterStater();
                    var queryFilterGenericArguments = filterType.GetInterfaces().FirstOrDefault(c => c.IsGenericType && typeof(IDbQueryFilter).IsAssignableFrom(c.GetGenericTypeDefinition())).GetGenericArguments();

                    _dbQueryFilterStater.DbContextIdentifierTypes = queryFilterGenericArguments.Skip(1);
                    _dbQueryFilterStater.DbEntityType = queryFilterGenericArguments.First();

                    var _dbContextIdentifierTypes = _dbQueryFilterStater.DbContextIdentifierTypes;
                    if (!(_dbContextIdentifierTypes != null && _dbContextIdentifierTypes.Count() > 0 && !_dbContextIdentifierTypes.Any(u => u == dbContextIdentifierType)))
                    {
                        var queryFilterTypeInstance = Activator.CreateInstance(filterType);
                        var hasQueryFilterMethod = filterType.GetMethod(nameof(IDbQueryFilterOfT<IDbEntityBase>.HasQueryFilter));

                        _dbQueryFilterStater.QueryFilters = hasQueryFilterMethod.Invoke(queryFilterTypeInstance, new object[] { dbContext }).Adapt<IEnumerable<LambdaExpression>>();
                    }

                    _dbQueryFilterStaters.TryAdd(filterType, _dbQueryFilterStater);
                    dbQueryFilterStater = _dbQueryFilterStater;
                }

                // 只有等于数据库上下文标识器才会被初始化
                var dbContextIdentifierTypes = dbQueryFilterStater.DbContextIdentifierTypes;
                if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Count() > 0 && !dbContextIdentifierTypes.Any(u => u == dbContextIdentifierType)) continue;

                var queryFilters = dbQueryFilterStater.QueryFilters;
                if (queryFilters == null || queryFilters.Count() == 0) continue;

                var entityTypeBuilder = modelBuilder.Entity(dbQueryFilterStater.DbEntityType);

                // 目前并不支持多个过滤器，只会应用最后一个：https://docs.microsoft.com/zh-cn/ef/core/querying/filters
                foreach (var queryFilter in queryFilters)
                {
                    if (queryFilter != null)
                    {
                        entityTypeBuilder.HasQueryFilter(queryFilter);
                    }
                }
            }
        }
        #endregion
    }
}
