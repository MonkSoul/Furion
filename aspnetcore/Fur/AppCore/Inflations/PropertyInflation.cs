using Fur.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.AppCore.Inflations
{
    /// <summary>
    /// 属性包装器
    /// </summary>
    [NonInflated]
    internal sealed class PropertyInflation
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        internal Assembly ThisAssembly { get; set; }

        /// <summary>
        /// 所在类型
        /// </summary>
        internal Type ThisDeclareType { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        internal Type PropertyType { get; set; }

        /// <summary>
        /// 属性特性集合
        /// </summary>
        internal IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}