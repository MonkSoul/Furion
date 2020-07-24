using Fur.ApplicationBase.Attributes;
using System;
using System.Collections.Generic;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// 种子数据状态器
    /// <para>用来存储反射解析结果，避免重复解析</para>
    /// </summary>
    [NonWrapper]
    public class DbSeedDataStater
    {
        /// <summary>
        /// 实体模型类型
        /// </summary>
        public Type DbEntityType { get; set; }

        /// <summary>
        /// 数据库上下文标识器
        /// </summary>
        public IEnumerable<Type> DbContextIdentifierTypes { get; set; }

        /// <summary>
        /// 种子数据
        /// </summary>
        public IEnumerable<object> SeedDatas { get; set; }
    }
}
