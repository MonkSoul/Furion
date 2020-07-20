using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.ApplicationBase.Wrappers
{
    /// <summary>
    /// 方法参数包装类
    /// <code>sealed</code>
    /// <para>主要用来装载解决方案项目中方法参数常用属性及附加属性， 避免重复反射读取</para>
    /// </summary>
    public sealed class ParameterWrapper
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
        /// 所在方法
        /// </summary>
        public MethodInfo ThisMethod { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public ParameterInfo Parameter { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 自定义特性集合
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}