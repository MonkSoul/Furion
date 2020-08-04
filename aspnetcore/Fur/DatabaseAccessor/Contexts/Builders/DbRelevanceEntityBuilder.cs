using System;
using System.Collections.Generic;

namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// 数据库关联实体构建器
    /// </summary>
    /// <remarks>
    /// <para>主要用于解决自动配置数据库上下文实体重复扫描引擎的性能问题</para>
    /// </remarks>

    internal sealed class DbRelevanceEntityBuilder
    {
        /// <summary>
        /// 关联实体类型
        /// </summary>
        internal Type DbRelevanceEntityType { get; set; }

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
        internal Dictionary<Type, IEnumerable<Type>> GenericArgumentTypesForInterfaces { get; set; }

        /// <summary>
        /// 基类泛型参数集合
        /// </summary>
        internal IEnumerable<Type> GenericArgumentTypesForBaseType { get; set; }
    }
}