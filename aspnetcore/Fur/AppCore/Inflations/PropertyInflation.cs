using Fur.AppCore.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.AppCore.Inflations
{
    /// <summary>
    /// 属性包装器
    /// </summary>
    [NonInflated]
    public sealed class PropertyInflation
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        public Assembly ThisAssembly { get; set; }

        /// <summary>
        /// 所在类型
        /// </summary>
        public Type ThisDeclareType { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// 属性特性集合
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}