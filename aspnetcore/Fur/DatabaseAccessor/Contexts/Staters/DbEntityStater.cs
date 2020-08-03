using Fur.AppCore.Attributes;
using System;
using System.Collections.Generic;

namespace Fur.DatabaseAccessor.Contexts.Staters
{
    /// <summary>
    /// 数据库实体类型状态器
    /// </summary>
    [NonInflated]
    internal sealed class DbEntityStater
    {
        /// <summary>
        /// 数据库实体关联类型
        /// </summary>
        internal Type DbEntityRelevanceType { get; set; }

        /// <summary>
        /// 是否是数据库实体类型
        /// </summary>
        internal bool IsDbEntityType { get; set; }

        /// <summary>
        /// 是否是数据库无键实体类型
        /// </summary>
        internal bool IsDbNoKeyEntityType { get; set; }

        /// <summary>
        /// 接口泛型参数集合
        /// </summary>
        internal Dictionary<Type, IEnumerable<Type>> InterfaceGenericArgumentTypes { get; set; }

        /// <summary>
        /// 基类泛型类型集合
        /// </summary>
        internal IEnumerable<Type> BaseTypeGenericArgumentTypes { get; set; }
    }
}