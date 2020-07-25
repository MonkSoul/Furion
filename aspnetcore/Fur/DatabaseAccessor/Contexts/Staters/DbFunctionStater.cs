using Fur.ApplicationBase;
using Fur.ApplicationBase.Attributes;
using Fur.ApplicationBase.Wrappers;
using Fur.DatabaseAccessor.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// 数据库函数状态器
    /// <para>用来存储反射解析结果，避免重复解析</para>
    /// </summary>
    [NonWrapper]
    internal class DbFunctionStater
    {
        /// <summary>
        /// 数据库函数状态器集合
        /// <para>避免重复解析状态器</para>
        /// </summary>
        private static readonly ConcurrentDictionary<MethodWrapper, DbFunctionStater> _dbFunctionStaters;

        /// <summary>
        /// 应用所有标识为数据库函数的方法
        /// </summary>
        private readonly static IEnumerable<MethodWrapper> _dbFunctionMethods;

        #region 构造函数（静态） + static DbFunctionStater()
        /// <summary>
        /// 构造函数（静态）
        /// </summary>
        static DbFunctionStater()
        {
            _dbFunctionStaters ??= new ConcurrentDictionary<MethodWrapper, DbFunctionStater>();
            _dbFunctionMethods = ApplicationCore.ApplicationWrapper.PublicMethodWrappers.Where(u => u.IsStaticMethod && u.Method.IsDefined(typeof(DbEFFunctionAttribute)) && u.ThisDeclareType.IsAbstract && u.ThisDeclareType.IsSealed);
        }
        #endregion

        /// <summary>
        /// <see cref="DbEFFunctionAttribute"/> 特性对象
        /// </summary>
        internal DbEFFunctionAttribute DbEFFunctionAttribute { get; set; }

        #region 配置数据库函数 + internal static void Configure(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        /// <summary>
        /// 配置数据库函数
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContextIdentifierType">数据库上下文标识器</param>
        internal static void Configure(ModelBuilder modelBuilder, Type dbContextIdentifierType)
        {
            foreach (var dbFunction in _dbFunctionMethods)
            {
                // 缓存解析结果，避免重复解析
                if (!_dbFunctionStaters.TryGetValue(dbFunction, out DbFunctionStater dbFunctionStater))
                {
                    var _dbFunctionStater = new DbFunctionStater
                    {
                        DbEFFunctionAttribute = dbFunction.CustomAttributes.FirstOrDefault(u => u is DbEFFunctionAttribute) as DbEFFunctionAttribute
                    };

                    _dbFunctionStaters.TryAdd(dbFunction, _dbFunctionStater);
                    dbFunctionStater = _dbFunctionStater;
                }

                // 只有等于数据库上下文标识器才会被初始化
                var dbContextIdentifierTypes = dbFunctionStater.DbEFFunctionAttribute.DbContextIdentifierTypes;
                if (dbContextIdentifierTypes != null && dbContextIdentifierTypes.Length > 0 && !dbContextIdentifierTypes.Contains(dbContextIdentifierType)) continue;

                modelBuilder.HasDbFunction(dbFunction.Method);
            }
        }
        #endregion
    }
}
