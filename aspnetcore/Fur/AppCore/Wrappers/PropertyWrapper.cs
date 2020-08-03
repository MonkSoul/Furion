using Fur.AppCore.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.AppCore.Wrappers
{
    /// <summary>
    /// 属性包装器
    /// <code>sealed</code>
    /// <para>主要用来装载解决方案项目中类型属性常用属性及附加属性， 避免重复反射读取</para>
    /// </summary>
    [NonWrapper]
    public sealed class PropertyWrapper
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
        public Type Type { get; set; }

        /// <summary>
        /// 自定义特性集合
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}