using Fur.ApplicationBase.Attributes;
using System;
using System.Collections.Generic;

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
        /// 配置实例
        /// </summary>
        internal string EntityName { get; set; }

        /// <summary>
        /// 数据库上下文标识器
        /// </summary>
        internal IEnumerable<Type> DbContextIdentifierTypes { get; set; }
    }
}
