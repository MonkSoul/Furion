using Fur.ApplicationBase;
using Fur.ApplicationBase.Attributes;
using Fur.ApplicationBase.Wrappers;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// 无键实体状态器
    /// <para>用来存储反射解析结果，避免重复解析</para>
    /// </summary>
    [NonWrapper]
    internal class DbNoKeyEntityStater
    {
        /// <summary>
        /// 无键实体状态器集合
        /// <para>避免重复解析状态器</para>
        /// </summary>
        private static readonly ConcurrentDictionary<Type, DbNoKeyEntityStater> _dbNoKeyEntityStaters;

        /// <summary>
        /// 应用所有标识为无键实体的类型
        /// </summary>
        private readonly static IEnumerable<TypeWrapper> _noKeyEntityTypes;

        #region 构造函数（静态） + static DbNoKeyEntityStater()
        /// <summary>
        /// 构造函数（静态）
        /// </summary>
        static DbNoKeyEntityStater()
        {
            _dbNoKeyEntityStaters ??= new ConcurrentDictionary<Type, DbNoKeyEntityStater>();
            _noKeyEntityTypes ??= ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers.Where(u => u.CanBeNew && typeof(IDbNoKeyEntity).IsAssignableFrom(u.Type));
        }
        #endregion

        /// <summary>
        /// 配置实例
        /// </summary>
        internal string EntityName { get; set; }

        /// <summary>
        /// 数据库上下文标识器
        /// </summary>
        internal IEnumerable<Type> DbContextIdentifierTypes { get; set; }

        #region 配置无键实体 + internal static void Configure(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        /// <summary>
        /// 配置无键实体
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        internal static void Configure(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        {
            foreach (var noKeyEntityType in _noKeyEntityTypes)
            {
                var dbEntityType = noKeyEntityType.Type;

                // 缓存解析结果，避免重复解析
                if (!_dbNoKeyEntityStaters.TryGetValue(dbEntityType, out DbNoKeyEntityStater dbNoKeyEntityStater))
                {
                    var _dbNoKeyEntityStater = new DbNoKeyEntityStater
                    {
                        EntityName = (Activator.CreateInstance(dbEntityType) as IDbNoKeyEntity).DB_DEFINED_NAME
                    };
                    var dbEntityBaseType = dbEntityType.BaseType;

                    if (dbEntityBaseType.IsGenericType && typeof(IDbNoKeyEntity).IsAssignableFrom(dbEntityBaseType.GetGenericTypeDefinition()))
                    {
                        _dbNoKeyEntityStater.DbContextIdentifierTypes = dbEntityBaseType.GetGenericArguments();
                    }

                    _dbNoKeyEntityStaters.TryAdd(dbEntityType, _dbNoKeyEntityStater);
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
    }
}
